using System;

namespace ProgramowanieObiektowe
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Person person = Person.Of("tet");
            Console.WriteLine(person.FirstName);
            DateTime date = DateTime.Now;
            date = date.AddDays(1);
            date = new DateTime(DateTime.Now.Year, date.Month, date.Month);
            string a = "test";
            string b = "bool";
            Console.WriteLine($"{a}, {b}");
            a += b;
            Console.WriteLine($"{a}");
        }
    }

    public enum Currency
    {
        PLN = 1,
        USD,
        EUR
    }

    public class Money
    {
        private readonly decimal _value;
        private readonly Currency _currency;
        private Money(decimal value, Currency currency)
        {
            _value = value;
            _currency = currency;
        }

        public static Money? Of(decimal value, Currency currency)
        {
            return value < 0 ? null : new Money(value, currency);
        }

        public static Money? OfWithException(decimal value, Currency currecy)
        {
           return Of(value,currency)
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
