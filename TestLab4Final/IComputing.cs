using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLab1;
using System.Numerics;

namespace TestLab4Final
{
    interface IComputing
    {
        string Name
        {
            get;
        }
        Fraction Compute(Func<Fraction, Fraction> f, Fraction border_1, Fraction border_2, Fraction eps);
    }
}
