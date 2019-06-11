using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CCMS.Data;
using CCMS.Models;
using CCMS.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CCMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmail _emailService;

        private readonly CCMSBuildDbContext _context;

        public HomeController(CCMSBuildDbContext context, IEmail emailService)
        {
            _context = context;

            _emailService = emailService;

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

        [HttpPost]
        public async Task<IActionResult> Contact(string email, string subject, string message)
        {
            if (email != null && subject != null && message != null)
            {
                await _emailService.SendEmail(email, subject, message);

                ModelState.AddModelError("email", "Message Sent");

                return View();
            }

            ModelState.AddModelError("email", "All Fields Must Be Completed");

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

        [HttpPost]
        public IActionResult Petition(string Name, string Orginization)
        {
            if (Name != null && Orginization != null)
            {
                Petition petition = new Petition();

                petition.Name = Name;

                petition.Orginization = Orginization;

                petition.Date = DateTime.Now;

                _context.Petition.Add(petition);

                _context.SaveChanges();

                PetitionViewModel petitionViewModel1 = new PetitionViewModel();

                petitionViewModel1.Signatures = _context.Petition.OrderByDescending(x => x.Date).ToList();

                petitionViewModel1.SigCount = _context.Petition.Count();

                petitionViewModel1.UniqueOrginizations = _context.Petition.GroupBy(g => g.Orginization).Count();

                ModelState.AddModelError("Name", "Thank You!");

                return View(petitionViewModel1);
            }

            PetitionViewModel petitionViewModel = new PetitionViewModel();

            petitionViewModel.Signatures = _context.Petition.OrderByDescending(x => x.Date).ToList();

            petitionViewModel.SigCount = _context.Petition.Count();

            petitionViewModel.UniqueOrginizations = _context.Petition.GroupBy(g => g.Orginization).Count();

            ModelState.AddModelError("Name", "All Fields Must Be Completed");

            return View(petitionViewModel);
        }

    }
}