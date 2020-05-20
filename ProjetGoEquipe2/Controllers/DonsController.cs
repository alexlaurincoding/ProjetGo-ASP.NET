using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetGoEquipe2.Controllers
{
    public class DonsController : Controller
    {
        // GET: Dons
        public ActionResult Index()
        {
            return View();
        }

        // GET: Dons/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult DonCompleter()
        {
            return View();
        }

        public ActionResult DonAnnuler()
        {
            return View();
        }

        // GET: Dons/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dons/Create
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

        // GET: Dons/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Dons/Edit/5
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

        // GET: Dons/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Dons/Delete/5
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
