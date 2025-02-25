

namespace MedCore.Domain.Base
{
    public class OperationResult
    {
        public readonly object? message;

        public string Message { get; set; } = string.Empty;

        public bool Success { get; set; }
        public dynamic Data { get; set; } = new object();
    }
}
