using System;
using DbKursovaya.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;


namespace DbKursovaya
{
    class Program
    {
        static private SalaryContext _context = new SalaryContext();

        static void Main(string[] args)
        {
            Console.WriteLine("Меню:");
            Console.WriteLine("1) Выборка");
            Console.WriteLine("2) Добавление");
            Console.WriteLine("3) Редактирование");
            Console.WriteLine("4) Удаление");
            Console.WriteLine("5) Выбрать клиентов приглашенных сотрудником по имени 'Леопольд'");
            Console.WriteLine("6) Поднять зарплату до 80000 всем, у кого она ниже 80000");
            Console.WriteLine("7) Выход");
            Console.Write("Выберите пункт меню: ");
            string option = Console.ReadLine();
            while (option != "7")
            {
                switch (option)
                {
                    case "1":
                        Console.WriteLine("Какую таблицу выбрать ? (клиенты, сотрудники, организации, зарплаты, связь клиенты с сотрудниками)\n");
                        string selectOption = Console.ReadLine();
                        SelectWhichTable(selectOption);
                        break;

                    case "2":
                        Console.WriteLine("В какую таблицу добавить запись ? (клиенты, сотрудники, организации, зарплаты, связь клиенты с сотрудниками)\n");
                        string addOption = Console.ReadLine();
                        AddToWhichTable(addOption);
                        break;

                    case "3":
                        Console.WriteLine("В какой таблице обновить запись ? (клиенты, сотрудники, организации, зарплаты, связь клиенты с сотрудниками)\n");
                        string updateOption = Console.ReadLine();
                        UpdateWhichTable(updateOption);
                        break;

                    case "4":
                        Console.WriteLine("Из какой таблицы удалить запись ? (клиенты, сотрудники, организации, зарплаты, связь клиенты с сотрудниками)\n");
                        string deleteOption = Console.ReadLine();
                        DeleteFromWhichTable(deleteOption);
                        break;

                    case "5":
                        Console.WriteLine(SelectClientsOfLeopold());
                        break;

                    case "6":
                        RaiseSalary();
                        break;

                    default:
                        Console.WriteLine("Нет такой таблицы!");
                        break;
                }
                Console.WriteLine();
                Console.WriteLine("Меню:");
                Console.WriteLine("1) Выборка");
                Console.WriteLine("2) Добавление");
                Console.WriteLine("3) Редактирование");
                Console.WriteLine("4) Удаление");
                Console.WriteLine("5) Выбрать клиентов приглашенных сотрудником по имени 'Леопольд'");
                Console.WriteLine("6) Поднять зарплату до 80000 всем, у кого она ниже 80000");
                Console.WriteLine("7) Выход");
                Console.Write("Выберите пункт меню: ");
                option = Console.ReadLine();
            }
        }

        static void RaiseSalary()
        {
            var employees = _context.Employees
                .Where(e => e.SalaryFixed < 80000)
                .ToList();

            foreach (var e in employees)
            {
                e.SalaryFixed = 80000;
            }

            _context.SaveChanges();
        }

        static string SelectClientsOfLeopold()
        {           
            var sb = new StringBuilder();

            var employeeClients = _context.EmployeeClients
                .Include(ec => ec.Employee)
                .Include(ec => ec.Client)
                .Where(ec => ec.Employee.Name.Equals("Леопольд"))
                .ToList();

            foreach(var ec in employeeClients)
            {
                sb.AppendLine($"Id_Клиента:{ec.ClientId}, Имя_клиента:{ec.Client.Name}, " +
                    $"Id_сотрудника:{ec.EmployeeId}, Имя_сотрудника:{ec.Employee.Name};");
            }

            return sb.ToString().TrimEnd();
        }

        static void SelectWhichTable(string selectOption)
        {
            switch(selectOption)
            {
                case "клиенты":
                    Console.WriteLine(SelectAllClients());
                    break;

                case "сотрудники":
                    Console.WriteLine(SelectAllEmployees());
                    break;

                case "организации":
                    Console.WriteLine(SelectAllOrganizations());
                    break;

                case "зарплаты":
                    Console.WriteLine(SelectAllSalaries());
                    break;

                case "связь клиенты с сотрудниками":
                    Console.WriteLine(SelectAllEmployeeClient());
                    break;

                default:
                    Console.WriteLine("Нет такой таблицы!");
                    break;

            }
        }

