using Entities.DataTransferObjects;
using Entities.LinkModels;
using Entities.Models;
using Entities.RequestFeatures;

namespace Services.Contracts
{
    public interface IBookService
    {
        Task<(LinkResponse linkResponse, MetaData metaData)> GetAllBooksAsync(BookLinkParameters linkParameters,bool trackChanges=false);

        Task<List<BookDetailsDto>> GetAllBooksAsync(bool trackChanges);

        Task<BookDto> GetOneBookByIdAsync(int id, bool trackChanges=false);

        Task<BookDto> CreateOneBookAsync(BookCreateDto book);

        Task UpdateOneBookAsync(int id, BookUpdateDto bookDto, bool trackChanges = false);

        Task DeleteOneBookAsync(int id, bool trackChanges = false);

        Task<(BookUpdateDto bookUpdateDto, Book book)> GetOneBookForPatchAsync(int id, bool trackChanges);
    }
}
