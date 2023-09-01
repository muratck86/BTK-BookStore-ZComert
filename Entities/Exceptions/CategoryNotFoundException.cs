namespace Entities.Exceptions
{
    public sealed class CategoryNotFoundException : NotFoundExceptionBase
    {
        public CategoryNotFoundException(int id) : base($"No such category with id: {id}")
        {
        }
    }
}
