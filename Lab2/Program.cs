using System;
using System.Collections.Generic;

namespace Lab2
{
    class Program
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

                if(v is Car)
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

            foreach(var element in names)
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
            var count=0;
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

    interface IAggregate
    {
        IIterator createIterator();
    }

    interface IIterator
    {
        bool HasNext();
        int GetNext();
    }

    class IntAggregate : IAggregate
    {
        internal int _a = 4;
        internal int _b = 6;
        internal int _c = 2;
        public IIterator createIterator()
        {
           return new IntIterator(this);
        }
    }

    class IntIterator : IIterator
    {
        private IntAggregate _aggregate;
        private int count = 0;

        public IntIterator(IntAggregate aggregate)
        {
            _aggregate = aggregate;
        }

        public int GetNext()
        {
            if(count == 3)
            {
                return _aggregate._c;
            }
            switch (++count)
            {
                case 1:
                    return _aggregate._a;
                case 2:
                    return _aggregate._b;
                case 3:
                    return _aggregate._c;
                default:
                    throw new Exception();
                
            }
        }

        public bool HasNext()
        {
            return count < 3;
        }
    }

    //niżej pojazdy

    public abstract class Vehicle
    {
        public double Weight { get; init; }
        public int MaxSpeed { get; init; }
        protected int _mileage;
        public int Mealeage
        {
            get { return _mileage; }
        }
        public abstract decimal Drive(int distance);
        public override string ToString()
        {
            return $"Vehicle{{ Weight: {Weight}, MaxSpeed: {MaxSpeed}, Mileage: {_mileage} }}";
        }
    }

    public class Car : Vehicle
    {
        public bool isFuel { get; set; }
        public bool isEngineWorking { get; set; }
        public override decimal Drive(int distance)
        {
            if (isFuel && isEngineWorking)
            {
                _mileage += distance;
                return (decimal)(distance / (double)MaxSpeed);
            }
            return -1;
        }
        public override string ToString()
        {
            return $"Car{{Weight: {Weight}, MaxSpeed: {MaxSpeed}, Mileage: {_mileage}}}";
        }
    }

    public class Bicycle : Vehicle
    {
        public bool isDriver { get; set; }
        public override decimal Drive(int distance)
        {
            if (isDriver)
            {
                _mileage += distance;
                return (decimal)(distance / (double)MaxSpeed);
            }
            return -1;
        }
        public override string ToString()
        {
            return $"Bicycle{{Weight: {Weight}, MaxSpeed: {MaxSpeed}, Mileage: {_mileage}}}"; ;
        }
    }

    //public abstract class Scooter : Vehicle, IElectric
    //{
    //    public int Supply()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    //public class ElectricScooter : Vehicle, IElectric
    //{
    //    public bool isDriver { get; set; }
    //    public override decimal Drive(int distance)
    //    {
    //        if (isDriver)
    //        {
    //            _mileage += distance;
    //            return (decimal)(distance / (double)MaxSpeed);
    //        }
    //        return -1;
    //    }

    //    public int Supply()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    //public class KickScooter : Vehicle, IElectric
    //{
    //    public override decimal Drive(int distance)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public int Supply()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}



    public abstract class Cooker : IElectric
    {
        public int Supply()
        {
            throw new NotImplementedException();
        }
    }

    interface IElectric
    {
        int Supply();
    }

    interface IElectricScooter : IElectric
    {
        decimal Drive(int distance);

    }

    //Cw 1
    abstract class Scooter : Vehicle
    {

    }
    class ElectricScooter : Scooter
    {
        private int _BatteriesLevel { get; set; }
        private int MaxRange { get; set; }
        public void ChargeBatteries()
        {
            _BatteriesLevel = 100;
        }

        public override decimal Drive(int distance)
        {
            _BatteriesLevel -= distance * (int)(Weight * 0.2);
            MaxRange = _BatteriesLevel * 4;
            if (MaxRange < distance) return -1;
            return distance / MaxSpeed;
        }
    }

    class KickScooter : Scooter
    {
        public override decimal Drive(int distance)
        {
            throw new NotImplementedException();
        }
    }
    //cw 2
    interface ISwimmingable
    {
        public decimal Swim(int distance);
    }
    interface IFlyable
    {
        public decimal Fly(int distance);
    }

    class Duck : ISwimmingable, IFlyable
    {
        public decimal size;
        public decimal Fly(int distance)
        {
            throw new NotImplementedException();
        }

        public decimal Swim(int distance)
        {
            throw new NotImplementedException();
        }
    }

    
    class Wasp : IFlyable
    {
        public decimal size;
        public decimal Fly(int distance)
        {
            throw new NotImplementedException();
        }
    }
    class Hydroplane : ISwimmingable, IFlyable
    {
        public decimal size;
        public decimal Fly(int distance)
        {
            throw new NotImplementedException();
        }

        public decimal Swim(int distance)
        {
            throw new NotImplementedException();
        }
    }

}

