using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingTracker.DataModels
{
    public interface IContext
    {
        //void RegisterAdded(IEntity entity, IRepository repository);
        //void RegisterChanged(IEntity entity, IRepository repository);
        //void RegisterRemoved(IEntity entity, IRepository repository);

        void Commit();
    }
}

