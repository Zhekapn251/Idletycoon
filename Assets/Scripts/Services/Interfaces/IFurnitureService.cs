using System;
using Datas.Furniture;
using Furnitures;

namespace Services.Interfaces
{
    public interface IFurnitureService
    {
        Furniture GetFurniture(FurnitureType furnitureType);
        event Action OnFurnitureCreated;
        void UpgradeFurniture();
        void SelectFurniture(FurnitureData furnitureData);
        FurnitureData[] GetFurnitureArray(FurnitureType furnitureType);
        void BuyNewFurniture(FurnitureType furnitureType);
        event Action<FurnitureData> OnFurnitureSelected;
        float DefineCheck();
        void PressOnFurniture(FurnitureData data);
    }
}