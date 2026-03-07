using System;
using System.Collections.Generic;
using System.Linq;

// ── Generic DataProcessor class ──────────────────────────────────────────────

public class DataProcessor<T>
{
    private List<T> dataList = new List<T>();

    // Adds a single item to the internal list
    public void AddData(T item)
    {
        dataList.Add(item);
    }

    // Performs type-appropriate operations and displays results
    public void ProcessData()
    {
        if (dataList.Count == 0)
        {
            Console.WriteLine("  (no data to process)");
            return;
        }

        Console.WriteLine($"  Items ({dataList.Count}): {string.Join(", ", dataList)}");

        // Numeric operations for int
        if (dataList is List<int> intList)
        {
            Console.WriteLine($"  Sorted   : {string.Join(", ", intList.OrderBy(x => x))}");
            Console.WriteLine($"  Sum      : {intList.Sum()}");
            Console.WriteLine($"  Average  : {intList.Average():F2}");
            Console.WriteLine($"  Min / Max: {intList.Min()} / {intList.Max()}");
        }
        // Numeric operations for double
        else if (dataList is List<double> dblList)
        {
            Console.WriteLine($"  Sorted   : {string.Join(", ", dblList.OrderBy(x => x))}");
            Console.WriteLine($"  Sum      : {dblList.Sum():F2}");
            Console.WriteLine($"  Average  : {dblList.Average():F2}");
            Console.WriteLine($"  Min / Max: {dblList.Min():F2} / {dblList.Max():F2}");
        }
        // String operations
        else if (dataList is List<string> strList)
        {
            Console.WriteLine($"  Sorted (alpha): {string.Join(", ", strList.OrderBy(s => s))}");
            Console.WriteLine($"  Longest word  : {strList.OrderByDescending(s => s.Length).First()}");
            Console.WriteLine($"  Shortest word : {strList.OrderBy(s => s.Length).First()}");
            Console.WriteLine($"  All uppercase : {string.Join(", ", strList.Select(s => s.ToUpper()))}");
        }
        // Fallback: just display the items
        else
        {
            Console.WriteLine("  (no additional statistics available for this type)");
        }
    }
}

// ── Entry point ───────────────────────────────────────────────────────────────

class Program
{
    static void Main()
    {
        // ── Integer processor ─────────────────────────────────────────────────
        Console.WriteLine("=== Integer DataProcessor ===");
        var intProcessor = new DataProcessor<int>();
        intProcessor.AddData(42);
        intProcessor.AddData(7);
        intProcessor.AddData(19);
        intProcessor.AddData(3);
        intProcessor.AddData(55);
        intProcessor.ProcessData();

        Console.WriteLine();

        // ── String processor ──────────────────────────────────────────────────
        Console.WriteLine("=== String DataProcessor ===");
        var stringProcessor = new DataProcessor<string>();
        stringProcessor.AddData("Banana");
        stringProcessor.AddData("Apple");
        stringProcessor.AddData("Kiwi");
        stringProcessor.AddData("Mango");
        stringProcessor.AddData("Strawberry");
        stringProcessor.ProcessData();

        Console.WriteLine();

        // ── Double processor ──────────────────────────────────────────────────
        Console.WriteLine("=== Double DataProcessor ===");
        var doubleProcessor = new DataProcessor<double>();
        doubleProcessor.AddData(3.14);
        doubleProcessor.AddData(2.71);
        doubleProcessor.AddData(1.62);
        doubleProcessor.AddData(9.81);
        doubleProcessor.AddData(6.67);
        doubleProcessor.ProcessData();
    }
}

