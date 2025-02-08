
namespace MedCore.Domain.Base
{
    public abstract  class AuditEntity
    {
        protected AuditEntity()
        {
            this.CreatedAt = DateTime.Now;
        }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
