using EmployeeEFCore.Contexts;
using EmployeeEFCore.Models;
using Microsoft.EntityFrameworkCore;

using AppDbContext db = new AppDbContext();

while (true)
{
    Console.WriteLine("\n===== MENU =====");
    Console.WriteLine("1. Department yarat");
    Console.WriteLine("2. Department sil");
    Console.WriteLine("3. Departmentleri goster");
    Console.WriteLine("4. Employee yarat");
    Console.WriteLine("5. Employee sil");
    Console.WriteLine("6. Employeeleri goster");
    Console.WriteLine("0. Cixis");

    Console.Write("Secim: ");
    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            bool flowControl = CreateDepartment(db);
            if (!flowControl)
            {
                break;
            }
            break;
        case "2":
            DeleteDepartment(db);
            break;
        case "3":
            ShowDepartments(db);
            break;
        case "4":
            (bool? flowControl, object value) = NewMethod(db);
            if (flowControl == false)
            {
                break;
            }
            else if (flowControl == true)
            {
                return value;
            }

            break;
        case "5": 
            
            break;
        case "6":
            ShowEmployees(db);
            break;
        case "0": return;
        default: Console.WriteLine("Yanlis secim!"); break;
    }

}

static void ShowDepartments(AppDbContext db)
{
    var departments = db.Departments.ToList();
    Console.WriteLine("Departments:");
    foreach (var d in departments)
    {
        Console.WriteLine($"Id: {d.Id}, Name: {d.Name}, Capacity: {d.Capacity}");
    }
}

static bool CreateDepartment(AppDbContext db)
{
    Console.WriteLine("Department adi:");
    string name = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(name))
    {
        Console.WriteLine("Zehmet olmasa duzgun ad daxil edin:");
        return false;
    }
    Console.WriteLine("Capacity:");
    int capacity;
    while (!int.TryParse(Console.ReadLine(), out capacity))
    {
        Console.WriteLine("Zehmet olmasa duzgun capacity daxil edin:");
    }

    Department department = new Department
    {
        Name = name,
        Capacity = capacity
    };

    db.Departments.Add(department);
    db.SaveChanges();

    Console.WriteLine("Department yaradildi.");
    return true;
}

static void DeleteDepartment(AppDbContext db)
{
    ShowDepartments(db);
    Console.WriteLine("Silmek istediyiniz departmentin ID-ni daxil edin");
    int deptId;
    while (!int.TryParse(Console.ReadLine(), out deptId))
    {
        Console.WriteLine("Zehmet olmasa duzgun ID daxil edin:");
    }
    var deptToDelete = db.Departments.FirstOrDefault(d => d.Id == deptId);
    if (deptToDelete != null)
    {
        db.Departments.Remove(deptToDelete);
        db.SaveChanges();
        Console.WriteLine("Department silindi.");
    }
    else
    {
        Console.WriteLine("Department tapilmadi.");
    }
}

static void ShowEmployees(AppDbContext db)
{
    var employees = db.Employees.Include(x => x.Department).ToList();
    employees.ForEach(e => Console.WriteLine(e));
    Console.WriteLine();
}

static (bool? flowControl, object value) NewMethod(AppDbContext db)
{
    ShowDepartments(db);
    Console.WriteLine("Departmenti daxi edin");
    string deptInput = Console.ReadLine();
    if (string.IsNullOrEmpty(deptInput = Console.ReadLine()))
    {
        Console.WriteLine("Zehmet olmasa düzgün daxil edin");
    }
    Console.WriteLine("Employee adi: ");
    string name;
    if (string.IsNullOrWhiteSpace(name = Console.ReadLine()))
    {
        Console.WriteLine("Zehmet olmasa duzgun ad daxil edin:");
        return (flowControl: false, value: null);
    }

    Console.WriteLine("Salary: ");
    decimal salary;
    while (!decimal.TryParse(Console.ReadLine(), out salary))
    {
        Console.WriteLine("Zehmet olmasa duzgun salary daxil edin:");
    }

    Console.WriteLine("Department Id: ");
    int depId;
    while (!int.TryParse(Console.ReadLine(), out depId))
    {
        Console.WriteLine("Zehmet olmasa duzgun Department ID daxil edin:");
    }

    Department dep = db.Departments.Find(depId);

    int currentCount = db.Employees.Count(e => e.DepartmentId == depId);
    if (currentCount >= dep.Capacity)
    {
        Console.WriteLine("Bu department doludur. Employee elave etmek olmaz");
        return true;
    }

    Employee emp = new Employee
    {
        Name = name,
        Salary = salary,
        DepartmentId = depId
    };

    db.Employees.Add(emp);
    db.SaveChanges();
    Console.WriteLine("Employee yaradildi.");
    return (flowControl: null, value: null);
}