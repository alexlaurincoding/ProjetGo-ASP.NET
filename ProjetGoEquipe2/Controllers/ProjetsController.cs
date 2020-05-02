using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        // GET: Projet/MesProjets (Accueil membres)
        public ActionResult MesProjets()
        {
            if (Session["Connected"] == null || (bool)Session["Connected"] == false)
            {
                return RedirectToAction("Identifier", "Membres");
            }
            return View();
        }

        // GET: Projets/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["Connected"] == null || (bool)Session["Connected"] == false)
            {
                return RedirectToAction("Identifier", "Membres");
            }

            Projet projet = Singleton.Instance.db.Projets.Where(p => p.idProjet == id).FirstOrDefault();

            return View(projet);
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
        public ActionResult Ajouter(Projet projet, string statut, string description, string sommaire, string budget, DateTime debutEstime, DateTime finEstimee, string frequence)
        {
            try { 
            

                int frequnceNum;
                double budgetNum;
                bool success;

                projet.statut = statut;
                projet.idResponsable = (string)Session["Usager"];
                projet.descriptionCourte = description;
                projet.sommaire = sommaire;
                projet.debutEstime = debutEstime.Date;
                projet.finEstimee = finEstimee.Date;
                success = Int32.TryParse(frequence, out frequnceNum);
                if (success)    projet.frequenceComptesRendus = frequnceNum;
                success = Double.TryParse(budget, out budgetNum);
                if (success)    projet.budget = budgetNum;
                Singleton.Instance.db.Projets.Add(projet);
                Singleton.Instance.db.SaveChanges();
                return RedirectToAction("MesProjets", "Projets");

            }
            catch
            {
                return View();
            }
        }


        // GET: Projets/Modifier/5
        public ActionResult Modifier(int? id)
        {
            if (Session["Connected"] == null || (bool)Session["Connected"] == false)
            {
                return RedirectToAction("Identifier", "Membres");
            }

            Projet projet = Singleton.Instance.db.Projets.Where(p => p.idProjet == id).FirstOrDefault();

            return View(projet);
        }

        // POST: Projets/Edit/5
        [HttpPost]
        public ActionResult Modifier(int id, Projet projetModifie, string description )
        {
            try
            {
                if (ModelState.IsValid)
                {

                    Projet ancienneVersion = Singleton.Instance.db.Projets.Where(p => p.idProjet == id).FirstOrDefault();
                    ancienneVersion.budget = projetModifie.budget;
                    ancienneVersion.dateProchainCompteRendu = projetModifie.dateProchainCompteRendu;
                    ancienneVersion.debutEstime = projetModifie.debutEstime;
                    ancienneVersion.debutReel = projetModifie.debutReel;
                    ancienneVersion.descriptionCourte = description;
                    ancienneVersion.etatAvancement = projetModifie.etatAvancement;
                    ancienneVersion.finEstimee = projetModifie.finEstimee;
                    ancienneVersion.finReelle = projetModifie.finReelle;
                    ancienneVersion.frequenceComptesRendus = projetModifie.frequenceComptesRendus;
                    ancienneVersion.sommaire = projetModifie.sommaire;
                    ancienneVersion.statut = projetModifie.statut;
                    ancienneVersion.titre = projetModifie.titre; 
                    Singleton.Instance.db.SaveChanges();
                    return RedirectToAction("MesProjets", "Projets");
                }
                MessageBox.Show("Un probleme");

                return View();

            }
            catch
            {
                return View();
            }
        }

        // GET: Projets/Delete/5
        public ActionResult Effacer(int? id)
        {
            if (Session["Connected"] == null || (bool)Session["Connected"] == false)
            {
                return RedirectToAction("Identifier", "Membres");
            }

            Projet projet = Singleton.Instance.db.Projets.Find(id);
            return View(projet);
        }

        // POST: Projets/Delete/5
        [HttpPost]
        public ActionResult Effacer(int id)
        {
            try
            {
                Projet projet = Singleton.Instance.db.Projets.Find(id);
                Singleton.Instance.db.Projets.Remove(projet);
                Singleton.Instance.db.SaveChanges();
                return RedirectToAction("MesProjets", "Projets");
            }
            catch
            {
                return View();
            }
        }
    }
}
