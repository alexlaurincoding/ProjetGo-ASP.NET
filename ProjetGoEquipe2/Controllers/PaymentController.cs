using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetGoEquipe2.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        public ActionResult PaymentWithPaypal()
        {
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            try
            {

                string PayerID = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(PayerID))
                {
                    string baseURL = Request.Url.Scheme + "://" + Request.Url.Authority + "PaymentWithPaypal/PaymentWithPaypal?";
                    var Guid = Convert.ToString((new Random()).Next(100000000));
                    var createPayment = this.CreatePayment(apiContext, baseURL + "guid=" + Guid);
                }
            }
            catch (Exception)
            {

               // throw;
            }
            //return View();
        }

        private Cotisation CreatePayment(APIContext apiContext, string redirectURL)
        {
            
        }
    }
}