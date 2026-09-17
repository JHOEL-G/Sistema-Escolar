using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Sistema_Escolar.Application.APIs;
using Sistema_Escolar.Application.Interfaces;
using Sistema_Escolar.Application.Interfaces.BucketService;
using Sistema_Escolar.Application.Interfaces.IKeycloakService;
using Sistema_Escolar.Application.Interfaces.InterfaceGeneralService;
using Sistema_Escolar.Application.Interfaces.IServices;
using Sistema_Escolar.Application.Services;
using Sistema_Escolar.Application.Services.AwsService;
using Sistema_Escolar.Application.Services.Keycloak;
using Sistema_Escolar.Infrastructure;
using Sistema_Escolar.Infrastructure.Data;
using Sistema_Escolar.Infrastructure.Repositories;
using Sistema_Escolar_Confia.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

builder.Services.AddMemoryCache();
builder.Services.AddHttpClient("KeycloakClient", client =>
{
    client.Timeout = TimeSpan.FromSeconds(30);
})
.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback =
        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
});

builder.Services.AddSingleton(new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    PropertyNameCaseInsensitive = true,
    WriteIndented = false
});

builder.Services.Configure<IISServerOptions>(options =>
{
    options.MaxRequestBodySize = 1073741824; 
});

builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 1073741824; 
});

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 1073741824; 
    options.ValueLengthLimit = int.MaxValue;
    options.MultipartHeadersLengthLimit = int.MaxValue;
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://k3y.financialsoft.site/realms/CREDITFS";
        options.RequireHttpsMetadata = false;

        options.MetadataAddress = "https://k3y.financialsoft.site/realms/CREDITFS/.well-known/openid-configuration";
        options.SaveToken = false;

        options.BackchannelHttpHandler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "https://k3y.financialsoft.site/realms/CREDITFS",
            ValidateAudience = false,        
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.FromMinutes(1)
        };

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                if (builder.Environment.IsDevelopment())
                {
                    Console.WriteLine("❌ Auth Error: " + context.Exception.Message);
                }
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddDbContext<ConfiaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBFORGE_SQL")));

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<ITemaRepository, TemaRepository>();
builder.Services.AddScoped<IRolRepository, RolRepository>();
builder.Services.AddScoped<IPropiedadRepository, PropiedadRepository>();
builder.Services.AddScoped<ICursoRepository, CursoRepository>();
builder.Services.AddScoped<IOuRespository, OuRepository>();
builder.Services.AddScoped<IRegistroPrevioRepository, RegistroPrevioRepository>();
builder.Services.AddScoped<IUsuarioCursoRepository, UsuarioCursoRepository>();
builder.Services.AddScoped<ICrearPreguntaRepository, CrearPreguntaRepository>();
builder.Services.AddScoped<IPreguntaVideoRepository, PreguntaVideoRepository>();
builder.Services.AddScoped<IGestionCursoRepository, GestionCursoRepository>();
builder.Services.AddScoped<IRutaAprendizajeRepository, RutaAprendizajeRepository>();
builder.Services.AddScoped<IGestionDocumentoRepository, GestionDocumentoRepository>();
builder.Services.AddScoped<IConfiguracionNotificacionesRepository, ConfiguracionNotificacionesRepository>();
builder.Services.AddScoped<IFormularioRepository, FormularioRepository>();
builder.Services.AddScoped<INotificacionRepository, NotificacionRepository>();
builder.Services.AddScoped<IModulosRecursosRepository, ModulosRecursosRepository>();


builder.Services.AddScoped<IRecursoForoRepository, ForoRepository>();
builder.Services.AddScoped<IRecursoEvaluacionRepository, EvaluacionRepository>();
builder.Services.AddScoped<IRecursoVideoRepository, VideoRepository>();
builder.Services.AddScoped<IRecursoTareaRepository, TareaRepository>();
builder.Services.AddScoped<IRecursoEmbebidoRepository, EmbebidoRepository>();
builder.Services.AddScoped<IRecursoZoomRepository, ZoomRepository>();
builder.Services.AddScoped<IRecursoScormRepository, ScormRepository>();
builder.Services.AddScoped<IRecursoEncuestaRepository, EncuestaRepository>();
builder.Services.AddScoped<IRecursoSesionPresencialRepository, SesionPresencialRepository>();
builder.Services.AddScoped<IRecursoEvaluacionPresencialRepository, EvaluacionPresencialRepository>();
builder.Services.AddScoped<IRecursoLecturaRepository, LecturaRepository>();



builder.Services.AddScoped<ILecturaService, LecturaService>();
builder.Services.AddScoped<IEvaluacionPresencialService, EvaluacionPresencialService>();
builder.Services.AddScoped<ISesionPresencialService, SesionPresencialService>();
builder.Services.AddScoped<IEncuestaService, EncuestaService>();
builder.Services.AddScoped<IScormService, ScormService>();
builder.Services.AddScoped<IZoomService, ZoomService>();
builder.Services.AddScoped<IEmbebidoService, EmbebidoService>();
builder.Services.AddScoped<ITareaService, TareaService>();
builder.Services.AddScoped<IVideoService, VideoService>();
builder.Services.AddScoped<IEvaluacionService, EvaluacionService>();
builder.Services.AddScoped<IForoService, ForoService>();
builder.Services.AddScoped<IUsuarioCursoService, UsuarioCursoService>();


builder.Services.AddScoped<IRecursoModuloService, RecursoModuloService>();
builder.Services.AddScoped<IModulosRecursosService, ModulosRrecursosService>();

builder.Services.AddScoped<INotificacionService, NotificacionService>();
builder.Services.AddScoped<IFormularioService, FormularioService>();
builder.Services.AddScoped<IConfiguracionNotificacionesService, ConfiguracionNotificacionesService>();
builder.Services.AddScoped<IGestionDocumentoService, GestionDocumentoService>();
builder.Services.AddScoped<IRutaAprendizajeService, RutaAprendizajeService>();
builder.Services.AddScoped<IGestionCursoService, GestionCursoService>();
builder.Services.AddScoped<IPreguntaVideoService, PreguntaVideoService>();
builder.Services.AddScoped<ICearPreguntaService, CrearPreguntaService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IKeycloakService, KeycloakService>();
builder.Services.AddScoped<ICursoService, CursoService>();
builder.Services.AddScoped<IOuService, OuService>();
builder.Services.AddScoped<IRolService, RolService>();
builder.Services.AddScoped<IPropiedadService, PropiedadService>();
builder.Services.AddScoped<IFileStorageService, WasabiStorageService>();
builder.Services.AddScoped<IRegistroPrevioService, RegistroPrevioService>();
builder.Services.AddScoped<ITemaService, TemaSerice>();
builder.Services.AddScoped<KeycloakDataExtractor>();

var MyAllowSpecificOrigins = "ConfiaSistemaReact";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("https://universidad.financialsoft.site", "http://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<ImpersonationMiddleware>();

app.MapControllers();

app.Run();
