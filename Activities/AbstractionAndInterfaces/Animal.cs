namespace AbstractionAndInterfaces;

public abstract class Animal
{
    public string Name { get; set; }

    protected Animal(string name)
    {
        Name = name;
    }

    public abstract void MakeSound();
    public abstract void Move();
}

public class Cat : Animal
{
    public Cat(string name) : base(name) { }

    public override void MakeSound()
    {
        Console.WriteLine($"{Name} (Cat) says: Meow!");
    }

    public override void Move()
    {
        Console.WriteLine($"{Name} (Cat) moves: Prowls silently on padded paws.");
    }
}

public class Dog : Animal
{
    public Dog(string name) : base(name) { }

    public override void MakeSound()
    {
        Console.WriteLine($"{Name} (Dog) says: Woof!");
    }

    public override void Move()
    {
        Console.WriteLine($"{Name} (Dog) moves: Runs and bounds across the yard.");
    }
}
