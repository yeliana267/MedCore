using System.ComponentModel.DataAnnotations;

namespace MedCore.Model.Models.medical
{
    public class SpecialtiesModel
    {
        [Key]
        public short Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string SpecialtyName { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
