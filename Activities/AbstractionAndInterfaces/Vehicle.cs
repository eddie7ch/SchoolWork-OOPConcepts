namespace AbstractionAndInterfaces;

public abstract class Vehicle
{
    public string Make { get; set; }
    public string Model { get; set; }

    protected Vehicle(string make, string model)
    {
        Make = make;
        Model = model;
    }

    public abstract void Drive();
    public abstract void Stop();
}

public class Car : Vehicle
{
    public Car(string make, string model) : base(make, model) { }

    public override void Drive()
    {
        Console.WriteLine($"{Make} {Model} (Car): Accelerates smoothly down the road.");
    }

    public override void Stop()
    {
        Console.WriteLine($"{Make} {Model} (Car): Brakes gently and parks.");
    }
}

public class Truck : Vehicle
{
    public Truck(string make, string model) : base(make, model) { }

    public override void Drive()
    {
        Console.WriteLine($"{Make} {Model} (Truck): Revs the engine and hauls the load.");
    }

    public override void Stop()
    {
        Console.WriteLine($"{Make} {Model} (Truck): Applies air brakes and pulls over.");
    }
}
