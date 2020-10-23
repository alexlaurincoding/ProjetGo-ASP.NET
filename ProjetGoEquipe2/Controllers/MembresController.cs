﻿using Microsoft.Ajax.Utilities;
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
            return RedirectToAction("MesProjets", "Projets");
        }


        // GET: Membre/Inscription
        public ActionResult Inscription()
        {
            return View();
        }

        // POST: Membre/Inscription
        [HttpPost]
        public ActionResult Inscription(Membre membre, string repeter)
        {
            Console.WriteLine("Dans Membre, inscription()");
            if (membre.motPasse.IsNullOrWhiteSpace() || membre.nomUsager.IsNullOrWhiteSpace() || membre.nom.IsNullOrWhiteSpace() || membre.prenom.IsNullOrWhiteSpace() || membre.email.IsNullOrWhiteSpace())
            {
                ViewBag.Erreur = "Oubli";
                ViewBag.Message = "Les champs nom, prenom, nom d'usager, mot de passe et courriel sont obligatoires.";
                return View();

            }

            if (membre.motPasse != repeter)
            {
                ViewBag.Erreur = "Repeter";
                ViewBag.Message = "Le mot de passe n'a pas été répété correctement";
                return View();
            }

            foreach (Membre m in Singleton.Instance.db.Membres)
            {
                if (membre.nomUsager == m.nomUsager)
                {
                    ViewBag.Erreur = "Usager";
                    ViewBag.Message = "Ce nom d'usager existe déjà.";
                    return View();
                }
                if (membre.email == m.email)
                {
                    ViewBag.Erreur = "Courriel";
                    ViewBag.Message = "Ce courriel est déjà utilisé par quelqu'un.";
                    return View();
                }
            }
                
            try { 

                var inscription = Request.Form["inscritMailingList"];
                
                membre.inscritMailingList = inscription == "1" ? true : false;

                Singleton.Instance.db.Membres.Add(membre);
                Singleton.Instance.db.SaveChanges();
                Session["Connected"] = true;
                Session["Usager"] = membre.nomUsager;
                membre.statut = "Attente";
                membre.dateProchaineCotisation = DateTime.Now.AddDays(28);
                membre.dateAdhesion = DateTime.Now;
                return RedirectToAction("SendEmail", "Utils");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ConfirmationCreation()
        {
            if (Session["Connected"] == null || (bool)Session["Connected"] == false)
            {
                return RedirectToAction("Identifier");
            }
            Membre membre = Singleton.Instance.db.Membres.Find(Session["Usager"]);

            //Envoyer email de confirmation
            if (DateTime.Today == membre.dateAdhesion)
            {

            }

            return View();
        }

        // GET: Membre/Cotisation
        public ActionResult Cotisation()
        {
            if (Session["Connected"] == null || (bool)Session["Connected"] == false)
            {
                return RedirectToAction("Identifier");
            }

            return View();
        }

        // GET: Membre/RenouvAbonnementSucces
        public ActionResult RenouvAbonnementSucces()
        {
            if (Session["Connected"] == null || (bool)Session["Connected"] == false)
            {
                return RedirectToAction("Identifier");
            }

            Cotisation cotisation = new Cotisation();
            cotisation.nomUsager = (string)Session["Usager"];
            cotisation.montant = 10;
            cotisation.dateTransaction = DateTime.Now;

            Singleton.Instance.db.Cotisations.Add(cotisation);

            try
            {
                Singleton.Instance.db.SaveChanges();
                Membre membre = Singleton.Instance.db.Membres.Find((string)Session["Usager"]);
                membre.dateProchaineCotisation = DateTime.Now.AddDays(365);
                membre.statut = "Actif";
                try
                {
                    Singleton.Instance.db.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("Erreur sauvegarde membres");
                }
            }
            catch
            {
                MessageBox.Show("Erreur sauvegarde cotisation");
            }
 
            return View();
        }

        public ActionResult RenouvAbonnementCancel()
        {
            if (Session["Connected"] == null || (bool)Session["Connected"] == false)
            {
                return RedirectToAction("Identifier");
            }
            return View();
        }
       
        // GET: Membre/Profil
        public ActionResult Profil()
        {
            if (Session["Connected"] == null || (bool)Session["Connected"] == false)
            {
                return RedirectToAction("Identifier");
            }
            Membre membre = Singleton.Instance.db.Membres.Find((string)Session["Usager"]);
         
            return View(membre);
        }
       
        // GET: Membre/Identifier
        public ActionResult EditMembre()
        {
            if (Session["Connected"] == null || (bool)Session["Connected"] == false)
            {
                return RedirectToAction("Identifier");
            }
            Membre membre = Singleton.Instance.db.Membres.Find((string)Session["Usager"]);
            
            return View(membre);
        }
        [HttpPost]
        public ActionResult EditMembre(Membre membreModifier, string repeter, string inscritMailingList)
        {
            Membre ancienMembre = Singleton.Instance.db.Membres.Find((string)Session["Usager"]);

            if (membreModifier.motPasse.IsNullOrWhiteSpace() || membreModifier.nom.IsNullOrWhiteSpace() || membreModifier.prenom.IsNullOrWhiteSpace() || membreModifier.email.IsNullOrWhiteSpace())
            {
                ViewBag.Erreur = "Oubli";
                ViewBag.Message = "Les champs nom, prenom, nom d'usager, mot de passe et courriel sont obligatoires.";
                return View(ancienMembre);

            }

            if (membreModifier.motPasse != repeter)
            {
                ViewBag.Erreur = "Repeter";
                ViewBag.Message = "Le mot de passe n'a pas été répété correctement";
                return View(ancienMembre);
            }

            if (membreModifier.email != ancienMembre.email)
            {
                foreach (Membre m in Singleton.Instance.db.Membres)
                {
                    if (m.email == membreModifier.email)
                    {
                        ViewBag.Erreur = "Existant";
                        ViewBag.Message = "Ce courriel est déjà assigné à quelqu'un d'autre";
                        return View(ancienMembre);
                    }
                }
            
                
            }

            ancienMembre.inscritMailingList = inscritMailingList == "1" ? true : false;
            ancienMembre.nom = membreModifier.nom;
            ancienMembre.prenom = membreModifier.prenom;
            ancienMembre.email = membreModifier.email;
            ancienMembre.numeroTel = membreModifier.numeroTel;
            ancienMembre.adresse = membreModifier.adresse;
            
            try
            {
                
                Singleton.Instance.db.SaveChanges();
                return RedirectToAction("Profil");
            }
            catch
            {
            }
            return View(membreModifier);
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
            if (membre.motPasse.IsNullOrWhiteSpace() || membre.nomUsager.IsNullOrWhiteSpace())
            {
                ViewBag.Erreur = "Vide";
                ViewBag.Message = "Les champs nom d'usager et mot de passe sont obligatoires.";
                return View();

            }
            foreach (Membre m in Singleton.Instance.db.Membres)
            {
                if (m.nomUsager == membre.nomUsager)
                {
                    if (m.motPasse == membre.motPasse)
                    {
                        Session["Connected"] = true;
                        Session["Usager"] = membre.nomUsager;
                        if (membre.nomUsager == "Admin")
                        {
                            return RedirectToAction("Threads", "Utils");
                        }
                        return RedirectToAction("Index");
                    }
                }
            }
            ViewBag.Erreur = "Invalide";
            ViewBag.Message = "Combinaison entrée invalide.";
            return View();
        }

        public ActionResult Deconnexion()
        {
            if (Session["Connected"] == null || (bool)Session["Connected"] == false)
            {
                return RedirectToAction("Identifier");
            }

            Session["Connected"] = false;
            return RedirectToAction("Index", "Home");
        }

    }
}
