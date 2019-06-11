using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CCMS.Data;
using CCMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace CCMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly CCMSBuildDbContext _context;

        public HomeController(CCMSBuildDbContext context)
        {
            _context = context;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Progress()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Petition()
        {
            PetitionViewModel petitionViewModel = new PetitionViewModel();

            petitionViewModel.Signatures = _context.Petition.OrderByDescending(x => x.Date).ToList();

            petitionViewModel.SigCount = _context.Petition.Count();

            petitionViewModel.UniqueOrginizations = _context.Petition.GroupBy(g => g.Orginization).Count();

            return View(petitionViewModel);
        }

        public IActionResult Sign(string Name, string Orginization)
        {
            if (Name != null && Orginization != null)
            {
                Petition petition = new Petition();

                petition.Name = Name;

                petition.Orginization = Orginization;

                petition.Date = DateTime.Now;

                _context.Petition.Add(petition);

                _context.SaveChanges();

                return RedirectToAction("Petition");
            }

            ModelState.AddModelError("Name", "Name is Required");

            ModelState.AddModelError("Orginization", "Orginization is Required");

            return RedirectToAction("Petition");
        }
    }
}