

namespace MedCore.Model.Models.system
{
    public class StatusModel
    {
        public int StatusID { get; set; }
        public string StatusName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
