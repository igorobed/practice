using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLab3
{
    class Program
    {
        delegate Fraction MyFunction(Fraction x); 
        static void Main(string[] args)
        {
            try
            {
                //задаю интервал
                Fraction[] interval = new Fraction[2] {new Fraction(-6), new Fraction(-3)};
                //задаю точность
                Fraction EPS = new Fraction(1, 10000);
                //присваиваю значение делегату
                MyFunction @delegate = f1;
                //вызываю функцию и печатую значение
                Fraction r = Func(interval, @delegate, EPS);
                Console.WriteLine(r.ToString(3));
                Console.WriteLine(f1(r).ToString(7));
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

        private static Fraction f1(Fraction x)
        {
            return x * x - (new Fraction(10));
        }

        private static Fraction f2(Fraction x)
        {
            return x * x - (new Fraction(5)) * x + (new Fraction(4));
        }

        private static Fraction f3(Fraction x)
        {
            return (new Fraction(3)) * x - (new Fraction(5));
        }

        //поиск корня уравнения методом дихотоми    [a,b]
        private static Fraction Func(Fraction[] interval, MyFunction del, Fraction eps)
        {
            Fraction fr_0 = new Fraction(0);
            if ((del(interval[0]) * del(interval[1])) >= fr_0)
            {
                throw new ArgumentException("The specified interval does not meet the requirements of the algorithm");
            }
            
            Fraction a = interval[0];
            Fraction b = interval[1];

            if (b <= a)
            {
                throw new ArgumentException("Incorrect interval: b <=");
            }
            Fraction c;
            Fraction fr_2 = new Fraction(2);
            
            while ((b - a) > eps)
            {
                c = (a + b) / fr_2;
                if ((del(b) * del(c)) < fr_0)
                {
                    a = (Fraction)c.Clone();
                }
                else
                {
                    b = (Fraction)c.Clone();
                }
            }
            return (a + b) / fr_2;
        }
    }
}
