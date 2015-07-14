using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingTracker.DataAccess.Repository;
using TrainingTracker.DataModels;

namespace TrainingTracker.DataAccess
{
    public class RepositoryFactory
    {
        public static ITrainerRepository CreateTrainingRepository(IDatabaseContext ctx)
        {
            return new TrainerRepository(ctx);
        }

        public static ITrainingPlanRepository CreateTrainingPlanRepository(IDatabaseContext ctx)
        {
            return new TrainingPlanRepository(ctx);
        }
    }
}