        static void UpdateWhichTable(string updateOption)
        {
            switch (updateOption)
            {
                case "клиенты":
                    Console.Write("Введите Id клиента: ");
                    int clientId = int.Parse(Console.ReadLine());
                    Console.WriteLine("Если поле менять не нужно, то введите 'не менять'.");
                    Console.Write("Введите новое имя клиента: ");
                    string clientName = Console.ReadLine();
                    UpdateClient(clientId, clientName);
                    Console.WriteLine("Клиент успешно обновлён!");
                    break;

                case "сотрудники":
                    Console.Write("Введите Id сотрудника: ");
                    int empId = int.Parse(Console.ReadLine());
                    Console.WriteLine("Если поле менять не нужно, то введите 'не менять'.");
                    Console.Write("Введите новое имя сотрудника: ");
                    string empName = Console.ReadLine();
                    Console.Write("Введите новую зарплату сотрудника: ");
                    string salary = Console.ReadLine();
                    Console.Write("Введите новую должность сотрудника: ");
                    string position = Console.ReadLine();
                    Console.Write("Введите новый Id организации сотрудника: ");
                    string orgId = Console.ReadLine();
                    UpdateEmployee(empId, salary, empName, position, orgId);
                    Console.WriteLine("Сотрудник успешно обновлён!");
                    break;

                case "организации":
                    Console.Write("Введите Id организации: ");
                    int organizationId = int.Parse(Console.ReadLine());
                    Console.WriteLine("Если поле менять не нужно, то введите 'не менять'.");
                    Console.Write("Введите новое название организации: ");
                    string orgName = Console.ReadLine();
                    UpdateOrganization(organizationId, orgName);
                    Console.WriteLine("Организация успешно обновлена!");
                    break;

                case "зарплаты":
                    Console.Write("Введите Id зарплаты: ");
                    int salaryId = int.Parse(Console.ReadLine());
                    Console.WriteLine("Если поле менять не нужно, то введите 'не менять'.");
                    Console.Write("Введите новый налог (в процентах) на зарплату: ");
                    string tax = Console.ReadLine();
                    Console.Write("Введите новый размер зарплаты: ");
                    string sum = Console.ReadLine();
                    Console.Write("Введите новый Id_сотрудника: ");
                    string employeeId = Console.ReadLine();
                    Console.Write("Введите новые дату и время (гггг-ММ-дд ЧЧ:мм) выдачи зарплаты: ");
                    string paymentDateTime = Console.ReadLine();
                    UpdateSalary(salaryId, tax, sum, employeeId, paymentDateTime);
                    Console.WriteLine("Зарплата успешно обновлена!");
                    break;

                case "связь клиенты с сотрудниками":
                    Console.Write("Введите Id сотрудника: ");
                    int eId = int.Parse(Console.ReadLine());
                    Console.Write("Введите Id клиента: ");                   
                    int сId = int.Parse(Console.ReadLine());
                    Console.WriteLine("Если поле менять не нужно, то введите 'не менять'.");
                    Console.Write("Введите новые дату и время (гггг-ММ-дд ЧЧ:мм) привлечения клиента сотрудником: ");
                    string attractingDate = Console.ReadLine();
                    Console.Write("Введите новые дату и время (гггг-ММ-дд ЧЧ:мм) сделки: ");
                    string dealDate = Console.ReadLine();
                    Console.Write("Введите новую сумму сделки: ");
                    string dealsum = Console.ReadLine();
                    Console.Write("Введите новый процент сотруднику со сделки: ");
                    string dealPercentage = Console.ReadLine();
                    UpdateEmployeeClient(eId, сId, attractingDate, dealsum, dealPercentage, dealDate);
                    Console.WriteLine("Связь клиенты с сотрудниками успешно обновлена!");
                    break;

                default:
                    Console.WriteLine("Нет такой таблицы!");
                    break;

            }
        }


