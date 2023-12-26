using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BloodDonor_CRUD.Models;

namespace BloodDonor_CRUD.Controllers
{
    public class DonorsController : Controller
    {
        private readonly DonorContext _context;

        public DonorsController(DonorContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var data = await _context.Donors.Include(i => i.DonationList).ToListAsync();


            ViewBag.Count = data.Count;
            ViewBag.GrandTotal = data.Sum(i => i.DonationList.Sum(l => l.YearsSinceLastDonation));
            ViewBag.Average = data.Count > 0 ? data.Average(i => i.DonationList.Sum(l => l.YearsSinceLastDonation)) : 0;

            return View(data);
        }




        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donor = await _context.Donors.Include(i => i.DonationList)
                .FirstOrDefaultAsync(m => m.DonorId == id);
            if (donor == null)
            {
                return NotFound();
            }

            return View(donor);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Donor());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DonorName,BirthOfDate,BloodType,ContactNo,DonationList")] Donor donor, string command = "")
        {
            if (command == "Add")
            {
                donor.DonationList.Add(new());
                return View(donor);
            }

            else if (command.Contains("delete"))
            {
                int idx = int.Parse(command.Split('-')[1]);

                donor.DonationList.RemoveAt(idx);

                ModelState.Clear();

                return View(donor);
            }


            if (ModelState.IsValid)
            {
                _context.Add(donor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(donor);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donor = await _context.Donors.Include(i => i.DonationList).FirstOrDefaultAsync(i => i.DonorId == id);

            if (donor == null)
            {
                return NotFound();
            }
            return View(donor);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DonorId,DonorName,BirthOfDate,BloodType,ContactNo, DonationList")] Donor donor, string command = "")
        {
            if (command == "Add")
            {
                donor.DonationList.Add(new());
                return View(donor);
            }

            else if (command.Contains("delete"))
            {
                int idx = int.Parse(command.Split('-')[1]);

                donor.DonationList.RemoveAt(idx);
                ModelState.Clear();

                return View(donor);
            }


            if (id != donor.DonorId)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonorExists(donor.DonorId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(donor);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donor = await _context.Donors.Include(i => i.DonationList)
                .FirstOrDefaultAsync(m => m.DonorId == id);
            if (donor == null)
            {
                return NotFound();
            }

            return View(donor);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {


            await _context.Database.ExecuteSqlAsync($"exec spDeleteDonor {id}");

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonorExists(int id)
        {
            return _context.Donors.Any(e => e.DonorId == id);
        }
    }
}
