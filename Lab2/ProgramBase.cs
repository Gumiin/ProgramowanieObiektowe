using System;
using System.Collections.Generic;

namespace Lab2
{
    public class ProgramBase
    {
        static void Main(string[] args, IFlyable[] IcanFly)
        {
            Car car = new Car()
            {
                isEngineWorking = true,
                isFuel = true,
                MaxSpeed = 100
            };
            Vehicle vehicle = car;
            Vehicle anotherVehicle = new Bicycle();
            Vehicle[] vehicles = new Vehicle[3];
            vehicles[0] = car;
            vehicles[1] = anotherVehicle;
            vehicles[2] = new Car();
            //Vehicle[] vehicles ={
            //    new Bicycle(){Weight = 15, MaxSpeed = 30, isDriver = true},
            //    //new Car(){Weight = 900, MaxSpeed = 120, isFuel = true, isEngineWorking = true},
            //    new Bicycle(){Weight = 21, MaxSpeed = 40, isDriver = true},
            //    new Bicycle(){Weight = 19, MaxSpeed = 35, isDriver = true},
            //    new Car(){Weight = 1200, MaxSpeed = 130, isFuel = true, isEngineWorking = true}
            //};
            Console.WriteLine("Hello World!");
            foreach (Vehicle v in vehicles)
            {
                Console.WriteLine(v);
                Console.WriteLine(v.Drive(14));

                if (v is Car)
                {
                    Car currentCar = (Car)v;
                    Console.WriteLine(currentCar.isEngineWorking);
                }
            }

            //IElectric[] electrics = new IElectric[3];
            //electrics[0] = new Scooter();
            //electrics[1] = new Cooker();
            IAggregate aggregate = new IntAggregate();
            IIterator iterator = aggregate.createIterator();
            while (iterator.HasNext())
            {
                Console.WriteLine(iterator.GetNext());
            }

            List<string> names = new List<string>()
            {
                "Łukasz",
                "Tomek",
                "Marek  liceum"

            };


            List<string>.Enumerator enumerator = names.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current);
            }

            foreach (var element in names)
            {
                Console.WriteLine(element);
            }

            int[] arrray = { 1, 2, 3 };

            IFlyable[] p = {
                new Duck() { size = 5 },
                new Wasp() { size = 21 },
                new Duck() { size = 1 },
                new Duck() { size = 6 },
                new Hydroplane()
            };
            var count = 0;
            foreach (var fly in IcanFly)
            {
                if ((fly is IFlyable) && (fly is ISwimmingable))
                {
                    count++;
                    Console.WriteLine(fly.GetType().Name);
                }
            }
            Console.WriteLine(count);
        }
    }
}