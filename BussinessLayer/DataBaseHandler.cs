using Microsoft.EntityFrameworkCore.Query.Internal;
using ModelLayer;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BussinessLayer
{
    public class DataBaseHandler : IDataBaseHandler
    {
        private readonly EmployeeContext _employeeContext;
        public DataBaseHandler(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }
        public EmployeeDetail Get(string username)
        {
            return _employeeContext.employee.FirstOrDefault(i => i.username==username);
        }
        public List<EmployeeDetail> GetByOrder()
        {
            return _employeeContext.employee.OrderBy(s => s.Salary).ToList();
        }
        public List<EmployeeDetail> GetByOrderDesc()
        {
            return _employeeContext.employee.OrderByDescending(s => s.Salary).ToList();
        }
        public List<EmployeeDetail> Get()
        {
            return _employeeContext.employee.ToList();
        }

        public void delete(string username)
        {
            EmployeeDetail emp= _employeeContext.employee.FirstOrDefault(i =>i.username==username);
            if(emp!=null)
            {
                _employeeContext.Remove(emp);
                _employeeContext.SaveChanges();
            }
        }
         public void create(EmployeeDetail employee)
        {
            _employeeContext.employee.Add(employee);
            _employeeContext.SaveChanges();
        }
        public void Edit(EmployeeDetail employee)
        {
            EmployeeDetail emp = _employeeContext.employee.FirstOrDefault(i => i.username == employee.username);
            if (emp != null)
            {
                _employeeContext.Remove(emp);
                _employeeContext.employee.Add(employee);
                _employeeContext.SaveChanges();
            }
            
        }
    }
}
