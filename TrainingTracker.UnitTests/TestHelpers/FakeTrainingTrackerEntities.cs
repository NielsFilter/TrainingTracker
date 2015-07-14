using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingTracker.DataModels;

namespace TrainingTracker.UnitTests.TestHelpers
{
    public class FakeTrainingTrackerEntities : IDatabaseContext
    {
        public Dictionary<Type, object> Sets = new Dictionary<Type, object>();
        public List<object> Added = new List<object>();
        public List<object> Updated = new List<object>();
        public List<object> Removed = new List<object>();

        public bool IsSaved { get; set; }

        public void AddSet<T>(IQueryable<T> objects)
        {
            this.Sets.Add(typeof(T), objects);
        }

        #region IDatabaseContext Members

        public IQueryable<T> Query<T>() where T : class
        {
            return Sets[typeof(T)] as IQueryable<T>;
        }

        public void Add<T>(T entity) where T : class
        {
            this.Added.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            this.Updated.Add(entity);
        }

        public void Remove<T>(T entity) where T : class
        {
            this.Removed.Add(entity);
        }

        public void Save()
        {
            this.IsSaved = true;
        }

        #endregion
    }
}
