🍽️ Restaurant Manager
Show Image
Show Image
Show Image
Show Image

A robust, enterprise-level web application built with ASP.NET Core MVC and .NET 8, demonstrating clean architecture principles for managing restaurant digital operations.


📋 Table of Contents

Overview
Key Features
Technical Stack
Architecture & Design Patterns
Database Schema
Getting Started
Project Structure
Screenshots
Contributing
License
Contact


🌟 Overview
Restaurant Manager is a full-featured restaurant management system that streamlines menu management, inventory tracking, and customer ordering. Built with modern web development practices, this application showcases real-world implementation of design patterns, secure authentication, and efficient data management.
Whether you're learning ASP.NET Core or building a production-ready restaurant platform, this project provides a solid foundation with scalable architecture and best practices.

✨ Key Features
🍕 Dynamic Product Management

Complete CRUD operations for menu items
Image upload and management for products
Category-based product organization
Real-time stock tracking and updates

📦 Ingredient & Inventory Tracking

Comprehensive ingredient database
Many-to-Many relationship between products and ingredients
Automatic stock reduction on order placement
Low stock alerts and inventory reports

🛒 Shopping Cart & Ordering System

Session-based shopping cart functionality
Real-time cart updates without page reload
Order history tracking for customers
Price snapshot preservation for historical accuracy

🔐 Role-Based Security

ASP.NET Core Identity integration
Admin role: Full menu and inventory management
Customer role: Browse, order, and track purchases
Secure authentication and authorization

🏛️ Advanced Data Access

Generic Repository Pattern implementation
Unit of Work pattern for transaction management
Separation of concerns with clean architecture
Testable and maintainable codebase


🛠️ Technical Stack
CategoryTechnologyFramework.NET 8 (ASP.NET Core MVC)LanguageC# 12DatabaseSQL Server 2019+ORMEntity Framework Core 8.0AuthenticationASP.NET Core IdentityFrontendRazor Pages, Bootstrap 5, JavaScript (ES6+)ValidationFluentValidation / Data AnnotationsDependency InjectionBuilt-in ASP.NET Core DI Container

🏗️ Architecture & Design Patterns
1. Clean Architecture
The project follows a layered architecture approach:

Presentation Layer: MVC Controllers and Razor Views
Business Logic Layer: Services and Domain Models
Data Access Layer: Repositories and EF Core Context
Cross-Cutting Concerns: Identity, Logging, Validation

2. Generic Repository Pattern
Abstracts data access logic for better testability and maintainability:
csharppublic interface IRepository<T> where T : class
{
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
}
3. Unit of Work Pattern
Ensures transactional consistency across multiple repositories:
csharppublic interface IUnitOfWork : IDisposable
{
    IRepository<Product> Products { get; }
    IRepository<Order> Orders { get; }
    Task<int> SaveAsync();
}
```

---

## 📊 Database Schema

### Entity Relationship Diagram
```
┌─────────────────┐       ┌──────────────────┐       ┌─────────────────┐
│  ApplicationUser│       │     Category     │       │   Ingredient    │
├─────────────────┤       ├──────────────────┤       ├─────────────────┤
│ Id (PK)         │       │ Id (PK)          │       │ Id (PK)         │
│ UserName        │       │ Name             │       │ Name            │
│ Email           │       │ Description      │       │ Stock           │
│ Role            │       └──────────────────┘       │ Unit            │
└─────────────────┘                │                 └─────────────────┘
         │                         │                          │
         │                         │                          │
         │              ┌──────────▼──────────┐               │
         │              │      Product        │               │
         │              ├─────────────────────┤               │
         │              │ Id (PK)             │               │
         │              │ Name                │               │
         │              │ Description         │               │
         │              │ Price               │               │
         │              │ Stock               │               │
         │              │ ImageUrl            │               │
         │              │ CategoryId (FK)     │               │
         │              └─────────────────────┘               │
         │                         │                          │
         │                         │                          │
         │              ┌──────────▼──────────────────────────▼────┐
         │              │       ProductIngredient (Join Table)     │
         │              ├──────────────────────────────────────────┤
         │              │ ProductId (PK, FK)                       │
         │              │ IngredientId (PK, FK)                    │
         │              │ Quantity                                 │
         │              └──────────────────────────────────────────┘
         │                                    
