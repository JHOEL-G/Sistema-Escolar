using Sistema_Escolar.Application.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema_Escolar.Application.Interfaces.InterfaceGeneralService
{
    public interface IRecursoModuloService
    {
        ILecturaService Lectura {  get; }
        IEvaluacionPresencialService EvaluacionPrecencial {  get; }
        ISesionPresencialService Sesion {  get; }
        IEncuestaService Encuesta { get; }
        IScormService Scorm { get; }
        IZoomService Zoom { get; }
        IEmbebidoService Embebido { get; }
        ITareaService Tarea { get; }
        IVideoService Video { get; }
        IEvaluacionService Evaluacion { get; }
        IForoService Foro { get; }
        IPreguntaVideoService Pregunta { get; }
    }
}
