using Datas.Employeers;
using Employees;

namespace Factories.Interfaces
{
    public interface IEmployeeFactory
    {
        Employee CreateEmployee(EmployeeType employeeType);
    }
}