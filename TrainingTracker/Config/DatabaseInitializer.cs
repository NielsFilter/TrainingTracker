using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingTracker.DataModels.Config
{
    public class DatabaseInitializer : IDatabaseInitializer<TrainingTrackerDbContext>
    {
        public void InitializeDatabase(TrainingTrackerDbContext dbContext)
        {
            if (!dbContext.Database.Exists())
            {
                // Create new database
                dbContext.Database.Create();

                // Add initial data
                this.seed(dbContext);

                // Commit changes.
                dbContext.SaveChanges();
            }
        }

        private void seed(TrainingTrackerDbContext dbContext)
        {
        }
    }
}
