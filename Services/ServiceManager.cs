using AutoMapper;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IBookService> _bookService;
        private readonly Lazy<IAuthenticationService> _authenticationService;
        private readonly Lazy<ICategoryService> _categoryService;
        private readonly Lazy<IAuthorService> _authorService;

        public ServiceManager(
            IRepositoryManager repositoryManager,
            ILoggerService loggerService,
            IMapper mapper,
            IConfiguration configuration,
            IBookLinks bookLinks,
            IAuthorLinks authorLinks,
            UserManager<User> userManager)
        {
            _authenticationService = new Lazy<IAuthenticationService>(() =>
            new AuthenticationManager(loggerService, mapper, userManager, configuration));

            _bookService = new Lazy<IBookService>(() => new BookManager(repositoryManager, mapper, bookLinks));
            _categoryService = new Lazy<ICategoryService>(() => new CategoryManager(repositoryManager, mapper));
            _authorService = new Lazy<IAuthorService>(() => new AuthorManager(repositoryManager, mapper, authorLinks));
        }

        public IBookService BookService => _bookService.Value;

        public IAuthenticationService AuthenticationService => _authenticationService.Value;

        public IAuthorService AuthorService => _authorService.Value;

        public ICategoryService CategoryService => _categoryService.Value;
    }
}
