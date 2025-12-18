using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeEFCore.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public override string ToString()
        {
            return $"{Id}. {Name} - {Salary} - {Department.Name}";
        }

    }
}

