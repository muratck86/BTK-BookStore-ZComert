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
	```
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
	```
		[
		  {
			"path": "price",
			"op": "replace",
			"value": 150
		  }
		]
	```
	- Example operation2:
	```
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
	```
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
	```
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