

namespace MedCore.Application.Dtos.Insurance.NetworkType
{
    public class SaveNetworkTypeDto : NetworkTypeDto
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
