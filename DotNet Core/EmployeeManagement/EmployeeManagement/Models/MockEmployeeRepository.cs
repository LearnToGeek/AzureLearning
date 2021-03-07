using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList { get; set; }

        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>()
            {
                new Employee() { Id = 1, Name = "Rohit", Department = "IT", Email="rohit@gmail.com"},
                new Employee() { Id = 2, Name = "Bhavik", Department = "HR", Email="bhavik@gmail.com"},
                new Employee() { Id = 3, Name = "Piyush", Department = "Worker", Email="piyush@gmail.com"}
            };
        }

        public Employee GetEmployee(int Id)
        {
            return _employeeList.FirstOrDefault(e => e.Id == Id);
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _employeeList;
        }
    }
}
