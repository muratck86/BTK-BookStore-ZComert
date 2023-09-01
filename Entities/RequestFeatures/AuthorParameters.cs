namespace Entities.RequestFeatures
{
    public class AuthorParameters : RequestParameters
    {
        public String? SearchTerm { get; set; }

        public AuthorParameters()
        {
            OrderBy = "LastName";
        }
    }

}
