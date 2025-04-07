using System.ComponentModel.DataAnnotations;

namespace MedCore.Web.Models.Medical.AvailabilityModes
{
    public class EditAvailabilityModesModel
    {
        [Required(ErrorMessage = "El ID es obligatorio.")]
        public short AvailabilityModesId { get; set; }

        [Required(ErrorMessage = "El modo de disponibilidad es obligatorio.")]
        public required string AvailabilityMode { get; set; }
    }
}
