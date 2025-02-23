using Hangfire;
using Microsoft.Extensions.Logging;
using ETL.Application.Interfaces;
using ETL.Domain.Entities;
using Newtonsoft.Json;

namespace ETL.Jobs.Services;
public class BookUpsertJob
{
    private readonly IBookService _bookService;
    private readonly ILogger<BookUpsertJob> _logger;
    //private const string ApiUrl = "https://hp-api.onrender.com/api/books";
    private const string ApiUrl = "https://potterapi-fedeperin.vercel.app/en/books";

    public BookUpsertJob(IBookService bookService, ILogger<BookUpsertJob> logger)
    {
        _bookService = bookService;
        _logger = logger;
    }

    //[AutomaticRetry(Attempts = 3)]
    //public async Task RunAsync()
    //{
    //    _logger.LogInformation("Starting Book Upsert Job...");
    //    await _bookService.UpsertBooksAsync();
    //    _logger.LogInformation("Book Upsert Job Completed.");
    //}
    [AutomaticRetry(Attempts = 3)]
    public async Task RunAsync()
    {
        _logger.LogInformation("Starting Book Upsert Job...");

        try
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(ApiUrl);
            var books = JsonConvert.DeserializeObject<List<Book>>(response);

            if (books != null && books.Any())
            {
                await _bookService.UpsertBooksAsync(books);
                _logger.LogInformation($"Successfully upserted {books.Count} books.");
            }
            else
            {
                _logger.LogWarning("No books found in the API response.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to complete Book Upsert Job.");
        }
    }
}