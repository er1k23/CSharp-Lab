# CSharp-Lab

A collection of C# and .NET projects, laboratory exercises, experiments, and backend applications developed while learning the C# ecosystem.

The repository covers everything from fundamental C# concepts and algorithms to database integration, REST APIs, authentication, background services, Telegram bots, and web-based reporting.

---

## 📚 Repository Overview

The repository is organized around several areas of C# and .NET development:

- C# fundamentals and language features
- Generics and generic constraints
- File I/O and binary data processing
- Data structures and algorithms
- IEEE-754 floating-point representation
- PostgreSQL and Entity Framework Core
- RESTful Web APIs
- Authentication and authorization
- Dependency Injection
- Background services and workers
- Telegram Bot development
- Webhook integration
- MVC / Razor views
- Data analytics and reporting
- Console application architecture

---

## 🚀 Main Projects

### 💼 Job Recruitment API

A backend REST API for managing a job recruitment system.

The project focuses on building a structured ASP.NET Core Web API with database integration and authentication.

**Key concepts:**

- ASP.NET Core Web API
- Controllers
- REST endpoints
- Entity Framework Core
- PostgreSQL
- Dependency Injection
- Basic Authentication
- Authorization policies
- Password hashing
- Service layer
- Middleware
- OpenAPI / Swagger

**Technologies:**

`C#` · `ASP.NET Core` · `EF Core` · `PostgreSQL` · `Npgsql` · `OpenAPI`

---

### 💰 Telegram Finance Bot

A personal finance assistant built with ASP.NET Core and Telegram Bot API.

The bot allows users to record expenses through Telegram and provides financial summaries and reports.

The project evolved from a simple Telegram bot into a more structured backend application with services, command handlers, database persistence, background workers, Webhooks, and a web-based financial dashboard.

**Features:**

- Telegram commands
- Expense tracking
- Expense categories and notes
- Daily and monthly statistics
- Spending analytics
- Top spending categories
- 7-day spending comparison
- 14-day daily activity
- Recent transaction history
- Daily financial digest
- Web-based financial report
- Telegram Webhook support
- PostgreSQL persistence

**Architecture includes:**

- Dependency Injection
- Interfaces and services
- Command Handler pattern
- Update processing layer
- Background workers
- Entity Framework Core
- DTOs
- MVC / Razor views
- Webhook integration

**Technologies:**

`C#` · `ASP.NET Core` · `Telegram.Bot` · `EF Core` · `PostgreSQL` · `Razor` · `HTML/CSS`

---

### 📚 Library.Pg

A backend/database learning project focused on working with PostgreSQL through Entity Framework Core.

The project explores:

- EF Core
- PostgreSQL
- DbContext
- Entities
- Database configuration
- Migrations
- CRUD operations
- ORM concepts

**Technologies:**

`C#` · `.NET` · `EF Core` · `PostgreSQL` · `Npgsql`

---

### 🌐 Social Network

A C# console application experimenting with the basic structure of a social-network backend.

The project includes concepts such as:

- Users
- Registration
- Services
- PostgreSQL integration
- Password hashing
- Database operations

**Technologies:**

`C#` · `PostgreSQL` · `Npgsql`

---

### ❌ Tic-Tac-Toe

A console-based Tic-Tac-Toe game designed with separated responsibilities and a custom menu/navigation system.

The project contains dedicated components for:

- Game board
- Cells
- Player marks
- User sessions
- Menus
- Menu navigation
- Settings
- Game flow

The project is useful for practicing object-oriented design and organizing a console application into independent components.

**Technologies:**

`C#` · `OOP` · `Console Application`

---

## 🧩 C# Exercises

### Generics Exercises

A collection of exercises focused on C# generics and type constraints.

Topics include:

- Generic classes
- Generic methods
- Generic algorithms
- `where` constraints
- Generic factory pattern
- Interfaces with generic constraints
- `Func<>` and lambda expressions
- Generic collections

Examples include a generic `Pair<T1, T2>`, collection filtering/projecting, and a generic factory for initializing entities.

