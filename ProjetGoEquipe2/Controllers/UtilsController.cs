using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace ProjetGoEquipe2.Controllers
{
    public class UtilsController : Controller
    {
        // GET: Utils
        public ActionResult SendEmail()
        {
            Console.WriteLine("Dans sendEmail()");
            if (Session["Connected"] == null || (bool)Session["Connected"] == false)
            {
                return RedirectToAction("Identifier");
            }
            Membre membre = Singleton.Instance.db.Membres.Find((string)Session["Usager"]);
            string message = "Merci de vous être joint à ProjetGo, " + membre.prenom + ". Vous avez maintenant accès aux différentes fontionnalités de membres. Assurez-vous de payer votre cotisatio annuelle d'ici 28 jours.\n\nL'Équipe de ProjetGo";
            string sujet = "Inscription à ProjetGo complétée avec succès!";
            
            return SendEmail(membre.email, sujet, message);        
        }

        [HttpPost]
        public ActionResult SendEmail(string receiver, string subject, string message)
        {
            Console.WriteLine("Dans sendEmail " + receiver +  subject +  message + ")");
            try
            {
                if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress("projetgo2020@gmail.com", "ProjetGo");
                    var receiverEmail = new MailAddress(receiver, "Receiver");
                    var password = "laMeilleure2!";
                    var sub = subject;
                    var body = message;

                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }
                    return RedirectToAction("ConfirmationCreation", "Membres");
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }
            return View();
        }

        // GET: LeveeFonds/MesLevees
        public ActionResult ToggleCompletes()
        {
            if (Session["Connected"] == null || (bool)Session["Connected"] == false)
            {
                return RedirectToAction("Identifier", "Membres");
            }

            if (Session["Completes"] == null || (bool)Session["Completes"] == false)
                Session["Completes"] = true;
            else
                Session["Completes"] = false;

            string action = (string)TempData["Action"];
            string controller = (string)TempData["Controller"];

            return RedirectToAction(action, controller);
        }

        public ActionResult modifierThreadCompteRendus(int id)
        {
            if (Session["Connected"] == null || (bool)Session["Connected"] == false || (string)Session["Usager"] != "Admin" )
            {
                return RedirectToAction("Identifier", "Membres");
            }

            Gestion gestionnaire = Singleton.Instance.db.Gestions.Find("Admin");

            if (id == 1)
            {   // Lancer le thread
                gestionnaire.VerifierCompteRendusAbsents = true;
                try
                {
                    Singleton.Instance.db.SaveChanges();
                    ThreadCompteRendus thr = new ThreadCompteRendus();
                    ViewBag.Actif = true;
                }
                catch (Exception e) { }
            }

            if (id == 2)
            {   // terminer le thread
                gestionnaire.VerifierCompteRendusAbsents = false;
                try
                {
                    Singleton.Instance.db.SaveChanges();
                    ViewBag.Actif = false;

                }
                catch (Exception e) { }
            }
            return View("Threads");
        }

        public ActionResult Threads()
        {
            if (Session["Connected"] == null || (bool)Session["Connected"] == false || (string)Session["Usager"] != "Admin")
            {
                return RedirectToAction("Identifier", "Membres");
            }
            Gestion gestion = Singleton.Instance.db.Gestions.Find("Admin");
            
            if (gestion.VerifierCompteRendusAbsents == true)
            {
                ViewBag.Actif = true;
            }
            else
            {
                ViewBag.Actif = false;
            }

            return View();
        }


    }
}