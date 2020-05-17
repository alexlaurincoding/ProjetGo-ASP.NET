using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
           
            ViewBag.Affiche = Session["Completees"] != null && (bool)Session["Completees"] == true ? true : false;
            if (Session["Connected"] == null || (bool)Session["Connected"] == false)
            {
                return RedirectToAction("Identifier", "Membres");
            }
            return View();
        }

        // GET: LeveeFonds/MesLevees
        public ActionResult MesLeveesCompletees()
        {
            if (Session["Connected"] == null || (bool)Session["Connected"] == false)
            {
                return RedirectToAction("Identifier", "Membres");
            }
            if (Session["Completees"] == null || (bool)Session["Completees"] == false)
                Session["Completees"] = true;
            else
                Session["Completees"] = false;

            return RedirectToAction("MesLevees");
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
                if (levee.dateDebut == null || levee.dateFin == null)
                {
                    ViewBag.Erreur = "Oubli";
                    ViewBag.Message = "Veuillez entrer des dates de début et de fin.";
                    return View(levee);
                }

                levee.montantObtenu = 0;
                Singleton.Instance.db.LeveeFonds.Add(levee);

                Singleton.Instance.db.SaveChanges();
                return RedirectToAction("MesProjets", "Projets");

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
                Singleton.Instance.db.SaveChanges();
                return RedirectToAction("MesLevees");
            }
            catch
            {
                return View();
            }
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
    }
}