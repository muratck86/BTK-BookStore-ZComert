using Entities.RequestFeatures;


namespace Entities.DataTransferObjects
{
    public record AuthorLinkParameters : LinkParameters
    {
        public AuthorParameters AuthorParameters { get; init; }
    }
}
