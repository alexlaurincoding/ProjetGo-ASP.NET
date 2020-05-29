using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security;
using System.Web;
using System.Windows;

namespace ProjetGoEquipe2
{
    public class Check
    {
        public static void verifierDatesCR()
        {
            foreach (Projet projet in Singleton.Instance.db.Projets)
            {
                if (projet.dateProchainCompteRendu != null && projet.dateProchainCompteRendu < DateTime.Today)
                {
                    DateTime? dateprochain = projet.dateProchainCompteRendu;
                    string email = "girouxcoding@gmail.com";
                    string prenom = projet.Membre.prenom;
                    string titre = projet.titre;
                    envoyerEmailCRManquant(email, prenom, titre, dateprochain);
                }
            }
        }

        public static void envoyerEmailCRManquant(string email, string prenom, string projet, DateTime? dateLimite)
        {
            string message = "Bonjour " + prenom + ",\n\nNous souhaitons simplement vous informer qu'un compte-rendu était prévu pour votre projet '" + projet + "' en date du " + @Convert.ToString(string.Format("{0:dd/MM/yyyy}", dateLimite)) + ". Merci de soumettre votre compte-rendu promptement, ou d'aviser la direction de tout imprévu important.\n\nL'Équipe de ProjetGo";
            string sujet = "Retard pour la soumission d'un compte-rendu";

            var senderEmail = new MailAddress("projetgo2020@gmail.com", "ProjetGo");
            var receiverEmail = new MailAddress(email, "Receiver");
            var password = "laMeilleure2!";
            var sub = sujet;
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
                Subject = sujet,
                Body = body
            })
            {
                smtp.Send(mess);
            }

        }
    }
}