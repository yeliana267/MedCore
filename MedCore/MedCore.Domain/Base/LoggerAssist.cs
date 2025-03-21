using MedCore.Domain.Repository;
using Serilog;
using ILogger = Serilog.ILogger;

namespace MedCore.Domain.Base
{
    public class LoggerAssist<T>: ILoggerAssist<T> where T : class
    {
        private static readonly ILogger _logger = Log.Logger;

        public void Info(string mensaje)
        {
            _logger.Information(mensaje);
        }

        public void Advertencia(string mensaje)
        {
            _logger.Warning(mensaje);
        }

        public void Error(string mensaje, Exception ex)
        {
            _logger.Error(ex, mensaje);
        }

        public void Error(string mensaje)
        {
            _logger.Error(mensaje);
            throw new Exception(mensaje);
        }

        public void Info(string mensaje, Exception ex)
        {
           _logger.Information(mensaje, ex);
        }
    }
}
