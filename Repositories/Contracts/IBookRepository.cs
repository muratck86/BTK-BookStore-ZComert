using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IBookRepository : IRepositoryBase<Book>
    {
        void CreateOneBook(Book book);
        void DeleteOneBook(Book book);
        IQueryable<Book> GetAllBooks(bool trackChanges);
        IQueryable<Book> GetOneBookById(int id, bool trackChanges);
        void UpdateOneBook(Book book);
    }
}
