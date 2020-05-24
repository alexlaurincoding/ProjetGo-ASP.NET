using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetGoEquipe2
{
    public class Singleton
    {

        private static Singleton instance = null;

        public ProjetGoDataEntities db = new ProjetGoDataEntities();

        private Singleton()
        {
        }

        public static Singleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Singleton();
                }
                return instance;
            }
        }

    }
}