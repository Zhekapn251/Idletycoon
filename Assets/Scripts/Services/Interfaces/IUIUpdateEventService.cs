using System;

namespace Services.Interfaces
{
    public interface IUIUpdateEventService
    {
        event Action<float> OnMoneyChanged;
        event Action OnDataChanged;
    }
}