using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace TestLab2
{
    sealed class Complex : IEquatable<Complex>, IEquatable<int>, IComparable, IComparable<Complex>, ICloneable
    {
        private Fraction _real;
        private Fraction _imaginary;
        private Fraction EPS = new Fraction(1, 1000); 

        #region constructors
        public Complex()
        {
            _real = new Fraction(0);
            _imaginary = new Fraction(0);
        }

        public Complex(Fraction real)
        {
            _real = real;
            _imaginary = new Fraction(0);
        }

        public Complex(Fraction real, Fraction imaginary)
        {
            _real = real;
            _imaginary = imaginary;
        }
        #endregion

        #region properties
        public Fraction Real
        {
            get
            {
                return _real;
            }
        }

        public Fraction Imaginary
        {
            get
            {
                return _imaginary;
            }
        }
        #endregion

        #region ToString
        public override string ToString()
        {
            return $"({_real}) + ({_imaginary}) * I";
        }
        #endregion

        #region interfaces
        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }
            if (obj is Complex cmplx)
            {
                return Equals(cmplx);
            }
            if (obj is int @int)
            {
                return Equals(@int);
            }
            return false;
        }

        public bool Equals(Complex cmplx)
        {
            if (cmplx is null)
            {
                return false;
            }
            return (_real == cmplx._real) && (_imaginary == cmplx._imaginary);
        }

        public bool Equals(int @int)
        {
            //как быть с интом??
            /*if (@int is null)
            {
                return false;
            }*/
            Fraction fr_int = new Fraction(@int);
            Fraction fr_zero = new Fraction(0);
            return (_real == fr_int) && (_imaginary == fr_zero);
        }

        public object Clone()
        {
            return new Complex(Real, Imaginary);
        }

        public int CompareTo(object obj)//сюда над добавить сравнение с интом?
        {
            if (obj is null)//попробовать в этом случае кидать исключение, как советует илья ArgumentNullException
            {
                return 1;
            }
            if (obj is Complex cmplx)
            {
                return CompareTo(cmplx);
            }
            throw new ArgumentException("Invalid type");
        }

        public int CompareTo(Complex cmplx)
        {
            if (cmplx is null)
            {
                return 1;
            }
            var realComparisonResult = Real.CompareTo(cmplx.Real);
            if (realComparisonResult == 0)
            {
                var imaginaryComparisonResult = Imaginary.CompareTo(cmplx.Imaginary);
                return imaginaryComparisonResult;
            }
            return realComparisonResult;
        }
        #endregion

        #region OverloadingLogicalOperators
        public static bool operator ==(Complex first, Complex second)
        {
            if (ReferenceEquals(first, null) && ReferenceEquals(second, null))//можн через is или ==
            {
                return true;
            }
            else if (first is object && second is null)
            {
                return first.Equals(second);
            }
            else if (first is null && second is object)
            {
                return second.Equals(first);
            }
            else
            {
                return first.Equals(second);
            }
        }

        public static bool operator !=(Complex first, Complex second)
        {
            return !(first == second);
        }

        public static bool operator >(Complex first, Complex second)
        {
            return first.CompareTo(second) == 1;
        }

        public static bool operator <(Complex first, Complex second)
        {
            return first.CompareTo(second) == -1;
        }

        public static bool operator >=(Complex first, Complex second)
        {
            return first.CompareTo(second) >= 0;
        }

        public static bool operator <=(Complex first, Complex second)
        {
            return first.CompareTo(second) <= 0;
        }
        #endregion

        #region OverloadingArithmeticOperators
        public static Complex Sum(Complex first, Complex second)
        {
            if (first is null || second is null)
            {
                throw new ArgumentException("Argument is null");
            }
            return new Complex(first._real + second._real, first._imaginary + second._imaginary);
        }

        public static Complex operator +(Complex first, Complex second)
        {
            return Sum(first, second);
        }

        public static Complex Minus(Complex first, Complex second)
        {
            if (first is null || second is null)
            {
                throw new ArgumentException("Argument is null");
            }
            return new Complex(first._real - second._real, first._imaginary - second._imaginary);
        }

        public static Complex operator -(Complex first, Complex second)
        {
            return Minus(first, second);
        }

        public static Complex Mul(Complex first, Complex second)
        {
            if (first is null || second is null)
            {
                throw new ArgumentException("Argument is null");
            }
            return new Complex(first._real * second._real - first._imaginary * second._imaginary,
                               first._real * second._imaginary + first._imaginary * second._real);
        }

        public static Complex operator *(Complex first, Complex second)
        {
            return Mul(first, second);
        }

        public static Complex Div(Complex first, Complex second)
        {
            if (first is null || second is null)
            {
                throw new ArgumentException("Argument is null");
            }
            Fraction fr_zero = new Fraction(0);
            if (second.Real == fr_zero && second.Imaginary == fr_zero)
            {
                throw new DivideByZeroException(nameof(second));
            }
            Fraction denom = second.Real.Pow(2) + second.Imaginary.Pow(2);
            return new Complex((first.Real * second.Real + first.Imaginary * second.Imaginary) / denom,
                               (second.Real * first.Imaginary - second.Imaginary * first.Real) / denom);
        }

        public static Complex operator /(Complex first, Complex second)
        {
            return Div(first, second);
        }
        #endregion

        #region MathAlgorithms
        //модуль комплексного числа
        public Fraction Abs()
        {
            if (this is null)
            {
                throw new ArgumentException("Incorrect");
            }
            Fraction res = Real * Real + Imaginary * Imaginary;
            return res.SqrtNewton(EPS);

        }

        //над использовать формулу Муавра, либо можн так??
        public Complex Pow(int n)
        {
            if (n > 6 || n < -6)
            {
                throw new ArgumentException("Argument too large");
            }
            if (n == 0)
            {
                return new Complex(new Fraction(1), new Fraction(0));
            }
            Complex temp, res;
            if (n > 0)
            {
                temp = (Complex)Clone();//здесь не возникнет ошибки????
                res = (Complex)temp.Clone();
            }
            else
            {
                temp = (new Complex(new Fraction(1), new Fraction(0))) / ((Complex)Clone());
                res = (Complex)temp.Clone();
            }
            
            for (int i = 1; i < Math.Abs(n); i++)
            {
                res = res * temp;
            }
            return res;
        }

        //аргумент комплексного числа
        public Fraction Arg()
        {
            Fraction res = Imaginary / Real;
            return res.ATanTaylor(7);
        }


        #endregion

    }
}
