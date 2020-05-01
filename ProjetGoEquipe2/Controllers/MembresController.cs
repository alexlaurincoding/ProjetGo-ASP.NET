using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Windows;

namespace ProjetGoEquipe2.Controllers
{
    public class MembresController : Controller
    {

        // GET: Membre
        public ActionResult Index()
        {
            if (Session["Connected"] == null || (bool)Session["Connected"] == false)
            {
                return RedirectToAction("Identifier");
            }
            return View();
        }


        // GET: Membre/Inscription
        public ActionResult Inscription()
        {
            return View();
        }

        // POST: Membre/Inscription
        [HttpPost]
        public ActionResult Inscription(Membre membre)
        {
            try
            {
                Singleton.Instance.db.Membres.Add(membre);
                Singleton.Instance.db.SaveChanges();
                Session["Connected"] = true;
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Membre/Identifier
        public ActionResult Identifier()
        {
            return View();
        }

        // POST: Membre/Identifier
        [HttpPost]
        public ActionResult Identifier(Membre membre)
        {
            foreach (Membre m in Singleton.Instance.db.Membres)
            {
                if (m.nomUsager == membre.nomUsager)
                {
                    if (m.motPasse == membre.motPasse)
                    {
                        Session["Connected"] = true;
                        Session["Usager"] = membre.nomUsager;
                        return RedirectToAction("Index");
                    }
                }
            }
            ViewBag.Erreur = "Informations invalides.";
            return View();
        }

        public ActionResult Deconnexion()
        {

            Session["Connected"] = false;
            return RedirectToAction("Index", "Home");
        }


        // GET: Membre/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
