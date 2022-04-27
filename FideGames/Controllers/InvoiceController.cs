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
        invoice_status status = new invoice_status();

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
            var clients = db.clients.ToList();
            List<SelectListItem> clientsList = new List<SelectListItem>();
            foreach (var client in clients)
            {
                clientsList.Add(new SelectListItem
                {
                    Value = client.client_id.ToString(),
                    Text = (
                    client.client_id_card + " " + client.client_name1 + " " + client.client_name2 + " " + client.client_lastname1 + " " + client.client_lastname2
                    )
                });
            }
            ViewBag.Clients = clientsList;
            ViewBag.Products = db.product.ToList();

            return View();
        }

        // POST: Invoice/Create
        [HttpPost]
        public ActionResult Create(invoice invoice)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.invoice.Add(invoice);
                    db.SaveChanges();
                    ViewBag.Mensaje = "Se ha Creado Factura";
                    return RedirectToAction("Index");
                }
                catch
                {
                    var clients = db.clients.ToList();
                    List<SelectListItem> clientsList = new List<SelectListItem>();
                    foreach (var client in clients)
                    {
                        clientsList.Add(new SelectListItem
                        {
                            Value = client.client_id.ToString(),
                            Text = (
                            client.client_id_card + " " + client.client_name1 + " " + client.client_name2 + " " + client.client_lastname1 + " " + client.client_lastname2
                            )
                        });
                    }
                    ViewBag.Clients = clientsList;
                    ViewBag.error = "Error al incluir el Dato";
                    return View();
                }
            }
            else
            {
                return Redirect("Create");
            }

        }

        public ActionResult Add(int id)
        {
            List<SelectListItem> ProductsList = new List<SelectListItem>();
            var Products = db.product.ToList();

            foreach (var product in Products)
            {
                ProductsList.Add(new SelectListItem
                {
                    Value = product.product_id.ToString(),
                    Text = (product.product_name + " Precio: ₡" + product.price)
                });
            };

            List<SelectListItem> InvoiceList = new List<SelectListItem>();
            var Invlice = db.invoice.ToList();
            foreach (var invoice in Invlice)
            {
                InvoiceList.Add(new SelectListItem
                {
                    Value = invoice.id_invoice.ToString(),
                    Text = invoice.payment_des
                });
            }

            ViewBag.Product = ProductsList;
            ViewBag.Invoice = InvoiceList;
            ViewBag.InvoiceNumber = id;
            return View();
        }

        [HttpPost]
        public ActionResult Add(invoice_detail detail)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int id = Convert.ToInt32(detail.product_id);
                    var product = db.product.Find(id);
                    detail.product_price = product.price;
                    product.quantities = (product.quantities) - 1;
                    var invoiceUpdate = db.invoice.Find(id);
                    invoiceUpdate.sale_total = (invoiceUpdate.sale_total) + product.price;
                    db.invoice_detail.Add(detail);
                    db.Entry(product).State = System.Data.Entity.EntityState.Modified;
                    db.Entry(invoiceUpdate).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.error = "Error al incluir el Dato";
                    return View(detail.id_invoice);
                }

            }
            else
            {
                ViewBag.error = "Error en el Modelo";
                return View(detail.id_invoice);
            }


        }

        // GET: Invoice/Edit/5
        public ActionResult Edit(int id)
        {
            invoice data = db.invoice.Find(id);

            List<SelectListItem> StateList = new List<SelectListItem>();
            List<invoice_status> state = db.invoice_status.ToList();
            foreach (var item in state)
            {
                StateList.Add(new SelectListItem
                {
                    Value = item.invoice_status_id.ToString(),
                    Text = item.invoice_status_name
                });
            }

            ViewBag.Status = StateList;

            return View(data);
        }

        // POST: Invoice/Edit/5
        [HttpPost]
        public ActionResult Edit(invoice invoice)
        {
            if (true)
            {
                try

                {
                    db.Entry(invoice).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.error = "Error al incluir el Dato";
                    List<SelectListItem> StateList = new List<SelectListItem>();
                    List<invoice_status> state = db.invoice_status.ToList();
                    foreach (var item in state)
                    {
                        StateList.Add(new SelectListItem
                        {
                            Value = item.invoice_status_id.ToString(),
                            Text = item.invoice_status_name
                        });
                    }

                    ViewBag.Status = StateList;

                    return View(invoice.id_invoice);
                }
            }
            else
            {
                ViewBag.error = "Error con el Modelo";
                return View(invoice.id_invoice);
            }

        }
    }
}
