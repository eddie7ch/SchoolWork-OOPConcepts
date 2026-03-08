// Base class
public class Vehicle
{
    public string Manufacturer { get; }
    public int Year { get; }

    public Vehicle(string manufacturer, int year)
    {
        Manufacturer = manufacturer;
        Year = year;
    }
}
