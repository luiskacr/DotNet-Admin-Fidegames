using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FideGames.Models;
using FideGames.Clases;

namespace FideGames.Controllers
{
    [Authorize]
    public class InvoiceController : Controller
    {
        proyectoFideGamesEntities1 db = new proyectoFideGamesEntities1();
        invoice invoice = new invoice();
        invoice_detail details = new invoice_detail();

        // GET: Invoice
        public ActionResult Index()
        {
            IEnumerable<invoice> list = db.invoice.ToList();
            return View(list);
        }

        // GET: Invoice/Details/5
        public ActionResult Details(int id)
        {
            DetailInvoiceViewData data = new DetailInvoiceViewData();
            invoice invoice = db.invoice.Find(id);
            IEnumerable<invoice_detail> detailsList = (from invoice_detail in db.invoice_detail
                                                       where invoice_detail.id_invoice == id
                                                       select invoice_detail).ToList();
            data.invoice = invoice;
            data.invoice_detail = detailsList;
            
            return View(data);
        }

        // GET: Invoice/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Invoice/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Invoice/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Invoice/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Invoice/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Invoice/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
