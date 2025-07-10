# SantaMaoApi

## Overview

Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna
aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis
aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint
occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.

## How to test the application

Before starting the tests, it is necessary to run an image of the database in docker, so in the project root folder,
enter the command below and press enter:

```powershell
> docker compose up -d
```

To __install__ EF:

```powershell
dotnet tool install --global dotnet-ef
```[Ambev.DeveloperEvaluation.Unit.csproj](Tests/Ambev.DeveloperEvaluation.Unit/Ambev.DeveloperEvaluation.Unit.csproj)

To __add__ migrations:

```powershell
dotnet ef migrations add InitialMigration --startup-project .\src\Ambev.DeveloperEvaluation.WebApi\Ambev.DeveloperEvaluation.WebApi.csproj --project .\src\Ambev.DeveloperEvaluation.Infrastructure\Ambev.DeveloperEvaluation.Infrastructure.csproj --output-dir EF/Migrations --verbose
```

To __execute__ migrations:

```powershell
dotnet ef database update --startup-project .\src\Ambev.DeveloperEvaluation.WebApi\Ambev.DeveloperEvaluation.WebApi.csproj --project .\src\Ambev.DeveloperEvaluation.Infrastructure\Ambev.DeveloperEvaluation.Infrastructure.csproj
```

```information
Note: the database image is only needed for the application integration tests
```

To __test__ the application, in the project root folder, enter the command below and press enter:

...to run all tests

```powershell
> dotnet test .\Ambev.DeveloperEvaluation.sln
```

...to run only application integration tests

```powershell
> dotnet test .\tests\Ambev.DeveloperEvaluation.Integration\
```

...to run only application logic unit tests

```powershell
> dotnet test .\tests\Ambev.DeveloperEvaluation.Unit\
```

At the end of the tests do not forget to run the command below and press enter

```powershell
> docker compose down
```

