namespace Entities.DataTransferObjects
{
    public record BookDetailsDto : BookDto
    {
        public String CategoryName { get; init; }
        public String AuthorName { get; init; }
    }
}
