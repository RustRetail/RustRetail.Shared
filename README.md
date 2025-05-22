# RustRetail.SharedKernel

**RustRetail.SharedKernel** is a NuGet package that provides a shared kernel for microservices in the RustRetail e-commerce system.

It includes reusable building blocks and domain primitives that ensure consistency and promote DDD (Domain-Driven Design) principles across all services.

This project is built with .NET 9.

## Components

- **Domain:** The Domain layer encapsulates core business logic and shared concepts.
  - **Abstractions:** Contains abstract base classes and interfaces that define contracts for domain entities, repositories, and services.
  - **Models:** Includes domain models that represent core business objects with behavior.
  - **ValueObjects:** Provides immutable value objects that model concepts like Money, Email, Address, etc.
  - **Events:** Defines domain events used to signal important changes or actions within the domain.
  - **Enums:** Contains enumerations used across the domain, ensuring consistent values and avoiding magic strings or numbers.
  - **Extensions:** Includes utility extension methods that enhance readability and maintainability of domain logic.
