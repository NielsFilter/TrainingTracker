using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingTracker.DataModels.Models;

namespace TrainingTracker.DataAccess.Repository
{
    public interface ITrainingPlanRepository : IRepositoryBase<TrainingPlan>
    {
        TrainingPlan GetByName(int trainingID, string name);
        IQueryable<TrainingPlan> GetTrainingPlanList(int trainingID, string searchText);
    }
}
