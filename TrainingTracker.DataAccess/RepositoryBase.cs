using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingTracker.DataModels;

namespace TrainingTracker.DataAccess
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class, IModel
    {
        public IDatabaseContext DbContext { get; private set; }
        public RepositoryBase(IDatabaseContext ctx)
        {
            if (ctx == null)
            {
                throw new ArgumentNullException("ctx");
            }
            this.DbContext = ctx;
        }

        public void AddNew(T model)
        {
            this.DbContext.Add<T>(model);
        }

        public T GetById(int id)
        {
            return this.DbContext.Query<T>().FirstOrDefault(t => t.Id == id);
        }

        public void Delete(T model)
        {
            this.DbContext.Remove<T>(model);
        }

        public IQueryable<T> ListAll()
        {
            return this.DbContext.Query<T>();
        }

        public void SaveChanges()
        {
            this.DbContext.Save();
        }
    }
}
