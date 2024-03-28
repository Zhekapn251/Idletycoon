using System.Collections.Generic;
using Datas.Employeers;
using DefaultNamespace.Factories;
using Employees;
using Factories.Interfaces;
using Services;
using UnityEngine;

namespace Factories.Implementation
{
    public class EmployeeFactory : IEmployeeFactory
    {
        private Dictionary<EmployeeType, EmployeePrefabStructure> _employeePrefabStructures;
        private Transform _employeeSpawnPoint;
        
        public EmployeeFactory(EmployeePrefabsSO employeePrefabsSo, Transform employeeSpawnPoint)
        {
            _employeePrefabStructures = new Dictionary<EmployeeType, EmployeePrefabStructure>();
            foreach (var employeePrefabStructure in employeePrefabsSo.EmployeePrefabStructures)
            {
                _employeePrefabStructures.Add(employeePrefabStructure.EmployeeType, employeePrefabStructure);
            }
            _employeeSpawnPoint = employeeSpawnPoint;
        }
        
        public Employee CreateEmployee(EmployeeType employeeType)
        {
            var prefabPath = _employeePrefabStructures[employeeType].
                PrefabsPaths[Random.Range(0, _employeePrefabStructures[employeeType].PrefabsPaths.Length)];
            var employee = Object.Instantiate(ResourcesLoader.LoadResource<Employee>(prefabPath));
            employee.transform.position = _employeeSpawnPoint.position;
            return employee;
        }
    }
}