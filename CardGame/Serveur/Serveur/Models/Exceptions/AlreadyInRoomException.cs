using System;
using System.Runtime.Serialization;

namespace Serveur.Models.Exceptions
{
    /// <summary>
    /// Exception thrown when an ApplicationUser wants to join a Room where he's already in.
    /// </summary>
    [Serializable]
    public class AlreadyInRoomException : Exception
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