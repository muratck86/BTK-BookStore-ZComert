using Entities.DataTransferObjects;
using Entities.Models;

namespace Services.Contracts
{
    public interface IBookService
    {
        IEnumerable<BookDto> GetAllBooks(bool trackChanges=false);
        Book? GetOneBookById(int id, bool trackChanges=false);
        Book CreateOneBook(Book book);
        void UpdateOneBook(int id, BookUpdateDto bookDto, bool trackChanges = false);
        void DeleteOneBook(int id, bool trackChanges = false);

    }
}
