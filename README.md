[![NuGet Package](https://img.shields.io/nuget/v/Valobtify.AspNetCore.WebApi)](https://www.nuget.org/packages/Valobtify.AspNetCore.WebApi/)

### Table of Contents

- [Overview](#overview)
- [Installation](#installation)
- [Model Mapping](#model-mapping)
- [Swagger Support](#swagger-support)

---

### Overview

`Valobtify.AspNetCore.WebApi` is an extension of the `Valobtify` library that simplifies the integration of value objects into ASP.NET Core Web APIs. It provides built-in support for model mapping and Swagger documentation, making it easier to work with value objects in your API projects.

---

### Installation

To install the `Valobtify.AspNetCore.WebApi` package, run the following command in your terminal:

```shell
dotnet add package Valobtify.AspNetCore.WebApi
```

Ensure you have the required .NET SDK installed.

---

### Model Mapping

To enable model mapping in your ASP.NET Core Web API, add the following configuration to your `Program.cs` file:

```csharp
builder.Services.AddValobtifyConverters(Assembly.GetExecutingAssembly());
```

This ensures that `Valobtify` correctly maps your value objects within your application.

---

### Swagger Support

To integrate `Valobtify` with Swagger and automatically generate schema filters for value objects, add this configuration to your `Program.cs` file:

```csharp
builder.Services.AddSwaggerGen(c => c.AddValobtifySchemaFilters());
```

