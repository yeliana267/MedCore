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

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}