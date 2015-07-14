using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrainingTracker.DataModels;
using TrainingTracker.Domain.Service;

namespace TrainingTracker.Presentation.MVC
{
    public class ServiceFactory
    {
        private static IDatabaseContext getContext()
        {
            return new TrainingTrackerDbContext();
        }

        public static TrainerService GetTrainerService()
        {
            return new TrainerService(getContext());
        }

        public static TrainingPlanService GetTrainingPlanService()
        {
            return new TrainingPlanService(getContext());
        }
    }
}