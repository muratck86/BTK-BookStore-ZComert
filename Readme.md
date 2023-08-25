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
# Using Postman
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
## Randdom Functions
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
	
