using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingTracker.DataModels.Models;

namespace TrainingTracker.DataAccess.Repository
{
    public interface ITrainerRepository : IRepositoryBase<Trainer>
    {
        Trainer GetByEmailAddress(string emailAddress);

        IQueryable<Trainer> GetTrainingList(string searchText);
    }
}
