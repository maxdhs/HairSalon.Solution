using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class StylistController : Controller
    {
        [HttpGet("/stylists")]
        public ActionResult Index()
        {
            List<Stylist> allStylist = Stylist.GetAll();
            return View(allStylist);
        }

        [HttpGet("/stylists/{id}")]
        public ActionResult Show(int id)
        {
            List<Stylist> foundStylist = Stylist.Find(id);
            return View(foundStylist);
        }

        [HttpGet("/stylists/new")]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost("/stylists")]
        public ActionResult Create(string StylistName)
        {
            Stylist newStylist = new Stylist(StylistName);
            newStylist.Save();
            List<Stylist> allStylist = Stylist.GetAll();
            return View("Index", allStylist);
        }
    }
}