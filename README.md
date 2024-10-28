[![NuGet Package](https://img.shields.io/nuget/v/Valobtify.AspNetCore.WebApi)](https://www.nuget.org/packages/Valobtify.AspNetCore.WebApi/)

### Table of content
- [Installation](#installation)
- [Model mapping](#Model-Mapping)
- [Swagger support](#Swagger-support)

### Installation
```shell
dotnet add package Valobtify
```

### Model mapping

```csharp
builder.Services.AddValobtifyConverters(Assembly.GetExecutingAssembly());
```

### Swagger support

```csharp
builder.Services.AddSwaggerGen(c => c.AddValobtifySchemaFilters())
```