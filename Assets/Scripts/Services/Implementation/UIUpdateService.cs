using System;
using Services.Interfaces;

namespace DefaultNamespace.Services
{
    public class UIUpdateService : IUIUpdateService, IUIUpdateEventService
    {
        public event Action<float> OnMoneyChanged;
        public event Action OnDataChanged; 

        public void ChangeMoney(float money) => 
            OnMoneyChanged?.Invoke(money);

        public void ChangeData() => 
            OnDataChanged?.Invoke();
    }
}