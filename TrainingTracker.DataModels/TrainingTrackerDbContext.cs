using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingTracker.DataModels.Models;

namespace TrainingTracker.DataModels
{
    public class TrainingTrackerDbContext : DbContext, IDatabaseContext
    {
        public TrainingTrackerDbContext()
            : base("name=DefaultConnection")
        {
        }

        public TrainingTrackerDbContext(string nameOfConnString)
            : base("name=" + nameOfConnString)
        {
        }

        public DbSet<Trainer> Trainings { get; set; }
        public DbSet<TrainingPlan> TrainingPlans { get; set; }
        public DbSet<TrainingSession> TrainingSessions { get; set; }

        public IQueryable<T> Query<T>() where T : class
        {
            return this.Set<T>();
        }

        public void Add<T>(T entity) where T : class
        {
            this.Set<T>().Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            this.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public void Remove<T>(T entity) where T : class
        {
            this.Set<T>().Remove(entity);
        }

        public void Save()
        {
            this.SaveChanges();
        }
    }
}
