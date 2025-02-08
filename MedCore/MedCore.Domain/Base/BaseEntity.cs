
namespace MedCore.Domain.Base
{
    public abstract class BaseEntity<Ttype> : AuditEntity
    {
        public Ttype Id {get; set;}
    }
}
