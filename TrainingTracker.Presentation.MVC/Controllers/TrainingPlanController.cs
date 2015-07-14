using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingTracker.Presentation.MVC.Helpers;

namespace TrainingTracker.Presentation.MVC.Controllers
{
    public class TrainingPlanController : Controller
    {
        //
        // GET: /TrainingPlan/
        [Authorize]
        public ActionResult Index()
        {
            var loggedInTrainer = AuthenticationHelper.GetLoggedInTrainer();
            var sTrainingPlan = ServiceFactory.GetTrainingPlanService();
            var lstTraingPlans = sTrainingPlan.ShowAllMyTrainingPlans(loggedInTrainer.Id);

            return View(lstTraingPlans);
        }

        //
        // GET: /TrainingPlan/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /TrainingPlan/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /TrainingPlan/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /TrainingPlan/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /TrainingPlan/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /TrainingPlan/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /TrainingPlan/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
