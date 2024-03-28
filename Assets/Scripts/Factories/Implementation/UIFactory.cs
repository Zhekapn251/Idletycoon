using System.Collections.Generic;
using Datas;
using Datas.Employeers;
using Datas.Furniture;
using Factories.Interfaces;
using UnityEngine;

namespace Factories.Implementation
{
    public class UIFactory : IUIFactory
    {
        private Dictionary<FurnitureType, Sprite> _furnitureIcons;
        private Dictionary<EmployeeType, Sprite[]> _employeeIcons;
        
        public UIFactory(UiIconsSO uiIconsSo)
        {
            _furnitureIcons = new Dictionary<FurnitureType, Sprite>();
            _employeeIcons = new Dictionary<EmployeeType, Sprite[]>();
            
            foreach (var furnitureIcon in uiIconsSo.FurnitureIcons)
            {
                _furnitureIcons.Add(furnitureIcon.Type, furnitureIcon.Icon);
            }
            
            foreach (var employeeIcon in uiIconsSo.EmployeeIcons)
            {
                _employeeIcons.Add(employeeIcon.Type, employeeIcon.Icon);
            }
        }
        public Sprite GetFurnitureIcon(FurnitureType type) => 
            _furnitureIcons[type];

        public Sprite GetEmployeeIcon(EmployeeType type, int number) => 
            _employeeIcons[type][number];
        
    }
}