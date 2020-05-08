using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetGoEquipe2
{
    public class PaypalConfiguration
    {
        public readonly static string clientId;
        public readonly static string clientSecret;
        static PaypalConfiguration()
        {
            var config = getConfig();
            clientId = "Af-MNhlnByUOaXVzbpr5YwsKErgcA82FCgOIRqWBQLH_0KfpIbTIFurOIqlwrSgXJDqsTeoaYx86oL-I";
            clientSecret = "EC8tPw50eQyasUO-6xm2mmNqibYGG1RgEqHlXRdomLKm2wh7X4cXMgqgj08bkQyUGDecqu0exxS_2Nwp";
        }

        private static Dictionary<string, string> getConfig()
        {
            return PayPal.Api.ConfigManager.Instance.GetProperties();
        }

        private static string GetAccessToken()
        {
            string accessToken = new OAuthTokenCredential (clientId, clientSecret, getConfig()).GetAccessToken();
            return accessToken;
        }

        public static APIContext GetAPIContext()
        {
            APIContext apiContext = new APIContext(GetAccessToken());
            apiContext.Config = getConfig();
            return apiContext;
        }
    }
}