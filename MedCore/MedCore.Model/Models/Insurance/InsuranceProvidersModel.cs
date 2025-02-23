using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedCore.Model.Models.Insurance
{
    public class InsuranceProvidersModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(15)]
        public string ContactNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string? Website { get; set; }

        [Required]
        [StringLength(255)]
        public string Address { get; set; }

        [Required]
        [StringLength(100)]
        public string? City { get; set; }

        [Required]
        [StringLength(100)]
        public string? State { get; set; }

        [Required]
        [StringLength(100)]
        public string? Country { get; set; }

        [Required]
        [StringLength(10)]
        public string? ZipCode { get; set; }

        [Required]
        public string CoverageDetails { get; set; }

        [Required]
        [StringLength(255)]
        public string? LogoUrl { get; set; }

        [Required]
        public bool IsPreferred { get; set; }

        [Required]
        [ForeignKey("NetworkType")]
        public int NetworkTypeId { get; set; }

        [Required]
        [StringLength(15)]
        public string? CustomerSupportContact { get; set; }

        [Required]
        [StringLength(255)]
        public string? AcceptedRegions { get; set; }

        [Required]
        public decimal? MaxCoverageAmount { get; set; }

    }
}
