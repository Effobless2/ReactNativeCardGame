using System;
using System.Runtime.Serialization;

namespace Serveur.Models.Exceptions
{
    [Serializable]
    internal class UserIsUndefinedException : Exception
    {
        public UserIsUndefinedException()
        {
        }

        public UserIsUndefinedException(string message) : base(message)
        {
        }

        public UserIsUndefinedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserIsUndefinedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}