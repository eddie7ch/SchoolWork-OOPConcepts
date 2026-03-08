using System;
using System.Collections.Generic;
using System.Linq;

class Movie
{
    public string Title { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public double Rating { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        List<Movie> movies = new List<Movie>
        {
            new Movie { Title = "The Shawshank Redemption", Genre = "Drama",   Rating = 9.3 },
            new Movie { Title = "The Godfather",             Genre = "Crime",   Rating = 9.2 },
            new Movie { Title = "Pulp Fiction",              Genre = "Crime",   Rating = 8.9 },
            new Movie { Title = "The Dark Knight",           Genre = "Action",  Rating = 9.0 },
            new Movie { Title = "Schindler's List",          Genre = "Drama",   Rating = 8.9 },
            new Movie { Title = "Inception",                 Genre = "Sci-Fi",  Rating = 8.8 },
            new Movie { Title = "Interstellar",              Genre = "Sci-Fi",  Rating = 8.6 },
            new Movie { Title = "The Matrix",                Genre = "Action",  Rating = 8.7 },
            new Movie { Title = "Goodfellas",                Genre = "Crime",   Rating = 8.7 },
            new Movie { Title = "Forrest Gump",              Genre = "Drama",   Rating = 8.8 },
            new Movie { Title = "The Silence of the Lambs",  Genre = "Thriller",Rating = 8.6 },
            new Movie { Title = "Se7en",                     Genre = "Thriller",Rating = 8.6 },
            new Movie { Title = "Avengers: Endgame",         Genre = "Action",  Rating = 8.4 },
            new Movie { Title = "The Prestige",              Genre = "Thriller",Rating = 8.5 },
            new Movie { Title = "Arrival",                   Genre = "Sci-Fi",  Rating = 7.9 },
        };

        // Display available genres
        var genres = movies.Select(m => m.Genre).Distinct().OrderBy(g => g).ToList();
        Console.WriteLine("Available genres: " + string.Join(", ", genres));
        Console.Write("\nEnter your preferred movie genre: ");
        string input = Console.ReadLine()?.Trim() ?? "";

        var recommendations = movies
            .Where(m => m.Genre.Equals(input, StringComparison.OrdinalIgnoreCase))
            .OrderByDescending(m => m.Rating)
            .ToList();

        if (recommendations.Count == 0)
        {
            Console.WriteLine($"\nNo movies found for genre \"{input}\".");
        }
        else
        {
            Console.WriteLine($"\nTop-rated {recommendations[0].Genre} movies:\n");
            Console.WriteLine($"{"#",-4} {"Title",-40} {"Rating",6}");
            Console.WriteLine(new string('-', 52));
            int rank = 1;
            foreach (var movie in recommendations)
            {
                Console.WriteLine($"{rank,-4} {movie.Title,-40} {movie.Rating,6:F1}");
                rank++;
            }
        }
    }
}
