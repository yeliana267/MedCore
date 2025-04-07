using System.ComponentModel.DataAnnotations;

namespace MedCore.Web.Models.Medical.AvailabilityModes
{
    public class CreateAvailabilityModesModel
    {
        [Required(ErrorMessage = "El modo de disponibilidad es obligatorio.")]
        public required string AvailabilityMode { get; set; }
    }
}