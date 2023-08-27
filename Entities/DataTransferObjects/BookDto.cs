namespace Entities.DataTransferObjects
{
    [Serializable]
    public record BookDto (int Id,  string Title, decimal Price) { }
}
