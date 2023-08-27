namespace Entities.DataTransferObjects
{
    public record BookUpdateDto
    {
        public int Id { get; init; }
        public string Title { get; init; }
        public decimal Price { get; init; }
    }
}
