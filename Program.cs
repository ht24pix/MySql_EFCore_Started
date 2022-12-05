// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using MySql_EFCore_Started;

Console.WriteLine("Hello, World!");

InsertData();
PrintData();


 static void InsertData()
    {
    using (var context = new LibraryContext())
    {
        // Creates the database if not exists
        context.Database.EnsureCreated();

        // Adds a publisher
        var publisher = new Publisher
        {
            Name = "Mariner Books"
        };
        context.Publisher.Add(publisher);

        // Adds some books
        context.Book.Add(new Book
        {
            ISBN = "978-0544003415",
            Title = "The Lord of the Rings",
            Author = "J.R.R. Tolkien",
            Language = "English",
            Pages = 1216,
            Publisher = publisher
        });
        context.Book.Add(new Book
        {
            ISBN = "978-0547247762",
            Title = "The Sealed Letter",
            Author = "Emma Donoghue",
            Language = "English",
            Pages = 416,
            Publisher = publisher
        });

        // Saves changes
        context.SaveChanges();
    }
}

 static void PrintData()
{
    // Gets and prints all books in database
    using (var context = new LibraryContext())
    {
        var books = context.Book
          .Include(p => p.Publisher);
        foreach (var book in books)
        {
            var data = new System.Text.StringBuilder();
            data.AppendLine($"ISBN: {book.ISBN}");
            data.AppendLine($"Title: {book.Title}");
            data.AppendLine($"Publisher: {book.Publisher.Name}");
            Console.WriteLine(data.ToString());
        }
    }
}
