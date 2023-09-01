using Entities.RequestFeatures;


namespace Entities.DataTransferObjects
{
    public record BookLinkParameters : LinkParameters
    {
        public BookParameters BookParameters { get; init; }

    }
}
