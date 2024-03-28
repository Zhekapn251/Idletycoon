using Datas;
using DefaultNamespace;
using Services.Interfaces;

namespace Services.Implementation
{
    public class PersistantDataService : IPersistantDataService, IMoneyData
    {
        private readonly IUIUpdateService _uiUpdateService;

        public PersistantDataService(IUIUpdateService uiUpdateService)
        {
            _uiUpdateService = uiUpdateService;
        } 

        public GameData GameData { get; set; } = new GameData();

        public void SaveData() => 
            JsonDataHandler.SaveToJson(GameData, Constants.DataPath);

        public void LoadData() => 
            JsonDataHandler.LoadFromJson<GameData>(Constants.DataPath);

        public void AddMoney(float money)
        {
            if(money < 0)
                return;

            GameData.Money += money;
            _uiUpdateService.ChangeMoney(GameData.Money);
        }

        
        public void SubtractMoney(float money)
        {
            if(money>0 || GameData.Money < money)
                return;

            GameData.Money -= money;
            _uiUpdateService.ChangeMoney(GameData.Money);
        }
    }
}