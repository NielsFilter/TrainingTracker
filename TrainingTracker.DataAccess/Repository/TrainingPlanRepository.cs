using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingTracker.DataModels;
using TrainingTracker.DataModels.Models;

namespace TrainingTracker.DataAccess.Repository
{
    public class TrainingPlanRepository : RepositoryBase<TrainingPlan>, ITrainingPlanRepository
    {
        public TrainingPlanRepository(IDatabaseContext ctx)
            : base(ctx)
        {
        }

        public TrainingPlan GetByName(int trainingID, string name)
        {
            return base.DbContext.Query<TrainingPlan>()
                .FirstOrDefault(tp => tp.TrainerID == trainingID && tp.Name == name);
        }

        public IQueryable<TrainingPlan> GetTrainingPlanList(int trainingID, string searchText)
        {
            return base.DbContext.Query<TrainingPlan>()
                .Where(tp => tp.TrainerID == trainingID && tp.Name.Contains(searchText));
        }
    }
}
