using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FideGames.Models;
using FideGames.Clases;

namespace FideGames.Controllers
{

    public class HomeController : Controller
    {

        proyectoFideGamesEntities1 db = new proyectoFideGamesEntities1();
        Users users = new Users();
        Converter converter = new Converter();  

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Users user) {
            if (ModelState.IsValid) {
                user.password = converter.ConverttoSha256(user.password);
                bool IsValidUser = Isvalid(user);
                if (IsValidUser) {
                    FormsAuthentication.SetAuthCookie(user.userName, false);
                    Session["usuario"] = user;
                    Session["name"] = user.userName;
                    Users us = findUser(user);
                    Session["id"] = us.userId;
                    if (us.userImage == null)
                    {
                        Session["image"] = "";
                    }
                    else {
                        Session["image"] = us.userImage;
                    } 
                    Session["Rol"] = us.rol;
                    return RedirectToAction("Index", "Home");
                }
            }
            //ModelState.AddModelError("", "Usuario o Contrasena Invalido");
            ViewBag.Mensaje = "Usuario o Contrasena Invalido";
            return View("Login");
        }

        [Authorize]
        public ActionResult logout() {
            Session["usuario"] = null;
            Session["name"] = null;
            Session["id"] = null;
            Session["image"] = null;
            Session["Rol"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        [Authorize]
        public ActionResult myProfile(int id) {
            Users user = findUsersById(id);
            return View(user);
        }


        [Authorize]
        public ActionResult ChangePassword(int Id) {
            Users user = findUsersById(Id);
            return View(user);
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(Users user) {
            int userid = user.userId;
            user.password = converter.ConverttoSha256(user.password);
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.Mensaje = "Se ha Cambiado la Contrasena";
                    return View("Index", user);
                }
                catch (Exception ex)
                {
                    ViewBag.error = " Al Guardar en la Base de Datos";
                    return ChangePassword(userid);
                }
            }
            else { 
            
            ViewBag.error = " Al Guardar en la Base de Datos";
                    return ChangePassword(userid);
            }

        }

        public bool Isvalid(Users user)
        {
            return db.Users.Where(u => u.userName == user.userName && u.password == user.password).FirstOrDefault() != null;
        }

        public Users findUser(Users user) {

            return db.Users.Where(u => u.userName == user.userName && u.password == user.password).FirstOrDefault<Users>();
        }
        public Users findUsersById(int id)
        {

            return db.Users.Where(u => u.userId == id).FirstOrDefault<Users>();
        }
        public Users findUsersByName(String name)
        {

            return db.Users.Where(u => u.userName == name).FirstOrDefault<Users>();
        }


    }
}