# DataStorage
 
#Backend (.NET Core 6 API)
Implement JWT-based authentication:

Generate JWT tokens upon successful authentication.
Store essential claims inside the JWT.
Configure token expiration and refresh token mechanism (optional).
Use DI.

Create Authentication Endpoints:
Login Endpoint: Authenticate users, return JWT on success. 
#Username: admin
#Password: password
Protected API Endpoint: Example GET /api/upload, accessible only with a valid JWT.

Implement Middleware for JWT Validation:
Configure JWT Authentication in Program.cs.
Use JwtBearerDefaults to validate incoming tokens.
Use DI to inject middleware dependencies.

Protect Routes Using [Authorize] Attributes:
Secure endpoints using [Authorize].

Logging & Exception Handling:
Use Serilog or built-in logging (ILogger) for tracking authentication and security events.
Implement global exception handling middleware for structured error responses.

#Frontend (Vue 3)
Set Up Vue Router with Navigation Guards:
Define public (login) and protected (upload) route.
Implement route guards to restrict access to authenticated users.

JWT Storage & Authentication State Management:
Use localStorage to store JWT tokens.
Implement auto-login using stored JWT.

Use Axios for API Requests:
Set up Axios interceptors to attach JWT in headers (Authorization: Bearer <token>).
Handle 401 responses (redirect to login on token expiry).

Protect Routes Based on Authentication State:
Redirect users to login if they attempt to access protected pages without authentication.
Show/hide UI elements based on authentication status.

Why JWT?
Stateless: No session storage required on the server.
Self-contained: Carries user authentication information.
Easy validation: Middleware can validate JWT without querying a database.
Scalability: Ideal for microservices & distributed applications.




