using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;

namespace ProjetGoEquipe2.Controllers
{
    public class DesignController : Controller
    {
        // GET: Design
        public PartialViewResult Menus(string Categorie, string Emphase)
        {
            Dictionary<string, string> parametres = new Dictionary<string, string>();
            
            parametres.Add("Categorie", Categorie);
            parametres.Add("Emphase", Emphase);
            return PartialView("~/Views/Shared/Menus.cshtml", parametres);
        }
    }
}