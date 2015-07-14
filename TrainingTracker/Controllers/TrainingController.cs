using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingTracker.DataModels;
using TrainingTracker.DataModels.Models;
using TrainingTracker.Domain.Service;

namespace TrainingTracker.Controllers
{
    public class TrainingController : Controller
    {
        private TrainerService _sTraining;
        public TrainingController()
        {
            var dbCtx = new DatabaseContextFactory().Create();
            _sTraining = new TrainerService(dbCtx);
        }

        //
        // GET: /Training/
        public ActionResult Index()
        {
            var trainings = _sTraining.ShowAllMyTrainings();
            return View(trainings);
        }

        //
        // GET: /Training/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Training/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Training/Create
        [HttpPost]
        public ActionResult Create(Trainer training)
        {
            if (ModelState.IsValid)
            {
                this._sTraining.RegisterNewTrainer(training);
                return RedirectToAction("Index");
            }

            return View(training);
        }

        //
        // GET: /Training/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Training/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Trainer training)
        {
            if (ModelState.IsValid)
            {
                //this._sTraining.AddMeAsANewTrainer(training);
                return RedirectToAction("Index");
            }

            return View();
        }

        //
        // GET: /Training/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Training/Delete/5
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
