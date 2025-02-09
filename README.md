# Employee-System

A simple C# console application that connects to an MS SQL Server database to manage employee records. The application demonstrates how to perform CRUD operations using ADO.NET and provides a menu-driven interface for user interaction.

## Setup Instructions

### 1. Clone the Repository

Clone this repository to your local machine using the following commands:

```bash
git clone https://github.com/yourusername/EmployeeManagement.git
cd EmployeeManagement
```

## 2. Set Up the Database
1. Open SQL Server Management Studio (SSMS).
 2. Execute the following SQL script to create the database and table:
```SQL
CREATE DATABASE EmployeeDB;

USE EmployeeDB;

CREATE TABLE Employees (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Salary FLOAT NOT NULL,
    Gender INT NOT NULL,  
    Age INT NOT NULL
);
```
## 3. Verify the Connection String
In Program.cs, the connection string is set as follows:
```csharp
string connectionString = "Server=.\\SQLEXPRESS;Database=EmployeeDB;Trusted_Connection=True;";
```
If your SQL Server instance or authentication method differs, update the connection string accordingly.