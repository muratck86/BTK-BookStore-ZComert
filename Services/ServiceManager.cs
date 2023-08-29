using AutoMapper;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IBookService> _bookService;

        public ServiceManager(
            IRepositoryManager repositoryManager,
            IMapper mapper,
            IBookLinks bookLinks)
        {
            _bookService = new Lazy<IBookService>(() => 
            new BookManager(repositoryManager, mapper, bookLinks));
        }

        public IBookService BookService => _bookService.Value;
    }
}
