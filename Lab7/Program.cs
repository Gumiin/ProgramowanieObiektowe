using System;

namespace Lab7
{
    delegate double operation(double a, double b);

    delegate bool stringPredicate(string c);
    public class Program
    {
        public static double Add(double a, double b)
        {
            return a + b;
        }

        public static double Mul(double a, double b)
        {
            return a * b;
        }

        public static bool CheckIfLengthEquals5(string c)
        {
            return c.Length == 5;

        }
        static void Main(string[] args)
        {
            operation op = Add;
            Console.WriteLine(op.Invoke(2, 4));
            op = Mul;
            Console.WriteLine(op.Invoke(2, 4));

            stringPredicate tt = CheckIfLengthEquals5;
            Console.WriteLine(tt.Invoke("dxfyfd"));

            Func<double, double, double> funcOperator = delegate (double a, double b)
            {
                return a * b;
            };

            Func<int, string> changeToString = delegate (int s)
            {
                return String.Format("{0:x}", s);
                //return s.ToString();
            };
            Console.WriteLine(changeToString.Invoke(14));
            Predicate<string> OnlyFive = CheckIfLengthEquals5;
            Predicate<int> InRange = delegate (int a)
            {
                return a >= 0 && a <= 100;
            };
            Console.WriteLine(InRange.Invoke(14));
            Func<int, int, int, bool> InRange1 = delegate (int value, int min, int max)
            {
                return value >= min && value <= max;
            };
            Action<string> Print = delegate (string s)
            {
                Console.WriteLine(s);
            };
            Console.WriteLine("--------");
            Action<string> PrintLambda = s=>Console.WriteLine(s);
            Func<int,int,int,bool> InRangeLambda = (a,b,c) => a>=b && a<=c;
            Console.WriteLine(InRangeLambda.Invoke(10, 0, 101));
            operation DIV = (a, b) =>
            {
                if (b != 0)
                    return a / b;
                else
                    throw new Exception("b is zero!");
            };
            Console.WriteLine(DIV.Invoke(5, 3));
        }
    }
}
