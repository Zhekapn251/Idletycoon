using System;
using System.Collections.Generic;
using Datas.Employeers;
using Datas.Furniture;
using Employees;
using Furnitures;
using Services.Interfaces;
using Tasks;
using Unity.AI.Navigation;
using UnityEngine;
using Zenject;

namespace Restaurant
{
    public class StaffController: MonoBehaviour
    {
        [SerializeField] NavMeshSurface surface;
        private Queue<Task> tasks = new Queue<Task>();  
        private bool isChefBusy = false;
        private List<Employee> employees = new List<Employee>();
        private IFurnitureService _furnitureService;
        private IStaffService _staffService;
        private IPersistantDataService _persistantDataService;

        [Inject]
        public void Construct(IStaffService staffService, 
            IFurnitureService furnitureService,
            IPersistantDataService persistantDataService)    
        {
            _persistantDataService = persistantDataService;
            _staffService = staffService;
            _furnitureService = furnitureService;
            _furnitureService.OnFurnitureCreated += Load;
        }
        
        private void Load()
        {
            surface.BuildNavMesh();
            _persistantDataService.GameData.employees.ForEach(employee =>
            {
                var newEmployee = _staffService.HireStaff(employee.Type, true);
                newEmployee.EmployeeData = employee;
                InitializeEmployee(newEmployee);
            });
        }

        private void InitializeEmployee(Employee employee)
        {
            employee.OnEmployeeStateChanged += SetTasks;
            SetTasks(employee);
        }

        private void SetTasks(Employee employee)
        {
            switch (employee)
            {
                case Chef chef:
                    chef.AddTask(new Task(_furnitureService.GetFurniture(FurnitureType.ServingTable).GetComponent<ServingTable>().CreateWaitForOrderTask()));
                    chef.AddTask(new Task(_furnitureService.GetFurniture(FurnitureType.Fridge).GetComponent<IInteractable>()));
                    chef.AddTask(new Task(_furnitureService.GetFurniture(FurnitureType.CuttingTable).GetComponent<IInteractable>()));
                    chef.AddTask(new Task(_furnitureService.GetFurniture(FurnitureType.Oven).GetComponent<IInteractable>()));
                    chef.AddTask(new Task(_furnitureService.GetFurniture(FurnitureType.ServingTable).GetComponent<IInteractable>()));
                    chef.AddTask(new Task(_furnitureService.GetFurniture(FurnitureType.CleaningShelves).GetComponent<IInteractable>()));
                    break;  
                case Waiter waiter:
                    waiter.AddTask(new Task(_furnitureService.GetFurniture(FurnitureType.Table).GetComponent<Table>().CreateWaitForVisitorTask()));
                    waiter.AddTask(new Task(_furnitureService.GetFurniture(FurnitureType.Table).GetComponent<IInteractable>()));
                    waiter.AddTask(new Task(_furnitureService.GetFurniture(FurnitureType.ServingTable).GetComponent<IInteractable>()));
                    waiter.AddTask(new Task(_furnitureService.GetFurniture(FurnitureType.ServingTable).GetComponent<ServingTable>().CreateWaitForDinnerTask()));
                    waiter.AddTask(new Task(_furnitureService.GetFurniture(FurnitureType.ServingTable).GetComponent<IInteractable>()));
                    waiter.AddTask(new Task(_furnitureService.GetFurniture(FurnitureType.Table).GetComponent<IInteractable>()));    
                    waiter.AddTask(new Task(_furnitureService.GetFurniture(FurnitureType.CleaningShelves).GetComponent<IInteractable>()));
                    break;
                case Receptionist receptionist:
                    receptionist.AddTask(new Task(_furnitureService.GetFurniture(FurnitureType.Reception).GetComponent<IInteractable>()));
                    receptionist.AddTask(new Task(_furnitureService.GetFurniture(FurnitureType.Reception).GetComponent<Reception>().CreateWaitForVisitorTask()));
                    receptionist.AddTask(new Task(_furnitureService.GetFurniture(FurnitureType.Table).GetComponent<IInteractable>()));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(employee));
            }
        }
        
    }
}