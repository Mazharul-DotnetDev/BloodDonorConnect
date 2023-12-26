using Microsoft.EntityFrameworkCore;

namespace BloodDonor_CRUD.Models
{
    public class DonorContext : DbContext
    {
        public DbSet<Donor> Donors { get; set; }
        public DbSet<Donation> Donations { get; set; }

        public DonorContext(DbContextOptions opt) : base(opt)
        {

        }
    }
}
