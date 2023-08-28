using Entities.DataTransferObjects;
using Entities.Models;

namespace Services.Contracts
{
    public interface IBookService
    {
        IEnumerable<BookDto> GetAllBooks(bool trackChanges=false);

        BookDto GetOneBookById(int id, bool trackChanges=false);

        BookDto CreateOneBook(BookCreateDto book);

        void UpdateOneBook(int id, BookUpdateDto bookDto, bool trackChanges = false);

        void DeleteOneBook(int id, bool trackChanges = false);

        (BookUpdateDto bookUpdateDto, Book book) GetOneBookForPatch(int id, bool trackChanges);

    }
}
