# Identity Service Setup Guide

## Required NuGet Packages

Run these commands to install the necessary packages:

```powershell
dotnet add src/Services/Identity/Identity.API/Identity.API.csproj package Microsoft.AspNetCore.Identity.EntityFrameworkCore --version 9.0.0
dotnet add src/Services/Identity/Identity.API/Identity.API.csproj package Microsoft.AspNetCore.Authentication.JwtBearer --version 9.0.0
dotnet add src/Services/Identity/Identity.API/Identity.API.csproj package Microsoft.EntityFrameworkCore.SqlServer --version 9.0.0
dotnet add src/Services/Identity/Identity.API/Identity.API.csproj package Microsoft.EntityFrameworkCore.Tools --version 9.0.0
dotnet add src/Services/Identity/Identity.API/Identity.API.csproj package AutoMapper.Extensions.Microsoft.DependencyInjection --version 12.0.1
dotnet add src/Services/Identity/Identity.API/Identity.API.csproj package FluentValidation.AspNetCore --version 11.3.0
```

## Database Migration

After installing packages, create and apply the initial migration:

```powershell
# Navigate to the Identity.API project
cd src/Services/Identity/Identity.API

# Create initial migration
dotnet ef migrations add InitialCreate

# Update the database
dotnet ef database update

# Navigate back to root
cd ../../../..
```

## Testing the API

### Register a new user
```powershell
curl -X POST http://localhost:5001/api/auth/register `
  -H "Content-Type: application/json" `
  -d '{\"email\":\"test@example.com\",\"password\":\"Test123!\",\"firstName\":\"Test\",\"lastName\":\"User\"}'
```

### Login
```powershell
curl -X POST http://localhost:5001/api/auth/login `
  -H "Content-Type: application/json" `
  -d '{\"email\":\"test@example.com\",\"password\":\"Test123!\"}'
```

The response will include a JWT token that can be used to authenticate requests to other services.
