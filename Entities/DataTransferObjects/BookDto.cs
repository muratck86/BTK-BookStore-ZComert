namespace Entities.DataTransferObjects
{
    public record BookDto
    {
        public int Id { get; init; }
        public string Title { get; init; }
        public decimal Price { get; init; }
        public int PageCount { get; init; }
        public int Edition { get; init; }
        public int ReleaseYear { get; init; }
    }
}
