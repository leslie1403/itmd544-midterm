using Library_api.Data;
using Library_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library_api.Controllers;

[ApiController]
[Route("/")]
public class BooksController : ControllerBase
{
    private static readonly HashSet<string> ValidGenres =
    [
        "fiction", "non-fiction", "fantasy", "sci-fi", "mystery"
    ];

    [HttpGet]
    public ActionResult<IEnumerable<Book>> ListBooks()
    {
        return Ok(BookStore.Books);
    }

    [HttpPost]
    public ActionResult<Book> CreateBook([FromBody] BookCreate input)
    {
        if (!IsValidCreate(input, out var error))
        {
            return BadRequest(new ErrorResponse { Message = error });
        }

        var book = new Book
        {
            Id = BookStore.GenerateId(),
            Title = input.Title,
            Author = input.Author,
            Isbn = input.Isbn,
            Genre = input.Genre,
            PublishedYear = input.PublishedYear
        };

        BookStore.Books.Add(book);
        return StatusCode(201, book);
    }

    [HttpGet("{id}")]
    public ActionResult<Book> GetBookById(string id)
    {
        var book = BookStore.Books.FirstOrDefault(b => b.Id == id);

        if (book is null)
        {
            return NotFound(new ErrorResponse { Message = "Book not found." });
        }

        return Ok(book);
    }

    [HttpPatch("{id}")]
    public ActionResult<Book> UpdateBook(string id, [FromBody] BookUpdate input)
    {
        var book = BookStore.Books.FirstOrDefault(b => b.Id == id);

        if (book is null)
        {
            return NotFound(new ErrorResponse { Message = "Book not found." });
        }

        if (!IsValidUpdate(input, out var error))
        {
            return BadRequest(new ErrorResponse { Message = error });
        }

        if (input.Title is not null) book.Title = input.Title;
        if (input.Author is not null) book.Author = input.Author;
        if (input.Isbn is not null) book.Isbn = input.Isbn;
        if (input.Genre is not null) book.Genre = input.Genre;
        if (input.PublishedYear.HasValue) book.PublishedYear = input.PublishedYear.Value;

        return Ok(book);
    }

    [HttpDelete("{id}")]
    public ActionResult<Book> DeleteBook(string id)
    {
        var book = BookStore.Books.FirstOrDefault(b => b.Id == id);

        if (book is null)
        {
            return NotFound(new ErrorResponse { Message = "Book not found." });
        }

        BookStore.Books.Remove(book);
        return Ok(book);
    }

    [HttpGet("stats")]
    public ActionResult<BookStats> GetStats()
    {
        var totalBooks = BookStore.Books.Count;

        var averagePublishedYear = totalBooks == 0
            ? 0
            : BookStore.Books.Average(b => b.PublishedYear);

        var booksByGenre = BookStore.Books
            .GroupBy(b => b.Genre)
            .ToDictionary(group => group.Key, group => group.Count());

        return Ok(new BookStats
        {
            TotalBooks = totalBooks,
            AveragePublishedYear = averagePublishedYear,
            BooksByGenre = booksByGenre
        });
    }

    private static bool IsValidCreate(BookCreate input, out string error)
    {
        if (string.IsNullOrWhiteSpace(input.Title) ||
            string.IsNullOrWhiteSpace(input.Author) ||
            string.IsNullOrWhiteSpace(input.Isbn) ||
            string.IsNullOrWhiteSpace(input.Genre))
        {
            error = "All required fields must be provided.";
            return false;
        }

        if (!ValidGenres.Contains(input.Genre))
        {
            error = "Invalid genre value.";
            return false;
        }

        error = string.Empty;
        return true;
    }

    private static bool IsValidUpdate(BookUpdate input, out string error)
    {
        if (input.Genre is not null && !ValidGenres.Contains(input.Genre))
        {
            error = "Invalid genre value.";
            return false;
        }

        error = string.Empty;
        return true;
    }
}