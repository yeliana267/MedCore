using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace MedCore.Web.Models
{
    public class OperationResult
    {
        public bool Success { get; set; }
        public object Message { get; set; }
        public object Data { get; set; }
    }
}
