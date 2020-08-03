using MarketMVC.Data;
using MarketMVC.Helpers;
using MarketMVC.Models;
using MarketMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarketMVC.Controllers
{
    public class OrdersController : Controller
    {
        private MarketMVCContext db = new MarketMVCContext();


        // GET: Orders
        public ActionResult NewOrder()
        {
            var orderView = new OrderView();
            orderView.Customer = new Models.Customer();
            orderView.Products = new List<ProductOrder>();
            //Construir a view baseada neste controlador

            Session["OrderView"] = orderView;//variavel de sessao

            //var list = db.Customers.ToList();
            //passar os dados para o lado da view
            ViewBag.CustomerId = new SelectList(CombosHelper.GetCustomersNames(), "CustomerID", "CustomerFullName");


            return View(orderView);
        }

        public ActionResult AddProduct()
        {

            ViewBag.ProductID = new SelectList(CombosHelper.GetProducts(), "ProductID", "Description");

            return View();
        }
        
        [HttpPost]
        public ActionResult AddProduct(ProductOrder productOrder)
        {
            var orderView = Session["orderView"] as OrderView;//isto serve para passar a orderview que esta em cima num outro controlador para este controlador

            var ProductID = int.Parse(Request["ProductID"]);//isto tras o valor da combo que e escolhido quando se carrega no botao adiciona produto


            //Caso nao haja produto escolhido
            if (ProductID==0)
            {
                ViewBag.ProductID = new SelectList(CombosHelper.GetProducts(), "ProductID", "Description");
                ViewBag.Error = "Deve selecionar um produto";

                return View(productOrder);
            }

            //Verifica se produto existe
            var Product = db.Products.Find(ProductID);

            if (Product==null)
            {
                ViewBag.ProductID = new SelectList(CombosHelper.GetProducts(), "ProductID", "Description");
                ViewBag.Error = "O produto selecionado nao se encontra disponivel";

                return View(productOrder);
            }

            productOrder = orderView.Products.Find(p => p.ProductID == ProductID);

            if (productOrder==null)
            {
                productOrder = new ProductOrder
                {
                    Description = Product.Description,//Isto e possivel porque o product order herda do product
                    Price = Product.Price,
                    ProductID = Product.ProductID,
                    Quantity = float.Parse(Request["Quantity"])//Valor que esta na caixa quantity do lado do form
                };
                orderView.Products.Add(productOrder);
            }
            else
            {
                productOrder.Quantity += float.Parse(Request["Quantity"]);//vai buscar a quantidade da caixa de texto e adiciona
            }

            ViewBag.CustomerID = new SelectList(CombosHelper.GetCustomersNames(), "CustomerID", "CustomerFullName");

            return View("NewOrder", orderView);

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}