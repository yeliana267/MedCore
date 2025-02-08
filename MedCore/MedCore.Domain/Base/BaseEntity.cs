
namespace MedCore.Domain.Base
{
    public abstract class BaseEntity<Ttype> 
    {
        public Ttype Id { get; set; }
    }
}
