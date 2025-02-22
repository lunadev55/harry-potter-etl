mkdir ETL
cd ETL

dotnet new sln -n ETL

dotnet new webapi -n ETL.API

dotnet new worker -n ETL.WorkerService

dotnet new classlib -n ETL.Domain

dotnet new classlib -n ETL.Application

dotnet new classlib -n ETL.Infrastructure

dotnet new classlib -n ETL.Persistence

dotnet new classlib -n ETL.Shared

dotnet new classlib -n ETL.Jobs

dotnet sln add ETL.API/ETL.API.csproj

dotnet sln add ETL.WorkerService/ETL.WorkerService.csproj

dotnet sln add ETL.Domain/ETL.Domain.csproj

dotnet sln add ETL.Application/ETL.Application.csproj

dotnet sln add ETL.Infrastructure/ETL.Infrastructure.csproj

dotnet sln add ETL.Persistence/ETL.Persistence.csproj

dotnet sln add ETL.Shared/ETL.Shared.csproj

dotnet sln add ETL.Jobs/ETL.Jobs.csproj
