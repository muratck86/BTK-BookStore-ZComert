namespace Entities.DataTransferObjects
{
    public record CategoryDto
    {
        public int Id { get; init; }
        public String? Name { get; set; }
    }
}
