using System;

namespace warehouse.Exceptions.Exceptions
{
    public class CannotParse : Exception
    {
        public CannotParse(string message) : base(message)
        {
        }
    }
}