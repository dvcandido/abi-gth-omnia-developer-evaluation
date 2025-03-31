# Developer Evaluation Project

The **DeveloperStore.Sales API** is a solution developed to manage sales records within the DeveloperStore ecosystem, following the principles of Domain-Driven Design (DDD).

## Technologies Used

- .NET 8.0  
- C#  
- PostgreSQL  
- Docker  
- Docker Compose  

## Prerequisites

Before starting, make sure you have the following installed:

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)  
- [Docker](https://www.docker.com/)  
- [Docker Compose](https://docs.docker.com/compose/)  

## How to Run the Project

1. Clone this repository:  
   ```bash
   git clone https://github.com/dvcandido/abi-gth-omnia-developer-evaluation
   cd abi-gth-omnia-developer-evaluation
   ```

2. Run the project using Docker Compose:  
   ```bash
   docker-compose up --build
   ```
4. Access the API through Swagger:  
   ```bash
   `http://localhost:5050/swagger`
   ```

## How to Test

You can test the endpoints directly through Swagger.  
Alternatively, you can use the `.http` files located in the `.\src\Ambev.DeveloperEvaluation.WebApi` directory:

- `WebApi.Carts.http`  
- `WebApi.Products.http`  
- `WebApi.Users.http`  

These files allow you to send requests to the API directly from your IDE or code editor.

### How to Use `.http` Files

- [Use .http files in Visual Studio 2022](https://learn.microsoft.com/pt-br/aspnet/core/test/http-files?view=aspnetcore-9.0)  
- [Use .http files in Visual Studio Code (REST Client extension)](https://marketplace.visualstudio.com/items?itemName=humao.rest-client)
