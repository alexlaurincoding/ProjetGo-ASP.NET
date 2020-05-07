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

        // GET: Projets/AVenir
        public ActionResult AVenir()
        {

            return View();
        }

        // GET: Projets/Proposes
        public ActionResult Proposes()
        {
            if (Session["Connected"] == null || (bool)Session["Connected"] == false)
            {
                return RedirectToAction("Index", "Projets");
            }

            return View();
        }

        // GET: Projets/Completes
        public ActionResult Completes()
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

            Projet projet = Singleton.Instance.db.Projets.Find(id);

            if (projet == null || projet.idResponsable != (string)Session["Usager"])
            {
                return RedirectToAction("MesProjets");
            }

            return View(projet);
        }

        // GET: Projets/DetailsPublic/5
        public ActionResult DetailsPublic(int? id)
        {

            Projet projet = Singleton.Instance.db.Projets.Find(id);

            if (projet == null || projet.visibilite == "Prive" || projet.visibilite == "Membres" && (Session["Connected"] == null || (bool)Session["Connected"] == false))
            {
                return RedirectToAction("Index");
            }

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
        public ActionResult Ajouter(Projet projet, string visibilite, string statut, string description, string sommaire, string budget, DateTime debutEstime, DateTime finEstimee, string frequence)
        {
            try { 
            

                int frequnceNum;
                double budgetNum;
                bool success;
                projet.visibilite = visibilite;
                projet.idResponsable = (string)Session["Usager"];
                projet.descriptionCourte = description;
                projet.sommaire = sommaire;
                projet.debutEstime = debutEstime.Date;
                projet.finEstimee = finEstimee.Date;
                projet.totalFondsCollectes = 0;
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

            if (projet == null || projet.idResponsable != (string)Session["Usager"])
            {
                return RedirectToAction("MesProjets");
            }

            return View(projet);
        }

        // POST: Projets/Edit/5
        [HttpPost]
        public ActionResult Modifier(int id, Projet projetModifie, string description, string visibilite)
        {
            Projet ancienneVersion = Singleton.Instance.db.Projets.Where(p => p.idProjet == id).FirstOrDefault();
            ancienneVersion.visibilite = visibilite;
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
            ancienneVersion.totalFondsCollectes = projetModifie.totalFondsCollectes;
            ancienneVersion.totalDepenes = projetModifie.totalDepenes;


            try
            {
                Singleton.Instance.db.SaveChanges();
               
                return RedirectToAction("MesProjets", "Projets");

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

            if (projet == null || projet.idResponsable != (string)Session["Usager"])
            {
                return RedirectToAction("MesProjets");
            }

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
