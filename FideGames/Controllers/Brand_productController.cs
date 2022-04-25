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
    public class Brand_productController : Controller
    {
        // GET: Brand_product
        Models.proyectoFideGamesEntities1 db = new proyectoFideGamesEntities1();
        // Listar Marcas
        public ActionResult Listabrand_product()
        {
            IEnumerable<brand_product> list = db.brand_product.ToList();
            return View(list);
        }

        //crear marca
        public ActionResult Crearbrand_product()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crearbrand_product([Bind(Include = "brand_product_id,brand_product_Name")] brand_product brand_Product)
        {
            if (ModelState.IsValid)
            {
                db.brand_product.Add(brand_Product);
                db.SaveChanges();
                ViewBag.exito = "Se ha agregado el nuevo producto";
            }
            return RedirectToAction("Listabrand_product");
        }

        // Detalles de marca
        public ActionResult Detallarbrand_product(int id)
        {
            brand_product brand_Product = db.brand_product.Find(id);
            return View(brand_Product);
        }
        public ActionResult Editarbrand_product(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            brand_product brand_Product = db.brand_product.Find(id);
            if (brand_Product == null)
            {
                return HttpNotFound();
            }
            return View(brand_Product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editarbrand_product([Bind(Include = "brand_product_id,brand_product_Name")] brand_product brand_Product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(brand_Product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Listabrand_product");
            }
            return View(brand_Product);
        }
        //Eliminar marca
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Eliminarbrand_product(brand_product brand_Product)
        {
            db.Entry(brand_Product).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            ViewBag.exito = "Se ha eliminado la marca";
            return RedirectToAction("Listabrand_product");

        }
    }
}