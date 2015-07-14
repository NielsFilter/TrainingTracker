using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingTracker.DataModels;

namespace TrainingTracker.Presentation.MVC.Config
{
    public class DatabaseConfig
    {
        public static void RegisterDatabase()
        {
            // Turn off EF migrations, we'll initialize the database ourselves.
            Database.SetInitializer(new DatabaseInitializer());

            //using(TrainingTrackerDbContext db = new TrainingTrackerDbContext())
            //{
            //    db.Database.Initialize(true);
            //}
        }

        private static void seed(TrainingTrackerDbContext dbContext)
        {
        }
    }
}