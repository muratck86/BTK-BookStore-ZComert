namespace Entities.Exceptions
{
    public abstract class NotFoundExceptionBase : Exception
    {
        protected NotFoundExceptionBase(string message) : base(message) { }
    }
}
