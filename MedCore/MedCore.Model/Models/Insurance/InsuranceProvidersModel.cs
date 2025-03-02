using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedCore.Model.Models.Insurance
{
    public class InsuranceProvidersModel
    {
        [Key]
        public int InsuranceProviderID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(15)]
        public string ContactNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

       
        [StringLength(255)]
        public string? Website { get; set; }

        [Required]
        [StringLength(255)]
        public string Address { get; set; }

        
        [StringLength(100)]
        public string? City { get; set; }

        
        [StringLength(100)]
        public string? State { get; set; }

       
        [StringLength(100)]
        public string? Country { get; set; }


        [StringLength(10)]
        public string? ZipCode { get; set; }

        [Required]
        public string CoverageDetails { get; set; }

       
        [StringLength(255)]
        public string? LogoUrl { get; set; }

        [Required]
        public bool IsPreferred { get; set; }

        [Required]
        [ForeignKey("NetworkType")]
        public int NetworkTypeId { get; set; }

      
        [StringLength(15)]
        public string? CustomerSupportContact { get; set; }

       
        [StringLength(255)]
        public string? AcceptedRegions { get; set; }

       
        public decimal? MaxCoverageAmount { get; set; }

    }
}