┌────────▼────────┐              ┌──────────────────┐
│     Order       │              │    OrderItem     │
├─────────────────┤              ├──────────────────┤
│ Id (PK)         │◄─────────────│ Id (PK)          │
│ UserId (FK)     │              │ OrderId (FK)     │
│ OrderDate       │              │ ProductId (FK)   │
│ TotalAmount     │              │ Quantity         │
│ Status          │              │ PriceAtOrder     │
└─────────────────┘              └──────────────────┘
Key Tables
ApplicationUser (extends IdentityUser)
Custom user entity with additional properties for restaurant-specific features.
Product
ColumnTypeDescriptionIdintPrimary KeyNamenvarchar(100)Product nameDescriptionnvarchar(500)Product descriptionPricedecimal(18,2)Current priceStockintAvailable quantityImageUrlnvarchar(255)Product image pathCategoryIdintForeign Key to Category
Ingredient
ColumnTypeDescriptionIdintPrimary KeyNamenvarchar(100)Ingredient nameStockintAvailable quantityUnitnvarchar(20)Measurement unit
ProductIngredient (Many-to-Many Join Table)
ColumnTypeDescriptionProductIdintComposite PK, FK to ProductIngredientIdintComposite PK, FK to IngredientQuantitydecimal(10,2)Amount needed per product
Order & OrderItem
Orders store a price snapshot at the time of purchase in OrderItem.PriceAtOrder to maintain historical accuracy, even if product prices change later.

🚀 Getting Started
Prerequisites
Before you begin, ensure you have the following installed:

Visual Studio 2022 (Community, Professional, or Enterprise)
.NET 8 SDK
SQL Server 2019+ (Express, Developer, or LocalDB)
Git

Installation

Clone the repository

bashgit clone https://github.com/gaurav1Nn/Restaurant_Project.git
cd Restaurant_Project

Update Connection String

Open appsettings.json and modify the connection string to match your SQL Server instance:
json{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=RestaurantDB;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}

Restore NuGet Packages

bashdotnet restore

Apply Database Migrations

Using Package Manager Console in Visual Studio:
powershellAdd-Migration InitialCreate
Update-Database
Or using .NET CLI:
bashdotnet ef migrations add InitialCreate
dotnet ef database update

Seed Initial Data (Optional)

Run the application once to seed default roles and an admin user, or manually execute the seed data script.

Run the Application

Press F5 in Visual Studio or use:
bashdotnet run
```

The application will launch at `https://localhost:5001` (or the port specified in `launchSettings.json`).

### Default Credentials

After seeding, you can log in with:

- **Admin Account**
  - Email: `admin@restaurant.com`
  - Password: `Admin@123`

- **Customer Account**
  - Email: `customer@restaurant.com`
  - Password: `Customer@123`

---

## 📁 Project Structure
```
Restaurant_Project/
│
├── Controllers/           # MVC Controllers
│   ├── HomeController.cs
│   ├── ProductController.cs
│   ├── OrderController.cs
│   └── CartController.cs
│
├── Models/               # Domain Models & ViewModels
│   ├── Product.cs
│   ├── Ingredient.cs
│   ├── Order.cs
│   └── ViewModels/
│
├── Data/                 # Database Context & Migrations
│   ├── ApplicationDbContext.cs
│   └── Migrations/
│
├── Repositories/         # Repository Pattern Implementation
│   ├── IRepository.cs
│   ├── Repository.cs
│   └── IUnitOfWork.cs
│
├── Views/                # Razor Views
│   ├── Home/
│   ├── Product/
│   ├── Order/
│   └── Shared/
│
├── wwwroot/              # Static Files
│   ├── css/
│   ├── js/
│   └── images/
│
├── appsettings.json      # Configuration
├── Program.cs            # Application Entry Point
└── README.md

📸 Screenshots

Add screenshots of your application here to showcase the UI and features


🤝 Contributing
Contributions are welcome! If you'd like to improve this project:

Fork the repository
Create a feature branch (git checkout -b feature/AmazingFeature)
Commit your changes (git commit -m 'Add some AmazingFeature')
Push to the branch (git push origin feature/AmazingFeature)
Open a Pull Request

Please make sure to update tests as appropriate and follow the existing code style.

📄 License
This project is licensed under the MIT License - see the LICENSE file for details.

📧 Contact
Gaurav - @gaurav1Nn
Project Link: https://github.com/gaurav1Nn/Restaurant_Project

🙏 Acknowledgments

ASP.NET Core Documentation
Entity Framework Core
Bootstrap
Font Awesome


<div align="center">
⭐ Star this repository if you found it helpful!
Made with ❤️ by Gaurav
</div>
