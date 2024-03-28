using System;

namespace Services.Implementation
{
    public interface ITimeService
    {
        event Action<float> OnHourChanged;
        event Action<float> OnMinutesChanged;
    }
}