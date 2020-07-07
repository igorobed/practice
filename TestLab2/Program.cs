using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLab2
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Complex a = new Complex(new Fraction(1), new Fraction(3));
                Complex b = new Complex(new Fraction(-2), new Fraction(1));
                Console.WriteLine(a);
                Console.WriteLine(a + b);
                Console.WriteLine(a * b);
                Console.WriteLine(a - b);
                Console.WriteLine(a / b);
                Console.WriteLine(a == b);
                Console.WriteLine(a != b);
                Console.WriteLine(a > b);
                Console.WriteLine((b.Abs()).ToString(5));
                Console.WriteLine((a.Arg()).ToString(5));
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
    }
}
