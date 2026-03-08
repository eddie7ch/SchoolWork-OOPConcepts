using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<double> numbers = new List<double>();
        Console.WriteLine("=== Average Calculator ===");
        Console.WriteLine("Enter numbers one at a time. Enter -1 to finish and calculate the average.");

        // --- Input Loop ---
        while (true)
        {
            Console.Write("Enter a number: ");
            string? input = Console.ReadLine();

            try
            {
                double number = Convert.ToDouble(input);

                if (number == -1)
                    break;

                numbers.Add(number);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Invalid input. The number is too large or too small.");
            }
        }

        // --- Average Calculation ---
        try
        {
            if (numbers.Count == 0)
                throw new DivideByZeroException("No numbers were entered. Cannot divide by zero.");

            double sum = 0;
            foreach (double n in numbers)
                sum += n;

            double average = sum / numbers.Count;
            Console.WriteLine($"\nNumbers entered: {string.Join(", ", numbers)}");
            Console.WriteLine($"Average: {average}");
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine($"Division by zero error: {ex.Message}");
        }

        // --- Test Case 3: Null Reference Exception ---
        try
        {
            string? x = null;
            int result = x!.Length;   // deliberately triggers NullReferenceException
        }
        catch (NullReferenceException ex)
        {
            Console.WriteLine($"Null reference error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}

