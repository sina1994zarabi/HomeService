# HomeService Web Application

**A comprehensive ASP.NET Core MVC & Web API platform for managing home service requests**, connecting clients with experts, and providing admin controls for full lifecycle and financial management.

---

## Table of Contents
- [Overview](#overview)
- [Features](#features)
- [Architecture](#architecture)
- [Technology Stack](#technology-stack)
- [Prerequisites](#prerequisites)


---

## Overview
HomeService is an online marketplace platform for home services (e.g., cleaning, handyman, repairs). It supports three user roles:

- **Client**: Browse services, submit service requests, compare expert offers, confirm and pay securely, leave reviews.
- **Expert**: View matching requests, submit offers, confirm assignments, complete jobs, track earnings.
- **Admin**: Manage services, categories, subcategories, review comments, control service‑request lifecycles, and oversee financial transactions.

---

## Features

1. **User Management**
   - Registration & Login via ASP.NET Core Identity
   - Role-based areas (Client, Expert, Admin) with separate layouts
   - Profile editing with image upload

2. **Service Catalog**
   - Categorized services (Category → SubCategory → Service)
   - CRUD operations via Admin panel

3. **Service Requests & Offers**
   - Clients submit requests with booking date
   - Experts view relevant requests and submit offers
   - Status-driven workflow: AwaitingOffers → PendingClientConfirmation → PendingProviderConfirmation → InProgress → Completed → AwaitingPayment → Paid
   - Clients choose offers, experts confirm, admin approves transactions

4. **Payments & Reviews**
   - Admin-handled fee deduction and expert payout
   - Clients leave reviews and ratings post‑completion

5. **Admin Dashboard**
   - Manage all entities with RBAC
   - Financial overview (account balance, transaction approvals)

6. **API Endpoints**
   - Public RESTful APIs for categories, subcategories, services
   - Swagger / Postman testing ready

7. **Logging & Monitoring**
   - Serilog with sinks (Console, Seq, MS SQL Server)
   - Custom middleware & action filters for structured logs

8. **Caching**
   - In‑memory caching for frequently used data (categories, services)

9. **Responsive UI**
   - Bootstrap 5 + RTL support
   - Card-based layouts, modals, notifications via TempData

---

## Architecture

```
Client (MVC + Web API)  <-->  Application Services  <-->  Domain Entities & DTOs  <-->  Repositories (EF Core / Dapper)
                                                                    ^
                                                                    |
                                                          Identity  (AppUser, Roles)
                                                                    |
                                                            SQL Server Database
```  

- **Presentation Layer**: ASP.NET Core MVC areas (Client, Expert, Admin), Razor views, shared layouts.
- **Web API Layer**: `[ApiController]` endpoints under `/api/*` for headless integrations.
- **Application Layer**: Business logic via `*AppService` interfaces and implementations.
- **Domain Layer**: Entities, Enums, Data Transfer Objects (DTOs).
- **Infrastructure Layer**: Repositories (EF Core + Dapper), Logging, Caching.

---

## Technology Stack

- **Framework**: .NET 7 / .NET 8 (adjust to your target)
- **Web Framework**: ASP.NET Core MVC, Razor Pages
- **Identity**: ASP.NET Core Identity with custom `AppUser`
- **ORM**: Entity Framework Core (SQL Server), optional Dapper for specific queries
- **Logging**: Serilog (Console, Seq, MSSQL)
- **Caching**: In‑Memory Cache
- **Client**: Bootstrap 5, FontAwesome, jQuery (for minor interactivity)

---

## Prerequisites

- [.NET SDK (7 or 8)](https://dotnet.microsoft.com/download)
- SQL Server (Express, LocalDB, or full version)
- (Optional) [Seq](https://datalust.co/seq) for log monitoring
- Visual Studio 2022 / VS Code

---

