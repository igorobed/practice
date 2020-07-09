using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLab1;
using System.Numerics;

namespace TestLab4Final
{
    class Simpson : IComputing
    {
        private string _name = "simpson method";

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
            Fraction fr_1 = new Fraction(1);
            Fraction fr_2 = new Fraction(2);
            Fraction fr_3 = new Fraction(3);
            int n = 2;
            Fraction h = (b - a) / (new Fraction(n));
            Fraction sum1 = new Fraction(0);
            Fraction sum2 = new Fraction(0);
            for (int i = 1; i <= n; i++)
            {
                Fraction xi = a + (new Fraction(i)) * h;
                if (i <= n - 1)
                {
                    sum1 = sum1 + f(xi);
                }
                Fraction xi_1 = a + (new Fraction(i - 1)) * h;
                sum2 = sum2 + f((xi + xi_1) / fr_2);
            }
            Fraction answ1 = (h / fr_3) * ((fr_1 / fr_2) * f(a) + sum1 + fr_2 * sum2 + (fr_1 / fr_2) * f(b));

            n += 100;
            h = (b - a) / (new Fraction(n));
            sum1 = new Fraction(0);
            sum2 = new Fraction(0);
            for (int i = 1; i <= n; i++)
            {
                Fraction xi = a + (new Fraction(i)) * h;
                if (i <= n - 1)
                {
                    sum1 = sum1 + f(xi);
                }
                Fraction xi_1 = a + (new Fraction(i - 1)) * h;
                sum2 = sum2 + f((xi + xi_1) / fr_2);
            }
            Fraction answ2 = (h / fr_3) * ((fr_1 / fr_2) * f(a) + sum1 + fr_2 * sum2 + (fr_1 / fr_2) * f(b));

            while (Fraction.Abs(answ1 - answ2) >= eps)
            {
                answ1 = (Fraction)answ2.Clone();
                n += 100;
                h = (b - a) / (new Fraction(n));
                sum1 = new Fraction(0);
                sum2 = new Fraction(0);
                for (int i = 1; i <= n; i++)
                {
                    Fraction xi = a + (new Fraction(i)) * h;
                    if (i <= n - 1)
                    {
                        sum1 = sum1 + f(xi);
                    }
                    Fraction xi_1 = a + (new Fraction(i - 1)) * h;
                    sum2 = sum2 + f((xi + xi_1) / fr_2);
                }
                answ2 = (h / fr_3) * ((fr_1 / fr_2) * f(a) + sum1 + fr_2 * sum2 + (fr_1 / fr_2) * f(b));
            }
            return answ2;
        }
    }
}
