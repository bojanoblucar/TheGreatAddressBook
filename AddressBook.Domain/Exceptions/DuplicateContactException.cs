using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBook.Domain.Exceptions
{
    public class DuplicateContactException : Exception
    {
        private static string ErrorMessage = "Contact with combination of name and address already exists";

        public DuplicateContactException() : base(ErrorMessage)
        {

        }
    }
}
