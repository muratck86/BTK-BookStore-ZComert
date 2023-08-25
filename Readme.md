# A RESTful API for a Book Store
##  Creating the first project
- Create BookStore Folder and a BookStore solution in it.
- Create BookStore/BookStoreApi Folder and BookStoreApi "webapi" (NET6.0) project and add it to the solution.
- Delete example model and controller.
- Add a Book model under Models (create) folder.
- Add a BooksController (Empty WebApi Controller) under Controllers folder.
### Adding In Memory Mock Data
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