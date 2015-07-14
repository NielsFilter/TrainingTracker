using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using TrainingTracker.DataModels.Models;
using TrainingTracker.Domain.Service;
using System.Web.Security;
using TrainingTracker.Presentation.MVC.Models;
using System.Runtime.Serialization.Json;
using TrainingTracker.Common.ExtensionMethods;

namespace TrainingTracker.Presentation.MVC.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        public AccountController()
        {

        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginTrainerViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var sTrainer = ServiceFactory.GetTrainerService();

                var newTrainer = new Trainer()
                {
                    EmailAddress = model.EmailAddress,
                    Name = model.EmailAddress,
                    Password = model.Password
                };

                var trainer = sTrainer.LoginTrainer(model.EmailAddress, model.Password);
                if (trainer != null)
                {
                    this.signInTrainer(trainer, model.RememberMe);
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterTrainerViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var sTrainer = ServiceFactory.GetTrainerService();
                    var trainer = sTrainer.RegisterNewTrainer(model.EmailAddress, model.Name, model.Password);

                    this.signInTrainer(trainer, model.RememberMe);
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }

                // TODO: Try catch? - AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            this.signOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Manage()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(ManageUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                // TODO:
            }

            return View(model);
        }

        #region Helpers

        private void signInTrainer(Trainer trainer, bool rememberMe)
        {
            var cookie = FormsAuthentication.GetAuthCookie(trainer.EmailAddress, rememberMe);
            var ticket = FormsAuthentication.Decrypt(cookie.Value);

            var newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate, ticket.Expiration,
            ticket.IsPersistent, trainer.ToJson(1), ticket.CookiePath);

            var encTicket = FormsAuthentication.Encrypt(newTicket);

            // Use existing cookie. Could create new one but would have to copy settings over...
            cookie.Value = encTicket;

            Response.Cookies.Add(cookie);
        }

        private void signOut()
        {
            FormsAuthentication.SignOut();
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        #endregion
    }
}