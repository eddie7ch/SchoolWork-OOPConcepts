namespace AbstractionAndInterfaces;

public interface ICalculator
{
    double Add(double a, double b);
    double Subtract(double a, double b);
}

public class BasicCalculator : ICalculator
{
    public double Add(double a, double b)
    {
        double result = a + b;
        Console.WriteLine($"BasicCalculator: {a} + {b} = {result}");
        return result;
    }

    public double Subtract(double a, double b)
    {
        double result = a - b;
        Console.WriteLine($"BasicCalculator: {a} - {b} = {result}");
        return result;
    }
}

public class ScientificCalculator : ICalculator
{
    // Adds using floating-point summation with compensated rounding (Kahan algorithm)
    public double Add(double a, double b)
    {
        double sum = a + b;
        double compensation = (a - (sum - b)) + (b - (sum - a));
        double result = sum + compensation;
        Console.WriteLine($"ScientificCalculator (compensated): {a} + {b} = {result}");
        return result;
    }

    // Subtracts and returns the absolute difference
    public double Subtract(double a, double b)
    {
        double result = Math.Abs(a - b);
        Console.WriteLine($"ScientificCalculator (|a - b|): |{a} - {b}| = {result}");
        return result;
    }
}
