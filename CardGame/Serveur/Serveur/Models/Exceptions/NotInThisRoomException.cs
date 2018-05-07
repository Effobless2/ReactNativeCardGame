using System;
using System.Runtime.Serialization;

namespace Serveur.Models.Exceptions
{
    /// <summary>
    /// Exception thrown when an ApplicationUser 
    /// must be Removed from a Room where he's not in.
    /// </summary>
    [Serializable]
    internal class NotInThisRoomException : Exception
    {
        public NotInThisRoomException()
        {
        }

        public NotInThisRoomException(string message) : base(message)
        {
        }

        public NotInThisRoomException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotInThisRoomException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}