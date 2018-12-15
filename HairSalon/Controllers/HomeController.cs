using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public ActionResult Index()
        {
            List<Stylist> foundStylist = Stylist.Find(8);
            Specialty foundSpecialty = Specialty.Find(3);
            foundStylist[0].AddSpecialty(foundSpecialty);
            return View();
        }
    }
}
