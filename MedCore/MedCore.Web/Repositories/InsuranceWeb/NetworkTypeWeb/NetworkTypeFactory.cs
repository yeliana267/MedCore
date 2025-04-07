
using MedCore.Application.Dtos.Insurance.NetworkType;
using MedCore.Web.Models.Insurance.InsuranceProviders;
using MedCore.Web.Models.Insurance.NetworkType;

namespace MedCore.Web.Repositories.InsuranceWeb.NetworkTypeWeb
{
    public class NetworkTypeFactory
    {

        public static SaveNetworkTypeDto CreateSaveDto(CreateNetworkTypeModel model)
        => new()
        {
            Name = model.Name,
            Description = model.Description,
            CreatedAt = DateTime.UtcNow
        };

        public static UpdateNetworkTypeDto CreateUpdateDto(EditNetworkTypeModel model)
            => new()
            {
                NetworkTypeId = model.NetworkTypeId,
                Name = model.Name,
                Description = model.Description,
                UpdatedAt = DateTime.UtcNow
            };
    }
}

