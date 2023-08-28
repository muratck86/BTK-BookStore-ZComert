namespace Entities.Exceptions
{
    public class InvalidIdBadRequestException : BadRequestException 
    {
        public InvalidIdBadRequestException(string message) : base(message) { }

    }
}