        static void AddToWhichTable(string addOption)
        {
            switch (addOption)
            {
                case "клиенты":
                    Console.Write("Введите Id клиента: ");
                    int clientId = int.Parse(Console.ReadLine());
                    Console.Write("Введите имя клиента: ");
                    string clientName = Console.ReadLine();
                    AddClient(clientId, clientName);
                    Console.WriteLine("Клиент успешно добавлен!");
                    break;

                case "сотрудники":
                    Console.Write("Введите Id сотрудника: ");
                    int empId = int.Parse(Console.ReadLine());
                    Console.Write("Введите имя сотрудника: ");
                    string empName = Console.ReadLine();
                    Console.Write("Введите зарплату сотрудника: ");
                    int salary = int.Parse(Console.ReadLine());
                    Console.Write("Введите должность сотрудника: ");
                    string position = Console.ReadLine();
                    Console.Write("Введите Id организации сотрудника: ");
                    int orgId = int.Parse(Console.ReadLine());
                    AddEmployee(empId, salary, empName, position, orgId);
                    Console.WriteLine("Сотрудник успешно добавлен!");
                    break;

                case "организации":
                    Console.Write("Введите Id организации: ");
                    int organizationId = int.Parse(Console.ReadLine());
                    Console.Write("Введите название организации: ");
                    string orgName = Console.ReadLine();
                    AddOrganization(organizationId, orgName);
                    Console.WriteLine("Организация успешно добавлена!");
                    break;

                case "зарплаты":
                    Console.Write("Введите Id зарплаты: ");
                    int salaryId = int.Parse(Console.ReadLine());
                    Console.Write("Введите налог (в процентах) на зарплату: ");
                    int tax = int.Parse(Console.ReadLine());
                    Console.Write("Введите размер зарплаты: ");
                    int sum = int.Parse(Console.ReadLine());
                    Console.Write("Введите Id_сотрудника: ");
                    int employeeId = int.Parse(Console.ReadLine());
                    Console.Write("Введите дату и время (гггг-ММ-дд ЧЧ:мм) выдачи зарплаты: ");
                    DateTime paymentDateTime = DateTime.Parse(Console.ReadLine());
                    AddSalary(salaryId, tax, sum, employeeId, paymentDateTime);
                    Console.WriteLine("Зарплата успешно добавлена!");
                    break;

                case "связь клиенты с сотрудниками":
                    Console.Write("Введите Id сотрудника: ");
                    int eId = int.Parse(Console.ReadLine());
                    Console.Write("Введите Id клиента: ");
                    int сId = int.Parse(Console.ReadLine());
                    Console.Write("Введите дату и время (гггг-ММ-дд ЧЧ:мм) привлечения клиента сотрудником: ");
                    DateTime attractingDate = DateTime.Parse(Console.ReadLine());
                    Console.Write("Введите дату и время (гггг-ММ-дд ЧЧ:мм) сделки: ");
                    DateTime dealDate = DateTime.Parse(Console.ReadLine());
                    Console.Write("Введите сумму сделки: ");
                    int dealsum = int.Parse(Console.ReadLine());
                    Console.Write("Введите процент сотруднику со сделки: ");
                    int dealPercentage = int.Parse(Console.ReadLine());
                    AddEmployeeClient(eId, сId, attractingDate, dealsum, dealPercentage, dealDate);
                    break;

                default:
                    Console.WriteLine("Нет такой таблицы!");
                    break;

            }
        }

        static void DeleteFromWhichTable(string deleteOption)
        {
            switch (deleteOption)
            {
                case "клиенты":
                    Console.Write("Введите id клиента: ");
                    int clientId = int.Parse(Console.ReadLine());
                    DeleteClient(clientId);
                    Console.WriteLine("Клиент успешно удалён!");
                    break;

                case "сотрудники":
                    Console.Write("Введите id сотрудника: ");
                    int employeeId = int.Parse(Console.ReadLine());
                    DeleteEmployee(employeeId);
                    Console.WriteLine("Сотрудник успешно удалён!");
                    break;

                case "организации":
                    Console.Write("Введите id организации: ");
                    int orgId = int.Parse(Console.ReadLine());
                    DeleteOrg(orgId);
                    Console.WriteLine("Организация успешно удалена!");
                    break;

                case "зарплаты":
                    Console.Write("Введите id зарплаты: ");
                    int salaryId = int.Parse(Console.ReadLine());
                    DeleteSalary(salaryId);
                    Console.WriteLine("Зарплата успешно удалена!");
                    break;

                case "связь клиенты с сотрудниками":
                    Console.Write("Введите id сотрудника: ");
                    int eId = int.Parse(Console.ReadLine());
                    Console.Write("Введите id клиента: ");
                    int сId = int.Parse(Console.ReadLine());                   
                    DeleteEmployeeClient(eId, сId);
                    Console.WriteLine("Связь клиенты с сотрудниками успешно удалена!");
                    break;

                default:
                    Console.WriteLine("Нет такой таблицы!");
                    break;

            }
        }

