using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingTracker.DataModels;
using TrainingTracker.DataModels.Models;

namespace TrainingTracker.DataAccess.Repository
{
    public class TrainerRepository : RepositoryBase<Trainer>, ITrainerRepository
    {
        public TrainerRepository(IDatabaseContext ctx)
            : base(ctx)
        {
        }

        public IQueryable<Trainer> GetTrainingList(string searchText)
        {
            return base.DbContext.Query<Trainer>().Where(t => t.Name.Contains(searchText));
        }

        public Trainer GetByEmailAddress(string emailAddress)
        {
            if(string.IsNullOrWhiteSpace(emailAddress))
            {
                return null;
            }

            return base.DbContext.Query<Trainer>().FirstOrDefault(t => t.EmailAddress == emailAddress);
        }
    }
}
