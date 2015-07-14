using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingTracker.Common.Exceptions;
using TrainingTracker.DataAccess;
using TrainingTracker.DataAccess.Repository;
using TrainingTracker.DataModels;
using TrainingTracker.DataModels.Models;

namespace TrainingTracker.Domain.Service
{
    public class TrainingPlanService
    {
        private ITrainingPlanRepository _repTrainingPlan;

        public TrainingPlanService(IDatabaseContext ctx)
        {
            this._repTrainingPlan = RepositoryFactory.CreateTrainingPlanRepository(ctx);
        }

        public void AddANewTrainingPlan(TrainingPlan plan)
        {
            // Validations
            if (plan == null)
            {
                throw new ArgumentNullException("plan");
            }

            if (string.IsNullOrWhiteSpace(plan.Name))
            {
                throw new ArgumentException("Training plan must have a name.");
            }

            if (string.IsNullOrWhiteSpace(plan.Details))
            {
                throw new ArgumentException("Training plan must have details.");
            }

            if (plan.TrainerID <= 0)
            {
                throw new ArgumentNullException("Training plan does not have TrainingID");
            }

            // Check if Training name already exists.
            if (this._repTrainingPlan.GetByName(plan.TrainerID, plan.Name) != null)
            {
                throw new DuplicateRecordException(String.Format("Training Plan '{0}' already exists for current training.", plan.Name));
            }

            // Add a new Training instance.
            this._repTrainingPlan.AddNew(plan);

            // Persist changes to database.
            this._repTrainingPlan.SaveChanges();
        }

        public List<TrainingPlan> ShowAllMyTrainingPlans(int trainingID)
        {
            return this._repTrainingPlan.ListAll()
                .Where(tp => tp.TrainerID == trainingID)
                .ToList();
        }
    }
}
