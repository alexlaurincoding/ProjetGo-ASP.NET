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

                List<Donateur> donateur = new List<Donateur>();

                string nomDonateur = Request.Form["last_name"];
                string prenomDonateur = Request.Form["first_name"];
                string adresseDonateur = Request.Form["address_street"];
                string villeDonateur = Request.Form["address_city"];
                string provinceDonateur = Request.Form["address_state"];
                string codePostal = Request.Form["adress_zip"];
                string payerEmail = Request.Form["payer_email"];

                foreach(Donateur d in Singleton.Instance.db.Donateurs)
                {
                    if (d.emailDonateur == payerEmail)
                    {
                        donateur.Add(d);
                    }
                }

                if (donateur.Count == 0)
                {
                   Donateur donateur1 = new Donateur();
                    donateur1.nomDonateur = nomDonateur;
                    donateur1.prenomDonateur = prenomDonateur;
                    donateur1.adresseDonateur = adresseDonateur;
                    donateur1.villeDonateur = villeDonateur;
                    donateur1.provinceDonateur = provinceDonateur;
                    donateur1.cpDonateur = codePostal;
                    donateur1.emailDonateur = payerEmail;
                    try
                    {

                    }
                    catch
                    {

                    }
                }

                string transactionId = Request.Form["txn_id"];
                string dateDon = Request.Form["payment_date"];
                string amount = Request.Form["mc_gross"];
                //string idDonateur
                //string idLeveeDeFond

                //string paymentStatus = Request.Form["payment_status"];
                //string receiverEmail = Request.Form["receiver_email"];
                
               
                
                // check that Payment_status=Completed
                // check that Txn_id has not been previously processed
                // check that Receiver_email is your Primary PayPal email
                // check that Payment_amount/Payment_currency are correct
                // process payment
               
                
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