using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using TrainingTracker.Common.ExtensionMethods;
using TrainingTracker.DataModels.Models;

namespace TrainingTracker.Presentation.MVC.Helpers
{
    public class AuthenticationHelper
    {
        public static Trainer GetLoggedInTrainer()
        {
            var cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie == null)
            {
                return null;
            }
            var decrypted = FormsAuthentication.Decrypt(cookie.Value);

            if (!string.IsNullOrEmpty(decrypted.UserData))
            {
                return decrypted.UserData.FromJson<Trainer>();
            }
            return null;
        }
    }
}
