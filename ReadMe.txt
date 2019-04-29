Implementation:

The web api is implemented at http://localhost:9001/
and the "to do" web API will expose following methods:

Action						HTTP method			Relative URI
Get a list of all todos		GET					/api/todos
Get a todo by ID			GET					/api/todos/id
Get a todo by duedate		GET					/api/todos?duedate=duedate
Create a new todo			POST				/api/todos
Update a todo				PUT					/api/todos/id
Delete a todo				DELETE				/api/todos/id

