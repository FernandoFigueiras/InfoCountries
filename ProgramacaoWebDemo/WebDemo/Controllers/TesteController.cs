using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebDemo.Controllers
{
    public class TesteController : Controller
    {
        // GET: Teste
        //public string Index()//route
        //{
        //    return "Hello World";
        //}

        public ActionResult Index()//route
        {

            //ViewData["Nome"] = "Jonas";
            //ViewData["Email"] = "jonas@slb.pt";
            //ViewData["Curso"] = "TPSI";
            //ViewData["DataInicio"] = new DateTime(2020, 01, 10);


            ViewBag.Nome = "Jonas";
            ViewBag.Email = "jonas@slb.pt";
            ViewBag.Curso = "TPSI";
            ViewBag.DataInicio = new DateTime(2020, 01, 10);


            ViewBag.Paises = new List<string>()
            {
                "Brasil",
                 "Portugal",
                  "Espanha",
                   "França",
                    "Italia",
            };


            ViewData["Paises"] = new List<string>()
            {
                "Brasil",
                 "Portugal",
                  "Espanha",
                   "França",
                    "Italia",
            };

            return View();
        }

        public string Inicio()
        {
            return "A minha primeira aplicação ASP.NET MVC";
        }

        public string Saudacao(int? ID)//O ponto de interrogação é para permitir não pôr nada no parametro
        {
            return $"Código recebido da URL = {ID.ToString()}";
        }
    }
}