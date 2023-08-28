using Entities.Models;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EfCore
{
    public static class BookRepositoryExtensions
    {
        public static IQueryable<Book> FilterByPrice(this IQueryable<Book> books, uint minPrice, uint maxPrice)
        {
            return books.Where(book => book.Price <= maxPrice && book.Price >= minPrice);
        }
    }
}
