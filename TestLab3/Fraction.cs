using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLab3
{
    class Fraction : ICloneable, IComparable, IEquatable<Fraction>, IComparable<Fraction>
    {
        //numerator - числитель
        private BigInteger _numerator;
        //denominator - знаменатель
        private BigInteger _denominator;

        #region constructors
        public Fraction()
        {
            _numerator = 1;
            _denominator = 1;
        }

        //подразумевается, что в знаменателе стоит 1
        public Fraction(BigInteger num)
        {
            _numerator = num;
            _denominator = 1;
        }
        public Fraction(BigInteger num, BigInteger denom)
        {
            if (denom == 0)
            {
                throw new ArgumentException("Denominator can not be zero");
            }
            _numerator = num;
            _denominator = denom;
            if (_denominator < 0)
            {
                _numerator = _numerator * (-1);
                _denominator = _denominator * (-1);
            }
            BigInteger temp = NOD(_numerator, _denominator);
            _numerator = _numerator / temp;
            _denominator = _denominator / temp;
        }
        #endregion

        #region properties
        public BigInteger Numerator
        {
            get
            {
                return _numerator;
            }
        }

        public BigInteger Denominator
        {
            get
            {
                return _denominator;
            }
        }
        #endregion

        #region ToString
        public override string ToString()
        {
            return $"<{_numerator}>/<{_denominator}>";
        }

        //печать с заданным числом знаков после запятой
        public string ToString(int n)
        {
            double result = (double)this._numerator / (double)this._denominator;
            string form = "#.";
            for (int i = 0; i < n; i++)
            {
                form += '#';
            }
            return result.ToString(form);
        }
        #endregion

        #region MathAlgorithms
        //поиск наибольшего общего делителя(Алгоритм Евклида)
        private static BigInteger NOD(BigInteger a, BigInteger b)
        {
            if (a == 0 && b == 0)
            {
                throw new ArgumentException("NOD incorrect arguments");
            }
            if (a < 0) a = -1 * a;
            if (b < 0) b = -1 * b;
            while (b != 0)
            {
                BigInteger temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        //поиск наименьшего общего кратного(нужно для сложения и вычитания)
        private static BigInteger NOK(BigInteger a, BigInteger b)
        {
            if (a == 0 && b == 0)
            {
                throw new ArgumentException("NOK incorrect arguments");
            }
            if (a < 0) a = -1 * a;
            if (b < 0) b = -1 * b;
            /*if ((a * b) < 0)
            {
                return (-1 * (a * b)) / NOD(a, b);
            }
            else
            {
                return (a * b) / NOD(a, b);
            }*/
            return (a * b) / NOD(a, b);
        }

        private static Fraction Abs(Fraction x)
        {
            if (x is null)
            {
                throw new NullReferenceException("An empty object is passed");
            }
            if (x._numerator < 0)
            {
                return new Fraction(-1 * x._numerator, x._denominator);
            }
            else
            {
                return new Fraction(x._numerator, x._denominator);
            }
        }

        //метод Ньютона
        public Fraction SqrtNewton(Fraction eps)
        {
            if (eps <= (new Fraction(0)))
            {
                throw new ArgumentException("eps must be greater than zero");
            }
            if (this._numerator < 0)
            {
                throw new ArgumentException("There must be a number greater than or equal to zero under the root");
            }
            Fraction x = new Fraction();
            Fraction frConst = new Fraction(2);
            for (; ; )
            {
                Fraction nx = (x + (this / x)) / frConst;
                if (Abs(x - nx) < eps)
                {
                    break;
                }
                x = (Fraction)nx.Clone();
            }
            return x;
        }
        #endregion

        #region OverloadingArithmeticOperators
        public static Fraction Sum(Fraction first, Fraction second)
        {
            if (first is null || second is null)
            {
                throw new ArgumentException("Argument is null");
            }
            BigInteger nok = NOK(first._denominator, second._denominator);
            return new Fraction((nok / first._denominator) * first._numerator + (nok / second._denominator) * second._numerator, nok);
        }

        public static Fraction operator +(Fraction first, Fraction second)
        {
            if (first is null || second is null)
            {
                throw new ArgumentException("Argument is null");
            }
            return Sum(first, second);
        }

        public static Fraction Minus(Fraction first, Fraction second)
        {
            if (first is null || second is null)
            {
                throw new ArgumentException("Argument is null");
            }
            BigInteger nok = NOK(first._denominator, second._denominator);
            return new Fraction((nok / first._denominator) * first._numerator - (nok / second._denominator) * second._numerator, nok);
        }

        public static Fraction operator -(Fraction first, Fraction second)
        {
            if (first is null || second is null)
            {
                throw new ArgumentException("Argument is null");
            }
            return Minus(first, second);
        }

        public static Fraction Mul(Fraction first, Fraction second)
        {
            if (first is null || second is null)
            {
                throw new ArgumentException("Argument is null");
            }
            return new Fraction(first._numerator * second._numerator, first._denominator * second._denominator);
        }

        public static Fraction operator *(Fraction first, Fraction second)
        {
            if (first is null || second is null)
            {
                throw new ArgumentException("Argument is null");
            }
            return Mul(first, second);
        }

        public static Fraction Div(Fraction first, Fraction second)
        {
            if (first is null || second is null)
            {
                throw new ArgumentException("Argument is null");
            }
            if (second._numerator == 0)
            {
                throw new DivideByZeroException();
            }
            return new Fraction(first._numerator * second._denominator, first._denominator * second._numerator);
        }

        public static Fraction operator /(Fraction first, Fraction second)
        {
            if (first is null || second is null)
            {
                throw new ArgumentException("Argument is null");
            }
            if (second._numerator == 0)
            {
                throw new DivideByZeroException();
            }
            return Div(first, second);
        }

        public Fraction Pow(int n)
        {
            if (n == 0)
            {
                return new Fraction(1, 1);
            }
            if (n > 6 || n < (-6))
            {
                throw new ArgumentException("Too much degree");
            }

            if (this._numerator == 0)
            {
                return new Fraction(0);
            }
            BigInteger numer;
            BigInteger denom;
            if (n > 0)
            {
                numer = this._numerator;
                denom = this._denominator;

            }
            else
            {
                numer = this._denominator;
                denom = this._numerator;
            }
            n = Math.Abs(n);
            for (int i = 1; i < n; i++)
            {
                numer *= this._numerator;
                denom *= this._denominator;
            }

            return new Fraction(numer, denom);

        }
        #endregion


        #region interfaces
        public object Clone()
        {
            return new Fraction(this._numerator, this._denominator);
        }

        public int CompareTo(object obj)
        {
            if (obj is null)
            {
                return 1;
            }

            if (obj is Fraction)
            {
                return CompareTo(obj as Fraction);
            }

            throw new ArgumentException("Object is not a Fraction");
        }

        public int CompareTo(Fraction other)
        {
            if (other == null)
            {
                return 1;
            }
            BigInteger first = other._denominator * this._numerator;
            BigInteger second = other._numerator * this._denominator;
            return first.CompareTo(second);
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }
            if (obj is Fraction)
            {
                return Equals(obj as Fraction);
            }
            return false;
        }

        public bool Equals(Fraction obj)
        {
            if (obj is null)
            {
                return false;
            }
            if (_numerator == obj._numerator && _denominator == obj._denominator)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region OverloadingLogicalOperators
        public static bool operator ==(Fraction first, Fraction second)
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

        public static bool operator !=(Fraction first, Fraction second)
        {
            return !(first == second);
        }

        public static bool operator >(Fraction first, Fraction second)
        {
            return first.CompareTo(second) == 1;
        }

        public static bool operator <(Fraction first, Fraction second)
        {
            return first.CompareTo(second) == -1;
        }

        public static bool operator >=(Fraction first, Fraction second)
        {
            return first.CompareTo(second) >= 0;
        }

        public static bool operator <=(Fraction first, Fraction second)
        {
            return first.CompareTo(second) <= 0;
        }
        #endregion
    }
}
