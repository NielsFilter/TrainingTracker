using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrainingTracker.Domain.Service;
using TrainingTracker.UnitTests.TestHelpers;
using System.Collections.Generic;
using TrainingTracker.DataModels.Models;

namespace TrainingTracker.UnitTests.TrainingServiceTests
{
    [TestClass]
    public class ShowAllMyTrainingsTest
    {
        private TrainerService _sTraining;
        private FakeTrainingTrackerEntities _dbCtx;

        public ShowAllMyTrainingsTest()
        {
        }

        #region Setup & Breakdown

        [TestInitialize]
        public void Initialize()
        {
            // Hook up fake data to context.
            this._dbCtx = new FakeTrainingTrackerEntities();

            // Add some fake data needed for these tests.
            this.createFakeData();

            // Create service
            this._sTraining = new Domain.Service.TrainerService(_dbCtx);
        }

        private void createFakeData()
        {
            var trainingTestData = new List<Trainer>()
            { 
                new Trainer() { Id = 1, Name = "Niels Training" },
                new Trainer() { Id = 2, Name = "Training Two" },
                new Trainer() { Id = 3, Name = "Another Training" }
            };

            this._dbCtx.AddSet<Trainer>(trainingTestData.AsQueryable());
        }

        #endregion

        #region Tests

        [TestMethod]
        [TestCategory("TrainingService")]
        public void Valid_List()
        {
            //var results = this._sTraining.ShowAllMyTrainings();

            //Assert.AreEqual(3, results.Count);
        }

        #endregion
    }
}
