using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using TestLab1;

namespace TestLab4Final
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Fraction EPS = new Fraction(1,10000);

                Fraction a = new Fraction(2);
                Fraction b = new Fraction(4);

                CentralRectangles CR = new CentralRectangles();
                LeftRectangles LR = new LeftRectangles();
                RightRectangles RR = new RightRectangles();
                Simpson S = new Simpson();
                Trapeze T = new Trapeze();


                Console.WriteLine(CR.Name);
                Console.WriteLine((CR.Compute(f3, a, b, EPS)).ToString(3));

                Console.WriteLine(LR.Name);
                Console.WriteLine((LR.Compute(f3, a, b, EPS)).ToString(3));

                Console.WriteLine(RR.Name);
                Console.WriteLine((RR.Compute(f3, a, b, EPS)).ToString(3));

                Console.WriteLine(S.Name);
                Console.WriteLine((S.Compute(f3, a, b, EPS)).ToString(3));

                Console.WriteLine(T.Name);
                Console.WriteLine((T.Compute(f3, a, b, EPS)).ToString(3));
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);

            }
            finally
            {
                Console.ReadKey();
            }
        }

        static Fraction f1(Fraction x)
        {
            return (new Fraction()) / (x - (new Fraction()));
        }

        static Fraction f2(Fraction x)
        {
            return x.Pow(3);
        }

        static Fraction f3(Fraction x)
        {
            return x.Pow(2) - x + (new Fraction());
        }

        static Fraction f4(Fraction x)
        {
            return x.Pow(6);
        }
    }
}
