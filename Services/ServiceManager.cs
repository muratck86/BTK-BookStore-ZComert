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

        public ServiceManager(
            IRepositoryManager repositoryManager,
            ILoggerService loggerService,
            IMapper mapper,
            IConfiguration configuration,
            IBookLinks bookLinks,
            UserManager<User> userManager
            )
        {
            _bookService = new Lazy<IBookService>(() => 
            new BookManager(repositoryManager, mapper, bookLinks));

            _authenticationService = new Lazy<IAuthenticationService>(() =>
            new AuthenticationManager(loggerService, mapper, userManager, configuration));
        }

        public IBookService BookService => _bookService.Value;

        public IAuthenticationService AuthenticationService => _authenticationService.Value;
    }
}
