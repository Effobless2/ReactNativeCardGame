using System;
using System.Runtime.Serialization;

namespace Serveur.Models.Exceptions
{
    /// <summary>
    /// Exception trown when an ApplicationUser wants 
    /// to play in a Room where the maximum number of 
    /// players are attempt.
    /// </summary>
    [Serializable]
    public class FulfillRoomException : Exception
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