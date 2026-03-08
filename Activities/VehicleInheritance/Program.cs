// Vehicle Inheritance Hierarchy

// ── Main program ──────────────────────────────────────────────────────────────
// Car demo
Car myCar = new Car("Toyota", 2022, 4);
Console.WriteLine($"Car  | Manufacturer: {myCar.Manufacturer} | Year: {myCar.Year} | Doors: {myCar.NumberOfDoors}");
myCar.Start();

Console.WriteLine();

// Motorcycle demo
Motorcycle myBike = new Motorcycle("Harley-Davidson", 2021, false);
Console.WriteLine($"Bike | Manufacturer: {myBike.Manufacturer} | Year: {myBike.Year} | Has Sidecar: {myBike.HasSideCar}");
myBike.Wheelie();

Console.WriteLine();

// Truck demo
Truck myTruck = new Truck("Ford", 2020, 5.5);
Console.WriteLine($"Truck| Manufacturer: {myTruck.Manufacturer} | Year: {myTruck.Year} | Capacity: {myTruck.CarryingCapacity} tons");
myTruck.LoadCargo(3.2);

