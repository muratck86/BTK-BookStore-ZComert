using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EfCore
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        private readonly Lazy<IBookRepository> _bookRepository;
        private readonly Lazy<ICategoryRepository> _categoryRepository;
        private readonly Lazy<IAuthorRepository> _authorRepository;
        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
            _bookRepository = new Lazy<IBookRepository>(() => new BookRepository(_context));
            _categoryRepository = new Lazy<ICategoryRepository>(() => new CategoryRepository(_context));
            _authorRepository = new Lazy<IAuthorRepository>(() => new AuthorRepository(_context));
        }

        public IBookRepository Book => _bookRepository.Value;
        public ICategoryRepository Category => _categoryRepository.Value;
        public IAuthorRepository Author => _authorRepository.Value;

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
