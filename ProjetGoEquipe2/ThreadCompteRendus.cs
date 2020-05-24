using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Windows;

namespace ProjetGoEquipe2
{
    public class ThreadCompteRendus
    {

        public const int QUOTIDIEN = (1000 * 3600 * 24);

        public static Timer montimer = new Timer(verification, null, 0, QUOTIDIEN);

        private static void verification(Object v)
        {
            Gestion gestionnaire = Singleton.Instance.db.Gestions.Find("Admin");

            if (gestionnaire.VerifierCompteRendusAbsents == false)
            {
                montimer.Change(Timeout.Infinite, Timeout.Infinite);
            }
            else
            {
                Check.verifierDatesCR();
            }
        }


    }
}