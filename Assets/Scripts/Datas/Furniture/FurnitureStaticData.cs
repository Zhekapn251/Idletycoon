using System;
using Datas.Employeers;
using UnityEngine;

namespace Datas.Furniture
{
    [CreateAssetMenu]
    public class StaticDataSO : ScriptableObject
    {
        public FurnitureStaticDataStructure[] FurnitureData;
        public EmployeeStaticDataStructure[] EmployeeData;
    }
    
    [Serializable]
    public class FurnitureStaticDataStructure
    {
        public FurnitureType Type;
        public int[] UpgradeCost;
        public float[] speed;
        public float[] avarageCost;
    }
    
    [Serializable] 
    public class EmployeeStaticDataStructure
    {
        public EmployeeType Type;
        public int[] UpgradeCost;
        public float[] speed;
        public float[] Salary;
    }
}