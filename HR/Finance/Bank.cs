using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AO.Finance
{
    public class Bank
    {
        private Finance _finance;
        private HRList _hrList;

        public Bank(Finance finance, HRList hrList)
        {
            _finance = finance;
            _hrList = hrList;
        }

        public void WithdrawSalary(string employeeID, int hoursWorked)
        {
            List<string> employeeInfo = _hrList.GetEmployeeInfo(employeeID);
            string employeeName = employeeInfo[0];
            string employeeBankCardNumber = employeeInfo[1];

            double salary = _finance.CalculateSalary(hoursWorked, employeeID);

            Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} - Transferring ${salary} from the company bank account to account ${employeeBankCardNumber} for employee {employeeName} with employee ID {employeeID}.");
        }
    }
}
