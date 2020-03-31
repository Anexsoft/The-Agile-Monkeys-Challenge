using System;

namespace CRM.Service.EventHandler.Identity.Exceptions
{
    public class UserSignInException : Exception
    {
        public UserSignInException(string username)
            : base($"Access denied by user: {username}")
        {

        }
    }
}
