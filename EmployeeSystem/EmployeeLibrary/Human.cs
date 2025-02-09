using System;
using System.Reflection;

namespace EmployeeLibrary
{
    public class Human
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; }

        public Human(string name, int age, Gender gender)
        {
            Name = name;
            Age = age;
            Gender = gender;
        }

        public override string ToString()
        {
            return $"Name: {Name}, Age: {Age}, Gender: {Gender}";
        }
    }
}
