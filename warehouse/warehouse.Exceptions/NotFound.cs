﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warehouse.Exceptions
{
    public class NotFound : Exception
    {
        public NotFound(string message) : base(message)
        {
            
        }
    }
}
