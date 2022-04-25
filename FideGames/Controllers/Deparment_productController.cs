using FideGames.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FideGames.Controllers
{
    public class Deparment_productController : Controller
    {
        // GET: Deparment_product
        Models.proyectoFideGamesEntities1 db = new proyectoFideGamesEntities1();
        // Listar Marcas
        public ActionResult Listadeparment_product()
        {
            IEnumerable<deparment_product> list = db.deparment_product.ToList();
            return View(list);
        }

        //crear marca
        public ActionResult Creardeparment_product()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Creardeparment_product([Bind(Include = "deparment_product_id,deparment_product_Name")] deparment_product deparment_Product)
        {
            if (ModelState.IsValid)
            {
                db.deparment_product.Add(deparment_Product);
                db.SaveChanges();
                ViewBag.exito = "Se ha agregado el nuevo departamento de productos";
            }
            return RedirectToAction("Listadeparment_product");
        }

        // Detalles de departamento
        public ActionResult Detallardeparment_product(int id)
        {
            deparment_product deparment_Product = db.deparment_product.Find(id);
            return View(deparment_Product);
        }
        public ActionResult Editardeparment_product(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            deparment_product deparment_Product = db.deparment_product.Find(id);
            if (deparment_Product == null)
            {
                return HttpNotFound();
            }
            return View(deparment_Product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editardeparment_product([Bind(Include = "deparment_product_id,deparment_product_Name")] deparment_product deparment_Product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deparment_Product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Listadeparment_product");
            }
            return View(deparment_Product);
        }
        //Eliminar marca
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Eliminardeparment_product(deparment_product deparment_Product)
        {
            db.Entry(deparment_Product).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            ViewBag.exito = "Se ha eliminado el departamento de productos";
            return RedirectToAction("Listadeparment_product");

        }
    }
}