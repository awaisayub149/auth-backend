Auth App

Brief description of your project.
This .NET Core 6.0 application provides a simple and secure authentication system for managing users 
using JSON Web Tokens (JWT). It follows a service-oriented architecture and utilizes MongoDB 
as the backend database.

Services Architecture: Structured application architecture with distinct services.
MongoDB Integration: User data storage and retrieval using MongoDB.
Public and Protected Routes: Differentiate between public and protected routes.

Usage
Ensure you have the following prerequisites installed:

.NET: DOT NET 6.0 SDK
MongoDB: dotnet add package MongoDB.Driver
JWT: system.IdentityModel.Tokens.Jwt" Version="6.15.0"
     Microsoft.AspNetCore.Authentication.JwtBearer" Version="6.0.0"     


Routes
Public Login: /api/public/login
Protected Users: /api/protected/users

JWT Authentication
Used JWT tokens, while login the user token is send to the user it will store in a session and accessible in app, 
and pass in headers for accessing the protected routes.
Example;-
/api/protected/users

Services Architecture
Briefly describe the services architecture of your application.

MongoDB
Explain how MongoDB is used in your application.

Contributing
If you want to contribute to this project, follow these steps.

Fork the repository.

Create a new branch.
Make your changes.
Submit a pull request.
