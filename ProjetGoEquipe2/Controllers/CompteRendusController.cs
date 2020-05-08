using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetGoEquipe2.Controllers
{
    public class CompteRendusController : Controller
    {

        // GET: CompteRendus
        public ActionResult Index()
        {
            if (Session["Connected"] == null || (bool)Session["Connected"] == false)
            {
                return RedirectToAction("Identifier", "Membres");
            }
            return View();
        }

        // GET: CompteRendus/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["Connected"] == null || (bool)Session["Connected"] == false)
            {
                return RedirectToAction("Identifier", "Membres");
            }

            CompteRendu cr = Singleton.Instance.db.CompteRendus.Where(c => c.idCompteRendu == id).FirstOrDefault();

            return View(cr);
        }

        // GET: CompteRendus/Ajouter
        public ActionResult Ajouter()
        {
            if (Session["Connected"] == null || (bool)Session["Connected"] == false)
            {
                return RedirectToAction("Identifier", "Membres");
            }
            return View();
        }

        // POST: CompteRendus/Ajouter
        [HttpPost]
        public ActionResult Ajouter(CompteRendu cr)
        {

            try
            {
                cr.dateCompteRendu = DateTime.Now;

                Singleton.Instance.db.CompteRendus.Add(cr);
                Singleton.Instance.db.SaveChanges();
                int joursAvantProchain = (int)cr.Projet.frequenceComptesRendus * 7;
                cr.Projet.dateProchainCompteRendu = DateTime.Now.AddDays(joursAvantProchain);
                try
                {
                    Singleton.Instance.db.SaveChanges();
                    return RedirectToAction("Index", "CompteRendus");
                }
                catch
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: CompteRendus/Modifier/5
        public ActionResult Modifier(int id)
        {
            if (Session["Connected"] == null || (bool)Session["Connected"] == false)
            {
                return RedirectToAction("Identifier", "Membres");
            }

            CompteRendu cr = Singleton.Instance.db.CompteRendus.Where(c => c.idCompteRendu == id).FirstOrDefault();

            return View(cr);
        }

        // POST: CompteRendus/Modifier/5
        [HttpPost]
        public ActionResult Modifier(int id, CompteRendu crModifie)
        {
            try
            {
                CompteRendu ancienneVersion = Singleton.Instance.db.CompteRendus.Where(c => c.idCompteRendu == id).FirstOrDefault();
                ancienneVersion.etatRisques = crModifie.etatRisques;
                ancienneVersion.informationsAJour = crModifie.informationsAJour;
                ancienneVersion.realisationsReportees = crModifie.realisationsReportees;
                ancienneVersion.sommaireRealisationsCompletees = crModifie.sommaireRealisationsCompletees;
                Singleton.Instance.db.SaveChanges();
                return RedirectToAction("Index", "CompteRendus");
            }
            catch
            {
                return View();
            }
        }

        // GET: CompteRendus/Effacer/5
        public ActionResult Effacer(int? id)
        {
            if (Session["Connected"] == null || (bool)Session["Connected"] == false)
            {
                return RedirectToAction("Identifier", "Membres");
            }

            CompteRendu cr = Singleton.Instance.db.CompteRendus.Find(id);
            return View(cr);
        }

        // POST: CompteRendus/Effacer/5
        [HttpPost]
        public ActionResult Effacer(int id)
        {
            try
            {
                CompteRendu cr = Singleton.Instance.db.CompteRendus.Find(id);
                Singleton.Instance.db.CompteRendus.Remove(cr);
                Singleton.Instance.db.SaveChanges();
                return RedirectToAction("Index", "CompteRendus");
            }
            catch
            {
                return View();
            }
        }
    }
}
