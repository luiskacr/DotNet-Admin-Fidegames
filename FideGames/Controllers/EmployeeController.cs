using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FideGames.Models;
using FideGames.Controllers;

namespace FideGames.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        proyectoFideGamesEntities1 db = new proyectoFideGamesEntities1();

        // GET: Employee
        public ActionResult Index()
        {
            IEnumerable<Employee> list = db.Employee.ToList();
            return View(list);
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            Employee employee = db.Employee.Find(id);
            return View(employee);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            var list = new List<String>() { "Hombre", "Mujer", "Otro" };
            ViewBag.list = list;   
            
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            try
            {
                db.Employee.Add(employee);
                db.SaveChanges();
                ViewBag.exito = "Se ha creado el empleado";
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.error = "Error al Crear al Empleado";
                var list = new List<String>() { "Hombre", "Mujer", "Otro" };
                ViewBag.list = list;
                return View();
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            Employee employee = db.Employee.Find(id);
            var list = new List<String>() { "Hombre", "Mujer", "Otro" };
            ViewBag.list = list;
            return View(employee);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(employee).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else {
                    ViewBag.error = "El Modelo no es Valido";
                    return View(employee.employeeId);
                }

            }
            catch
            {
                ViewBag.error = "Error en la Base de Datos al actualizar el empleado";
                return View(employee.employeeId);
            }
        }

        /*
                 // GET: Employee/Delete/5
            public ActionResult Delete(int id)
            {
                return View();
            }
         */


        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(Employee employee)
        {
            
            try
            {
                if (ModelState.IsValid)
                {
                    Employee delete = db.Employee.Find(employee.employeeId);
                    db.Entry(delete).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                    ViewBag.exito = "Se ha Eliminado el empleado" + employee.Users;
                    return RedirectToAction("Index");
                }
                else {
                    ViewBag.error = "El Modelo no es valido";
                    return RedirectToAction("Index", employee.employeeId);
                }

            }
            catch
            {

                ViewBag.error = "Error al Eliminar el empleado";
                return RedirectToAction("Index",employee.employeeId);
            }
        }

        public ActionResult CreateUser()
        {
            ViewBag.employeeId = new SelectList(db.Employee, "employeeId", "firstName");
            ViewBag.rol = new SelectList(db.Roles, "rolId", "roleName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser([Bind(Include = "userId,userName,password,user_active,userImage,employeeId,rol")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(users);
                db.SaveChanges();
                return RedirectToAction("ListaUsers", "Users");
            }

            ViewBag.employeeId = new SelectList(db.Employee, "employeeId", "firstName", users.employeeId);
            ViewBag.rol = new SelectList(db.Roles, "rolId", "roleName", users.rol);
            return View(users);
        }
    }
}
