namespace Entities.DataTransferObjects
{
    public record TokenDto
    {
        public String AccessToken { get; init; }
        public String RefreshToken { get; set; }
    }
}
