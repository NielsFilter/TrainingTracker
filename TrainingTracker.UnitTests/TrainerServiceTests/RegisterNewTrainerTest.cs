using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrainingTracker.DataAccess.Repository;
using TrainingTracker.UnitTests.TestHelpers;
using TrainingTracker.Common.Exceptions;
using TrainingTracker.DataAccess;
using System.Data.Entity;
using TrainingTracker.Domain.Service;
using TrainingTracker.DataModels.Models;

namespace TrainingTracker.UnitTests.TrainingServiceTestsTests
{
    [TestClass]
    public class RegisterNewTrainer
    {
        private TrainerService _sTraining;
        private FakeTrainingTrackerEntities _dbCtx;

        public RegisterNewTrainer()
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
                new Trainer() { Id = 1, EmailAddress = "filterniels@gmail.com", Name = "Niels Training", Password="password", PasswordSalt ="abcdefghijkl" },
                new Trainer() { Id = 2, Name = "Training Two" },
                new Trainer() { Id = 3, Name = "Another Training" }
            };

            this._dbCtx.AddSet<Trainer>(trainingTestData.AsQueryable());
        }

        #endregion

        #region Tests

        [TestMethod]
        [TestCategory("TrainingService")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NewTraining_Must_Have_an_EmailAddress()
        {
            string emailAddress = " ";
            string displayName = "Niels";
            string password = "password007";

            this._sTraining.RegisterNewTrainer(emailAddress, displayName, password);
        }

        [TestMethod]
        [TestCategory("TrainingService")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NewTraining_Must_Have_a_DisplayName()
        {
            string emailAddress = "filterniels@gmail.com";
            string displayName = " ";
            string password = "password007";

            this._sTraining.RegisterNewTrainer(emailAddress, displayName, password);
        }

        [TestMethod]
        [TestCategory("TrainingService")]
        [ExpectedException(typeof(ArgumentException))]
        public void NewTraining_Must_Have_a_Password()
        {
            string emailAddress = "filterniels@gmail.com";
            string displayName = "Niels";
            string password = " ";

            this._sTraining.RegisterNewTrainer(emailAddress, displayName, password);
        }

        [TestMethod]
        [TestCategory("TrainingService")]
        public void Prevent_Duplicate_Names_Check()
        {
            string emailAddress = "filterniels@gmail.com";
            string displayName = "Niels";
            string password = "password007";
            try
            {
                // This training name already exists. We expect a DuplicateRecordException
                this._sTraining.RegisterNewTrainer(emailAddress, displayName, password);
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
        [TestCategory("TrainingService")]
        public void Valid_Add()
        {
            string emailAddress = "myvalid@emailaddress.com";
            string displayName = "A Valid Training Name";
            string password = "password007";

            this._sTraining.RegisterNewTrainer(emailAddress, displayName, password);

            // Check if save was successful.
            Assert.AreEqual(true, this._dbCtx.IsSaved);
            Assert.AreEqual(1, this._dbCtx.Added.Count);
        }

        #endregion
    }
}
