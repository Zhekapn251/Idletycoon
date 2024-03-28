using System;

namespace Datas.Furniture
{
    [Serializable]
    public class FurnitureData
    {
        public int Level = 1;
        public int Id;
        public float Time;
        public float Price;
        public FurnitureType Type;
    }
}