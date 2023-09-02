using Entities.DataTransferObjects;
using Entities.LinkModels;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;
using Services.Contracts;

namespace Services
{
    public class AuthorLinks : IAuthorLinks
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly IDataShaper<AuthorDto> _dataShaper;

        public AuthorLinks(LinkGenerator linkGenerator, IDataShaper<AuthorDto> dataShaper)
        {
            _linkGenerator = linkGenerator;
            _dataShaper = dataShaper;
        }

        public LinkResponse TryGenerateLinks(
            IEnumerable<AuthorDto> authorsDto,
            string fields,
            HttpContext httpContext)
        {
            var shapedAuthors = ShapeData(authorsDto, fields);

            if(ShouldGenerateLinks(httpContext))
            {
                return ReturnLinkedAuthors(authorsDto,fields,httpContext,shapedAuthors);
            }
            return ReturnShapedAuthors(shapedAuthors);
        }



        private List<Entity> ShapeData(IEnumerable<AuthorDto> authorsDto, string fields)
        {
            return _dataShaper.ShapeData(authorsDto, fields).Select(a => a.Entity).ToList();
        }

        private bool ShouldGenerateLinks(HttpContext httpContext)
        {
            var mediaType = (MediaTypeHeaderValue)httpContext.Items["AcceptHeaderMediaType"];
            return mediaType.SubTypeWithoutSuffix
                .EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
        }
        private LinkResponse ReturnLinkedAuthors(IEnumerable<AuthorDto> authorsDto, string fields, HttpContext httpContext, List<Entity> shapedAuthors)
        {
            var authorDtoList = authorsDto.ToList();
            for(int i = 0; i < authorDtoList.Count; i++)
            {
                var authorLinks = CreateForAuthor(httpContext, authorDtoList[i], fields);
                shapedAuthors[i].Add("Links", authorLinks);
            }

            var authorCollection = new LinkCollectionWrapper<Entity>(shapedAuthors);
            CreateForAuthors(httpContext, authorCollection);
            return new LinkResponse { HasLinks = true, LinkedEntities = authorCollection };
        }

        private LinkResponse ReturnShapedAuthors(List<Entity> shapedAuthors)
        {
            return new LinkResponse
            {
                HasLinks = false,
                ShapedEntities = shapedAuthors
            };
        }

        private string GetControllerName(HttpContext httpContext)
        {
            return httpContext
                .GetRouteData()
                .Values["controller"]
                .ToString().ToLower();
        }

        private void CreateForAuthors(HttpContext httpContext, LinkCollectionWrapper<Entity> authorCollectionWrapper)
        {
            var controllerName = GetControllerName(httpContext);
            authorCollectionWrapper.Links.Add(new Link 
            {
                Href = $"/api/{controllerName}/",
                Rel = "self",
                Method = "GET"
            });
        }

        private List<Link> CreateForAuthor(HttpContext httpContext, AuthorDto authorDto, string fields)
        {
            var controllerName = GetControllerName(httpContext);
            var href = $"/api/{controllerName}/";
            var links = new List<Link>()
            {
                new Link {Href = href + $"{authorDto.Id}", Rel = "self", Method = "GET" },
                new Link {Href = href, Rel = "create", Method = "POST" },
                new Link {Href = href + $"{authorDto.Id}", Rel = "update", Method = "PUT" },
                new Link {Href = href + $"{authorDto.Id}", Rel = "delete", Method = "DELETE" }
            };
            return links;
        }
    }
}
