using AO;
using AO.Finance;
HRList Emp = new HRList();
var finance = new Finance(new HRList());
Emp.AddEmployee();
Emp.FireEmployee();
Emp.SearchEmployee();

Console.WriteLine("Enter the hours worked: ");
int hoursWorked = int.Parse(Console.ReadLine());
Console.WriteLine("Enter the employee ID: ");
string employeeID = Console.ReadLine();
finance.PrintSalary(hoursWorked, employeeID);
