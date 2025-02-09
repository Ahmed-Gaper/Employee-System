using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace EmployeeLibrary
{
    public class EmployeeDataAccess
    {
        private readonly string connectionString;

        public EmployeeDataAccess(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void InsertEmployee(Employee employee)
        {
            string query = "INSERT INTO Employees (Name, Salary, Gender, Age) VALUES (@Name, @Salary, @Gender, @Age)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Name", employee.Name);
                command.Parameters.AddWithValue("@Salary", employee.Salary);
                command.Parameters.AddWithValue("@Gender", (int)employee.Gender);
                command.Parameters.AddWithValue("@Age", employee.Age);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public List<Employee> GetEmployees(string orderBy = "")
        {
            List<Employee> employees = new List<Employee>();
            string query = "SELECT ID, Name, Salary, Gender, Age FROM Employees";

            if (!string.IsNullOrEmpty(orderBy))
            {
                query += " ORDER BY " + orderBy;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["ID"]);
                        string name = reader["Name"].ToString();
                        double salary = Convert.ToDouble(reader["Salary"]);
                        int genderInt = Convert.ToInt32(reader["Gender"]);
                        int age = Convert.ToInt32(reader["Age"]);

                        Gender gender = (Gender)genderInt;
                        Employee employee = new Employee(id, name, salary, gender, age);
                        employees.Add(employee);
                    }
                }
            }
            return employees;
        }

        public Employee SearchEmployee(string searchTerm)
        {
            string query = "SELECT ID, Name, Salary, Gender, Age FROM Employees " +
                           "WHERE ID = @Id OR Name LIKE @Name";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                if (int.TryParse(searchTerm, out int id))
                {
                    command.Parameters.AddWithValue("@Id", id);
                }
                else
                {
                    command.Parameters.AddWithValue("@Id", DBNull.Value);
                }
                command.Parameters.AddWithValue("@Name", "%" + searchTerm + "%");

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int empId = Convert.ToInt32(reader["ID"]);
                        string name = reader["Name"].ToString();
                        double salary = Convert.ToDouble(reader["Salary"]);
                        int genderInt = Convert.ToInt32(reader["Gender"]);
                        int age = Convert.ToInt32(reader["Age"]);
                        Gender gender = (Gender)genderInt;
                        return new Employee(empId, name, salary, gender, age);
                    }
                }
            }
            return null;
        }
    }
}
