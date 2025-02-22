using ETL.Application.Interfaces;
using ETL.Domain.Entities;
using ETL.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ETL.Infrastructure.Services
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BookService> _logger;

        public BookService(ApplicationDbContext context, ILogger<BookService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task UpsertBooksAsync(IEnumerable<Book> books)
        {
            foreach (var book in books)
            {
                var existingBook = await _context.Books
                    .FirstOrDefaultAsync(b => b.Id == book.Id);

                if (existingBook == null)
                {
                    _logger.LogInformation($"Adding new book: {book.Title}");
                    _context.Books.Add(book);
                }
                else
                {
                    _logger.LogInformation($"Updating existing book: {book.Title}");
                    existingBook.Title = book.Title;
                    existingBook.Author = book.Author;
                    existingBook.PublishedDate = book.PublishedDate;
                }
            }

            await _context.SaveChangesAsync();
            _logger.LogInformation("Books upserted successfully.");
        }
    }
}
