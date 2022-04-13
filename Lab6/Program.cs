using System;
using System.Collections.Generic;

namespace Lab6
{
    class Student
    {
        public string Name { get; set; }
        public int Ects { get; set; }

        public int CompareTo(Student other)
        {
            return Name.CompareTo(other.Name);
        }

        public override bool Equals(object obj)
        {
            return obj is Student student &&
                   Name == student.Name &&
                   Ects == student.Ects;
        }

        public override int GetHashCode()
        {
            Console.WriteLine("Student HashCode");
            return HashCode.Combine(Name, Ects);
        }

        public override string ToString()
        {
            return $"Name = {Name}, Ects = {Ects}";
        }


    }
    //record Student
    //{
    //    public string Name { get; set; }
    //    public int Ects { get; set; }
    //}
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            ICollection<string> names = new List<string>();
            names.Add("Adam");
            names.Add("Robert");
            names.Add("Ewa");
            Console.WriteLine(names.Contains("Robert"));
            names.Remove("Ewa");
            foreach(string name in names){
                Console.WriteLine(name);
            }
            var student1 = new Student() { Ects = 10, Name = "Ignacy Janicki" };
            var student2 = new Student() { Ects = 5, Name = "Jan Nowak" };
            var student3 = new Student() { Ects = 10, Name = "Piotr Kowalski" };
            var student4 = new Student() { Ects = 10, Name = "Marek z Liceum" };
            Console.WriteLine("---------------------------------");

            ICollection<Student> newList = new List<Student>();

            newList.Add(student1);
            newList.Add(student2);
            newList.Add(student3);
            newList.Add(student4);

            Console.WriteLine(newList.Contains(student1));
            newList.Remove(student4);
            foreach (Student name in newList)
            {
                Console.WriteLine(name.Name);
            }
            Console.WriteLine("---------------------------------");
            List<Student> list = (List<Student>)newList;
            Console.WriteLine(list[0]);
            list[0] = new Student() { Ects = 20, Name = "Karol Porębski" };
            list.Insert(0, new Student(){Ects = 20, Name ="Pudzian"});
            int index = list.IndexOf(new Student() { Ects = 20, Name = "Karol Porębski" });
            Console.WriteLine(index);
            foreach (Student name in newList)
            {
                Console.WriteLine(name.Name);
            }
            Console.WriteLine("---------------------------------");
            ISet<string> setNames = new HashSet<string>();
            setNames.Add("Ewa");
            setNames.Add("Adam");
            setNames.Add("Robert");
            setNames.Add("Marek");

            Console.WriteLine(string.Join(",", setNames));

            ISet<Student> setStudents = new HashSet<Student>();
            var student5 = new Student() { Ects = 10, Name = "Marek z Liceum" };
            setStudents.Add(student1);
            setStudents.Add(student4);
            setStudents.Add(student3);
            setStudents.Add(student4);
            setStudents.Add(student2);
            setStudents.Add(student5);
            foreach (Student name in setStudents)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine("---------------------------------");
            Console.WriteLine(setStudents.Contains(list[3]));
            list.Add(new Student() { Ects = 10, Name = "Marek z Liceum" });
            list.Add(new Student() { Ects = 10, Name = "Marek z Gimnazjum" });
            ISet<Student> set = new HashSet<Student>(list);
            ISet<Student> commonset = new HashSet<Student>(setStudents);
            commonset.IntersectWith(set);
            Console.WriteLine(string.Join(",", commonset));
            //ISet<Student> sortedSet = new SortedSet<Student>();
            //foreach(Student s in sortedSet)
            //{
            //    Console.WriteLine(s);
            //}
            Console.WriteLine("---------------------------------");
            var phone = new Dictionary<string, Student>();
            int i = 141523654;
            foreach (Student student in newList)
            {
                string j = i.ToString();
                if(!phone.ContainsKey(j))
                    phone.Add(j, student);
                i += 4645456;
            }
            foreach(var ss in phone)
            {
                Console.WriteLine($"{ss.Key}   {ss.Value}");
            }
            
        }
    }
}