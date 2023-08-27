namespace Entities.Exceptions
{
    public sealed class BookNotFoundException : NotFoundExceptionBase
    {
        public BookNotFoundException(int id): base($"No such book with id: {id}")
        {
            
        }
    }
}
