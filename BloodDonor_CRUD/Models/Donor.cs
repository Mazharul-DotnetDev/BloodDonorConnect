using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BloodDonor_CRUD.Models
{

    public class Donor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DonorId { get; set; }

        [Required(ErrorMessage = "Donor name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Donor name should be between 2 and 100 characters.")]
        public string DonorName { get; set; }

        [Required(ErrorMessage = "Birth of date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of Birth")]
        public DateTime BirthOfDate { get; set; } = DateTime.Now;

        [RegularExpression(@"^(A|B|AB|O)[+-]$", ErrorMessage = "Invalid blood type.")]
        [Display(Name = "Blood Type")]
        public string? BloodType { get; set; }

        [Phone(ErrorMessage = "Invalid contact number.")]
        [Display(Name = "Contact Number")]
        public string? ContactNo { get; set; }

        [Display(Name = "Donation List")]
        public List<Donation> DonationList { get; set; } = new List<Donation>();
    }


}
