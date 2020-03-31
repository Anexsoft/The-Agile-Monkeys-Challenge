using System;

namespace CRM.Service.EventHandler.Identity.Exceptions
{
    public class UserCreationException : Exception
    {
        public UserCreationException(string error)
            : base(error)
        {

        }
    }
}
