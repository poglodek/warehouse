using System;

namespace warehouse.Exceptions.Exceptions
{
    public class CannotDelete : Exception
    {
        public CannotDelete(string message) : base(message)
        {
        }
    }
}