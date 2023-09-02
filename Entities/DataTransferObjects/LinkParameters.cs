using Microsoft.AspNetCore.Http;


namespace Entities.DataTransferObjects
{
    public abstract record LinkParameters
    {
        public HttpContext HttpContext { get; init; }
    }
}
