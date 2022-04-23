using FideGames.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace FideGames.Controllers
{
    public class UsersController : Controller
    {
        proyectoFideGamesEntities1 db = new proyectoFideGamesEntities1();
        // GET: Users
        public ActionResult ListaUsers()
        {
            IEnumerable<Users> list = db.Users.ToList();
            return View(list);
        }

        // GET: Detalles
        public ActionResult Detallar(int id)
        {
            Users users = db.Users.Find(id);
            return View(users);
        }
        // POST: Employee/Delete/5

        [HttpPost]
        public ActionResult EliminarUsers(Users users)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    Users eliminar = db.Users.Find(users.userId);
                    db.Entry(eliminar).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                    ViewBag.exito = "Se ha Eliminado el usuario" + users.userName;
                    return RedirectToAction("ListaUsers");
                }
                else
                {
                    ViewBag.error = "El Modelo no es valido";
                    return RedirectToAction("ListaUsers", users.userId);
                }

            }
            catch
            {

                ViewBag.error = "Error al Eliminar el Usuario";
                return RedirectToAction("ListaUsers", users.userId);
            }
        }



    }
}