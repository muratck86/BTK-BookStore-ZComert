namespace Services.Contracts
{
    public interface IServiceManager
    {
        IBookService BookService { get; }
        IAuthenticationService AuthenticationService { get; }
        IAuthorService AuthorService { get; }
        ICategoryService CategoryService { get; }
    }
}
