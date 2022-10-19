﻿using Microsoft.EntityFrameworkCore.Query.Internal;
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
            return _employeeContext.employee.FirstOrDefault(i => i.Username==username);
        }

        public List<EmployeeDetail> Get()
        {
            return _employeeContext.employee.ToList();
        }
        public void delete(string username)
        {
            EmployeeDetail emp= _employeeContext.employee.FirstOrDefault(i =>i.Username==username);
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
    }
}