---

### Generic Specifications

An experiment with the Specification pattern and generic abstractions.

The project explores how specifications can be used to encapsulate reusable business/query conditions.

---

### File I/O Exercises

A set of exercises focused on working with the filesystem and streams.

Includes:

- Daily report archiving
- Inbox/file scanning
- Partial binary file downloading
- UTF-8 log processing
- File and directory operations
- Text and binary streams

These exercises focus on practical usage of the .NET I/O APIs.

---

### IEEE-754 Float Representation

A set of low-level programming exercises related to floating-point representation.

The project explores how floating-point numbers are represented internally using the IEEE-754 standard.

It also contains exercises involving custom list implementation and low-level data representation.

---

## 🧠 Concepts Practiced

Throughout the repository, the following C#/.NET concepts are explored:

### C# Language

- Classes and objects
- Interfaces
- Inheritance
- Encapsulation
- Generics
- Generic constraints
- Delegates
- Lambda expressions
- LINQ
- Async/await
- Exception handling
- Collections
- File I/O
- Streams

### .NET / ASP.NET Core

- Dependency Injection
- Service lifetimes
- Controllers
- Middleware
- Configuration
- Background services
- Hosted services
- REST APIs
- OpenAPI
- Authentication
- Authorization
- MVC
- Razor Views
- Webhooks

### Databases

- PostgreSQL
- Entity Framework Core
- `DbContext`
- Entities
- Relationships
- CRUD operations
- LINQ queries
- Migrations
- Database persistence

### Software Architecture

- Service Layer
- Repository-style abstractions
- Dependency Injection
- Command Handler pattern
- Specification pattern
- Separation of concerns
- DTOs
- Background workers

---

## 🛠️ Technologies

| Technology | Usage |
|---|---|
| **C#** | Main programming language |
| **.NET** | Application platform |
| **ASP.NET Core** | Web APIs and web applications |
| **Entity Framework Core** | ORM and database access |
| **PostgreSQL** | Relational database |
| **Npgsql** | PostgreSQL provider for .NET |
| **Telegram.Bot** | Telegram Bot API integration |
| **Razor** | Server-side web UI |
| **OpenAPI / Swagger** | API documentation |
| **Git / GitHub** | Version control |

---

## 📁 Repository Structure

```text
CSharp-Lab/
│
├── File-IO-Exercises/
├── GenericSpecifications/
├── Generics-Exercises/
├── IEEE754-Float-Representation/
│
├── JobRecruitmentApi/
├── Library.Pg/
├── SocialNetwork/
├── TTLCache/
│
├── TelegramFinanceBot/
├── Tic-Tac-Toe/
│
└── README.md
```

The repository intentionally contains projects with different levels of complexity, reflecting the progression from individual C# exercises to complete backend applications.

---

## 🎯 Purpose

This repository serves as a practical C#/.NET learning laboratory.

Instead of keeping concepts as isolated theoretical exercises, the projects gradually apply them in increasingly complex applications — from basic language features and algorithms to database-backed APIs, authentication, background processing, Telegram integrations, and web interfaces.

The main goal is to build a strong understanding of both **C# itself** and the **.NET backend ecosystem**.

---

## 📈 Learning Progression

```text
C# Fundamentals
       ↓
OOP & Generics
       ↓
Algorithms & Data Structures
       ↓
File I/O & Low-Level Concepts
       ↓
PostgreSQL & EF Core
       ↓
Dependency Injection & Services
       ↓
ASP.NET Core Web API
       ↓
Authentication & Authorization
       ↓
Background Workers
       ↓
Telegram Bot API
       ↓
Webhooks & MVC
       ↓
Analytics & Web Reporting
```

---

## 👨‍💻 Author

**Erik**

Computer Science / Applied Mathematics & Informatics student focused on backend development, data, and software engineering.

- GitHub: [@er1k23](https://github.com/er1k23)

---

## 📌 Status

This repository is continuously evolving as new C#/.NET concepts, projects, and experiments are added.
