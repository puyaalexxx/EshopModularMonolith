# EshopModularMonolith

## Features:

- Modular Monolith Architecture
- Vertical slice architecture for modules
- CQRS (Command Query Responsibility Segregation) via MediatR Nugget package
- Carter nugget package for minimal APIs endpoints
- DDD (Domain Driven Design) principles for modules (Entities, Aggregates)
- Domain events for inter-module communication (ProductCreatedEvent, ProductPriceChangedEvent)
- FluentValidation for request validation
- Global Exception handler with custom Exceptions
- Docker and docker-compose for containerization
- PostGresql as the database from docker-compose
- Entity Framework Core for data access
- DTos for data transfer
- Behaviors for logging and validation via MaediarR pipeline implementation
- Mapster Nugget package for object mapping
- Serilog for structured logging
- Serilog Sinqs Seq via docker compose