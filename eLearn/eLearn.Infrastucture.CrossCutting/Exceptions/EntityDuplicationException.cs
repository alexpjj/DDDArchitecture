using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLearn.Infrastucture.CrossCutting.Exceptions
{
    public class EntityDuplicationException : Exception
    {
        public EntityDuplicationException(string message) : base(message)
        {
        }
    }
}
