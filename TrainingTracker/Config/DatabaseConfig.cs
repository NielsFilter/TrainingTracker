using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMatrix.WebData;

namespace TrainingTracker.DataModels.Config
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

            WebSecurity.InitializeDatabaseConnection("DefaultConnection", "Trainer", "EmailAddress", "Name", true);
        }

        private static void seed(TrainingTrackerDbContext dbContext)
        {
        }
    }
}