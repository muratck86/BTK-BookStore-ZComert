using Entities.DataTransferObjects;
using Entities.LinkModels;
using Microsoft.AspNetCore.Http;

namespace Services.Contracts
{
    public interface IAuthorLinks
    {
        LinkResponse TryGenerateLinks(IEnumerable<AuthorDto> authorsDto, string fields, HttpContext httpContext);
    }
}
