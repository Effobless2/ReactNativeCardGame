using System;
using System.Runtime.Serialization;

namespace Serveur.Models.Exceptions
{
    [Serializable]
    internal class AlreadyInRoomException : Exception
    {
        public AlreadyInRoomException()
        {
        }

        public AlreadyInRoomException(string message) : base(message)
        {
        }

        public AlreadyInRoomException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AlreadyInRoomException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}