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

        [HttpGet("/stylists/{stylistId}")]
        public ActionResult Show(int stylistId)
        {
            Dictionary<string, object> myDic = new Dictionary<string, object> ();
            List<Stylist> foundStylist = Stylist.Find(stylistId);
            List<Client> stylistClients = Client.GetAllClientsByStylistId(stylistId);
            myDic.Add("stylist", foundStylist);
            myDic.Add("clients", stylistClients);
            return View(myDic);
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

        [HttpPost("/stylists/{stylistId}/clients/new")]
        public ActionResult CreateClient(string ClientName, int stylistId)
        {
            Client newClient = new Client(ClientName, stylistId);
            newClient.Save();
            Dictionary<string, object> myDic = new Dictionary<string, object> ();
            List<Stylist> foundStylist = Stylist.Find(stylistId);
            List<Client> stylistClients = Client.GetAllClientsByStylistId(stylistId);
            myDic.Add("stylist", foundStylist);
            myDic.Add("clients", stylistClients);
            return View("Show", myDic);
        }

        [HttpGet("/stylists/delete/all")]
        public ActionResult DeleteAll()
        {
            Stylist.ClearAll();
            return RedirectToAction("Index");
        }










    }
}