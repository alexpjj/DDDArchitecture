using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLearn.Domain.Exceptions
{
    [Serializable]
    public class ELearnEmailValidationException : Exception
    {
        public ELearnEmailValidationException() { }
        public ELearnEmailValidationException(string message) : base(message) { }
        public ELearnEmailValidationException(string message, Exception inner) : base(message, inner) { }
        protected ELearnEmailValidationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
