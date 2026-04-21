namespace Library_api.Models;

public class BookUpdate
{
    public string? Title { get; set; }
    public string? Author { get; set; }
    public string? Isbn { get; set; }
    public string? Genre { get; set; }
    public int? PublishedYear { get; set; }
}