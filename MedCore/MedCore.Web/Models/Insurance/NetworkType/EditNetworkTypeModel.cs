namespace MedCore.Web.Models.Insurance.NetworkType
{
    public class EditNetworkTypeModel
    {
        public int NetworkTypeId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}