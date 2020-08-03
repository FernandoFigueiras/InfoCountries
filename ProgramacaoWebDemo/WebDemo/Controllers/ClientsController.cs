using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;
using WebDemo.Models;

namespace WebDemo.Controllers
{
    public class ClientsController : Controller
    {
        // GET: Clients
        public ActionResult Detail()
        {
            Client client = new Client()
            {
                ClientId = 200,
                Name = "Jonas",
                Email = "jonas@slb.pt",
                PhoneNumber = "210000000"
            };


            ViewBag.Name = client.Name;
            ViewData["Email"] = client.Email;

            return View(client);
        }


        //public ActionResult SaveClient(int clientId, string name, string email, string phoneNumber)
        //{
        //    Client c = new Client
        //    {
        //        ClientId = clientId,
        //        Name = name,
        //        Email = email,
        //        PhoneNumber = phoneNumber
        //    };
        //}

        [HttpPost]//Isto é para o controlador saber que é um post
        public ActionResult SaveClient(Client client)
        {
            return View(client);
        }
    }
}