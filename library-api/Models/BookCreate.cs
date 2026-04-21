namespace Library_api.Models;

public class BookCreate
{
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Isbn { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public int PublishedYear { get; set; }
}