        static void DeleteSalary(int id)
        {
            var salary = _context.Salaries
                .Where(s => s.Id.Equals(id))
                .FirstOrDefault();

            _context.Salaries.Remove(salary);

            _context.SaveChanges();
        }

        static void DeleteOrg(int id)
        {
            var organization = _context.Organizations
                .Where(o => o.Id.Equals(id))
                .FirstOrDefault();

            _context.Organizations.Remove(organization);

            _context.SaveChanges();
        }

        static void DeleteEmployeeClient(int eId, int cId)
        {
            var employeeClient = _context.EmployeeClients
                .Where(ec => ec.EmployeeId.Equals(eId) && ec.ClientId.Equals(cId))
                .FirstOrDefault();

            _context.EmployeeClients.Remove(employeeClient);

            _context.SaveChanges();
        }

        static void DeleteEmployee(int id)
        {
            var employee = _context.Employees
                .Where(e => e.Id.Equals(id))
                .FirstOrDefault();

            _context.Employees.Remove(employee);

            _context.SaveChanges();
        }

        static void DeleteClient(int id)
        {
            var client = _context.Clients
                .Where(c => c.Id.Equals(id))
                .FirstOrDefault();

            _context.Clients.Remove(client);

            _context.SaveChanges();
        }

        static void UpdateSalary(int id, string tax, string sum, string empId, string paymentDate)
        {
            var salary = _context.Salaries
                .Where(s => s.Id.Equals(id))
                .ToList();

            foreach(var s in salary)
            {
                if(tax != "не менять")
                {
                    s.Tax = int.Parse(tax);
                }
                
                if(sum != "не менять")
                {
                    s.Sum = int.Parse(sum);
                }

                if(empId != "не менять")
                {
                    s.EmployeeId = int.Parse(empId);
                }

                if(paymentDate != "не менять")
                {
                    s.PaymentDate = DateTime.Parse(paymentDate);
                }
            }

            _context.SaveChanges();
        }

        static void UpdateOrganization(int id, string name)
        {
            var organization = _context.Organizations
                .Where(o => o.Id.Equals(id))
                .ToList();

            foreach (var o in organization)
            {
                if(name != "не менять")
                {
                    o.Name = name;
                }
            }

            _context.SaveChanges();
        }

        static void UpdateEmployeeClient(int empId, int clientId, string attractingDate, string dealSum, 
            string dealPercentage, string dealDate)
        {
            var employeeClient = _context.EmployeeClients
                .Where(ec => ec.EmployeeId.Equals(empId) && ec.ClientId.Equals(clientId))
                .ToList();
            
            foreach(var ec in employeeClient)
            {
                if(attractingDate != "не менять")
                {
                    ec.AttractingDate = DateTime.Parse(attractingDate);
                }

                if(dealSum != "не менять")
                {
                    ec.DealSum = int.Parse(dealSum);
                }

                if(dealPercentage != "не менять")
                {
                    ec.DealPercentage = int.Parse(dealPercentage);
                }

                if(dealDate != "не менять")
                {
                    ec.DealDate = DateTime.Parse(dealDate);
                }
            }

            _context.SaveChanges();
        }

        static void UpdateEmployee(int id, string salaryFixed, string name, string position, string orgId)
        {
            var employee = _context.Employees
                .Where(e => e.Id.Equals(id))
                .ToList();

            foreach(var e in employee)
            {
                if(salaryFixed != "не менять")
                {
                    e.SalaryFixed = int.Parse(salaryFixed);
                }

                if(name != "не менять")
                {
                    e.Name = name;
                }

                if (position != "не менять")
                {
                    e.Position = position;
                }

                if (orgId != "не менять")
                {
                    e.OrganizationId = int.Parse(orgId);
                }               
            }

            _context.SaveChanges();
        }

        static void UpdateClient(int id, string name)
        {
            var client = _context.Clients
                .Where(c => c.Id.Equals(id))
                .ToList();

            foreach(var c in client)
            {
                if (name != "не менять")
                {
                    c.Name = name;
                }
            }

            _context.SaveChanges();
        }

        static void AddSalary(int id, int tax, int sum, int employeeId, DateTime paymentDate)
        {
            Salary salary = new Salary
            {
                Id = id,
                Tax = tax,
                Sum = sum,
                EmployeeId = employeeId,
                PaymentDate = paymentDate
            };

            var salaries = _context.Salaries.Add(salary);

            _context.SaveChanges();
        }

