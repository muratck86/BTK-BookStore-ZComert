using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using System.Dynamic;

namespace Services.Contracts
{
    public interface IBookService
    {
        Task<(IEnumerable<ExpandoObject> books, MetaData metaData)> GetAllBooksAsync(BookParameters bookParameters,bool trackChanges=false);

        Task<BookDto> GetOneBookByIdAsync(int id, bool trackChanges=false);

        Task<BookDto> CreateOneBookAsync(BookCreateDto book);

        Task UpdateOneBookAsync(int id, BookUpdateDto bookDto, bool trackChanges = false);

        Task DeleteOneBookAsync(int id, bool trackChanges = false);

        Task<(BookUpdateDto bookUpdateDto, Book book)> GetOneBookForPatchAsync(int id, bool trackChanges);

    }
}
