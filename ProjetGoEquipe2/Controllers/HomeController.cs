using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace ProjetGoEquipe2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index() {

            MEMBRE bob = Singleton.Instance.db.MEMBRES.FirstOrDefault();
            ViewBag.Nom = bob.nom;
            return View();

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}