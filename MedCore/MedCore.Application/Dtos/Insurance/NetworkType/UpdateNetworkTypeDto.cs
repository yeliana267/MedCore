

namespace MedCore.Application.Dtos.Insurance.NetworkType
{
    public class UpdateNetworkTypeDto : NetworkTypeDto
    {
        public int NetworkTypeId { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
