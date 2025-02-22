using ETL.Domain.Entities;

namespace ETL.Application.Interfaces
{
    public interface IBookService
    {
        Task UpsertBooksAsync(IEnumerable<Book> books);
    }
}
