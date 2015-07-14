using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingTracker.Common.Exceptions;
using TrainingTracker.Common.Utils;
using TrainingTracker.DataAccess;
using TrainingTracker.DataAccess.Repository;
using TrainingTracker.DataModels;
using TrainingTracker.DataModels.Models;
using TrainingTracker.Common.ExtensionMethods;

namespace TrainingTracker.Domain.Service
{
    public class TrainerService
    {
        private ITrainerRepository _repTraining;

        #region Constants

        private const int MIN_PASSWORD_LENGTH = 6;
        private const int PASSWORD_SALT_LENGTH = 16;

        #endregion

        #region Constructors

        public TrainerService(IDatabaseContext ctx)
        {
            this._repTraining = RepositoryFactory.CreateTrainingRepository(ctx);
        }

        #endregion

        public Trainer RegisterNewTrainer(string emailAddress, string displayName, string password)
        {
            if (string.IsNullOrWhiteSpace(emailAddress))
            {
                throw new ArgumentNullException("Trainer must have a valid email address.");
            }

            if (displayName == null)
            {
                throw new ArgumentNullException("Trainer must have a valid Display Name");
            }

            if (string.IsNullOrWhiteSpace(password) || password.Length < MIN_PASSWORD_LENGTH)
            {
                throw new ArgumentException(string.Format("Password must be at least {0} characters.", MIN_PASSWORD_LENGTH));
            }

            // Check if Training name already exists.
            var trainer = _repTraining.GetByEmailAddress(emailAddress);
            if(trainer != null)
            {
                throw new DuplicateRecordException(String.Format("Cannot register Trainer. The email address '{0}' has already been used.", trainer.Name));
            }

            // Create a new trainer
            trainer = new Trainer();
            trainer.EmailAddress = emailAddress;
            trainer.Name = displayName;

            // Create Salt and hash the password.
            trainer.PasswordSalt = CryptoUtils.CreateSalt(PASSWORD_SALT_LENGTH);
            trainer.Password = CryptoUtils.CreatePasswordHash(password, trainer.PasswordSalt);

            // Add a new Training instance.
            this._repTraining.AddNew(trainer);

            // Persist changes to database.
            this._repTraining.SaveChanges();

            return trainer;
        }

        public Trainer LoginTrainer(string emailAddress, string password)
        {
            if (string.IsNullOrEmpty(emailAddress))
            {
                throw new ArgumentNullException("emailAddress");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("password");
            }

            // Find user
            var trainer = this._repTraining.GetByEmailAddress(emailAddress);
            if (trainer == null || trainer.PasswordSalt == null || trainer.Password == null)
            {
                return null;
            }

            // User exists, get salt and hash their entered password.
            string hashedPwd = CryptoUtils.CreatePasswordHash(password, trainer.PasswordSalt);

            if(hashedPwd == trainer.Password) // Important to test case sensitive with passwords. 
            {
                return trainer;
            }

            return null;
        }

        public void AddANewTrainingSession()
        {

        }

        public void ViewMyTrainingSessions()
        {

        }

        //private ITrainingRepository _trainingRepository;
        //private IContext _context;

        //public TrainingService(ITrainingRepository trainingRepository)
        //{
        //    this._trainingRepository = trainingRepository;
        //}

        //public void AddNewTrainingPlan(Training mTraining, TrainingPlan mTrainingPlan)
        //{
        //    if (mTraining == null)
        //    {
        //        throw new ArgumentNullException("mTraining");
        //    }

        //    if (mTrainingPlan == null)
        //    {
        //        throw new ArgumentNullException("mTrainingPlan");
        //    }

        //    mTraining.TrainingPlans.Add(mTrainingPlan);

        //    _trainingRepository.Commit();

        //    if (this.TrainingPlans.Contains(trainingPlan) || this.TrainingPlans.Select(tp => tp.Name).Contains(trainingPlan.Name))
        //    {
        //        throw new DuplicateRecordException(String.Format("Training plan '{0}' is already part of your training.", trainingPlan.Name));
        //    }

        //    this.TrainingPlans.Add(trainingPlan);
        //}

        //public List<TrainingPlan> FindAllMyTrainingPlans(string searchText)
        //{
        //    this.TrainingPlans.Add(trainingPlan);
        //}
    }
}
