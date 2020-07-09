using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLab5Final
{
    class RuleFalseException : Exception
    {
        public RuleFalseException(string message) : base(message)
        { }
    }
}
