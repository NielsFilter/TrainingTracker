using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingTracker.Common.Exceptions
{
    [Serializable]
    public class DuplicateRecordException : Exception
    {
        #region Constructors

        public DuplicateRecordException() { }
        public DuplicateRecordException(string message) : base(message) { }
        public DuplicateRecordException(string message, Exception inner) : base(message, inner) { }
        protected DuplicateRecordException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }

        #endregion
    }
}
