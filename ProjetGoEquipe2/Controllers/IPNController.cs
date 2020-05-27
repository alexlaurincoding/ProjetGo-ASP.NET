using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Windows;

namespace ProjetGoEquipe2.Controllers
{
    public class IPNController : Controller
    {
        [HttpPost]
        public HttpStatusCodeResult Receive()
        {
            MessageBox.Show("received");
            //Store the IPN received from PayPal
            LogRequest(Request);

            //Fire and forget verification task
            Task.Run(() => VerifyTask(Request));

            //Reply back a 200 code
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        private void VerifyTask(HttpRequestBase ipnRequest)
        {
            var verificationResponse = string.Empty;
            
            try
            {
                var verificationRequest = (HttpWebRequest)WebRequest.Create("https://www.sandbox.paypal.com/cgi-bin/webscr");

                //Set values for the verification request
                verificationRequest.Method = "POST";
                verificationRequest.ContentType = "application/x-www-form-urlencoded";
                var param = Request.BinaryRead(ipnRequest.ContentLength);
                var strRequest = Encoding.ASCII.GetString(param);

                //Add cmd=_notify-validate to the payload
                strRequest = "cmd=_notify-validate&" + strRequest;
                verificationRequest.ContentLength = strRequest.Length;

                //Attach payload to the verification request
                var streamOut = new StreamWriter(verificationRequest.GetRequestStream(), Encoding.ASCII);
                streamOut.Write(strRequest);
                streamOut.Close();

                //Send the request to PayPal and get the response
                var streamIn = new StreamReader(verificationRequest.GetResponse().GetResponseStream());
                verificationResponse = streamIn.ReadToEnd();
                MessageBox.Show(verificationResponse);
                streamIn.Close();

            }
            catch (Exception exception)
            {
                //Capture exception for manual investigation
            }

            ProcessVerificationResponse(verificationResponse);
        }


        private void LogRequest(HttpRequestBase request)
        {

            // Persist the request values into a database or temporary data store
        }

        private void ProcessVerificationResponse(string verificationResponse)
        {
            if (verificationResponse.Equals("VERIFIED"))
            {
                MessageBox.Show(verificationResponse);

                //Verifier si ce donateur est deja dans notre BD
                bool donateurExistant = false;
                Donateur donateur = new Donateur();
                foreach(Donateur d in Singleton.Instance.db.Donateurs)
                {
                    //Si donateur trouvé, le récupérer
                    if (d.emailDonateur == Request.Form["payer_email"])
                    {
                        donateur = d;
                        donateurExistant = true;
                        break;
                    }
                }
                //Si donateur pas trouvé, l'ajouter à la BD
                if (donateurExistant == false)
                {
                    donateur.nomDonateur = Request.Form["last_name"];
                    donateur.prenomDonateur = Request.Form["first_name"];
                    donateur.adresseDonateur = Request.Form["address_street"];
                    donateur.villeDonateur = Request.Form["address_city"];
                    donateur.provinceDonateur = Request.Form["address_state"];
                    donateur.cpDonateur = Request.Form["address_zip"];
                    donateur.emailDonateur = Request.Form["payer_email"];

                    try
                    {
                        Singleton.Instance.db.Donateurs.Add(donateur);
                        Singleton.Instance.db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                string id = Request.Form["item_name"];
              
                //Ajouter le Don a partir du Donateur.
               
                MessageBox.Show(id);
                int idLevee = Int32.Parse(id);
                string idTransactionId = Request.Form["txn_id"];
                string montant = Request.Form["mc_gross"];
                double montantDouble = Double.Parse(montant);

                Don don = new Don();
                don.dateDon = DateTime.Today;
                don.montantDon = montantDouble;
                don.idDonateur = donateur.idDonateur;
                don.idLeveeFonds = idLevee;
                don.numTransaction = idTransactionId;

                try
                {
                    Singleton.Instance.db.Dons.Add(don);
                    Singleton.Instance.db.SaveChanges();

                    //Updater le montant obtenu dans la Levee de Fonds et le Projet
                    LeveeFond levee = Singleton.Instance.db.LeveeFonds.Find(idLevee);
                    levee.montantObtenu += montantDouble;
                    levee.Projet.totalFondsCollectes += montantDouble;
                    try
                    {
                        Singleton.Instance.db.SaveChanges();
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                
            }
            else if (verificationResponse.Equals("INVALID"))
            {
                //Log for manual investigation
            }
            else
            {
                //Log error
            }
        }
    }
}