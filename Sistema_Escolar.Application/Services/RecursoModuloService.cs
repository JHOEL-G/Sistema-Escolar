using Microsoft.Extensions.DependencyInjection;
using Sistema_Escolar.Application.Interfaces.InterfaceGeneralService;
using Sistema_Escolar.Application.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.Services
{
    public class RecursoModuloService : IRecursoModuloService
    {
        private readonly IServiceProvider _service;

        public RecursoModuloService (IServiceProvider service)
        {
            _service = service;
        }


        public ILecturaService Lectura => _service.GetRequiredService<ILecturaService>();

        public IEvaluacionPresencialService EvaluacionPrecencial => _service.GetRequiredService<IEvaluacionPresencialService>();

        public ISesionPresencialService Sesion => _service.GetRequiredService<ISesionPresencialService>();

        public IEncuestaService Encuesta => _service.GetRequiredService<IEncuestaService>();

        public IScormService Scorm => _service.GetRequiredService<IScormService>();

        public IZoomService Zoom => _service.GetRequiredService<IZoomService>();

        public IEmbebidoService Embebido => _service.GetRequiredService<IEmbebidoService>();

        public ITareaService Tarea => _service.GetRequiredService<ITareaService>();

        public IVideoService Video => _service.GetRequiredService<IVideoService>();

        public IEvaluacionService Evaluacion => _service.GetRequiredService<IEvaluacionService>();

        public IForoService Foro => _service.GetRequiredService<IForoService>();

        public IPreguntaVideoService Pregunta => _service.GetRequiredService<IPreguntaVideoService>();
    }
}
