// ============================================================
// METHODS DEMO
// ============================================================
// This file demonstrates how methods encapsulate private fields.
// Before C# auto-properties were common, getter/setter methods
// were the primary encapsulation mechanism (still used today
// when complex logic is needed). Three method patterns:
//   1. Getter method  — retrieves the value of a private field
//   2. Setter method  — validates and sets a private field
//   3. Calculation    — uses private fields to produce a result
//       without ever exposing the raw fields themselves
// ============================================================

namespace EncapsulationDemo
{
    public class Rectangle
    {
        // ── Private fields ────────────────────────────────────────────────────
        // Width and height are private. No external code can read or write
        // them directly — all interaction goes through the methods below.
        private double _width;
        private double _height;
        private string _color;

        public Rectangle(double width, double height, string color)
        {
            // Use the setter methods during construction so that validation
            // logic runs even on initial assignment.
            SetWidth(width);
            SetHeight(height);
            SetColor(color);
        }

        // ── 1. GETTER METHODS ─────────────────────────────────────────────────
        // Return the value of a private field to the caller.
        // Encapsulation benefit: the backing field stays private; the method
        // is the only controlled read-path, allowing future changes (e.g. unit
        // conversion) without affecting calling code.

        public double GetWidth()  => _width;
        public double GetHeight() => _height;
        public string GetColor()  => _color;

        // ── 2. SETTER METHODS ─────────────────────────────────────────────────
        // Validate the incoming value before storing it in the private field.
        // Encapsulation benefit: invalid data (negative dimensions) is rejected
        // here rather than scattered across the codebase.

        public void SetWidth(double width)
        {
            if (width <= 0)
                throw new ArgumentException("Width must be greater than zero.");
            _width = width;
        }

        public void SetHeight(double height)
        {
            if (height <= 0)
                throw new ArgumentException("Height must be greater than zero.");
            _height = height;
        }

        public void SetColor(string color)
        {
            if (string.IsNullOrWhiteSpace(color))
                throw new ArgumentException("Color cannot be empty.");
            _color = color.Trim().ToLower();
        }

        // ── 3. CALCULATION METHODS ────────────────────────────────────────────
        // Perform computations using the private fields and return only the
        // result — the raw field values are never leaked.
        // Encapsulation benefit: the internal representation (_width, _height)
        // could change (e.g. to centimetres) and callers would be unaffected
        // as long as the method signatures remain the same.

        // Calculates and returns the area of the rectangle.
        public double CalculateArea()
        {
            return _width * _height;
        }

        // Calculates and returns the perimeter of the rectangle.
        public double CalculatePerimeter()
        {
            return 2 * (_width + _height);
        }

        // Determines whether the rectangle is a square based on private fields.
        public bool IsSquare()
        {
            return _width == _height;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"  Width      : {_width}");
            Console.WriteLine($"  Height     : {_height}");
            Console.WriteLine($"  Color      : {_color}");
            Console.WriteLine($"  Area       : {CalculateArea()}");
            Console.WriteLine($"  Perimeter  : {CalculatePerimeter()}");
            Console.WriteLine($"  Is Square? : {IsSquare()}");
        }
    }
}
