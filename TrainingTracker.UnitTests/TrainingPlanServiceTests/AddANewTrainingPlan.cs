using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using TrainingTracker.Common.Exceptions;
using TrainingTracker.DataModels.Models;
using TrainingTracker.Domain.Service;
using TrainingTracker.UnitTests.TestHelpers;

namespace TrainingTracker.UnitTests.TrainingPlanServiceTests
{
    [TestClass]
    public class AddANewTrainingPlan
    {
        private TrainingPlanService _sTrainingPlan;
        private Trainer _trainingData;
        private FakeTrainingTrackerEntities _dbCtx;

        public AddANewTrainingPlan()
        {
        }

        #region Setup & Breakdown

        [TestInitialize]
        public void Initialize()
        {
            // Hook up our fake context.
            this._dbCtx = new FakeTrainingTrackerEntities();

            // Add some fake data needed for these tests.
            this.createFakeData();

            // Create service
            this._sTrainingPlan = new Domain.Service.TrainingPlanService(this._dbCtx);
        }

        private void createFakeData()
        {
            var trainings = new List<Trainer>()
            {
                new Trainer() { Id = 1, Name = "Niels Training" }
            };

            var trainingPlans = new List<TrainingPlan>()
            {
                new TrainingPlan() { Id = 1, Name = "Pull ups", Details = "Do as many pullups as you can", TrainerID = 1 },
                new TrainingPlan() { Id = 2, Name = "10km Jog", Details = "Run as fast as you can for 10kms", TrainerID = 1 },
            };

            this._dbCtx.AddSet<Trainer>(trainings.AsQueryable());
            this._dbCtx.AddSet<TrainingPlan>(trainingPlans.AsQueryable());
        }

        #endregion

        #region Tests

        [TestMethod]
        [TestCategory("TrainingPlanService")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Avoid_Null_TrainingPlan_instance()
        {
            this._sTrainingPlan.AddANewTrainingPlan(null);
        }

        [TestMethod]
        [TestCategory("TrainingPlanService")]
        public void Check_Required_Fields_Set()
        {
            try
            {
                // No Name
                TrainingPlan tp = new TrainingPlan();
                tp.Name = null;
                tp.Details = "Details";
                tp.TrainerID = 1;

                this._sTrainingPlan.AddANewTrainingPlan(tp);
                Assert.Fail();
            }
            catch (ArgumentException)
            {
                // Must come here
            }
            catch (Exception)
            {
                Assert.Fail();
            }

            try
            {
                // No Details
                TrainingPlan tp = new TrainingPlan();
                tp.Name = "Name";
                tp.Details = null;
                tp.TrainerID = 1;

                this._sTrainingPlan.AddANewTrainingPlan(tp);
                Assert.Fail();
            }
            catch (ArgumentException)
            {
                // Must come here
            }
            catch (Exception)
            {
                Assert.Fail();
            }

            try
            {
                // No Training
                TrainingPlan tp = new TrainingPlan();
                tp.Name = "Name";
                tp.Details = "Details";
                tp.Trainer = null;

                this._sTrainingPlan.AddANewTrainingPlan(tp);
                Assert.Fail();
            }
            catch (ArgumentNullException)
            {
                // Must come here
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        [TestCategory("TrainingPlanService")]
        public void Prevent_Duplicates()
        {
            TrainingPlan tp = new TrainingPlan();
            tp.Name = "Pull ups"; // Name already exists
            tp.Details = "Details";
            tp.TrainerID = 1;

            try
            {
                this._sTrainingPlan.AddANewTrainingPlan(tp);
            }
            catch (DuplicateRecordException)
            {
                Assert.AreEqual(false, this._dbCtx.IsSaved);
                Assert.AreEqual(0, this._dbCtx.Added.Count);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        [TestCategory("TrainingPlanService")]
        public void Valid_Add()
        {
            TrainingPlan tp = new TrainingPlan();
            tp.Name = "Valid Training Plan";
            tp.Details = "This is a valid training plan";
            tp.TrainerID = 1;

            this._sTrainingPlan.AddANewTrainingPlan(tp);

            Assert.AreEqual(true, this._dbCtx.IsSaved);
            Assert.AreEqual(1, this._dbCtx.Added.Count);
        }

        #endregion
    }
}
