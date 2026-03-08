// ============================================================
// ACCESS MODIFIERS DEMO
// ============================================================
// This file demonstrates the five C# access modifiers and how
// they contribute to encapsulation by controlling which parts
// of the program can see and interact with each member.
// ============================================================

namespace EncapsulationDemo
{
    // ----- Base class used to illustrate access modifier scope -----
    public class Vehicle
    {
        // PUBLIC: accessible from anywhere — no restrictions.
        // Use for members that form the official public API of the class.
        public string Make { get; set; }

        // PRIVATE: accessible only within this class.
        // Hides internal state so no outside code can corrupt it directly.
        private string _engineSerialNumber;

        // PROTECTED: accessible within this class AND any derived class.
        // Allows subclasses to read/modify state that outsiders cannot touch.
        protected int _mileage;

        // INTERNAL: accessible anywhere within the same assembly (.dll / .exe)
        // but hidden from code in other assemblies.
        // Useful for helper members that should not be part of a public API.
        internal string AssemblyPlantCode { get; set; }

        // PROTECTED INTERNAL: accessible within the same assembly OR from a
        // derived class in any assembly. Combines protected + internal rules.
        protected internal string WarrantyCode { get; set; }

        // Constructor initialises the private field — the only controlled entry point.
        public Vehicle(string make, string serialNumber, int mileage,
                       string plantCode, string warrantyCode)
        {
            Make = make;
            _engineSerialNumber = serialNumber;   // private — set here in constructor
            _mileage = mileage;
            AssemblyPlantCode = plantCode;
            WarrantyCode = warrantyCode;
        }

        // A public method exposes the private serial number in a read-only way.
        // External code can retrieve the value but never write to it directly,
        // which is the essence of encapsulation / data hiding.
        public string GetEngineSerialNumber()
        {
            return _engineSerialNumber;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"  Make               : {Make}");
            Console.WriteLine($"  Engine Serial #    : {_engineSerialNumber}");   // accessible here (same class)
            Console.WriteLine($"  Mileage            : {_mileage} miles");
            Console.WriteLine($"  Assembly Plant Code: {AssemblyPlantCode}");
            Console.WriteLine($"  Warranty Code      : {WarrantyCode}");
        }
    }

    // ----- Derived class: can access protected and protected internal members -----
    public class ElectricVehicle : Vehicle
    {
        private int _batteryCapacityKWh;

        public ElectricVehicle(string make, string serial, int mileage,
                               string plant, string warranty, int batteryKWh)
            : base(make, serial, mileage, plant, warranty)
        {
            _batteryCapacityKWh = batteryKWh;
        }

        public void DisplayExtendedInfo()
        {
            Console.WriteLine($"  Make               : {Make}");                 // public — ok
            // Console.WriteLine(_engineSerialNumber);                           // COMPILE ERROR: private
            Console.WriteLine($"  Mileage            : {_mileage} miles");      // protected — ok in derived class
            Console.WriteLine($"  Warranty Code      : {WarrantyCode}");        // protected internal — ok
            Console.WriteLine($"  Battery Capacity   : {_batteryCapacityKWh} kWh");
        }
    }
}
