# NET CORE 5 - AUCTION MICROSERVICE

![Screenshot_1](https://user-images.githubusercontent.com/54249736/133939613-9a130d0d-f87d-40ef-a7b4-eb2adb879ca9.png)

It is a project that I developed with the microservice architecture approach. All elements of the project are hosted in docker so that they can communicate with each other. All web api projects, data layers and rabbitmq like technologies were stored on the same docker composite file. The business side of the project was managed by three microservices. (product, sourcing and ordering api). Built the resource service from a web api and mongo database. Also, real-time feature has been added to our UI project using SignalR. Also prepared the order micro service with web api and mssql database. The event bus library has been developed to communicate asynchronously between services. An API gateway microservice with ocelot was used to simplify communication between clients and services. Finally, a web project was prepared with .net core so that the user can use the entire project.

## Libraries
![Screenshot_2](https://user-images.githubusercontent.com/54249736/133939676-ffe154b8-e977-4ef0-b0fe-3e884f01a54f.png)

#### Installed Packages (ESourcing.APIGateway)
* Microsoft.VisualStudio.Azure.Containers.Tools.Targets
* Ocelot

#### Installed Packages (EventBusRabbitMQ)
* Microsoft.Extensions.Logging.Abstractions
* Newtonsoft.Json
* Polly
* RabbitMQ.Client

#### Installed Packages (ESourcing.Order)
* Microsoft.EntityFrameworkCore.Design
* Microsoft.VisualStudio.Azure.Containers.Tools.Targets
* Swashbuckle.AspNetCore

#### Installed Packages (ESourcing.Products)
* Microsoft.VisualStudio.Azure.Containers.Tools.Targets
* MongoDB.Bson
* MongoDB.Driver
* Swashbuckle.AspNetCore

#### Installed Packages (ESourcing.Sourcing)
* AutoMapper.Extensions.Microsoft.DependencyInjection
* Microsoft.VisualStudio.Azure.Containers.Tools.Targets
* MongoDB.Driver
* Swashbuckle.AspNetCore

#### Installed Packages (ESourcing.UI)
* FluentValidation.AspNetCore
* Mapster
* Microsoft.AspNetCore.Authentication.Facebook
* Microsoft.AspNetCore.Authentication.Google
* Microsoft.AspNetCore.Identity.EntityFrameworkCore
* Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation
* Microsoft.EntityFrameworkCore
* Microsoft.EntityFrameworkCore.Tools
* Microsoft.VisualStudio.Web.CodeGeneration.Design

#### Contact Addresses
##### Linkedin: [Send a message on Linkedin](https://www.linkedin.com/in/cihatsolak/) 
##### Twitter: [Go to @cihattsolak](https://twitter.com/cihattsolak)
##### Medium: [Read from medium](https://cihatsolak.medium.com/)
