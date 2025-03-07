# Flowspire
![Status](https://img.shields.io/badge/Status-In%20Development-yellow?style=flat-square)

**Flowspire** is a modern personal finance management API designed to help users track income and expenses, create budgets, and interact with financial advisors in real-time. Built with **Clean Architecture** and **Domain-Driven Design (DDD)** principles, the application provides a secure, scalable foundation for both users and financial advisors.

## 🎯 Project Overview
Flowspire aims to provide a robust and intuitive platform that enables:
- Comprehensive financial management with customizable categories, reports, and budgets
- Real-time communication between customers and financial advisors through an integrated chat system
- Secure authentication using JWT tokens with refresh token capability
- Role-based access control (Administrator, FinancialAdvisor, Customer)
- Enhanced user experience with real-time notifications and advanced validations

## 🛠️ Technology Stack
- **Backend Framework**: .NET 8.0
- **ORM**: Entity Framework Core with SQLite database
- **Authentication**: ASP.NET Identity with JWT tokens
- **Real-time Communication**: SignalR for chat and notifications
- **API Documentation**: Swagger/OpenAPI
- **Validation**: FluentValidation
- **Database**: SQLite (lightweight, perfect for development)

## 🏗️ Architecture
The project follows **Clean Architecture** principles with a clear separation of concerns:

```
Flowspire/
├── Flowspire.API             # Presentation layer (Controllers, Hubs)
├── Flowspire.Application     # Application layer (Services, DTOs)
├── Flowspire.Domain         # Domain layer (Entities, Interfaces, Hubs)
├── Flowspire.Infra         # Infrastructure layer (Repositories, DB Context)
└── Flowspire.Infra.IoC     # Dependency Injection configuration
```

### Key Features
- **User Management**: Registration, authentication, and role-based authorization
- **Financial Management**: Transactions, categories, and budgets
- **Advisor Integration**: Customer-advisor relationships and communication
- **Real-time Chat**: Integrated messaging system between customers and advisors
- **Financial Reports**: Comprehensive financial analysis and reporting

## 🚀 Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Visual Studio 2022 or another compatible IDE
- SQLite (included, no additional installation required)

### Installation

1. **Clone the Repository**
   ```bash
   git clone https://github.com/pedrolustosa/Flowspire.git
   cd Flowspire
   ```

2. **Restore Dependencies**
   ```bash
   dotnet restore
   ```

3. **Set Up the Database**
   ```bash
   dotnet ef migrations add InitialMigration --project Flowspire.Infra --startup-project Flowspire.API
   dotnet ef database update --project Flowspire.Infra --startup-project Flowspire.API
   ```

4. **Run the Application**
   ```bash
   cd Flowspire.API
   dotnet run
   ```

5. **Access the API**
   - Swagger UI: `https://localhost:5240/swagger`
   - API Endpoints: `https://localhost:5240/api`

## 🤝 Contributing
1. Fork the repository
2. Create your feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📄 License
This project is licensed under the MIT License - see the [LICENSE](LICENSE.txt) file for details.

## 🔗 Links
- [Documentation](https://github.com/pedrolustosa/Flowspire/wiki)
- [Issue Tracker](https://github.com/pedrolustosa/Flowspire/issues)

---
Built with ❤️ by [Pedro Lustosa](https://github.com/pedrolustosa)