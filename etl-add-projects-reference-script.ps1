cd ETL/

dotnet add ETL.API reference ETL.Application/ETL.Application.csproj

dotnet add ETL.Application reference ETL.Domain/ETL.Domain.csproj

dotnet add ETL.Application reference ETL.Shared/ETL.Shared.csproj

dotnet add ETL.Infrastructure reference ETL.Application/ETL.Application.csproj

dotnet add ETL.Infrastructure reference ETL.Domain/ETL.Domain.csproj

dotnet add ETL.Infrastructure reference ETL.Persistence/ETL.Persistence.csproj

dotnet add ETL.WorkerService reference ETL.Jobs/ETL.Jobs.csproj

dotnet add ETL.Jobs reference ETL.Application/ETL.Application.csproj

dotnet add ETL.Jobs reference ETL.Domain/ETL.Domain.csproj