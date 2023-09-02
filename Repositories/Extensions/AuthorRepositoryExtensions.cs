using Entities.Models;
using System.Linq.Dynamic.Core;

namespace Repositories.Extensions
{
    public static class AuthorRepositoryExtensions
    {
        public static IQueryable<Author> Search(this IQueryable<Author> authors, string searchText)
        {
            if (string.IsNullOrEmpty(searchText))
                return authors;

            var lowerCase = searchText.ToLower().Trim();
            return authors.Where(author => author.FirstName.ToLower().Contains(searchText)
            || author.LastName.ToLower().Contains(searchText));
        }

        public static IQueryable<Author> SortBy(this IQueryable<Author> authors, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return authors.OrderBy(author => author.FirstName);

            var orderQuery = OrderByQueryBuilder
                .CreateOrderQuery<Author>(orderByQueryString);

            if (orderQuery is null)
                return authors.OrderBy(a => a.FirstName);

            return authors.OrderBy(orderQuery);
        }
    }
}
