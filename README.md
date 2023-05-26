# ToyWebService
An example of a web service using all the features of the .net

This service is service of shop
Item - uniq entity, describes goods/product and its attributes.
Example - book "Around the world in 80 days"

# How to Run
Execute in terminal in root ToyWebService folder
- docker-compose up -d
- dotnet build
- dotnet run --project ToyWebService.Migrator
  - If there is error with message about creating DB - please, create it and re-run ToyWebService.Migrator
- dotnet run --project ToyGrpcService

# Used technologies, tools and hints
- gRPC
- PostgreSQL
- FluentMigrator
- docker
- Changelog