using System;
using System.Collections.Generic;
using Datas.Employeers;
using Datas.Furniture;

namespace Datas
{
    public class GameData
    {
        public List<EmployeeData> employees = new List<EmployeeData>()
        {
            new EmployeeData() {Type = EmployeeType.Receptionist, Level = 1},
            new EmployeeData() {Type = EmployeeType.Chef, Level = 1},
            new EmployeeData() {Type = EmployeeType.Waiter, Level = 1}
        };
        public List<FurnitureData> furnitures = new List<FurnitureData>()
        {
            new FurnitureData() {Type = FurnitureType.Table, Level = 1},
            new FurnitureData() {Type = FurnitureType.Fridge, Level = 1},
            new FurnitureData() {Type = FurnitureType.CuttingTable, Level = 1},
            new FurnitureData() {Type = FurnitureType.Oven, Level = 1},
            new FurnitureData() {Type = FurnitureType.ServingTable, Level = 1},
            new FurnitureData() {Type = FurnitureType.CleaningShelves, Level = 1},
            new FurnitureData() {Type = FurnitureType.Reception, Level = 1},
            new FurnitureData()   {Type = FurnitureType.ToiletSink, Level = 1},
            new FurnitureData() {Type = FurnitureType.Toilet, Level = 1},
            new FurnitureData() {Type = FurnitureType.CupBoard, Level = 1},
                    
        };
        public float Money = 1000f;

        public DateTime saveTime;
    }
}