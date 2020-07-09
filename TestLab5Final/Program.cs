using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLab5Final
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                /*var valid1 = BaseValidator<int>.CreateBuilder().Add(data => data % 2 == 0)
                                                              .Add(data => data > 3)
                                                              .GetValidator();
                valid1.Validate(2);*/

                var valid2 = BaseValidator<bool>.CreateBuilder().Add(data => data == true)
                                                              .GetValidator();
                valid2.Validate(true);

                var valid3 = BaseValidator<double>.CreateBuilder().GetValidator();
                valid3.Validate(3.21);
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