        static void AddOrganization(int id, string name)
        {
            Organization organization = new Organization
            {
                Id = id,
                Name = name
            };

            var organizations = _context.Organizations.Add(organization);

            _context.SaveChanges();
        }

        static void AddEmployeeClient(int employeeId, int clientId, DateTime attractingDateTime, int dealSum, int dealPercentage, DateTime dealDateTime)
        {
            EmployeeClient employeeClient = new EmployeeClient
            {
                EmployeeId = employeeId,
                ClientId = clientId,
                AttractingDate = attractingDateTime,
                DealSum = dealSum,
                DealPercentage = dealPercentage,
                DealDate = dealDateTime
            };

            var employeeClients = _context.EmployeeClients.Add(employeeClient);

            _context.SaveChanges();
        }

        static void AddEmployee(int id, int salary, string name, string position, int organizationId)
        {
            Employee employee = new Employee
            {
                Id = id,
                SalaryFixed = salary,
                Name = name,
                Position = position,
                OrganizationId = organizationId
            };

            var employees = _context.Employees.Add(employee);

            _context.SaveChanges();
        }

        static void AddClient(int id, string name)
        {
            Client client = new Client
            {
                Id = id,
                Name = name
            };

            var clients = _context.Clients.Add(client);

            _context.SaveChanges();
        }

        static string SelectAllEmployeeClient()
        {
            var employeeClients = _context.EmployeeClients
                .Select(ec => new
                {
                    ec.EmployeeId,
                    ec.AttractingDate,
                    ec.ClientId,
                    ec.DealSum,
                    ec.DealPercentage,
                    ec.DealDate
                })
                .OrderBy(ec => ec.EmployeeId)
                .ToList();

            var sb = new StringBuilder();

            foreach (var ec in employeeClients)
            {
                sb.AppendLine($"Id_Сотрудника:{ec.EmployeeId}, Дата_привлечения_клиента:{ec.AttractingDate}, Id_Клиента:{ec.ClientId}," +
                    $" Сумма_сделки:{ec.DealSum}, Процент_сотруднику:{ec.DealPercentage}, Дата_сделки:{ec.DealDate};");
            }

            return sb.ToString().TrimEnd();
        }

        static string SelectAllSalaries()
        {
            var salaries = _context.Salaries
                .Select(s => new
                {
                    s.Id,
                    s.Sum,
                    s.Tax,
                    s.EmployeeId,
                    s.PaymentDate
                })
                .OrderBy(s => s.Id)
                .ToList();

            var sb = new StringBuilder();

            foreach (var s in salaries)
            {
                sb.AppendLine($"Id:{s.Id}, Сумма:{s.Sum}, Налог:{s.Tax}, Id_Сотрудника:{s.EmployeeId}, Дата_выплаты:{s.PaymentDate};");
            }

            return sb.ToString().TrimEnd();
        }

        static string SelectAllOrganizations()
        {
            var organizations = _context.Organizations
                .Select(o => new
                {
                    o.Id,
                    o.Name
                })
                .OrderBy(o => o.Id)
                .ToList();

            var sb = new StringBuilder();

            foreach (var o in organizations)
            {
                sb.AppendLine($"Id:{o.Id}, Название:{o.Name};");
            }

            return sb.ToString().TrimEnd();
        }

        static string SelectAllEmployees()
        {
            var employees = _context.Employees
                .Select(e => new
                {
                    e.Id,
                    e.Name,
                    e.Position,
                    e.SalaryFixed,
                    e.OrganizationId
                })
                .OrderBy(e => e.Id)
                .ToList();

            var sb = new StringBuilder();

            foreach(var e in employees)
            {
                sb.AppendLine($"Id:{e.Id}, Имя:{e.Name}, Должность:{e.Position}, Зарплата:{e.SalaryFixed}, Id_Организации:{e.OrganizationId};");
            }

            return sb.ToString().TrimEnd();
        }

        static string SelectAllClients()
        {
            var clients = _context.Clients
                .Select(c => new
                {
                    c.Id,
                    c.Name
                })
                .OrderBy(c => c.Id)
                .ToList();

            var sb = new StringBuilder();

            foreach(var c in clients)
            {
                sb.AppendLine($"Id:{c.Id}, Имя:{c.Name};");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
