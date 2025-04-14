using Humanizer;
using MedCore.Application.Dtos.appointments.Appointments;
using MedCore.Application.Dtos.Insurance.InsuranceProvider;
using MedCore.Application.Dtos.Insurance.InsuranceProviders;
using MedCore.Web.Models.appointments;
using MedCore.Web.Models.Insurance.InsuranceProviders;

namespace MedCore.Web.Repositories.InsuranceWeb.InsurancePrividersWeb
{
    public class InsuranceProvidersFactory
    {
        public static SaveInsuranceProvidersDto CreateSaveDto(CreateInsuranceProvidersModel model)
        => new()
        {
            Name = model.Name,
            ContactNumber = model.ContactNumber,
            Email = model.Email,
            Website = model.Website,
            Address = model.Address,
            City = model.City,
            State = model.State,
            Country = model.Country,
            ZipCode = model.ZipCode,
            CoverageDetails = model.CoverageDetails,
            LogoUrl = model.LogoUrl,
            IsPreferred = model.IsPreferred,
            NetworkTypeId = model.NetworkTypeId,
            CustomerSupportContact = model.CustomerSupportContact,
            AcceptedRegions = model.AcceptedRegions,
            MaxCoverageAmount = model.MaxCoverageAmount,
            CreatedAt = DateTime.UtcNow
        };

        public static UpdateInsuranceProvidersDto CreateUpdateDto(EditInsuranceProvidersModel model)
            => new()
            {
                InsuranceProviderID = model.InsuranceProviderID,
                Name = model.Name,
                ContactNumber = model.ContactNumber,
                Email = model.Email,
                Website = model.Website,
                Address = model.Address,
                City = model.City,
                State = model.State,
                Country = model.Country,
                ZipCode = model.ZipCode,
                CoverageDetails = model.CoverageDetails,
                LogoUrl = model.LogoUrl,
                IsPreferred = model.IsPreferred,
                NetworkTypeId = model.NetworkTypeId,
                CustomerSupportContact = model.CustomerSupportContact,
                AcceptedRegions = model.AcceptedRegions,
                MaxCoverageAmount = model.MaxCoverageAmount, 
                UpdatedAt = DateTime.UtcNow
            };
    }
}
