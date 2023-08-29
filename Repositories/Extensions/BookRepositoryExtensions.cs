using Entities.Models;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Repositories.Extensions
{
    public static class BookRepositoryExtensions
    {
        public static IQueryable<Book> FilterByPrice(this IQueryable<Book> books, uint minPrice, uint maxPrice)
        {
            return books.Where(book => book.Price <= maxPrice && book.Price >= minPrice);
        }

        public static IQueryable<Book> Search(this IQueryable<Book> books, string searchText)
        {
            if (string.IsNullOrEmpty(searchText))
                return books;

            var lowerCase = searchText.ToLower().Trim();
            return books.Where(book => book.Title.ToLower().Contains(searchText));
        }
    
        public static IQueryable<Book> SortBy(this IQueryable<Book> books, string orderByQueryString)
        {
            if(string.IsNullOrWhiteSpace(orderByQueryString))
                return books.OrderBy(book => book.Id);

            var orderQuery = OrderByQueryBuilder
                .CreateOrderQuery<Book>(orderByQueryString);
            if(orderQuery is null)
                return books.OrderBy(b => b.Id);
            return books.OrderBy(orderQuery);
        }
    }
}
