# DataStorage
 
Backend (.NET Core 6 API)

Implement JWT-based authentication.
Use ASP.NET Core Identity for user management.
Create endpoints for login, registration, and protected data.
Implement JWT validation middleware.
Protect routes with [Authorize] attributes.

Frontend (Vue 3)

Set up Vue Router for navigation and route guards.
Implement JWT storage & authentication state using localStorage.
Use Axios for API requests with JWT token in headers.
Protect dashboard routes based on authentication state.


Why JWT?
JWT (JSON Web Token) is stateless, meaning no session storage is required on the server.
The token is self-contained, carrying user authentication information.
JWT can be validated easily via middleware without querying a database.


