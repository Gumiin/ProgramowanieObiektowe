using System;

namespace ProgramowanieObiektowe
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Person person = Person.Of("Karol");
            Console.WriteLine(person.FirstName);
            Console.WriteLine();
            DateTime date = DateTime.Parse("03-02-2022");
            Console.WriteLine(date);
            Console.WriteLine();
            DateTime newDate = date.AddDays(2);
            DateTime secondDate = new DateTime(DateTime.Now.Year, date.Month, date.Month);
            Console.WriteLine(date + " " + newDate);
            Console.WriteLine();
            string name = "adam";
            string name1 = "adam";
            string v = name.Substring(0, 2);
            Console.WriteLine(name == name1);
            Console.WriteLine();
            Money money = Money.Of(10, Currency.PLN);
            var result = money * 5;
            Console.WriteLine(result.Value);
            Console.WriteLine();
            var result2 = 9 * money;
            Console.WriteLine(result2.Value);
            Console.WriteLine();
            var result3 = 3.14m * money;
            Console.WriteLine(result3.Value);
            Console.WriteLine();
            Money sum = money + result2;
            Console.WriteLine(sum.Value + " " + sum.Currency);
            Console.WriteLine();
            //Money money2 = Money.Of(10, Currency.EUR);
            //Money sum2 = money2 + result3;
            //Console.WriteLine(sum.Value + " " + sum.Currency);
            Console.WriteLine();
            Console.WriteLine(sum > sum);
            Console.WriteLine(sum < sum);
            Console.WriteLine(sum >= sum);
            Console.WriteLine();
            Console.WriteLine(sum);

            Money[] price =
            {
                Money.Of(11, Currency.PLN),
                Money.Of(12, Currency.PLN),
                Money.Of(12, Currency.EUR),
                Money.Of(14, Currency.EUR),
                Money.Of(20, Currency.EUR)
            };

            Array.Sort(price);
            foreach(var m in price)
            {
                Console.WriteLine(m);
            }
        }
    }

    

    public enum Currency
    {
        PLN = 1,
        USD,
        EUR
    }

    public class Money : IEquatable<Money>, IComparable<Money>
    {
        private readonly decimal _value;
        private readonly Currency _currency;
        private Money(decimal value, Currency currency)
        {
            _value = value;
            _currency = currency;
        }
        
        public decimal Value
        {
            get { return _value; }
        }

        public Currency Currency
        {
            get { return _currency; }
        }

        public static Money? Of(decimal value, Currency currency)
        {
            return value < 0 ? null : new Money(value, currency);
        }

        public static Money? OfWithException(decimal value, Currency currency)
        {
            return Of(value, currency);
        }

        public static Money operator *(Money a, decimal b)
        {
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            
            return Money.Of(a._value * b, a._currency);
        }

        public static Money operator *(decimal b, Money a)
        {
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }

            return Money.Of(a._value * b, a._currency);
        }

        public static Money operator /(Money a, decimal b)
        {
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }

            return Money.Of(a._value * b, a._currency);
        }

        public static Money operator /(decimal b, Money a)
        {
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }

            return Money.Of(a._value * b, a._currency);
        }

        public static Money operator +(Money a, Money b)
        {
            if (a._currency != b._currency)
            {

                throw new ArgumentException("Diffrent currencies!");
            }
            else
            {
                return Money.Of(a._value + b._value, a._currency);
            }

        }
        public static Money operator -(Money a, Money b)
        {
            if (a._currency != b._currency)
            {

                throw new ArgumentException("Diffrent currencies!");
            }
            else
            {
                return Money.Of(a._value + b._value, a._currency);
            }

        }
        public static Money operator *(Money a, Money b)
        {
            if (a._currency != b._currency)
            {

                throw new ArgumentException("Diffrent currencies!");
            }
            else
            {
                return Money.Of(a._value + b._value, a._currency);
            }


        }
        public static Money operator /(Money a, Money b)
        {
            if (a._currency != b._currency)
            {

                throw new ArgumentException("Diffrent currencies!");
            }
            else
            {
                return Money.Of(a._value + b._value, a._currency);
            }
        }

        public static bool operator >(Money a, Money b)
        {
            if (a._currency != b._currency)
            {
                throw new ArgumentException("Diffrent currencies!");
            }
            return a.Value < b.Value;
        }

        public static bool operator <(Money a, Money b)
        {
            if (a._currency != b._currency)
            {
                throw new ArgumentException("Diffrent currencies!");
            }
            return a.Value < b.Value;
        }

        //public static bool operator ==(Money a, Money b)
        //{
        //    return a.Value == b.Value;
        //}

        //public static bool operator !=(Money a, Money b)
        //{
        //    return !(a == b);
        //}

        public static bool operator <=(Money a, Money b)
        {
            if (a._currency != b._currency)
            {
                throw new ArgumentException("Diffrent currencies!");
            }
            return a.Value <= b.Value;
        }
        public static bool operator >=(Money a, Money b)
        {
            if (a._currency != b._currency)
            {
                throw new ArgumentException("Diffrent currencies!");
            }
            return a.Value >= b.Value;
        }

        public static implicit operator decimal(Money money)
        {
            return money.Value;
        }
        public static explicit operator double(Money money)
        {
            return (double)money.Value;
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override bool Equals(object obj)
        {
            return obj is Money money &&
                   _value == money._value &&
                   _currency == money._currency;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_value, _currency);
        }



        public bool Equals(Money other)
        {
            return _value == other._value &&
                   _currency == other._currency;
        }

        public int CompareTo(Money other)
        {
            return other.Currency.CompareTo(_currency);
        }
    }
    class Person
    {
        private string _firstName;
        private Person(string firstName)
        {
            _firstName = firstName;
        }

        public static Person Of(string firstName)
        {
            if(firstName != null && firstName.Length >= 2)
            {
                return new Person(firstName);
            }
            else
            {
                throw new ArgumentOutOfRangeException("Imię zbyt krótkie");
            }
        }
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (value != null && value.Length >= 2)
                {
                    _firstName = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Imię zbyt krótkie");
                }
            }
        }
        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (value != null && value.Length >= 2)
                {
                    _lastName = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Nazwisko zbyt krótkie");
                }
            }
        }
    }

}
