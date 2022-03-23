using System;

namespace Lab3
{
    public  class Program
    {
        static void Main(string[] args)
        {
            Stack<int> stack = new Stack<int>();
            Stack<string> stackString = new Stack<string>();
            stack.Push(1);
            stack.Push(5);
            stack.Push(9);
            stack.Push(10);

            Console.WriteLine(stack.Pop());
            Console.WriteLine(stack.Pop());
            Console.WriteLine(stack.Pop());
            Console.WriteLine();
            Student student = new Student() { Egzam = 60 };
            Console.WriteLine(student.GetReward("Gratulacje"));
            ValueTuple<string, decimal, int> product = ValueTuple.Create("Laptop", 1200m, 2);
            Console.WriteLine();
            Console.WriteLine(product);
            ValueTuple<string, decimal, int> Laptop = ValueTuple.Create("Laptop", 1400m, 2);
            Console.WriteLine();
            Console.WriteLine(Laptop);
            Console.WriteLine(product == Laptop);
            (string name, decimal price, int quantity) tuple = ("Laptop2", 1500m, 4);
            Laptop = tuple;
            Console.WriteLine();
            Console.WriteLine(Laptop);
            var tuple1 = (name: "Laptop4", price: 1299);
            Console.WriteLine();
            Console.WriteLine(tuple1);
        } 
    }


    class Stack<T>
    {
        private T[] arr = new T[10]; //string
        private int _last = -1;
        public void Push(T item) //push(string item)
        {
            arr[++_last] = item;
        }
        public T Pop()  //string Pop()
        {
            return arr[_last--];
        }
    }

    class Student
    {
        private string _firstName;
        public int Egzam { get; set; }

        public void Push<T>(Stack<string> stack)
        {
            stack.Push(_firstName);
        }

        public T? GetReward<T>(T reward)
        {
            if (Egzam > 50)
                return reward;
            else
                return default;
        }
    }

}
