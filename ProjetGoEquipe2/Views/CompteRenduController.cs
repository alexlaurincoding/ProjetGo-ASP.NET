using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetGoEquipe2.Views
{
    public class CompteRenduController : Controller
    {
        // GET: CompteRendu
        public ActionResult Index()
        {
            if (Session["Connected"] == null || (bool)Session["Connected"] == false)
            {
                return RedirectToAction("Identifier", "Membres");
            }
            return View();
        }

        // GET: CompteRendu/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CompteRendu/Ajouter
        public ActionResult Ajouter()
        {
            if (Session["Connected"] == null || (bool)Session["Connected"] == false)
            {
                return RedirectToAction("Identifier", "Membres");
            }
            return View();
        }

        // POST: CompteRendu/Create
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

        // GET: CompteRendu/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CompteRendu/Edit/5
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

        // GET: CompteRendu/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CompteRendu/Delete/5
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
