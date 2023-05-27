# Maktaba .NET Microservices Application
I have implemented Maktaba Microservices application by .NET Core 7.0.

## Architecture Overview
Microservices - also known as the microservice architecture - is an architectural style that structures an application as a collection of services that are:
* Independently deployable
* Loosely coupled
* Organized around business capabilities
* Owned by a small team
The microservice architecture enables an organization to deliver large, complex applications rapidly, frequently, reliably and sustainably - a necessity for competing and winning in today’s world.

## List of Microservices and Description

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
* ASP.NET Core Web API application<br/>
* REST API, gRPC<br/>
* MS SQL Server Database<br/>
* Clean Architecture<br/>
* Repository Pattern Implementation<br/>
* CQRS and Mediatr<br/>
* Publish IntegrationEvents using RabbitMQ<br/>
* Containerization using Docker</td>
    </tr>
    <tr>
        <td align="center">2.</td>
        <td>Basket microservice</td>
        <td>* ASP.NET Core Web API application<br/> 
* REST API, gRPC<br/>
* Redis Database<br/>
* Clean Architecture<br/>
* Repository Pattern Implementation<br/>
* Publish IntegrationEvents using RabbitMQ<br/>
* Containerization using Docker</td>
    </tr>
    <tr>
        <td align="center">3.</td>
        <td>Orders Microservice</td>
        <td>* ASP.NET Core Web API application<br/>
* REST API, gRPC<br/>
* MS SQL Server Database<br/>
* Clean Architecture<br/>
* Repository Pattern Implementation<br/>
* CQRS and Mediatr<br/>
* Publish IntegrationEvents using RabbitMQ<br/>
* Containerization using Docker</td>
    </tr>
    <tr>
        <td align="center">4.</td>
        <td>Identity Microservice</td>
        <td>* ASP.NET Core Web API application 
* REST API, gRPC<br/>
* MS SQL Server Database<br/>
* Clean Architecture<br/>
* Repository Pattern Implementation<br/>
* JWT and Refresh Tokens<br/>
* Containerization using Docker</td>
    </tr>
    <tr>
        <td align="center">4.</td>
        <td>API Gateway Ocelot Microservice</td>
        <td>* Implement API Gateways with Ocelot<br/>
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

### TODO
- [ ] Add Unit Test
- [ ] Use MongoDB in catalog service
- [ ] Implement envoy Api Gateway
- [ ] Add Payment service
- [ ] Create CI/CD pipline by Junkies
- [ ] Use Apache Kafka
- [ ] Add SignalR
- [ ] Implement Front-End SPA project using AngularJS
