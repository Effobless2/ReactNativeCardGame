using System;
using System.Runtime.Serialization;

namespace Serveur.Models.Exceptions
{
    [Serializable]
    internal class RoomIsUndefinedException : Exception
    {
        public RoomIsUndefinedException()
        {
        }

        public RoomIsUndefinedException(string message) : base(message)
        {
        }

        public RoomIsUndefinedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RoomIsUndefinedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}