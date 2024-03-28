using Datas;

namespace Services.Interfaces
{
    public interface IPersistantDataService
    {
        GameData GameData { get; set; }
        void SaveData();
        void LoadData();
    }
}