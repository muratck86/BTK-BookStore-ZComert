# 1. A Brief Oveview of a RESTful API
##  Creating the first project
- Create BookStore Folder and a BookStore solution in it.
- Create BookStore/BookStoreApi Folder and BookStoreApi "webapi" (NET6.0) project and add it to the solution.
- Delete example model and controller.
- Add a Book model under Models (create) folder.
- Add a BooksController (Empty WebApi Controller) under Controllers folder.
### Adding In-Memory Mock Data
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
## Global and Collection variables
- We can use variables in order not to repeat urls.
- Select https://localhost:PORT in any request url field. Click on set as variable, define a name (baseUrl) and select global. This part of the url in the field will change to: {{baseUrl}}, change all of the other requests to this. (eg: {{baseUrl}}/api/books/2)
- Collection variables can be defined likewise, collection variables are valid only under the collection that is defined for.
- Variables can be used in the bodies of the requests too.
## Tests using postman
- Test functionality of postman can be used for requests,
	- Under the Tests tab of a selected request, test functions like below can be described:
	```js
	pm.test("Status code 200", function(){
    	pm.response.to.have.status(200)
    })
	```
## Random Functions
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

## EntitiyFramework Core
- Add package Microsoft.EntityFrameworkCore, either use nuget package manager or use Package Manager Console (as follows):
```
	Install-Package Microsoft.EntityFrameworkCore -Version 6.0.10 -ProjectName WebApi
```
- Add "ConnectionStrings" to appsettings.json file.
- Add Microsoft.EntityFrameworkCore.SqlServer package to the project by nuget or PMC:
```
	Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 6.0.10 -ProjectName WebApi
```
## Create Migations
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

## Data manipulation
- Create Controller Methods.
- To Use postman for testing, the request collection of the workspace that we created can be used by changing the port number of the beseUrl variable.
- For PATCH verb and its method, install NewtonsoftJson and JsonPatch packages and add services record in the Program.cs
# 4. Layered Architecture
## Entities Layer
- Add a Class Library project named Entitites
- Move Book class under the WebApi/Models folder to Entities/Models And Delete the WebApi/Models folder.
- Add reference to Entities into WebApi project and don't forget to edit namespaces while moving Book.class and resolvings.
## Repositories Layer
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
### Lazy Loading
- Refactor RepositoryManager
### Service Extensions
- Create Extensions folder under WebApi project.
- Create ServicesExtensions under Extensions folder.
### Integration of Repository Managers to Controller
- Inject and edit BookController to use RepositoryManager instead of Context.
- Add a method to ServicesExtension class for IoC
## Services Layer
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
## Presentation Layer
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
## Repository Context Factory
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
## Using Logger in the project
- Add logging lines wherever needed.

# Global Exception Handling
## Modeling Errors and Error Details
- Add an ErrorModel folder to Entities project
- Add a class ErrorDetails in this folder
## Using Exception Handler
- Add an ExceptionMiddlewareExtensions class into Extensions folder in the WebApi project
- Set all Exception Status codes to 500 for now.
- Configure Program.cs for the ConfigureExceptionHandler
- After these steps, the app may be tested. The Exceptions thrown in te app will be serialized as ErrorDetails and the StatusCode will be set to 500: Internal Server Error
- Remove try-catch blocks
## Custom Exceptions
- Add Exceptions folder under Entities
- Add a NotFound abstract class extends Exception into it.
- Add a BookNotFound sealed class extends NotFound
- Return to ExceptionMiddlewareExtensions class in the Extensions folder of the main project and edit it according to Exception type.
- Refactor Related methods that throw errors or return not found responses to throw BookNotFoundException.
- Optional: Add a new custom Exception; BadRequestException
# AutoMapper Implementation
- Install automapper.Extensions.Microsoft.DependencyInjection versin 12.0.0 to services project.
- In the Program.cs add record to services
- Add DataTransferObjects folder to Entities project
- Add BookUpdateDto "record" into this folder. Dtos are not intended for manipulation hence, they are defined as records. Records are much like classes, they are also reference types. We don't define set blocks for properties in the record, instead we define init blocks. So record types become immutable types.
- Add Utilities/AutoMapper folders under WebApi project.
- Add MappingProfile extends Profile class into AutoMapper folder.
- Add mappings into constructor using CreateMap method.
- Refactor BooksController and Service layer to use BookUpdateDto

