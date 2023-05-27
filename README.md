# Maktaba .NET Microservices Sample Reference Application
I have implemented Maktaba Microservices project using ASP.NET Core 7.0.

## Services: 
## List of micro-services and infrastructure components

<table>
   <thead>
    <th>№</th>
    <th>Service</th>
    <th>Description</th>
  </thead>
  <tbody>
    <tr>
        <td align="center">1.</td>
        <td>Catalog microservice</td>
        <td>
* ASP.NET Core Web API application 
* REST API, gRPC
* MS SQL Server Database
* Clean Architecture
* Repository Pattern Implementation
* Swagger Open API implementation	
* CQRS and Mediatr
* Publish IntegrationEvents using RabbitMQ
* Containerization using Docker</td>
    </tr>
    <tr>
        <td align="center">2.</td>
        <td>Basket microservice</td>
        <td>* ASP.NET Core Web API application 
* REST API, gRPC
* Redis Database
* Clean Architecture
* Repository Pattern Implementation
* Swagger Open API implementation	
* Publish IntegrationEvents using RabbitMQ
* Containerization using Docker</td>
    </tr>
    <tr>
        <td align="center">3.</td>
        <td>Orders Microservice</td>
        <td>* ASP.NET Core Web API application 
* REST API, gRPC
* MS SQL Server Database
* Clean Architecture
* Repository Pattern Implementation
* Swagger Open API implementation	
* CQRS and Mediatr
* Publish IntegrationEvents using RabbitMQ
* Containerization using Docker</td>
    </tr>
    <tr>
        <td align="center">4.</td>
        <td>Identity Microservice</td>
        <td>* ASP.NET Core Web API application 
* REST API, gRPC
* MS SQL Server Database
* Clean Architecture
* Repository Pattern Implementation
* Swagger Open API implementation	
* JWT and Refresh Tokens
* Containerization using Docker</td>
    </tr>
    <tr>
        <td align="center">4.</td>
        <td>API Gateway Ocelot Microservice</td>
        <td>* Implement **API Gateways with Ocelot**
* Containerization using Docker</td>
    </tr>
  </tbody>  
</table>

#### Microservices Resilience Implementations
* Making Microservices more **resilient Use IHttpClientFactory** to implement resilient HTTP requests
* Implement **Retry and Circuit Breaker patterns** with exponential backoff with IHttpClientFactory and **Polly policies**

#### Docker Compose establishment with all microservices on docker
* Containerization of microservices
* Containerization of databases
* Override Environment variables
