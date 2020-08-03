using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAplication2.Models;

namespace WebAplication2.Controllers
{
    public class ClientesController : Controller
    {
        // GET: Clientes
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Cliente cliente)
        {
            return View("ExibirDados", cliente);//Uma action pode chamar uma view de outra action sem correr a action
        }

        public ActionResult ExibirDados(Cliente cliente)//Esta action não vai ser executada, a view é chamada pela action do POST index
        {
            return View(cliente);
        }
    }
}