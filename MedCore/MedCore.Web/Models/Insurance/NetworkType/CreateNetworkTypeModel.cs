namespace MedCore.Web.Models.Insurance.NetworkType
{
    public class CreateNetworkTypeModel
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}