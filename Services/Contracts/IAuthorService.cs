using Entities.DataTransferObjects;
using Entities.LinkModels;
using Entities.RequestFeatures;

namespace Services.Contracts
{
    public interface IAuthorService
    {
        Task<(LinkResponse linkResponse, MetaData metaData)> GetAllAuthorsAsync(AuthorLinkParameters linkParameters, bool trackChanges=false);
        Task<AuthorDto> GetOneAuthorByIdAsync(int id, bool trackChanges=false);
        Task<AuthorDto> CreateOneAuthorAsync(AuthorCreateDto authorCreateDto);
        Task UpdateOneAuthorAsync(int id, AuthorUpdateDto authorUpdateDto, bool trackChanges = false);
        Task DeleteOneAuthorAsync(int id, bool trackChanges = false);

    }
}
