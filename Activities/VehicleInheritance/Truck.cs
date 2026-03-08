// Subclass: Truck
public class Truck : Vehicle
{
    public double CarryingCapacity { get; }

    public Truck(string manufacturer, int year, double carryingCapacity)
        : base(manufacturer, year)
    {
        CarryingCapacity = carryingCapacity;
    }

    public void LoadCargo(double weight)
    {
        Console.WriteLine($"  Cargo loaded onto the {Year} {Manufacturer} truck. Weight: {weight} tons.");
    }
}
