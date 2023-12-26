using BloodDonor_CRUD.Models;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonor_CRUD.ViewComponents
{
    public class DonationChart : ViewComponent
    {

        public IViewComponentResult Invoke(List<Donation> data)
        {

            ViewBag.Count = data.Count;
            ViewBag.Total = data.Sum(i => i.YearsSinceLastDonation);

            return View(data);
        }

    }
}
