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
            List<Specialty> specialties = foundStylist[0].GetSpecialties();
            List<Specialty> allSpecialties = Specialty.GetAll();
            myDic.Add("stylist", foundStylist);
            myDic.Add("clients", stylistClients);
            myDic.Add("specialties", specialties);
            myDic.Add("allspecialties", allSpecialties);
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
            List<Specialty> specialties = foundStylist[0].GetSpecialties();
            List<Specialty> allspecialties = Specialty.GetAll();

            myDic.Add("stylist", foundStylist);
            myDic.Add("clients", stylistClients);
            myDic.Add("specialties", specialties);
            myDic.Add("allspecialties", allspecialties);

            return View("Show", myDic);
        }

        [HttpGet("/stylists/delete/all")]
        public ActionResult DeleteAll()
        {
            Stylist.ClearAll();
            return RedirectToAction("Index");
        }

        [HttpGet("/stylists/{id}/delete/")]
        public ActionResult DeleteStylist(int id)
        {
            List<Stylist> foundStylistList = Stylist.Find(id);
            foundStylistList[0].DeleteStylist();
            return RedirectToAction("Index");
        }

        [HttpPost("/stylists/{id}/edit")]
        public ActionResult EditName(int id, string newName)
        {
            List<Stylist> foundStylistList = Stylist.Find(id);
            foundStylistList[0].EditName(newName);
            Dictionary<string, object> myDic = new Dictionary<string, object> ();
            List<Stylist> foundStylist = Stylist.Find(id);
            List<Client> stylistClients = Client.GetAllClientsByStylistId(id);
            List<Specialty> specialties = foundStylist[0].GetSpecialties();
            List<Specialty> allSpecialties = Specialty.GetAll();
            myDic.Add("stylist", foundStylist);
            myDic.Add("clients", stylistClients);
            myDic.Add("specialties", specialties);
            myDic.Add("allspecialties", allSpecialties);
            return View("Show", myDic);
        }


        [HttpPost("/stylists/{id}/specialty/new")]
        public ActionResult AddSpecialty(int id, int specialtyId)
        {
            List<Stylist> foundStylist = Stylist.Find(id);
            Specialty foundSpecialty = Specialty.Find(specialtyId);
            foundStylist[0].AddSpecialty(foundSpecialty);

            Dictionary<string, object> myDic = new Dictionary<string, object> ();
            List<Client> stylistClients = Client.GetAllClientsByStylistId(id);
            List<Specialty> specialties = foundStylist[0].GetSpecialties();
            List<Specialty> allSpecialties = Specialty.GetAll();
            myDic.Add("stylist", foundStylist);
            myDic.Add("clients", stylistClients);
            myDic.Add("specialties", specialties);
            myDic.Add("allspecialties", allSpecialties);
            return View("Show", myDic);
        }


    }
}