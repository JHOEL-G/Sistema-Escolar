using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.Interfaces.BucketService
{
    public interface IFileStorageService
    {
        Task<string> UploadFile(Stream fileStream, string fileName, string containerName);
        Task DeleteFile(string fileUrl, string containerName);
        string GetPresignedUrl(string uniqueFileName, int durationMinutes);
        Task<Stream> OptimizarVideoParaStreaming(Stream inputStream, string fileName);
        string ExtraerKeyRelativo(string url);
    }
}
