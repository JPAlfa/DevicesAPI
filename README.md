# Devices API

## How to Run the Project

### Prerequisites
- .NET SDK 9.0+
- Docker
- Docker Compose (optional)

### Start the database (SQL Server)

From the solution root:

```bash
docker-compose up -d
```
Make sure the container is running:

```bash
docker ps
```
### Database configuration


### Apply migrations

```bash
dotnet ef database update -p Devices.Infrastructure -s Devices.API
```
### Run the API

```bash
dotnet run --project Devices.API
```
### Swagger

```text
https://localhost:7022/swagger
```
---

## Overview

This project is a **RESTful API for device management**, built with a strong focus on **Clean Architecture**, **Domain-Driven Design (DDD)**, and **maintainability**.

It supports:
- Device creation
- Retrieval by ID
- Listing with filters
- Full and partial updates
- Deletion following business rules

---

## Architecture

The solution follows **Clean Architecture**, divided into:
```
Devices.Domain  
Devices.Application  
Devices.Infrastructure  
Devices.API  
Devices.Tests  
```
Domain: entities, value objects, business rules  

Application: use cases, commands, queries, abstractions  

Infrastructure: EF Core, repositories, persistence  

API: controllers, HTTP concerns, Swagger  

Tests: application-level unit tests  

---

## Design Patterns

- **Clean Architecture**
- **Domain-Driven Design (DDD)**
  - Entity (`Device`)
  - Value Object (`DeviceState`)
  - Domain exceptions
- **Repository Pattern**
- **Use Case Pattern**

Each business operation is represented by a dedicated use case.

---

## Device State Handling

`DeviceState` is implemented as a **Value Object** to centralize validation and normalization.

Supported states:
- `available`
- `in-use`
- `inactive`

Input is case-insensitive and normalized (e.g. `In Use`, `in_use`, `INUSE`).

---

## Business Rules

- When a device is **in-use**:
  - Name and brand cannot be changed
  - State can be changed
- Devices in-use cannot be deleted
- All rules are enforced inside the **domain layer**

---

## Tests

The project includes **application-level unit tests** covering:
- Success scenarios
- Domain rule violations

Tests use an in-memory repository and real domain entities.

Run tests with:

```bash
dotnet test
```
