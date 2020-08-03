using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppMVCStore.Context;
using WebAppMVCStore.Models;
using System.Net;
using System.Drawing.Printing;
using System.Data.Entity;

namespace WebAppMVCStore.Controllers
{
    public class ProductController : Controller
    {
        private StoreContext db = new StoreContext();//Isto é o que se fazia com o LINQ

        // GET: Product
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = db.Products.Find(id);  //vai à tabela procurar o produto que tem o id passado como argumento

            if (product==null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Product product) // Post do create porque já criamos a view do create para o GET
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Products.Add(product); //Adiciona o produto à base de dados
                    db.SaveChanges(); //Salva a alterações na base de dados
                    return RedirectToAction("Index");
                }

                return View(product);//retorna a view para o produto inserido
            }
            catch
            {
                return View(product); //Caso alguma coisa corra mal retorna a view para o produto inserido
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = db.Products.Find(id);

            if (product==null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            try
            {
                //Aqui utiliza-se o model state pois é um produto já existente
                if (ModelState.IsValid)//Se o produto é válido, como vem dos models coloca-se só model
                {
                    db.Entry(product).State= EntityState.Modified;//Isto vai colocar na base de dados como entry a entity modificada Isto vel do model
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(product);
            }
            catch
            {
                return View(product);
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = db.Products.Find(id);

            if (product==null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    product = db.Products.Find(id);
                    if (product==null)
                    {
                        return HttpNotFound();
                    }
                    db.Products.Remove(product);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(product);
            }
            catch
            {
                return View(product);
            }
        }
    }
}
