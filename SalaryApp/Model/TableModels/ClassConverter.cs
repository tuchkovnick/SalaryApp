using ColleguesLibrary.Classes;
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
        private readonly ApplicationContext _context;
        public ClassConverter(ApplicationContext context)
        {
            this._context = context;
        }

        //database -> buisness model
        public Employee GetEmployee (CompanyEmployee companyEmployee)
        {
            var employee = new Employee(companyEmployee.FIO, companyEmployee.WageBase, companyEmployee.EmploymentDate);
            bool isPositionValid = Enum.TryParse(companyEmployee.Position, out EmployeeType type);
            if (isPositionValid)
            {
                employee.SetType(type);
                employee.AddSubordinates(GetSubordinatesOfId(companyEmployee.Id));
            }
            else
            {
                throw new Exception("Position is not valid!");
            }
            return employee;

        }
        
        // gets subordinates from database
        private List<Employee> GetSubordinatesOfId(int id)
        {
            List<Employee> subordinates = new List<Employee>();
            _context.CompanyRelations.Load();
            //
            var relations = _context.CompanyRelations.Local.Where(r => r.BossId == id);

            var subordinateIds = relations.Select(s => s.SubordinateId);
            var subs = _context.CompanyEmployees.Where(e => subordinateIds.Contains(e.Id)).ToList();
            
            foreach (var subordinate in subs)
            {
                subordinates.Add(GetEmployee(subordinate));
            }
            return subordinates;
        }
    
        //gets hashes
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
