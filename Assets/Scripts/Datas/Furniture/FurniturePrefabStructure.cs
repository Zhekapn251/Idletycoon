using System;

namespace Datas.Furniture
{
    [Serializable]
    public class FurniturePrefabStructure
    {
        public FurnitureType FurnitureType;
        public string PrefabPath;
        public string[] ModelsPath;
    }
}