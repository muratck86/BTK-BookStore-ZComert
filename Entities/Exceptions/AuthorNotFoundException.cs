namespace Entities.Exceptions
{
    public sealed class AuthorNotFoundException : NotFoundExceptionBase
    {
        public AuthorNotFoundException(int id) : base($"No such author with id: {id}")
        {
            
        }
    }
}
