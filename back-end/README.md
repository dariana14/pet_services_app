## Useful commands in .net console CLI

Install tooling

~~~bash
dotnet tool update -g dotnet-ef
dotnet tool update -g dotnet-aspnet-codegenerator 
~~~

## EF Core migrations

Run from solution folder  

~~~bash
dotnet ef migrations --project App.DAL.EF --startup-project WebApp add initial
dotnet ef database   --project App.DAL.EF --startup-project WebApp update
dotnet ef database   --project App.DAL.EF --startup-project WebApp drop
~~~


## MVC controllers

Install from nuget:  
- Microsoft.VisualStudio.Web.CodeGeneration.Design
- Microsoft.EntityFrameworkCore.SqlServer


Run from WebApp folder!  

~~~bash
cd WebApp

dotnet aspnet-codegenerator controller -name LocationsController        -actions -m  App.Domain.Location        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
# use area
dotnet aspnet-codegenerator controller -name RefreshTokensController        -actions -m  App.Domain.Identity.AppRefreshToken        -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

dotnet aspnet-codegenerator controller -name LocationsController        -actions -m  App.Domain.Location        -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

cd ..
~~~

Api controllers
~~~bash
dotnet aspnet-codegenerator controller -name LocationsController  -m  App.Domain.Location        -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
~~~