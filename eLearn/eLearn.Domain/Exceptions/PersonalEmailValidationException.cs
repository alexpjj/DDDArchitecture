using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLearn.Domain.Exceptions
{

    [Serializable]
    public class PersonalEmailValidationException : Exception
    {
        public PersonalEmailValidationException() { }
        public PersonalEmailValidationException(string message) : base(message) { }
        public PersonalEmailValidationException(string message, Exception inner) : base(message, inner) { }
        protected PersonalEmailValidationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
