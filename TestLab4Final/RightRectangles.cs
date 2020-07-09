using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLab1;
using System.Numerics;

namespace TestLab4Final
{
    class RightRectangles : IComputing
    {
        private string _name = "right rectangle method";

        public string Name
        {
            get => _name;
        }
        public Fraction Compute(Func<Fraction, Fraction> f, Fraction a, Fraction b, Fraction eps)
        {
            if (eps <= (new Fraction(0)))
            {
                throw new ArgumentException("Incorrect argument eps");
            }
            if (b <= a)
            {
                throw new ArgumentException("Incorrect borders");
            }
            int n = 2;
            Fraction h = (b - a) / (new Fraction(n));
            Fraction sum = new Fraction(0);
            for (int i = 1; i <= n; i++)
            {
                Fraction x = a + (new Fraction(i)) * h;
                sum = sum + f(x);
            }
            Fraction answ1 = h * sum;

            n += 100;
            h = (b - a) / (new Fraction(n));
            sum = new Fraction(0);
            for (int i = 1; i <= n; i++)
            {
                Fraction x = a + (new Fraction(i)) * h;
                sum = sum + f(x);
            }
            Fraction answ2 = h * sum;

            while (Fraction.Abs(answ1 - answ2) >= eps)
            {
                answ1 = (Fraction)answ2.Clone();
                n += 100;
                h = (b - a) / (new Fraction(n));
                sum = new Fraction(0);
                for (int i = 1; i <= n; i++)
                {
                    Fraction x = a + (new Fraction(i)) * h;
                    sum = sum + f(x);
                }
                answ2 = h * sum;
            }
            return answ2;
        }
    }
}

