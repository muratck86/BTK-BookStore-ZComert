using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IBookService
    {
        IEnumerable<Book> GetAllBooks(bool trackChanges=false);
        Book? GetOneBookById(int id, bool trackChanges=false);
        Book CreateOneBook(Book book);
        void UpdateOneBook(int id, Book book, bool trackChanges = false);
        void DeleteOneBook(int id, bool trackChanges = false);

    }
}
