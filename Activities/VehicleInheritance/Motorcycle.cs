// Subclass: Motorcycle
public class Motorcycle : Vehicle
{
    public bool HasSideCar { get; }

    public Motorcycle(string manufacturer, int year, bool hasSideCar)
        : base(manufacturer, year)
    {
        HasSideCar = hasSideCar;
    }

    public void Wheelie()
    {
        Console.WriteLine($"  The {Year} {Manufacturer} motorcycle is performing a wheelie!");
    }
}
