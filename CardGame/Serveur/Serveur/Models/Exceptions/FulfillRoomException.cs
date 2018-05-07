using System;
using System.Runtime.Serialization;

namespace Serveur.Models.Exceptions
{
    [Serializable]
    internal class FulfillRoomException : Exception
    {
        public FulfillRoomException()
        {
        }

        public FulfillRoomException(string message) : base(message)
        {
        }

        public FulfillRoomException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FulfillRoomException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}