using System.ComponentModel.DataAnnotations;

namespace MedCore.Model.Models.medical
{
    public class AvailabilityModesModel
    {
        [Key]
        public short Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string? Description { get; set; }
    }
}