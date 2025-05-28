# RustRetail.Shared

**RustRetail.Shared** is a collection of shared libraries designed to support the RustRetail microservices ecosystem. This project serves as a learning platform to explore concepts like Clean Architecture, Domain-Driven Design (DDD), and modular .NET development.

⚠️ **Note:** This project is intended for educational purposes and may not be production-ready.

## 📦 Packages

The solution comprises the following NuGet packages:

- **RustRetail.SharedKernel**: Contains domain primitives, base classes, and interfaces that promote DDD principles across services.

- **RustRetail.SharedApplication**: Provides application-level abstractions, such as use case interfaces and DTOs.

- **RustRetail.SharedInfrastructure**: Includes implementations for cross-cutting concerns like logging, caching, and external integrations.

- **RustRetail.SharedPersistence**: Offers base classes and interfaces for data access and repository patterns.

Each package is versioned independently and can be consumed via NuGet.

## 🚀 Getting Started

To incorporate these packages into your projects:

1. **Install via NuGet Package Manager:**

```bash
   dotnet add package RustRetail.SharedKernel
   dotnet add package RustRetail.SharedApplication
   dotnet add package RustRetail.SharedInfrastructure
   dotnet add package RustRetail.SharedPersistence
```

📌 Ensure that the versions match the latest published versions on NuGet.org.

2. **Reference in your code:**

```c#
  using RustRetail.SharedKernel;
  using RustRetail.SharedApplication;
  using RustRetail.SharedInfrastructure;
  using RustRetail.SharedPersistence;
```
