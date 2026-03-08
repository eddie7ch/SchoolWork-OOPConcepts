// Subclass: Car
public class Car : Vehicle
{
    public int NumberOfDoors { get; }

    public Car(string manufacturer, int year, int numberOfDoors)
        : base(manufacturer, year)
    {
        NumberOfDoors = numberOfDoors;
    }

    public void Start()
    {
        Console.WriteLine($"  The {Year} {Manufacturer} car has started.");
    }
}
