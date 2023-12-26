using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BloodDonor_CRUD.Models
{
    public class Donation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DonationId { get; set; }

        [Display(Name = "Donation Place")]
        [StringLength(100, ErrorMessage = "Donation place should be less than 100 characters.")]
        public string? DonationPlace { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Amount of donation must be greater than 0.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Amount of Donation")]
        public decimal AmountOfDonation { get; set; }

        [Required(ErrorMessage = "Last donation date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Last Donation Date")]
        public DateTime LastDonation { get; set; } = DateTime.Now;

        [Display(Name = "Years Since Last Donation")]
        public int YearsSinceLastDonation => CalculateYearsSinceLastDonation();

        private int CalculateYearsSinceLastDonation()
        {
            int years = (int)((DateTime.Today - LastDonation).Days / 365.25);
            return years;
        }
    }

}
