using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.LinkModels;
using Entities.Models;
using Entities.RequestFeatures;
using Repositories.Contracts;

namespace Services.Contracts
{
    public class AuthorManager : IAuthorService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly IAuthorLinks _authorLinks;

        public AuthorManager(IRepositoryManager repositoryManager, IMapper mapper, IAuthorLinks authorLinks)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _authorLinks = authorLinks;
        }

        public async Task<(LinkResponse linkResponse, MetaData metaData)> GetAllAuthorsAsync(AuthorLinkParameters linkParameters, bool trackChanges = false)
        {
            var pagedAuthors = await _repositoryManager.Author.GetAllAuthorsAsync(linkParameters.AuthorParameters, trackChanges);
            var authorsDto = _mapper.Map<IEnumerable<AuthorDto>>(pagedAuthors);
            var linkResponse = _authorLinks.TryGenerateLinks(
                authorsDto, linkParameters.AuthorParameters.Fields, linkParameters.HttpContext);

            return (linkResponse, pagedAuthors.MetaData);
        }

        public async Task<AuthorDto> GetOneAuthorByIdAsync(int id, bool trackChanges = false)
        {
            var author = await GetOneAuthorHelperAsync(id, trackChanges);
            var authorDto = _mapper.Map<AuthorDto>(author);
            return authorDto;
        }

        public async Task<AuthorDto> CreateOneAuthorAsync(AuthorCreateDto authorCreateDto)
        {
            var author = _mapper.Map<Author>(authorCreateDto);
            _repositoryManager.Author.CreateOneAuthor(author);
            await _repositoryManager.SaveAsync();
            return _mapper.Map<AuthorDto>(author);
        }

        public async Task UpdateOneAuthorAsync(int id, AuthorUpdateDto authorUpdateDto, bool trackChanges = false)
        {
            var dbAuthor = await GetOneAuthorHelperAsync(id, trackChanges);
            _mapper.Map(authorUpdateDto,dbAuthor);

            _repositoryManager.Author.UpdateOneAuthor(dbAuthor);
            await _repositoryManager.SaveAsync();
        }

        public async Task DeleteOneAuthorAsync(int id, bool trackChanges = false)
        {
            var author = await GetOneAuthorHelperAsync(id,trackChanges);
            _repositoryManager.Author.DeleteOneAuthor(author);
            await _repositoryManager.SaveAsync();
        }

        private async Task<Author> GetOneAuthorHelperAsync(int id, bool trackChanges)
        {
            return await _repositoryManager.Author.GetOneAuthorByIdAsync(id, trackChanges)
                ?? throw new AuthorNotFoundException(id);
        }
    }
}
