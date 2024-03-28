using System;
using Datas.Employeers;
using Employees;

namespace Services.Interfaces
{
    public interface IStaffService
    {
        Employee HireStaff(EmployeeType employeeType, bool free);
        void FireStaff(EmployeeData employeeData);
        void PromoteStaff();
        void SelectStaff(EmployeeData employeeData);
        void UnSelectStaff();
        event Action<EmployeeData> OnEmployeeSelected;
        float GetSalary();
    }
}