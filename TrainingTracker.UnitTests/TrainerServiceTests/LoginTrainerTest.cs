using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingTracker.DataModels.Models;
using TrainingTracker.Domain.Service;
using TrainingTracker.UnitTests.TestHelpers;

namespace TrainingTracker.UnitTests.TrainerServiceTests
{
    [TestClass]
    public class LoginTrainerTest
    {
        private TrainerService _sTraining;
        private FakeTrainingTrackerEntities _dbCtx;

        public LoginTrainerTest()
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
                new Trainer() { Id = 1, EmailAddress = "filterniels@gmail.com", Name = "Niels Training", Password = "xxxxxx" },
                new Trainer() { Id = 2, EmailAddress = "training2@test.com", Name = "Training Two", Password = "yyyyyy" },
                new Trainer() { Id = 3, EmailAddress = "another@training.com", Name = "Another Training", Password = "zzzzzz" }
            };

            this._dbCtx.AddSet<Trainer>(trainingTestData.AsQueryable());
        }

        #endregion

        #region Tests

        [TestMethod]
        [TestCategory("TrainingService")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ValidLogin_CheckSuccess()
        {
            string username = "filterniels@gmail.com";
            string password = "xxxxxx";

            var trainer = this._sTraining.LoginTrainer(username, password);

            Assert.AreNotEqual(null, trainer);
        }

        [TestMethod]
        public void InvalidLogin_NoUsername()
        {

        }

        [TestMethod]
        public void InvalidLogin_NoPassword()
        {

        }

        [TestMethod]
        public void InvalidLogin_InvalidUsername()
        {

        }

        [TestMethod]
        public void InvalidLogin_InvalidPassword()
        {

        }

        #endregion
    }
}
