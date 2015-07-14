using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingTracker.DataModels
{
    public class DatabaseContextFactory
    {
        public IDatabaseContext Create()
        {
            return new TrainingTrackerDbContext();
        }
    }
}
