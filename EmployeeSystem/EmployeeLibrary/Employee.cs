using System;

namespace EmployeeLibrary
{
    public class Employee : IComparable<Employee>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Salary { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; set; }

        public Employee(string name, double salary, Gender gender, int age)
        {
            Name = name;
            Salary = salary;
            Gender = gender;
            Age = age;
        }

        public Employee(int id, string name, double salary, Gender gender, int age)
        {
            ID = id;
            Name = name;
            Salary = salary;
            Gender = gender;
            Age = age;
        }

        public int CompareTo(Employee other)
        {
            return ID.CompareTo(other.ID);
        }

        public override string ToString()
        {
            return $"ID: {ID} | Name: {Name} | Salary: {Salary} | Gender: {Gender} | Age: {Age}";
        }
    }
}
