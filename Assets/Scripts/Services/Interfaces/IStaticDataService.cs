using System.Collections.Generic;
using Datas.Employeers;
using Datas.Furniture;

namespace Services.Interfaces
{
    public interface IStaticDataService
    {
        int GetUpgradeCost(FurnitureType furnitureType, int level);
        int GetUpgradeCost(EmployeeType employeeType, int level);

        Dictionary<FurnitureType, FurnitureStaticDataStructure> GetFurnitureStaticData();

        EmployeeStaticDataStructure[] GetEmployeeStaticData();
        int GetEmployeePrice(EmployeeType employeeType);
        float GetEmployeeSalary(EmployeeType employeeDataType, int employeeDataLevel);
    }
}