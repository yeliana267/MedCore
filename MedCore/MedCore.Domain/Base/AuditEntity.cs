
using System.ComponentModel.DataAnnotations.Schema;

namespace MedCore.Domain.Base
{
    public abstract class AuditEntity
    {
        protected AuditEntity()
        {
            this.CreatedAt = DateTime.Now;
        }
        [NotMapped]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [NotMapped]
        public DateTime? UpdatedAt { get; set; }

        [NotMapped]
        public bool IsActive { get; set; } = true;
    }
}
