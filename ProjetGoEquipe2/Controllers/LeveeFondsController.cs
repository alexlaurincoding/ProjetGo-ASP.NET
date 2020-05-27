using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows;

namespace ProjetGoEquipe2.Controllers
{
    public class LeveeFondsController : Controller
    {
        // GET: LeveeFonds
        public ActionResult Index()
        {
            return View();
        }

        // GET: LeveeFonds/MesLevees
        public ActionResult MesLevees()
        {
           
            if (Session["Connected"] == null || (bool)Session["Connected"] == false)
            {
                return RedirectToAction("Identifier", "Membres");
            }

            string usager = (string)Session["Usager"];
            ViewBag.Completes = Singleton.Instance.db.LeveeFonds.Where(l => l.Projet.idResponsable == usager).Where(l => l.dateFin < DateTime.Today).ToList().Count() == 0 ? false : true;

            return View();
        }

        // GET: LeveeFonds/Ajouter
        public ActionResult Ajouter()
        {
            if (Session["Connected"] == null || (bool)Session["Connected"] == false)
            {
                return RedirectToAction("Identifier", "Membres");
            }


            return View();
        }

        // POST: LeveeFonds/Ajouter
        [HttpPost]
        public ActionResult Ajouter(LeveeFond levee)
        {
            try
            {
                if (levee.dateDebut == null || levee.dateFin == null || levee.message.IsNullOrWhiteSpace())
                {
                    ViewBag.Erreur = "Oubli";
                    ViewBag.Message = "Tous les champs sont obligatoires.";
                    return View(levee);
                }
                if (levee.dateDebut > levee.dateFin)
                {
                    ViewBag.Erreur = "FinAvant";
                    ViewBag.Message = "La date de fin ne peut précéder la date de début.";
                    return View(levee);
                }
                if (levee.dateDebut < DateTime.Today)
                {
                    ViewBag.Erreur = "DebutPasse";
                    ViewBag.Message = "La date de début ne peut être une date passée.";
                    return View(levee);
                }
               
                levee.montantObtenu = 0;
                Singleton.Instance.db.LeveeFonds.Add(levee);

                Singleton.Instance.db.SaveChanges();
                return RedirectToAction("MesLevees");

            }
            catch
            {
                return View();
            }
        }

        // GET: LeveeFonds/Modifier/5
        public ActionResult Modifier(int id)
        {
            if (Session["Connected"] == null || (bool)Session["Connected"] == false)
            {
                return RedirectToAction("Identifier", "Membres");
            }

            LeveeFond lev = Singleton.Instance.db.LeveeFonds.Where(l => l.IdLeveeFonds == id).FirstOrDefault();

            if (lev.dateFin < DateTime.Today)
            {
                return RedirectToAction("MesLevees", "LeveeFonds");
            }

            return View(lev);
        }

        // POST: LeveeFonds/Modifier/5
        [HttpPost]
        public ActionResult Modifier(int id, LeveeFond levModif)
        {
            LeveeFond ancienneVersion = Singleton.Instance.db.LeveeFonds.Where(l => l.IdLeveeFonds == id).FirstOrDefault();

            try
            {
                ancienneVersion.montantObtenu = levModif.montantObtenu;
                ancienneVersion.dateDebut = levModif.dateDebut;
                ancienneVersion.dateFin = levModif.dateFin;
                ancienneVersion.message = levModif.message;
                Singleton.Instance.db.SaveChanges();
                return RedirectToAction("MesLevees");
            }
            catch
            {
                return View();
            }
        }


        // GET: LeveeFonds/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["Connected"] == null || (bool)Session["Connected"] == false)
            {
                return RedirectToAction("Identifier", "Membres");
            }

            LeveeFond levee = Singleton.Instance.db.LeveeFonds.Find(id);

            if (levee == null || levee.Projet.idResponsable != (string)Session["Usager"])
            {
                return RedirectToAction("MesLevees");
            }

            return View(levee);
        }


        // GET: LeveeFonds/Effacer/5
        public ActionResult Effacer(int? id)
        {
            if (Session["Connected"] == null || (bool)Session["Connected"] == false)
            {
                return RedirectToAction("Identifier", "Membres");
            }

            LeveeFond lev = Singleton.Instance.db.LeveeFonds.Find(id);

            if (lev.dateDebut < DateTime.Today)
            {
                return RedirectToAction("MesLevees", "LeveeFonds");
            }

            return View(lev);
        }

        // POST: LeveeFonds/Effacer/5
        [HttpPost]
        public ActionResult Effacer(int id)
        {
            try
            {
                LeveeFond lev = Singleton.Instance.db.LeveeFonds.Find(id);
                Singleton.Instance.db.LeveeFonds.Remove(lev);
                Singleton.Instance.db.SaveChanges();
                return RedirectToAction("MesLevees");
            }
            catch
            {
                return View();
            }
        }

        // GET: LeveeFonds/Dons/5
        public ActionResult Dons(int? id)
        {
            if (Session["Connected"] == null || (bool)Session["Connected"] == false)
            {
                return RedirectToAction("Identifier", "Membres");
            }

            LeveeFond lev = Singleton.Instance.db.LeveeFonds.Find(id);

            if (lev == null || lev.Projet.idResponsable != (string)Session["Usager"])
            {
                return RedirectToAction("MesLevees");
            }

            return View(lev);
        }

        // GET: LeveeFonds/Donateur/5
        public ActionResult Donateurs(int? id)
        {
            if (Session["Connected"] == null || (bool)Session["Connected"] == false)
            {
                return RedirectToAction("Identifier", "Membres");
            }

            Donateur donateur = Singleton.Instance.db.Donateurs.Find(id);

            if (donateur == null)
            {
                return RedirectToAction("MesLevees");
            }

            return View(donateur);
        }
    }
}