# Auth App Backend

This .NET Core 6.0 application provides a simple and secure authentication system for managing users 
using JSON Web Tokens (JWT). It follows a service-oriented architecture and utilizes MongoDB 
as the backend database.

## Features
* MongoDB.
* JWT Authentication.
* Protected/Public Routes
    
## Installation
* Clone the repository <pre> git clone git@github.com:awaisayub149/auth-backend.git </pre>
* Install the Dotnet Version 6.
* Check the version dotnet --version.
* cd auth-backend.
* Run the project by the following command <pre>dotnet run</pre>.

## Routes
* Public: /api/public/login
* Protected: /api/protected/users
* If you want to contribute to this project, follow these steps.

## JWT Authentication
* Used JWT tokens, while login the user token is send to the user it will store in a session and accessible in app and pass in headers for accessing the protected routes.

### Example;-
/api/protected/users
