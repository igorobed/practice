using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Fraction c = new Fraction(3);
                Fraction a = new Fraction(34, 27);
                Fraction b = new Fraction(2, 4);
                Console.WriteLine(b);
                Console.WriteLine(a + b);
                Console.WriteLine(b - c);
                Console.WriteLine(c * b);
                Console.WriteLine(a.ToString(3));
                Console.WriteLine(a == a);
                Console.WriteLine(c > b);
                Console.WriteLine(c <= b);
                Fraction g = new Fraction(-1, 0);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }
    }
}
