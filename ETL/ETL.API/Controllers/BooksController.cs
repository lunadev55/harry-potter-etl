using Hangfire;
using Microsoft.AspNetCore.Mvc;
using ETL.Jobs.Services;

namespace MyProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookUpsertJob _bookUpsertJob;

        public BooksController(BookUpsertJob bookUpsertJob)
        {
            _bookUpsertJob = bookUpsertJob;
        }

        [HttpPost("upsert")]
        public async Task<IActionResult> UpsertBooksAsync()
        {
            await _bookUpsertJob.RunAsync();
            return Ok("Book upsert job triggered successfully.");
        }
    }
}
