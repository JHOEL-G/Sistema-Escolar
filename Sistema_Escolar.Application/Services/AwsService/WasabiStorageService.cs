using System;
using System.Collections.Generic;
using System.Text;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.Extensions.Configuration;
using Sistema_Escolar.Application.Interfaces.BucketService;
using Xabe.FFmpeg;
using Xabe.FFmpeg.Downloader;

namespace Sistema_Escolar.Application.Services.AwsService
{
    public class WasabiStorageService : IFileStorageService
    {
        private readonly string _accessKey;
        private readonly string _secretKey;
        private readonly string _bucketName;
        private readonly string _serviceUrl;
        private static bool _ffmpegDescargado = false;

        public WasabiStorageService(IConfiguration configuration)
        {
            _accessKey = configuration["WasabiSettings:AccessKey"] ?? "";
            _secretKey = configuration["WasabiSettings:SecretKey"] ?? "";
            _bucketName = configuration["WasabiSettings:BucketName"] ?? "";
            _serviceUrl = configuration["WasabiSettings:ServiceUrl"] ?? "";
        }

        public Task DeleteFile(string fileUrl, string containerName)
        {
            throw new NotImplementedException();
        }

        public async Task<string> UploadFile(Stream fileStream, string fileName, string containerName)
        {
            var config = new AmazonS3Config
            {
                ServiceURL = _serviceUrl,
                ForcePathStyle = true,
                AuthenticationRegion = "us-central-1" 
            };

            using var client = new AmazonS3Client(_accessKey, _secretKey, config);

            var uniqueFileName = $"{containerName}/{Guid.NewGuid()}{Path.GetExtension(fileName)}";

            var streamToUpload = await OptimizarVideoParaStreaming(fileStream, fileName);

            var putRequest = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = uniqueFileName,
                InputStream = streamToUpload,
                ContentType = GetContentType(fileName),
                DisablePayloadSigning = true
            };

            putRequest.Headers["x-amz-content-sha256"] = "UNSIGNED-PAYLOAD";

            await client.PutObjectAsync(putRequest);

            if (streamToUpload != fileStream) streamToUpload.Dispose();

            return uniqueFileName;
        }

        public string GetPresignedUrl(string uniqueFileName, int durationMinutes)
        {
            if (string.IsNullOrEmpty(uniqueFileName)) return null;

            if (uniqueFileName.StartsWith("http"))
            {
                var uri = new Uri(uniqueFileName);
                var path = uri.AbsolutePath.TrimStart('/');
                if (path.StartsWith(_bucketName + "/"))
                    uniqueFileName = path.Substring(_bucketName.Length + 1);
                else
                    uniqueFileName = path;
            }

            if (!uniqueFileName.Contains("/"))
                return null;

            var config = new AmazonS3Config
            {
                ServiceURL = _serviceUrl,
                ForcePathStyle = true,
                AuthenticationRegion = "us-central-1"
            };

            using var client = new AmazonS3Client(_accessKey, _secretKey, config);

            var request = new GetPreSignedUrlRequest
            {
                BucketName = _bucketName,
                Key = uniqueFileName,
                Expires = DateTime.UtcNow.AddMinutes(durationMinutes),
                Protocol = Protocol.HTTPS
            };

            return client.GetPreSignedURL(request);
        }

        private string GetContentType(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLower();
            return extension switch
            {
                ".pdf" => "application/pdf",
                ".mp4" => "video/mp4",
                ".png" => "image/png",
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                _ => "application/octet-stream",
            };
        }

        public async Task<Stream> OptimizarVideoParaStreaming(Stream inputStream, string fileName)
        {
            if (Path.GetExtension(fileName).ToLower() != ".mp4")
                return inputStream;

            await AsegurarFFmpeg();

            var tempInput = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.mp4");
            var tempOutput = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}_fast.mp4");

            try
            {
                using (var fs = File.Create(tempInput))
                    await inputStream.CopyToAsync(fs);

                var conversion = await FFmpeg.Conversions.FromSnippet.Convert(tempInput, tempOutput);
                conversion.AddParameter("-movflags faststart");
                conversion.AddParameter("-acodec copy");
                conversion.AddParameter("-vcodec copy");
                await conversion.Start();

                var outputBytes = await File.ReadAllBytesAsync(tempOutput);
                return new MemoryStream(outputBytes);
            }
            finally
            {
                if (File.Exists(tempInput)) File.Delete(tempInput);
                if (File.Exists(tempOutput)) File.Delete(tempOutput);
            }
        }

        private static async Task AsegurarFFmpeg()
        {
            if (_ffmpegDescargado) return;
            var ffmpegPath = Path.Combine(Path.GetTempPath(), "ffmpeg");
            Directory.CreateDirectory(ffmpegPath);
            FFmpeg.SetExecutablesPath(ffmpegPath);
            await FFmpegDownloader.GetLatestVersion(FFmpegVersion.Official, ffmpegPath);
            _ffmpegDescargado = true;
        }

        public string ExtraerKeyRelativo(string url)
        {
            if (string.IsNullOrEmpty(url) || !url.StartsWith("http")) return url;
            var uri = new Uri(url);
            var path = uri.AbsolutePath.TrimStart('/');
            return path.StartsWith(_bucketName + "/") ? path.Substring(_bucketName.Length + 1) : path;
        }
    }
}
