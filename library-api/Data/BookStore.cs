using Library_api.Models;

namespace Library_api.Data;

public static class BookStore
{
    public static List<Book> Books { get; } =
    [
        new Book
        {
            Id = "1",
            Title = "Harry Potter and the Sorcerer's Stone",
            Author = "J.K. Rowling",
            Isbn = "9780590353427",
            Genre = "fantasy",
            PublishedYear = 1997
        },
        new Book
        {
            Id = "2",
            Title = "Harry Potter and the Chamber of Secrets",
            Author = "J.K. Rowling",
            Isbn = "9780439064873",
            Genre = "fantasy",
            PublishedYear = 1998
        },
        new Book
        {
            Id = "3",
            Title = "Harry Potter and the Prisoner of Azkaban",
            Author = "J.K. Rowling",
            Isbn = "9780439136365",
            Genre = "fantasy",
            PublishedYear = 1999
        },
        new Book
        {
            Id = "4",
            Title = "Harry Potter and the Goblet of Fire",
            Author = "J.K. Rowling",
            Isbn = "9780439139601",
            Genre = "fantasy",
            PublishedYear = 2000
        },
        new Book
        {
            Id = "5",
            Title = "Harry Potter and the Order of the Phoenix",
            Author = "J.K. Rowling",
            Isbn = "9780439358071",
            Genre = "fantasy",
            PublishedYear = 2003
        },
        new Book
        {
            Id = "6",
            Title = "Harry Potter and the Half-Blood Prince",
            Author = "J.K. Rowling",
            Isbn = "9780439785969",
            Genre = "fantasy",
            PublishedYear = 2005
        },
        new Book
        {
            Id = "7",
            Title = "Harry Potter and the Deathly Hallows",
            Author = "J.K. Rowling",
            Isbn = "9780545010221",
            Genre = "fantasy",
            PublishedYear = 2007
        },
        new Book
        {
            Id = "8",
            Title = "The Hunger Games",
            Author = "Suzanne Collins",
            Isbn = "9781338334920",
            Genre = "fiction",
            PublishedYear = 2008
        }
    ];

    public static string GenerateId()
    {
        return Guid.NewGuid().ToString();
    }
}