using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class ClientController : Controller
    {
        [HttpGet("/clients")]
        public ActionResult Index()
        {
            List<Client> allClients = Client.GetAll();
            return View(allClients);
        }

        [HttpGet("/clients/{id}")]
        public ActionResult Show(int id)
        {
            Client foundClient = Client.Find(id);
            return View(foundClient);
        }

        [HttpPost("/clients/{id}/edit")]
        public ActionResult EditName(int id, string newName)
        {
            Client foundClient = Client.Find(id);
            foundClient.EditName(newName);
            Client updatedClient = Client.Find(id);
            return View("Show", updatedClient);
        }

        [HttpGet("/clients/delete/all")]
        public ActionResult DeleteAll()
        {
            Client.ClearAll();
            return RedirectToAction("Index");
        }

        [HttpGet("clients/{id}/delete")]
        public ActionResult DeleteClient(int id)
        {
            Client foundClient = Client.Find(id);
            foundClient.DeleteClient();
            return RedirectToAction("Index");
        }
        
    }
}