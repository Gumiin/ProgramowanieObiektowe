using Microsoft.EntityFrameworkCore;
using System;
using static lab9.Program;
using System.Linq;
using System.Xml.Linq;
using System.Net;

namespace lab9
{
    public class Program
    {
        static void Main(string[] args)
        {

            AppContext context = new AppContext();
            context.Database.EnsureCreated();

            IQueryable<Book> books = from book in context.Books
                                     where book.EditionYear > 2019
                                     select book;
            Console.WriteLine(string.Join("\n", books));

            var list = from book in context.Books
                       join author in context.Authors
                       on book.AuthorId equals author.Id
                       where book.EditionYear > 2019
                       select new { BookAuthor = author.Name, Title = book.Title };

            Console.WriteLine(string.Join("\n", list));
            foreach (var item in list)
            {
                Console.WriteLine(item.BookAuthor);
            }

            list = context.Authors.Join(
                context.Books.Where(b => b.EditionYear > 2019),
                a => a.Id,
                b => b.AuthorId,
                (a, b) => new { BookAuthor = a.Name, Title = b.Title }
                );
            Console.WriteLine("----------------------");
            foreach (var item in list)
            {
                Console.WriteLine(item.BookAuthor);
            }

            var listUnique = context.BookCopies.Join(
                            context.Books,
                            a => a.BookId,
                            b => b.Id,
                            (a, b) => new { UniqueNumber = a.UniqueNumber, Title = b.Title }
                            );

            Console.WriteLine("----------------------");
            foreach (var item in listUnique)
            {
                Console.WriteLine(item);
            }

            string xml =
                "<books>" +
                    "<book>" +
                        "<id>1</id>" +
                        "<title>C#</title>" +
                    "</book>" +
                    "<book>" +
                        "<id>2</id>" +
                        "<title>Asp.net</title>" +
                    "</book>" +
                "</books>";
            XDocument doc = XDocument.Parse(xml);
            var test = doc
                            .Elements("books")
                            .Elements("book")
                            .Select(x => new { Id = x.Elements("id").First().Value, Title = x.Elements("title").First().Value });


            foreach (var item in test)
            {
                Console.WriteLine(item);
            }


            WebClient client = new WebClient();
            client.Headers.Add("Accept", "application/xml");
            var xml2 = client.DownloadString("http://api.nbp.pl/api/exchangerates/tables/C");
            // uzyskac na listę (IEnumerable) obiektów z polami Code, Bid, Ask
            var rates = XDocument.Parse(xml2)
                .Elements("ArrayOfExchangeRate")
                .Elements("ExchangeRatesTable")
                .Elements("Rates")
                .Select(n => new
                {
                    Code = n.Element("Code").Value,
                    Ask = n.Element("Ask").Value,
                    Bid = n.Element("Bid").Value,
                });
            foreach (var item in rates)
            {
                Console.WriteLine(item);
            }

        }



    }

    public record Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int EditionYear { get; set; }
        public int AuthorId { get; set; }
    }

    public record BookCopy
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string UniqueNumber { get; set; }
    }

    public record Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }


    class AppContext : DbContext
    {

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookCopy> BookCopies { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("DATASOURCE=d:\\kodowanko\\vs c#\\programowanie obiektowe\\database\\base.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().ToTable("books").HasData(
                new Book() { Id = 1, AuthorId = 1, EditionYear = 2020, Title = "C#" },
                new Book() { Id = 2, AuthorId = 1, EditionYear = 2021, Title = "ASP.NET" },
                new Book() { Id = 3, AuthorId = 2, EditionYear = 2019, Title = "SQL" },
                new Book() { Id = 4, AuthorId = 2, EditionYear = 2018, Title = "Test" }
            );
            modelBuilder.Entity<Author>().ToTable("authors").HasData(
                new Author() { Id = 1, Name = "Jo Nesbo" },
                new Author() { Id = 2, Name = "Johny Depp" }
            );
            modelBuilder.Entity<BookCopy>().ToTable("bookcopier").HasData(
                new BookCopy() { Id = 1, BookId = 1, UniqueNumber = "yfdufaybdsabdk" },
                new BookCopy() { Id = 2, BookId = 1, UniqueNumber = "yfdufay" }
            );
        }
    }
}
