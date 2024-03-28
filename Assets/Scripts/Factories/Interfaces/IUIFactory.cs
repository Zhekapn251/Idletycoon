using Datas.Employeers;
using Datas.Furniture;
using UnityEngine;

namespace Factories.Interfaces
{
    public interface IUIFactory
    {
        Sprite GetFurnitureIcon(FurnitureType type);
        Sprite GetEmployeeIcon(EmployeeType type, int number);
    }
}