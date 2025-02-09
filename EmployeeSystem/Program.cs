using System;
using System.Collections.Generic;
using EmployeeLibrary;    

class Program
{
    public static void Main()
    {
        Console.CursorVisible = false;
        string connectionString = "Server=.\\SQLEXPRESS;Database=EmployeeDB;Trusted_Connection=True;";

        EmployeeDataAccess dataAccess = new EmployeeDataAccess(connectionString);

        int selectedIndex = 0;
        string[] options = { " New ", " Display ", " Sort by ID ", " Sort by Salary ", " Search ", " Exit " };

        while (true)
        {
            Console.Clear();
            int centerX = (120 / 2) - 10;
            int startY = (30 / 2) - (options.Length / 2);

            for (int i = 0; i < options.Length; i++)
            {
                Console.SetCursorPosition(centerX, startY + i * 3);
                if (i == selectedIndex)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine(options[i]);
                Console.ResetColor();
            }

            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key == ConsoleKey.UpArrow && selectedIndex > 0) selectedIndex--;
            else if (key.Key == ConsoleKey.DownArrow && selectedIndex < options.Length - 1) selectedIndex++;
            else if (key.Key == ConsoleKey.Enter)
            {
                Console.Clear();
                Console.CursorVisible = true;

                if (selectedIndex == 0) 
                {
                    try
                    {
                        Console.WriteLine("Enter Employee Details:");
                        Console.Write("Name: ");
                        string name = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(name))
                            throw new ArgumentException("Name cannot be empty.");

                        Console.Write("Salary: ");
                        if (!double.TryParse(Console.ReadLine(), out double salary))
                            throw new FormatException("Invalid salary format.");

                        Console.Write("Gender (0 for Male, 1 for Female): ");
                        if (!int.TryParse(Console.ReadLine(), out int genderInput) || (genderInput != 0 && genderInput != 1))
                            throw new FormatException("Invalid gender input.");
                        Gender gender = (Gender)genderInput;

                        Console.Write("Age: ");
                        if (!int.TryParse(Console.ReadLine(), out int age) || age <= 0)
                            throw new FormatException("Invalid age input.");

                        Employee employee = new Employee(name, salary, gender, age);
                        dataAccess.InsertEmployee(employee);
                        Console.WriteLine("\nEmployee Data Saved! Press any key to return...");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\nError: {ex.Message}");
                        Console.WriteLine("Please try again. Press any key to continue...");
                    }
                    Console.ReadKey();
                }
                else if (selectedIndex == 1) 
                {
                    List<Employee> employees = dataAccess.GetEmployees();
                    foreach (var emp in employees)
                    {
                        Console.WriteLine(emp);
                    }
                    Console.WriteLine("\nPress any key to return...");
                    Console.ReadKey();
                }
                else if (selectedIndex == 2)
                {
                    List<Employee> employees = dataAccess.GetEmployees("ID");
                    Console.WriteLine("Sorted by ID:");
                    foreach (var emp in employees)
                    {
                        Console.WriteLine(emp);
                    }
                    Console.WriteLine("\nPress any key to return...");
                    Console.ReadKey();
                }
                else if (selectedIndex == 3) 
                {
                    List<Employee> employees = dataAccess.GetEmployees("Salary");
                    Console.WriteLine("Sorted by Salary:");
                    foreach (var emp in employees)
                    {
                        Console.WriteLine(emp);
                    }
                    Console.WriteLine("\nPress any key to return...");
                    Console.ReadKey();
                }
                else if (selectedIndex == 4) //
                {
                    Console.Write("Enter ID or Name to search: ");
                    string input = Console.ReadLine();
                    Employee employee = dataAccess.SearchEmployee(input);
                    if (employee != null)
                        Console.WriteLine(employee);
                    else
                        Console.WriteLine("Employee not found.");
                    Console.WriteLine("\nPress any key to return...");
                    Console.ReadKey();
                }
                else if (selectedIndex == 5) 
                {
                    return;
                }

                Console.CursorVisible = false;
            }
        }
    }
}
