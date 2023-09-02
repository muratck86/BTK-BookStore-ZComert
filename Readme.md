# 1. A Brief Oveview of a RESTful API
## 1.1.  Creating the first project
- Create BookStore Folder and a BookStore solution in it.
- Create BookStore/BookStoreApi Folder and BookStoreApi "webapi" (NET6.0) project and add it to the solution.
- Delete example model and controller.
- Add a Book model under Models (create) folder.
- Add a BooksController (Empty WebApi Controller) under Controllers folder.
### 1.1.1. Adding In-Memory Mock Data
- Add a Data folder
- Add a static ApplicationContext class into Data folder
- Add Crud methods to controller.
- **For Patch verb,** JsonPatchDocument will be used,
	- for this, these two packages must be installed:
		- Microsoft.AspNetCore.Mvc.NewtonsoftJson (6.0.10)
		- Microsoft.AspNetCore.JsonPatch (6.0.10
	- after installing packages, NewtonsoftJson must be added to the services in the Program.cs:
		change this line,
		```C#
		builder.Services.AddControllers();
		```
		to this:
		```C#
		builder.Services.AddControllers()
			.AddNewtonsoftJson();
		```
	- How to send a PATCH request? Below is the body templete, notice that it is a list:
	```Json
		[
		  {
			"operationType": 0,
			"path": "string",
			"op": "string",
			"from": "string",
			"value": "string"
		  }
		]
	```
	- Example operation1:
	```Json
		[
		  {
			"path": "price",
			"op": "replace",
			"value": 150
		  }
		]
	```
	- Example operation2:
	```Json
		[
		  {
			"path": "price",
			"op": "replace",
			"value": 150
		  },
		  {
			"path": "title",
			"op": "replace",
			"value": "New Book Title"
		  }
		]
	```
# 2. Using Postman
- Create workspace under Workspaces tab.
	- Define a Name and description
	- Select personal
- Under the workspace select collections, then click on "+" button to create a collection. Name it to "Books"
- In the collection click on New Request, and create a new request for each of the operation in the controller with related http request verb. Don't forget to save each request.
## 2.1. Global and Collection variables
- We can use variables in order not to repeat urls.
- Select https://localhost:PORT in any request url field. Click on set as variable, define a name (baseUrl) and select global. This part of the url in the field will change to: {{baseUrl}}, change all of the other requests to this. (eg: {{baseUrl}}/api/books/2)
- Collection variables can be defined likewise, collection variables are valid only under the collection that is defined for.
- Variables can be used in the bodies of the requests too.
## 2.2. Tests using postman
- Test functionality of postman can be used for requests,
	- Under the Tests tab of a selected request, test functions like below can be described:
	```js
	pm.test("Status code 200", function(){
    	pm.response.to.have.status(200)
    })
	```
## 2.3. Random Functions
- Mock data may be produced using postman and selected requests can be sent multiple times using iterations.
	- To send a request 100 times, 
	- Click on ... of the collection name or right click on the collection name (Books),
	- Select run collection
	- Select Requests to repeat.
	- Enter number of iterations,
	- Optionally enter delay value between each request.
	- Run.

- An example request body for creating mock data using post:
	```Json
	{
	  "id": {{$randomInt}},
	  "title": "{{$randomWords}}",
	  "price": {{$randomPrice}}
	}
	```
	
# 3. A New Api project and EntityFramework Core
- Add a new ASP.NET Core Web API project using VS or terminal. Specifications:
	- NET6.0
	- No Authentication
	- Configure for Https
	- Use Controllers
	- Enable OpenAPI support
- Delete WeatherForecast model and controller.
- In the Solution Explorer, set this project as start up project.

## 3.1. EntitiyFramework Core
- Add package Microsoft.EntityFrameworkCore, either use nuget package manager or use Package Manager Console (as follows):
```
	Install-Package Microsoft.EntityFrameworkCore -Version 6.0.10 -ProjectName WebApi
```
- Add "ConnectionStrings" to appsettings.json file.
- Add Microsoft.EntityFrameworkCore.SqlServer package to the project by nuget or PMC:
```
	Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 6.0.10 -ProjectName WebApi
```
## 3.2. Create Migations
- Add Microsoft.EntityFrameworkCore.Tools Package to manage migrations. This package is needed for command sets related to migrations and db operations. In the PMC:
```
	Install-Package Microsoft.EntityFrameworkCore.Tools -Version 6.0.10 -ProjectName WebApi
```
- Another package needed for code-first approach is the design package, This package is needed for the app. In the PMC: 
```
	Install-Package Microsoft.EntityFrameworkCore.Design -Version 6.0.10 -ProjectName WebApi
```
- Create migration and database in the PMC (WebApi must be selected as default project):
```
	Add-Migration Init
	Update-Database
```
- At this point there was a punctuation error in the connection string in the appsettings.json file. It has to corrected in order to Update Database succeeds.

- Add "SeedData" migration and update db after creating BookConfig and overriding the OnModelCreating methot.

## 3.3. Data manipulation
- Create Controller Methods.
- To Use postman for testing, the request collection of the workspace that we created can be used by changing the port number of the beseUrl variable.
- For PATCH verb and its method, install NewtonsoftJson and JsonPatch packages and add services record in the Program.cs
# 4. Layered Architecture
## 4.1. Entities Layer
- Add a Class Library project named Entitites
- Move Book class under the WebApi/Models folder to Entities/Models And Delete the WebApi/Models folder.
- Add reference to Entities into WebApi project and don't forget to edit namespaces while moving Book.class and resolvings.
## 4.2. Repositories Layer
- Add a Class Library project named Repositories
- Add a Contracts folder under this project and add a generic interface named IRepositoryBase in it.
- Add CRUD method signatures.
- Add EfCore folder and move RepositoryContext from WebApi into it. 
- Remove EntitiyFrameworkCore package from WebApi and add the package to Repositories project to resolve usings.
- Add reference to Entities from this project.
- Add reference to this project from WebApi project.
- Move Config folder and the BookConfig class to Repositories/EfCore Project.
- Delete Migrations folder in the WebApi project.
- Resolve all usings.
- Create an abstract RespositoryBase generic class that implements IRepositoryBase under EfCore folder.
- Inject (protected) RepositoryContext into it to implement methods.
- Create IBookRepository extends IRepositoryBase under Contracts.
- Create BookRepository implements IBookRepository and extends BookRepositoryBase
- Create IRepositoryManager under Contracts
- Create RepositoryManager implements IRepositoryManager under EfCore
### 4.2.1. Lazy Loading
- Refactor RepositoryManager
### 4.2.2. Service Extensions
- Create Extensions folder under WebApi project.
- Create ServicesExtensions under Extensions folder.
### 4.2.3. Integration of Repository Managers to Controller
- Inject and edit BookController to use RepositoryManager instead of Context.
- Add a method to ServicesExtension class for IoC
## 4.3. Services Layer
- Add a class library named Services
- Add a folder named Contracts
- Add an interface named IBookService
- Add a class named BookService implements IBookService
- Add an interface IServiceManager
- Add a class ServiceManager implements IServiceManager
- Add reference to Entities project
- Add a ConfigureServiceManger method to ServicesExtensions
- Add this to Program.cs
- Inject ServiceManager to BooksController
- Edit BooksController Methods accordingly
## 4.4. Presentation Layer
- Add a class library project named Presentation under solution
- Rename the existing Class1 class to AssemblyReference
- Add Controllers folder
- Move BooksController in the WebApi project into this folder and delete them from WebApi
- Add package Microsoft.AspNetCore.Mvc.Core 2.2.5 to this project
- Add previously installed jsonPatch package to this project, remove from WebApi
- Add reference to Entities and Services projects
- Add this project to WebApis references
- In the Program.cs change this line:
	```C#
	builder.Services.AddControllers()
    .AddNewtonsoftJson();
	```
	to this:
	```C#
	builder.Services.AddControllers()
    .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly)
    .AddNewtonsoftJson();
	```
## 4.5. Repository Context Factory
- Drop database
- Since we removed Ef from WebApi to a separate project, we can not create a migration in WebApi (although we can create in the Repositories, which we don't want to.)
- Create a ContextFactory folder under WebApi
- Create a RepositoryContextFactory class in it.
- Make ConfigurationBuilder in this factory
- Make DbContextOptionsBuilder in this factory
- Create Init migration and update database

# 5 Logging with NLog
- Add ILoggerService interface into Services/Contracts
- Install NLog.Extensions.Logging package to Service project 
- Add LoggerManager class implements ILoggerService
- Add nlog.config xml file to the project
- Add record to Program.cs
	```C#
	LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(),"/nlog.config"));

	```
- Add an Extension Method to ServicesExtensions for IoC
- Add Configuration for IoC to Program.cs
## 5.1. Using Logger in the project
- Add logging lines wherever needed.

# 6. Global Exception Handling
## 6.1. Modeling Errors and Error Details
- Add an ErrorModel folder to Entities project
- Add a class ErrorDetails in this folder
## 6.2. Using Exception Handler
- Add an ExceptionMiddlewareExtensions class into Extensions folder in the WebApi project
- Set all Exception Status codes to 500 for now.
- Configure Program.cs for the ConfigureExceptionHandler
- After these steps, the app may be tested. The Exceptions thrown in te app will be serialized as ErrorDetails and the StatusCode will be set to 500: Internal Server Error
- Remove try-catch blocks
## 6.3. Custom Exceptions
- Add Exceptions folder under Entities
- Add a NotFound abstract class extends Exception into it.
- Add a BookNotFound sealed class extends NotFound
- Return to ExceptionMiddlewareExtensions class in the Extensions folder of the main project and edit it according to Exception type.
- Refactor Related methods that throw errors or return not found responses to throw BookNotFoundException.
- Optional: Add a new custom Exception; BadRequestException
# 7. AutoMapper Implementation
- Install automapper.Extensions.Microsoft.DependencyInjection versin 12.0.0 to services project.
- In the Program.cs add record to services
- Add DataTransferObjects folder to Entities project
- Add BookUpdateDto "record" into this folder. Dtos are not intended for manipulation hence, they are defined as records. Records are much like classes, they are also reference types. We don't define set blocks for properties in the record, instead we define init blocks. So record types become immutable types.
- Add Utilities/AutoMapper folders under WebApi project.
- Add MappingProfile extends Profile class into AutoMapper folder.
- Add mappings into constructor using CreateMap method.
- Refactor BooksController and Service layer to use BookUpdateDto
# 8. Content Negotiation
- Support or don't support various formats for Requests from Clients
- Consider formats like Json, Xml and accept or don't accept, and response accordingly.
- Configure Accept header
- Currently the application is closed to content negotiation, meaning that, no matter what format the client requests (application/json, text/csv, text/xml, application/xml etc.), the app returns application/json format.
- In the Program.cs Configure controllers,
	- add RespectBrowserAcceptHeader = true to enable content negotation.
	- add ReturnHttpNotAcceptable = true to send feedback that the format is not accepted with response 406 code.
	- To send responses in xml format, add AddXmlDataContractSerializerFormatters
	- Now the Api is able to respond either in Json or xml formats.
## 8.1. Serializability
- For now, the Api cannot return list of record objects which are defined by contstructor (no props defined explicitly), in xml format, because of not being able to serialize.
- To solve this add Serializable attribute. Bu this time, the result in xml requests will be quiet messy.
- The absolute solution is define the classess or records with default constructors and with properties.
## 8.2. Custom Formatter
- Add Formatters folder under Utilities folder of WebApi
- Add CsvOutputFormatter : TextOutputFormatter class in it to support csv response formats
- Resolve MediaTypeHeaderValue using Microsoft (not System)
- We can support only List or individual contents.
- We can select which properties to add to response.
- Add IMvcBuilderExtensions to Extensions folder.
- Configure Program.cs
- Notice that this will only work for BookDto type, so for GetOneBook request to work, we need to refactor that controller method to return BookDto.

# 9. Validation with Annotation
- Add a new abstract record model, BookManipulationDto, into DataTransferObjects folder in Entities.
- Add validation annotations to its properties.
- Extend BookUpdateDto to BookManipulationDto
- Add BookCreateDto:BookManipulationDto
- Edit Service Layer,
	- Edit IBookService
	- Edit BookManager
- Edit BooksController in Presentation Layer
- To Suppress default 404 Response status code for failed validations (ModelState.Invalid condition) Configure the Program.cs
- To return UnprocessableEntity response (Coce 422), refactor the BooksController
- The PatchOneBook method needs extra steps.
	- Remove NewtonsoftJson package from WebApi and install it to Presentation project.
	- Add ModelState arg to ApplyTo method in the PatchOneBook method body.
	- Add a method signature to IBookService and implement it in the BookManager
	- Refactor PatchOneBook method in the controller.
# 10. Async Programming
- APM: Asyncronous Programming Model
- EAP: Event-based Asyncronous Programming
- TAP: Task-based Asyncronous Programming
## 10.1. Task-based Asyncronous Programming (TAP)
Syncroned tasks procssed in the pipeline in the same Thread. Each Sync request is assigned to a Thread in the Thread pool. If there is no available Thread in the pool, then the sync request has to wait for a previous request to complete, and the assigned thread to return to the pool.  
The difference of the async request is, async request doesn't has to wait for the previous process to complete. Every step in the pipeline is processed by a thread.
- In he Repositories layer, refactor IBookRepository and BookRepository, refactor Get methods, wrap the retuned types into Task
- In the IRepositoryManager and RepositoryManager, refactor Save method.
- Rafactor the Methods the same way in the Services layer.
- Refactor the Methods in the Presentation layer.
# 11. Action Filters
## 11.1. ActionFilter
- Add ActionFilters folder into Presentation layer
- Create a class ValidationFilterAttribute : ActionFilterAttribute
- override needed Methods of the base class
- Add ValidationFilter attribute on Create and Update methods of the controller
- Remove BadRequest and UnprocessibleEntity blocks.
- Add IoC record to Program.cs
## 11.2. LogFilter
- Add LogDetails model into Entities/LogModel folder.
- Add LogFilterAttribute extends ActionFilterAttribute into Presentation/ActionFilters
- Add a method for Filter Attributes into ServicesExtensions in te main project.
- Call this method from Program.cs to add them to the services.
- Add attribute to BooksController to log all of the actions.
# 12. Pagination and Cors
## 12.1. Basic Pagination
- Add RequestFeatures folder into Entities project,
- Add RequestParameters abstract class in this folder
- Add BookParameters extends RequestParameters
- In the IBooksRepository refactor GetAll method signatures to  use BookParameters
- Refactor the implementation of IBookRepository
- Refactor the methods all the way up to Presentation layer.
## 12.2. Meta data & paged list
- Add MetaData and PagedList classes into RequestFeatures in the Entities project
- Refactor all layers' GetAll methods.
## 12.3. Cors (Cross Origin resource sharing) Configuration
- Add ConfigureCors method into ServicesExtensions
- Add ConfigureCors call to Program.cs
# 14. Filtering
- In the Entities project add properties to BookParameters class
- In the Exceptions folder, make BadRequestException abstract and create two sub-classes of this class. Refactor the ExceptionMiddlewareExtensions class in the WebApi/Extensions folder accordingly.
- In the Repositories project add BookRepositoriesExtensions class
- In the BookRepository refactor the method using the Extension method.

# 15. Searching
- Add a SearchTerm property into BookParameters.
- Refactor BookRepository to add "Search" method in the GetAll method.
- Create a folder named Extensions in the Repositories project
- Move BookRepositoryExtensions class into the folder
- Add a "Search" method to the BookRepositoryExtensions class.

# 16. Sorting
- Add OrderBy property into RequestParameters int the Entities project
- Add a constructor into BookParameters class to set a default OrderBy = "Id"
- Add a method named SortBy into BookRepositoryExtensions in the Repositories project
- Wrie the method that handles the queries like ..books?orderby=title,price
- Remember to resolve "BindingFlags" in the method using Refrection, not System
- install System.Linq.Dynamic.Core version 1.2.23
- If OrderBy doesn't resolve automatically after installing this package, manually add using System.Linq.Dynamic.Core
- Use this method to refactor the BookRepository.
- In the Repositories project, add OrderQueryBuilder class into Extensions folder.
- Use this Builders method to refactor the BookRepositoryExtensions class.

# 17. Data Shaping
Data shaping is not an essential feature that all apis need. By this feature we can let the client to choose which fields of the resources they want.
- To enable this feature for all resources, add Fields property to Request parameters, or otherwise to enable for specifice resources, add this property to that parameters of that resource. (eg: for only Book, ad this prop to BookParameters class)
- Add an interface into Contracts of Services layer
- Implement this interface in the Services layer.
	- Get the requested property names in the query
	- Get the public and instance properties of the resource type by reflection
	- Compare them to determine which of the properties requested and create a collection of them.
	- Fetch the values of the requested properties.
	- Create and return an ExpandoOblect from these properties and their values
	-If a collection is requested, do this for each object and create a collection of ExpandoObjects to return.
	- ExpandoObject is a dynamic object type to create in the runtime.
- Create an extension method for IoC in the ServicesExtensions class in the WebApi/Extensions
- Call this method in the Program.cs
In the IBookService, change the signature of IBookService to return ExpandoObject
- Change the implementation too, inject shaper into the class and refactor the GetAll method.
- Modify the ServiceManager since the contstructor now needs another parameter.
# 18. Hateoas (Hypermedia as the Engine of Application State)
To have Hypermedia support,
- Entities project, add LinkModels folder, into folder add Link class
- Create LinkResourceBase and LinkCollectionWrapper classes in the same folder
- Create the Entity class in the Models folder
- Create the ShapedEntity in the same folder.
- In the services layed, refactor IDataShaper to use Shaped entity instead of ExpanoObject, and refactor the implementation.
- Add LinkResponse class into LinkModels folder in the Entities
- In the ServicesExtensions add the AddCustomMediaTypes method and call this in the Program.cs
- Add ValidateMediaTypeAttribute class into the ActionFilters folder in te Presentation layer. Resolve MediaTypeHeaderValue in the if statement using Microsoft.
- Add this ValidateMediaTypeAttribute into ConfigureActionFilters call in the ServicesExtensions.cs
- Add this attribute onto GetAllBooksAsync method in the BooksController of the Presentation layer.
- install Microsoft.AspNetCore.Mvc.Abstractions version 2.2.0 package into Entities project.
- In the service layer, add IBookLinks interface into the contracts. Resolve HttpContext using Microsoft.AspNetCore.Http.
- Then implement the BookLinks class, CreateForBook private method will be implemented later.
- Create a LinkParameters record type into DataTransferObjects folder in  the Entities.
- Add IoC record of BookLinks into Program.cs
- In the service layer:
	- Change BookManager private fields and constructor.
	- Change GetAll method
	- Change IBookManager interface
	- Change ServiceManager constructor
- In the Presentation layer:
	- Change BooksController GetAll method
- Correct the ValidateMediaTypeAttribute and Program.cs, Add Mock links into CreateForBook method of the BookLinks class.
- Implement CreateForBook method in the BookLinks class
- Crete a new private method named CreateForBooks and refactor the ReturnLinkedBooks with this method.

# 19. Http OPTIONS and HEAD Requests
## 19.1. OPTIONS verb
- In the Presentation layer
	- Add a new method named GetBooksOptions with HttpOptions attribute.
	- Options request informs the Client Which Http verbs are allowed.
## 19.2. HEAD verb
- Head verb is about the Headers of a request and response. No Body.
- It has the same features with GET verb.
- No need for a method for HEAD. Add HttpHead attribute on GetAllBooks Method onto HttpGet attribute.
- Will return only headers

# 20. Root Documentation
- Add a RootController into Presentation layer
- Create the GetRoot method.
- Add mediaType support lines into ServicesExtensions class in the WebApi/Extensions folder.

# 21. Versioning
- Install Microsoft.AspNetCore.Mvc.Versioning (5.0.0) into presentation layer.
- In the ServiesExtensions add a Versioning config method. Add this to Program.cs
## 21.1. Books V2 - With params
- Add BooksV2Controller into controllers
- Add Version 1.0 attribute to BooksController and 2.0 to BooksV2Controller
- Create a GetAllBooksAsync method
- Add method to the IBookService and implement it in the BookService
- Cascade the method to Repositories layer.
## 21.2. Books V2 - With URL
- Add {v:apiversion}/ to Route attribute.
## 21.3. Books V2 - With Header
- Remove {v:apiversion}/ from Route attributes make it as before.
- In the ServicesExtensions add the ApiVersionReader line into AddApiVersioning method.
## 21.4. Deprecating Versions
- Add Deprecated = true to ApiVersion Attribute.
## 21.5. Convensions 
- In the ServicesExtensions, add Convensions lines
- Remove ApiVersion Attributes...

# 22. Caching
- Add ResponseCache attribute on GetAll method of the BooksController. By doing this, a new header will be added to the response, meaning the response is cachable.
- In the ServicesExtensions add a ConfigureResponseCaching method, and call this in the Program.cs
- Add app.UseResponseCaching() under the Cors config line in the Program.cs
- Test in the postman, (make sure in the settings of the postman, send no caching header option is off), in the firs response, we see a header "Cache-Control" showing duration of cache. If we send another request, we'll se an additional header "Age".
- We can create Cache Profiles for various resources.
	- In the Program.cs, in the AddControllers method, add cache profiles config.
	- Add ResponseCache attribute with CacheProfile parameter.
	- We'll see 300 seconds duration cache in the responses except the getall method, since it has its own ResponseCache attribute on it.
## 22.1. Caching with Marvin
- Install Marvin.Cache.Headers (6.0.0) package into Presentation layer.
- Add a ConfigureHttpCacheHeaders method into ServicesExtensions
- Call this from Program.cs and add UseHttpCacheHeaders() line (make sure it is) under the Cors line.
- Test the application, There will be 3 more headers in the response; ETag, Expires, Last-Modified.
- The ResponseCache attributes now can be removed.
- The default config is public cache, duration 60 seconds. We can change these in the config of Services.Extensions file or/and We can use HttpCacheExpiration attribute with parameters.

# 23. Rate Limiting
We can limit rate of requests. We'll respond with status code 429 Too many requests.
- Install AspNetCoreRateLimit -Version 4.0.1 package to WebApi project.
- In the Program.cs add AddMemoryCache to Services.
- Add a configuration method into ServicesExtensions
- Add a call to this method into Program.cs
- Add a UseIpRateLimiting call into Program.cs before Cors.
- In the testing, there will be new parameters in the response headers;
X-Rate-Limit-Limit, X-Rate-Limit-Remaining, X-Rate-Limit-Reset.
- The api will return Too Many Requests status code 429 when the number of requests exceeds the rate in the defined period.

# 24. Authentication and Authorization
Authentication ~ Login, Authorization ~ Permits  
Identity framework with JSON Web Token (JWT) will be used in this subject.
## 24.1. Identity
- Install Microsoft.AspNetCore.Identity.EntityFrameworkCore (6.0.0) package into Identity project.
- In the Entities project add User : IdentityUser class into Models folder.
- In the Repositories project, change RepositoryContext : DbContext to inherit from IdentityDbContext<User> and refactor OnModelCreating method.
- In the WebApi project, add a new config method for Identity
- In the Program.cs
	- AddAuthentication
	- ConfigureIdentity
	- UseAuthentication before UseAuthorization
- In the PM add a migration. Make sure default (target) project is WebApi
- Use Update-Database command to create tables.
## 24.2. Defining Roles
In the Repositories project
- Add RoleConfiguration class into EfCore.Config folder, add roles into it.
- In the RepositoryContext, add RoleConfiguration or use Assembly to get all type configs.
- Add migration and update database
## 24.3. User
- Add UserForRegistrationDto into Entities project DataTransferObjects folder
- Add mapping for this class into MappingProfile in the Utilities folder of WebApi
- In the Services layer, 
	- add an interface IAuthenticationService into contracts. Add AuthenticationManager class.
	- Add IAuthenticationService to IServiceManager
	- Add AuthenticationManager to ServiceManager
- In the Presentation layer,
	- Add a new controller AuthenticationController and the RegisterUser method.
## 24.5. JSON Web Token (JWT)
- In the WebApi project, add JwtSettings into appsettings.json
- Install Microsoft.AspNetCore.Authentication.JwtBearer (6.0.0) packet to WebApi project.
- Add a config method into ServicesExtensions and call in the Program.cs

## 24.6. Securing Endpoints
- Add Authorize attribute onto GetAll method to secure the method.

## 24.7. Authentication & JWT
- In the entities project Add UserForAuthenticationDto record type into the Dtos folder.
- To validate user, go to the Services Layer,
	- Add ValidateUser method signature into IAuthenticationService
	- Implement the method in the AuthenticationManager
	- Add CreateToken signature into IAuthenticationService
	- Install System.IdentityModel.Tokens.Jwt (6.14.1) package
	- Implement CreateToken method in the AuthenticationManager
- In the Presentation layer,
	- Add an Authenticate (login) method with HttpPost attribute
	- Add roles authentications to BooksController methods.
## 24.8. Refresh Token
- In the Entities project, add properties RefreshToken and RefreshTokenExpiryTime to User.
- Add a migration and update database
- Add a Dto named TokenDto
- In the Services layer, refactor CreateToken method of the IAuthenticationService interface and its implementation to return TokenDto instead of string.
- In the presentation layer refactor the Authenticate method of the AuthenticationController to return TokenDto.
- In the Services layer, add a Method named RefreshToken to IAuthenticationService and implement it in the AuthenticationManager.
	- While implementing the method, create a  RefreshTokenBadRequestException in the Entities/Exceptions.
- In the presentation layer, Create a new post method named Refresh into AuthenticationController.
# 25. Documentation
## 25.1. Configuring Swagger
- In the WebApi project add ConfigureSwagger method into ServicesExtensions.
- Change the records in the Program.cs (app.UseSwaggerUI, ConfigureSwagger)
- Go to Controllers in the presentation layer. To define of which version a controller is, add the ApiExplorerSettings Attribute.

# 26. Expanding Resources and Features
## 26.1. Adding Categories and Authors, Expanding Book
- Entities Project:
	- Add Author and Category models.
	- Create AuthorParameters in RequestFeatures
	- Create Dtos for Category and Author
	- Add BookLinkParameters into Dtos
	- Add custom exceptions.
- Repositories Layer:
	- In the RepositoryContext add DbSets for Categories and Authors
	- In the Contracts add interfaces for Author and Category
	- Add CategoryRepository and AuthorRepository into EfCore
	- In the extensions folder add an AuthorRepositoryExtensions class
	- Add Author and Category to IRepositoryManager and RepositoryManager
	- Add Configs for Author and Category.
- WebApi layer:
	- Add migration, drop database, and update database.
	- Add Mappings
	- Add Scoped IoC record for AuthorLinks.
- Services layer:
	- Add ICategoryService, IAuthorService, IAuthorlinks into Contracts and add their implemetations Manager classes.
- Presentation layer,
	- Create Controllers, add Authentications and versioning...
- Test the Api
## 26.2 Relations, One-To-Many Relation
- A Book has one Category, a category have many books.
- A Book has one Author, an author can publish  many books.
- Add CategoryId, Category, AuthorId and Author to Book.
- Add Books to Category and Author.
- Add BookDetailsDto and configure mapper to map from Book to this.
- Configure Repository to include Category and Author navigational properties to Book.
-Refactor BooksController get all book details and create book with AuthorId and CategoryId.
# 27 File Operations
## Upload
- Create new controller named FilesController, add an Upload POST method.
- Create a Media folder in the WebApi project.
## Download
- Create a Get method in the FilesController.