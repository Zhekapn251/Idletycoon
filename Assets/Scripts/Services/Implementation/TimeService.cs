using System;
using ModestTree.Util;
using Services.Interfaces;
using UnityEngine;
using Zenject;

namespace Services.Implementation
{
    public class TimeService: ITickable, ITimeService
    {
        
        public event Action<float> OnHourChanged;
        public event Action<float> OnMinutesChanged;
        private readonly IPersistantDataService _persistantDataService;
        private readonly IMoneyData _moneyData;
        private readonly IStaffService _staffService;
        float _hour = 0;
        float _minutes = 0;


        public TimeService(IPersistantDataService persistantDataService, 
            IMoneyData moneyData,
            IStaffService staffService)
        {
            _persistantDataService = persistantDataService;
            _moneyData = moneyData;
            _staffService = staffService;
        }


        public void Tick()
        {
            _minutes += Time.deltaTime;
            OnMinutesChanged?.Invoke(_minutes);
            if(_minutes >= 60)
            {
                _hour++;
                _minutes = 0;
                if(_hour >= 24)
                {
                    _hour = 0;
                    OnEndCharge();
                }
                OnHourChanged?.Invoke(_hour);
            }
        }
        
        private void OnEndCharge()
        {
            _moneyData.AddMoney(_staffService.GetSalary());
        }
    }
}