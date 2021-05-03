using ActiveEdge.Core;
using ActiveEdge.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveEdge.Infrastructure.Services
{
    public class EmployeeService
    {
        private List<EmployeeDto> _employees;

        public EmployeeService()
        {
            _employees = new List<EmployeeDto>();

            _employees.Add(new EmployeeDto { 
                Age=29,
                Created=DateTime.Now,
                CreatedBy= ActiveEdgeConstants.CREATED_BY_SYSTEM,
                EmployeeId="E00001",
                FirstName="John",
                JoinDate=new DateTime(2021,01,15),
                LastName="Keynes",
                IsDeleted=false
            });

            _employees.Add(new EmployeeDto
            {
                Age = 54,
                Created = DateTime.Now,
                CreatedBy = ActiveEdgeConstants.CREATED_BY_SYSTEM,
                EmployeeId = "E00001",
                FirstName = "Sarah",
                JoinDate = new DateTime(2020, 05, 24),
                LastName = "Robinson",
                IsDeleted=false
            });
        }

        public List<EmployeeDto> GetAll()
        {
            return _employees.Where(p=>!p.IsDeleted).ToList();
        }

        public EmployeeDto Get(string EmployeeId)
        {
            var employee = _employees.FirstOrDefault(p=>p.EmployeeId==EmployeeId && !p.IsDeleted);
            return employee;
        }

        public EmployeeDto Update(EmployeeDto employee)
        {
            var index = _employees.FindIndex(p => p.EmployeeId == employee.EmployeeId && !p.IsDeleted);
            if(index==-1)
            {
                return null;
            }

            _employees[index].FirstName = employee.FirstName;
            _employees[index].LastName = employee.LastName;
            _employees[index].Age = employee.Age;
            _employees[index].JoinDate = employee.JoinDate;
            _employees[index].LastModified = DateTime.Now;

            return _employees[index];
        }

        public EmployeeDto Add(EmployeeDto employee)
        {
            var index = _employees.FindIndex(p => p.EmployeeId == employee.EmployeeId && !p.IsDeleted);
            if (index >0)
            {
                return null;
            }

            _employees.Add(new EmployeeDto
            {
                Age = 29,
                Created = DateTime.Now,
                CreatedBy = ActiveEdgeConstants.CREATED_BY_SYSTEM,
                EmployeeId = "E00001",
                FirstName = "John",
                JoinDate = new DateTime(2021, 01, 15),
                LastName = "Keynes"
            });

            return employee;
        }

        public bool Delete(string employeeId)
        {
            var index = _employees.FindIndex(p => p.EmployeeId == employeeId && !p.IsDeleted);
            if (index ==-1)
            {
                return false;
            }

            _employees[index].IsDeleted = true;

            return true;
        }
    }
}
