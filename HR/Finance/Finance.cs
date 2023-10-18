using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AO.Finance
{
    public class Finance
    {
        public double Budget = 180000000;

        private readonly HRList _hrList;
        public Finance(HRList hrList)
        {
            _hrList = hrList;
        }

        public double CalculateSalary(int hoursWorked, string employeeID)
        {
            int employeeIndex = _hrList.EmployeeIDs.IndexOf(employeeID);
            if (employeeIndex >= 0)
            {
                double hourlyWage = 45.43;
                double salary = hoursWorked * hourlyWage;
                Budget = Budget - salary;
                
                return salary;
            }
            return 0;
        }

        public void PrintSalary(int hoursWorked, string employeeID)
        {
            if (_hrList == null)
            {
                throw new Exception("The List field is null.");
            }
            double salary = CalculateSalary(hoursWorked, employeeID);
            List<string> employeeInfo = _hrList.GetEmployeeInfo(employeeID);

            string employeeName = employeeInfo[0];
            string employeeBankCardNumber = employeeInfo[1];
            Console.WriteLine("The salary for employee ID {0} ({1}) is {2}$ - bank card number : {3}", employeeID, employeeName, salary, employeeBankCardNumber);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Budget: -{salary}$");
            Console.ResetColor();
        }

    }
}
