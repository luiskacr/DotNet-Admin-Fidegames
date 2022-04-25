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
    public class RolesController : Controller
    {
        // GET: Roles

        Models.proyectoFideGamesEntities1 db = new proyectoFideGamesEntities1();
        // Listar roles
        public ActionResult ListaRoles()
        {
            IEnumerable<Roles> list = db.Roles.ToList();
            return View(list);
        }
        public ActionResult CrearRol()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearRol([Bind(Include = "rolId,roleName")] Roles roles)
        {
            if (ModelState.IsValid)
            {db.Roles.Add(roles);
            db.SaveChanges();
            ViewBag.exito = "Se ha creado el Rol";
            }
            return RedirectToAction("ListaRoles");
        }

        // Detalles roles
        public ActionResult DetallarRol(int id)
        {
           Roles roles = db.Roles.Find(id);
            return View(roles);
        }
        public ActionResult EditarRol(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roles roles = db.Roles.Find(id);
            if (roles == null)
            {
                return HttpNotFound();
            }
            return View(roles);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarRol([Bind(Include = "rolId,roleName")] Roles roles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListaRoles");
            }
            return View(roles);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EliminarRol(Roles roles)
        {
            db.Entry(roles).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            ViewBag.exito = "Se ha Modificado el Rol";
            return RedirectToAction("ListaRoles");

        }


    }
}