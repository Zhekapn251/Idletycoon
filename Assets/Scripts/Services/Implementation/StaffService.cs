using System;
using System.Collections.Generic;
using Datas.Employeers;
using Employees;
using Factories.Interfaces;
using Services.Interfaces;
using UnityEngine;

namespace Services.Implementation
{
    public class StaffService : IStaffService
    {
        public event Action<EmployeeData> OnEmployeeSelected; 
        private readonly IUIUpdateService _uiUpdateService;
        private readonly ICameraService _cameraService;
        private readonly IEmployeeFactory _employeeFactory;
        private readonly IStaticDataService _staticDataService;
        private readonly IPersistantDataService _persistantDataService;

        private EmployeeData _selectedEmployee;
        private Employee _employeeSelected;
        private List<Employee> _employees = new List<Employee>();
        
        public StaffService(IUIUpdateService uiUpdateService, 
            ICameraService cameraService,
            IEmployeeFactory employeeFactory, 
            IStaticDataService staticDataService,
            IPersistantDataService persistantDataService)
        {
            _uiUpdateService = uiUpdateService;
            _cameraService = cameraService;
            _employeeFactory = employeeFactory;
            _staticDataService = staticDataService;
            _persistantDataService = persistantDataService;
        }
        
        public Employee HireStaff(EmployeeType employeeType, bool free)
        {
            int price = _staticDataService.GetEmployeePrice(employeeType);
            if(price > _persistantDataService.GameData.Money && !free)
                return null;
            
            Employee employee = _employeeFactory.CreateEmployee(employeeType);
            _employees.Add(employee);

            if (free) return employee;
            
            _persistantDataService.GameData.Money -= price;
            _uiUpdateService.ChangeMoney(_persistantDataService.GameData.Money);
            _uiUpdateService.ChangeData();

            return employee;
        }
        
        public void FireStaff(EmployeeData employeeData)
        {
            _uiUpdateService.ChangeMoney(100);
            _uiUpdateService.ChangeData();
        }
        public float GetSalary()
        {
            float salary = 0;
            foreach (var employee in _employees)
            {
                salary += employee.EmployeeData.Salary;
            }

            return salary;
        }
        public void PromoteStaff()
        {
            _employeeSelected.EmployeeData.Level++;
            float price = _staticDataService.GetUpgradeCost(_employeeSelected.EmployeeData.Type, _employeeSelected.EmployeeData.Level);
            if(price > _persistantDataService.GameData.Money) return;
            _persistantDataService.GameData.Money -= price;
            _uiUpdateService.ChangeMoney(_persistantDataService.GameData.Money);
            _uiUpdateService.ChangeData();
        }
        
        public void SelectStaff(EmployeeData employeeData)
        {
            _employeeSelected = _employees.Find(x => x.EmployeeData == employeeData);
            _cameraService.SetCameraPosition(_employeeSelected.transform.position);
            OnEmployeeSelected?.Invoke(employeeData);
        }

        public void UnSelectStaff()
        {
            _cameraService.SetCameraMode(CameraControl.CameraMode.Centered);
        }
    }
}