using System;

namespace KamtkSchedule.Application.Exceptions.Api
{
    [Serializable]
    public class IncorrectlyProvidedDataException : ApplicationException
    {
        public IncorrectlyProvidedDataException() { }
        public IncorrectlyProvidedDataException(string message) : base(message) { }
        public IncorrectlyProvidedDataException(string message, Exception inner) : base(message, inner) { }
        protected IncorrectlyProvidedDataException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
