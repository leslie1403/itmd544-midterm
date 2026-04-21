namespace Library_api.Models;

public class BookStats
{
    public int TotalBooks { get; set; }
    public double AveragePublishedYear { get; set; }
    public Dictionary<string, int> BooksByGenre { get; set; } = new();
}