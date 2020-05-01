using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows;

namespace ProjetGoEquipe2.Controllers
{
    public class ProjetsController : Controller
    {
        // GET: Projets
        public ActionResult Index()
        {
            return View();
        }

        // GET: Projets/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Projets/Ajouter
        public ActionResult Ajouter()
        {
            if (Session["Connected"] == null || (bool)Session["Connected"] == false)
            {
                return RedirectToAction("Identifier", "Membres");
            }
            return View();
        }

        // POST: Projets/Ajouter
        [HttpPost]
        public ActionResult Ajouter(Projet projet, string description, string sommaire, string budget, DateTime debutEstime, DateTime finEstimee, string frequence)
        {
            try
            {
                int frequnceNum;
                double budgetNum;
                bool success;

                projet.idResponsable = (string)Session["Usager"];
                projet.descriptionCourte = description;
                projet.sommaire = sommaire;
                projet.debutEstime = debutEstime;
                projet.finEstimee = finEstimee;
                success = Int32.TryParse(frequence, out frequnceNum);
                if (success)    projet.frequenceComptesRendus = frequnceNum;
                success = Double.TryParse(budget, out budgetNum);
                if (success)    projet.budget = budgetNum;
                Singleton.Instance.db.Projets.Add(projet);
                Singleton.Instance.db.SaveChanges();
                return RedirectToAction("Index", "Membres");

            }
            catch
            {
                return View();
            }
        }


        // GET: Projets/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Projets/Edit/5
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

        // GET: Projets/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Projets/Delete/5
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
