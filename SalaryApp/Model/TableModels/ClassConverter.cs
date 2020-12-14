using ColleguesLibrary.Classes;
using ColleguesLibrary.Interfaces;
using SalaryApp.Model.TableModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryApp.Model
{
    public class ClassConverter
    {
        private ApplicationContext _context;
        public ClassConverter(ApplicationContext context)
        {
            this._context = context;
        }

        //конвертер классов из БД в бизнес модель
        public Employee GetEmployee (CompanyEmployee companyEmployee)
        {            
            switch (companyEmployee.Position)
            {
                case "Employee":
                    {
                        Employee employee = new Employee(companyEmployee.Id, companyEmployee.FIO, 
                                                            companyEmployee.WageBase, companyEmployee.EmploymentDate
                                                            );
                        return employee;
                    }
                case "Manager":
                    {
                        //get subordinates
                        Manager employee = new Manager(companyEmployee.Id, companyEmployee.FIO, 
                                                       companyEmployee.WageBase, companyEmployee.EmploymentDate,
                                                       null);
                        employee.DirectSubordinates = GetSubordinates(employee);
                        return employee;
                    }
                case "Salesman":
                    {
                        //get subordinates
                        Salesman employee = new Salesman(companyEmployee.Id, companyEmployee.FIO, 
                                                         companyEmployee.WageBase, companyEmployee.EmploymentDate, 
                                                         null);
                        employee.DirectSubordinates = GetSubordinates(employee);
                        return employee;
                    }
            }
            throw new Exception("Неизвестная должность!");
        }
        
        //устанавливает всех прямых подчинённых сотрудника на основании БД
        private List<Employee> GetSubordinates(Manager boss)
        {
            List<Employee> subordinates = new List<Employee>();
            _context.CompanyRelations.Load();
            //
            var relations = _context.CompanyRelations.Local.Where(r => r.BossId == boss.Id);

            var subordinateIds = relations.Select(s => s.SubordinateId);
            var subs = _context.CompanyEmployees.Where(e => subordinateIds.Contains(e.Id)).ToList();
            
            foreach (var subordinate in subs)
            {
                subordinates.Add(GetEmployee(subordinate));
            }
            return subordinates;
        }
    
        //возвращает легитимные хеши для данного пользователя
        public List<string> GetEmployeePasswords(CompanyEmployee companyEmployee)
        {
            List<string> passwordInfos = new List<string>();
            passwordInfos.Add(companyEmployee.PassInfo);
            var relations = _context.CompanyRelations.Where(r=>r.SubordinateId == companyEmployee.Id);
            foreach(var r in relations)
            {
                CompanyEmployee boss = _context.CompanyEmployees.Where(e => e.Id == r.BossId).FirstOrDefault();
                if (boss != null)
                {
                    passwordInfos.AddRange(GetEmployeePasswords(boss));
                }
            }
            return passwordInfos;
        }
    
    }
}
