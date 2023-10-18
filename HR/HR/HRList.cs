using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using ConsoleTables;

namespace AO
{
    public class HRList : List<HR>
    {
        public List<string> Names { get; set; }
        public List<string> PhoneNumbers { get; set; }
        public List<string> BankCardNumbers { get; set; }
        public List<string> NationalIDs { get; set; }
        public List<string> Addresses { get; set; }
        public List<string> EmployeeIDs { get; set; }

        public HRList()
        {
            Names = new List<string>();
            PhoneNumbers = new List<string>();
            BankCardNumbers = new List<string>();
            NationalIDs = new List<string>();
            Addresses = new List<string>();
            EmployeeIDs = new List<string>();
        }

        public void AddEmployee()
        {
            bool continueAdding = true;

            while (continueAdding)
            {
                Console.WriteLine("Enter your name:");
                string name = Console.ReadLine();

                Console.WriteLine("Enter your nation ID:");
                string nationalID = Console.ReadLine();
            
                Console.WriteLine("Enter your phone number:");
                string phoneNumber = Console.ReadLine();
                if (!ValidatePhoneNumber(phoneNumber))
                {
                    Console.WriteLine("The phone number is not valid.");
                    return;
                }

                Console.WriteLine("Enter your bank card number:");
                string bankCardNumber = Console.ReadLine();
                if (!ValidateBankCardNumber(bankCardNumber))
                {
                    Console.WriteLine("The bank card number is not valid.");
                    return;
                }


                Console.WriteLine("Enter your address:");
                string address = Console.ReadLine();

                Names.Add(name);
                PhoneNumbers.Add(phoneNumber);
                BankCardNumbers.Add(bankCardNumber);
                NationalIDs.Add(nationalID);
                Addresses.Add(address);
                EI();
               
                Console.WriteLine("Do you want to add another employee? (Y/N)");
                string answer = KeyboardLock.GetYesNoAnswer();
                if (answer == "Y")
                {
                    continueAdding = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("The csv file was created !");
                    Console.ResetColor();
                    ExportToCSV();
                    continueAdding = false;
                }  
            }  
        }
        public void FireEmployee()
        {
            while (true)
            {
                Console.WriteLine("Enter the employee ID: ");
                string employeeID = Console.ReadLine();
                int employeeIndex = EmployeeIDs.IndexOf(employeeID);
                Console.WriteLine("Are you sure you want to fire {0}?", Names[employeeIndex]);
                string answer = Console.ReadLine();
                if (answer != "Y")
                {
                    Console.WriteLine("Employee was not fired.");
                    return;
                }
                if (employeeIndex >= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{Names[employeeIndex]} with employee ID ({employeeID}) and national ID ({NationalIDs[employeeIndex]}) has been fired.");
                    Console.ResetColor();
                    string nId = NationalIDs[employeeIndex];
                    //UpdateCSVFile(nId);
                    Names.RemoveAt(employeeIndex);
                    PhoneNumbers.RemoveAt(employeeIndex);
                    BankCardNumbers.RemoveAt(employeeIndex);
                    NationalIDs.RemoveAt(employeeIndex);
                    Addresses.RemoveAt(employeeIndex);
                    EmployeeIDs.RemoveAt(employeeIndex);
                    break;
                }
                else
                {
                    Console.WriteLine("Employee with employee ID {0} not found.", employeeID);
                }
            }
            

        }
        public void SearchEmployee()
        {
            Console.WriteLine("Enter the employee ID: ");
            string employeeID = Console.ReadLine();
            int employeeIndex = EmployeeIDs.IndexOf(employeeID);

            if (employeeIndex >= 0)
            {
                Console.WriteLine("Employee Information:");
                var table = new ConsoleTable("Name", "Phone Number", "National ID", "Employee ID", "Bank Card Number", "Address");
                table.AddRow(Names[employeeIndex], PhoneNumbers[employeeIndex],
                    NationalIDs[employeeIndex], employeeID, BankCardNumbers[employeeIndex], Addresses[employeeIndex]);
                Console.WriteLine(table);
            }
            else
            {
                Console.WriteLine($"Employee with employee ID {0} not found.", employeeID);
            }
        }
        public List<string> GetEmployeeInfo(string employeeID)
        {
            int employeeIndex = Names.IndexOf(employeeID);
            if (employeeIndex >= 0)
            {
                return new List<string> { Names[employeeIndex], BankCardNumbers[employeeIndex], PhoneNumbers[employeeIndex] };
            }
            return null;
        }

        private bool ValidatePhoneNumber(string phoneNumber)
        {
            if (phoneNumber.Length != 11)
            {
                return false;
            }
            Regex regex = new Regex(@"^\d{11}$");
            if (!regex.IsMatch(phoneNumber))
            {
                return false;
            }

            return true;
        }
        private bool ValidateBankCardNumber(string bankCardNumber)
        {
            if (bankCardNumber.Length != 16)
            {
                return false;
            }

            Regex regex = new Regex(@"^\d{16}$");
            if (!regex.IsMatch(bankCardNumber))
            {
                return false;
            }

            return true;
        }
        private void EI()
        {
            Random random = new Random();
            while (true)
            {
                int randomNumber = random.Next(100000, 999999);
                string randomNumberString = randomNumber.ToString();
                string employeeID = "11" + randomNumberString;
                if (!EmployeeIDs.Contains(employeeID))
                {
                    EmployeeIDs.Add(employeeID);
                    Console.WriteLine($"Your Employee ID : {employeeID}");
                    break;
                }
            }
        }
        private void ExportToCSV(string filePath = "")
        {
            if (filePath == "")
            {
                filePath = Path.Combine(@"C:\", "hr_list.csv");
            }
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Name,PhoneNumber,BankCardNumber,NationalID,Address,EmployeeID");

                for (int i = 0; i < Names.Count; i++)
                {
                    writer.WriteLine($"{Names[i]},{PhoneNumbers[i]},{BankCardNumbers[i]},{NationalIDs[i]},{Addresses[i]},{EmployeeIDs[i]}");
                }
            }
        }
        private static void UpdateCSVFile(string nId, string filePath = "")
        {
            if (filePath == "")
            {
                filePath = Path.Combine(@"C:\", "hr_list.csv");
            }

            using (StreamReader reader = new StreamReader(filePath))
            {
                string[] lines = reader.ReadToEnd().Split('\n');

                int employeeLine = -1;
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Split(',')[3].Equals(nId))
                    {
                        employeeLine = i;
                        break;
                    }
                }

                if (employeeLine != -1 && lines.Length >= 5)
                {
                    for (int i = 0; i < lines[employeeLine].Split(',').Length; i++)
                    {
                        lines[employeeLine] = lines[employeeLine].Replace(lines[employeeLine].Split(',')[i], "<font color=\"red\">" + lines[employeeLine].Split(',')[i] + "</font>");
                    }
                }

                using (StreamWriter writer = new StreamWriter(Path.Combine(filePath)))
                {
                    writer.WriteLine(string.Join("\n", lines));
                }
            }
                
        }

    }
}

