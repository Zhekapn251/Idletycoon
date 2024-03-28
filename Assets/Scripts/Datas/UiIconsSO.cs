using System;
using Datas.Employeers;
using Datas.Furniture;
using UnityEngine;

namespace Datas
{
    [CreateAssetMenu]
    public class UiIconsSO: ScriptableObject
    {
        public UiIconFurnitureData[] FurnitureIcons;
        public UiIconEmployeeData[] EmployeeIcons;
    }
    
    [Serializable]
    public class UiIconFurnitureData
    {
        public FurnitureType Type;
        public Sprite Icon;
    }
    
    [Serializable]
    public class UiIconEmployeeData
    {
        public EmployeeType Type;
        public Sprite[] Icon;
    }
}