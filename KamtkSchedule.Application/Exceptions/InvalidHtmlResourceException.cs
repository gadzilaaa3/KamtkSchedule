using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KamtkSchedule.Application.Exceptions
{
    [Serializable]
    public class InvalidHtmlResourceException : ApplicationException
    {
        public InvalidHtmlResourceException() { }
        public InvalidHtmlResourceException(string message) : base(message) { }
        public InvalidHtmlResourceException(string message, Exception inner) : base(message, inner) { }
        protected InvalidHtmlResourceException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
