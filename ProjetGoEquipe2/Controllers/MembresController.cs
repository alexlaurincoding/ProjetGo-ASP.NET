using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetGoEquipe2.Controllers
{
    public class MembresController : Controller
    {

        // GET: Membre
        public ActionResult Index()
        {
            return View();
        }


        // GET: Membre/Inscription
        public ActionResult Inscription()
        {
            return View();
        }

        // GET: Membre/Identifier
        public ActionResult Identifier()
        {
            return View();
        }

        // GET: Membre/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Membre/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Membre/Create
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

        // GET: Membre/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Membre/Edit/5
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

        // GET: Membre/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Membre/Delete/5
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
