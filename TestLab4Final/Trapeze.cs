using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLab1;
using System.Numerics;

namespace TestLab4Final
{
    class Trapeze : IComputing
    {
        private string _name = "trapeze method";

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
            Fraction fr_2 = new Fraction(2);

            int n = 2;
            Fraction h = (b - a) / (new Fraction(n));
            Fraction sum = new Fraction(0);

            for (int i = 1; i <= n; i++)
            {
                Fraction x_top = a + (new Fraction(i - 1)) * h;
                Fraction x_down = a + (new Fraction(i)) * h;
                sum = sum + (f(x_top) + f(x_down)) / fr_2;
            }

            Fraction answ1 = sum * h;

            n += 100;
            h = (b - a) / (new Fraction(n));
            sum = new Fraction(0);

            for (int i = 1; i <= n; i++)
            {
                Fraction x_top = a + (new Fraction(i - 1)) * h;
                Fraction x_down = a + (new Fraction(i)) * h;
                sum = sum + (f(x_top) + f(x_down)) / fr_2;
            }

            Fraction answ2 = sum * h;

            while (Fraction.Abs(answ1 - answ2) >= eps)
            {
                answ1 = (Fraction)answ2.Clone();
                n += 100;
                h = (b - a) / (new Fraction(n));
                sum = new Fraction(0);

                for (int i = 1; i <= n; i++)
                {
                    Fraction x_top = a + (new Fraction(i - 1)) * h;
                    Fraction x_down = a + (new Fraction(i)) * h;
                    sum = sum + (f(x_top) + f(x_down)) / fr_2;
                }

                answ2 = sum * h;
            }
            return answ2;
        }
    }
}
