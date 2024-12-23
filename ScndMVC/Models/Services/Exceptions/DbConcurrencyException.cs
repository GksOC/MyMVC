using System;

namespace ScndMVC.Models.Services.Exceptions
{
    public class DbConcurrencyException : ApplicationException 
    {
        public DbConcurrencyException(string message) : base(message)
        { 
            
        }
    }
}
