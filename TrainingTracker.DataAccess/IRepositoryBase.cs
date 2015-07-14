using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrainingTracker.DataModels;

namespace TrainingTracker.DataAccess
{
    public interface IRepositoryBase<T> where T : class, IModel
    {
        void AddNew(T model);
        T GetById(int id);
        void Delete(T model);
        IQueryable<T> ListAll();

        void SaveChanges();
    }
}
