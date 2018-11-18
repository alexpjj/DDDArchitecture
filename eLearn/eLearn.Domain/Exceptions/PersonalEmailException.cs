using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLearn.Domain.Exceptions
{

    [Serializable]
    public class PersonalEmailException : Exception
    {
        public PersonalEmailException() { }
        public PersonalEmailException(string message) : base(message) { }
        public PersonalEmailException(string message, Exception inner) : base(message, inner) { }
        protected PersonalEmailException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
