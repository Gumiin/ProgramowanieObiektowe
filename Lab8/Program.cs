using System;
using System.Linq;
using System.Collections.Generic;
namespace Lab8
{

    record Student (string Name, int Ects);
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            int[] ints = { 4, 5, 6, 8, 1, 2, 7, 8, 9 };
            Predicate<int> predicate = n =>
            {
                Console.WriteLine("Predykat dla " + n);
                return n % 2 == 0 && n > 4;
            };
            IEnumerable<int> enumerable = 
                from n in ints
                 where predicate.Invoke(n)
                 select n;
            int sum = enumerable.Sum();
            Console.WriteLine("Suma = " + sum);
            Console.WriteLine("Ewaluacja");
            Console.WriteLine(String.Join(",",enumerable));

            Console.WriteLine("-------------------------");
            string[] strings = { "aa", "bbb", "cc", "dddd", "abc", "bab" };
            var list = strings.Where(x => x.Length == 3).ToList();
            Console.WriteLine(string.Join(",", list).ToUpper());


            Student[] students =
            {
                new Student("Ewa", 10),
                new Student("Łukasz", 24),
                new Student("Tomek", 56),
                new Student("Paweł", 10),
                new Student("Marek", 32),
                new Student("Tomek", 10)
            };

            Console.WriteLine(string.Join("\n",
                from s in students
                where s.Ects>30
                orderby s.Name
                select s.Name
                ));
            Console.WriteLine(string.Join("\n", students.Where(x => x.Ects > 30)));
            IEnumerable<IGrouping<string, Student>> group = from s in students
            group s by s.Name;
            foreach(var item in group)
            {
                Console.WriteLine(item.Key +" "+ item.Count());
            }

            IEnumerable<(string Key, int)> naemsGroup = from s in students
            group s by s.Name into gr
            select (gr.Key, gr.Count());
            Console.WriteLine(string.Join("\n", naemsGroup));

            IEnumerable<(int Key, int)> ectsCount = from s in students
            group s by s.Ects into ect
            select (ect.Key, ect.Count());
            Console.WriteLine(string.Join ("\n", ectsCount));


            Console.WriteLine(string.Join("\n", students.OrderBy(x=>x.Name).ThenBy(x=>x.Ects)));
            Console.WriteLine("----------------");
            Console.WriteLine(string.Join("\n", students.GroupBy(x => x.Name).Select(x=>(x.Key, x.Count()))));
            var test = students.Where(x => x.Ects > 100).FirstOrDefault();
            Console.WriteLine(test);
            bool ppp= ints.All(x => x % 2 == 0);
            Console.WriteLine(ppp);
            bool ddd = ints.Any(x => x % 2 == 0);
            Console.WriteLine(ddd);

            Enumerable.Range(0, 100).Where(x=>x % 2 == 0).Sum();
            Random random = new Random();
            random.Next(5);
            Enumerable.Range(0, 1000);
            Random random2 = new Random();
            random2.Next(9);
            //wygenerować tablicę 1000 liczb losowych w zakresie od 0 do 9 
            //przy pomocy enumerable
            //wygeneruj tablicę liczb pierwszych mniejszych od 100
            int[] vs = Enumerable.Range(0, 1000).Select(x=> random.Next(10)).ToArray();
            Console.WriteLine(vs);
         }
    }
}
