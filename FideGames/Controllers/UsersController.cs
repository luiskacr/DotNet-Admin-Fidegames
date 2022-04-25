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
    public class UsersController : Controller
    {
        proyectoFideGamesEntities1 db = new proyectoFideGamesEntities1();
        // Listar Users
        public ActionResult ListaUsers()
        {
            IEnumerable<Users> list = db.Users.ToList();
            return View(list);
        }

        // Detalles users
        public ActionResult Detallar(int id)
        {
            Users users = db.Users.Find(id);
            return View(users);
        }


        // Editar Users
        public ActionResult EditarUsers(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            ViewBag.employeeId = new SelectList(db.Employee, "employeeId", "firstName", users.employeeId);
            ViewBag.rol = new SelectList(db.Roles, "rolId", "roleName", users.rol);
            return View(users);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarUsers([Bind(Include = "userId,userName,password,user_active,userImage,employeeId,rol")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListaUsers");
            }
            ViewBag.employeeId = new SelectList(db.Employee, "employeeId", "firstName", users.employeeId);
            ViewBag.rol = new SelectList(db.Roles, "rolId", "roleName", users.rol);
            return View(users);
        }

        // Eliminar Users
        public ActionResult EliminarUsers(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        //Eliminar Users
        [HttpPost, ActionName("EliminarUsers")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Users users = db.Users.Find(id);
            db.Users.Remove(users);
            db.SaveChanges();
            return RedirectToAction("ListaUsers");
        }



    }
}