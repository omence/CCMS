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

        /// <summary>
        /// sends Home page
        /// </summary>
        /// <returns>view</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// sends progress vlog view
        /// </summary>
        /// <returns>view</returns>
        public IActionResult Progress()
        {
            return View();
        }

        /// <summary>
        /// sends contact view
        /// </summary>
        /// <returns>view</returns>
        public IActionResult Contact()
        {
            return View();
        }

        /// <summary>
        /// Emails users message to admin
        /// </summary>
        /// <param name="email"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <returns>Email</returns>
        [HttpPost]
        public async Task<IActionResult> Contact(string email, string subject, string message)
        {
            if (email != null && subject != null && message != null)
            {
                await _emailService.SendEmail(email, subject, message);

                TempData["Message"] = "Sent, we will get back to you ASAP";

                return View();
            }

            ModelState.AddModelError("email", "All Fields Must Be Completed");

            return View();
        }

        /// <summary>
        /// Sends all petition signatures and counts to view
        /// </summary>
        /// <returns>view with object</returns>
        public IActionResult Petition()
        {
            PetitionViewModel petitionViewModel = new PetitionViewModel();

            petitionViewModel.Signatures = _context.Petition.OrderByDescending(x => x.Date).ToList();

            petitionViewModel.SigCount = _context.Petition.Count();

            petitionViewModel.UniqueOrginizations = _context.Petition.GroupBy(g => g.Orginization).Count();

            return View(petitionViewModel);
        }

        /// <summary>
        /// Creates a new signature for petition
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Orginization"></param>
        /// <returns>View with object</returns>
        [HttpPost]
        public IActionResult Petition(string Name, string Orginization)
        {
            if (Name != null && Orginization != null)
            {
                var checkForDuplicates = _context.Petition.FirstOrDefault(n => n.Name == Name && n.Orginization == Orginization);

                if (checkForDuplicates == null)
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

                    TempData["Success"] = "Thanks for the Support!";

                    return View(petitionViewModel1);
                }

                PetitionViewModel petitionViewModel2 = new PetitionViewModel();

                petitionViewModel2.Signatures = _context.Petition.OrderByDescending(x => x.Date).ToList();

                petitionViewModel2.SigCount = _context.Petition.Count();

                petitionViewModel2.UniqueOrginizations = _context.Petition.GroupBy(g => g.Orginization).Count();

                ModelState.AddModelError("Name", "Sorry You Can Only Sign Once");

                return View(petitionViewModel2);

            }

            PetitionViewModel petitionViewModel = new PetitionViewModel();

            petitionViewModel.Signatures = _context.Petition.OrderByDescending(x => x.Date).ToList();

            petitionViewModel.SigCount = _context.Petition.Count();

            petitionViewModel.UniqueOrginizations = _context.Petition.GroupBy(g => g.Orginization).Count();

            ModelState.AddModelError("Name", "All Fields Must Be Completed");

            return View(petitionViewModel);
        }

        /// <summary>
        /// sends donation view
        /// </summary>
        /// <returns>view</returns>
        public IActionResult Donation()
        {
            return View();
        }
    }
}