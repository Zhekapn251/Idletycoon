using System.Collections.Generic;
using Datas;
using Datas.Employeers;
using Datas.Furniture;
using Services.Interfaces;

namespace DefaultNamespace.Services
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<FurnitureType, FurnitureStaticDataStructure> _furnitureStaticData;
        private EmployeeStaticDataStructure[] _employeeStaticData;


        public StaticDataService(StaticDataSO staticDataSo)
        {
            _furnitureStaticData = new Dictionary<FurnitureType, FurnitureStaticDataStructure>();
            foreach (var furnitureData in staticDataSo.FurnitureData)
            {
                _furnitureStaticData.Add(furnitureData.Type, furnitureData);
            }
            _employeeStaticData = staticDataSo.EmployeeData;
        }
        
        public int GetUpgradeCost(FurnitureType furnitureType, int level)
        {
            return _furnitureStaticData[furnitureType].UpgradeCost[level - 1];
        }
        
        public int GetUpgradeCost(EmployeeType employeeType, int level)
        {
            foreach (var employeeStaticDataStructure in _employeeStaticData)
            {
                if (employeeStaticDataStructure.Type != employeeType) continue;
                return employeeStaticDataStructure.UpgradeCost[level - 1];
            }
            return 0;
        }

        public Dictionary<FurnitureType, FurnitureStaticDataStructure> GetFurnitureStaticData()
        {
            return _furnitureStaticData;
        }
        
        public EmployeeStaticDataStructure[] GetEmployeeStaticData()
        {
            return _employeeStaticData;
        }

        public int GetEmployeePrice(EmployeeType employeeType)
        {
            return _employeeStaticData[(int) employeeType].UpgradeCost[0];
        }

        public float GetEmployeeSalary(EmployeeType employeeDataType, int employeeDataLevel)
        {
            return _employeeStaticData[(int) employeeDataType].Salary[employeeDataLevel - 1];
        }
    }
}