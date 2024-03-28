using System;
using System.Collections.Generic;
using System.Linq;
using Datas;
using Datas.Furniture;
using DefaultNamespace.Factories;
using Factories.Interfaces;
using Furnitures;
using Services.Interfaces;
using UnityEngine;

namespace Services.Implementation
{
    public class FurnitureService : IFurnitureService
    {
        private List<Furniture> Furniture { get; set; } = new List<Furniture>();
        public event Action OnFurnitureCreated;
        public event Action<FurnitureData> OnFurnitureSelected;
        public float DefineCheck()
        {
            float check = 0;
            foreach (var furniture in Furniture)
            {
                check += furniture.Data.Price;
            }

            return check;
        }

        private IFurnitureFactory _furnitureFactory;
        private Furniture _selectedFurniture;

        private readonly IPersistantDataService _persistantDataService;

        private readonly ICameraService _cameraService;
        private readonly IUIUpdateService _uiUpdateService;
        private readonly IStaticDataService _staticDataService;
        private readonly IPointerOverUIService _pointerOverUIService;


        public FurnitureService(IFurnitureFactory furnitureFactory, 
            IPersistantDataService persistantDataService,
            ICameraService cameraService,
            IUIUpdateService uiUpdateService, 
            IStaticDataService staticDataService,
            IPointerOverUIService pointerOverUIService)
        {
            _furnitureFactory = furnitureFactory;
            _persistantDataService = persistantDataService;
            _cameraService = cameraService;
            _uiUpdateService = uiUpdateService;
            _staticDataService = staticDataService;
            _pointerOverUIService = pointerOverUIService;
            Start();
        }

        public void UpgradeFurniture()
        {
            int price = _staticDataService.GetUpgradeCost(_selectedFurniture.Data.Type, _selectedFurniture.Data.Level);
            if(price > _persistantDataService.GameData.Money) return;
            _persistantDataService.GameData.Money -= price;
            _uiUpdateService.ChangeMoney(_persistantDataService.GameData.Money);
            _selectedFurniture.Upgrade();
            UpdateData();
        }

        private void UpdateData()
        {
            GameData gameData = _persistantDataService.GameData;
            _selectedFurniture.Data.Level++;
            _selectedFurniture.Data.Price = _staticDataService.GetFurnitureStaticData()[_selectedFurniture.Data.Type]
                .avarageCost[_selectedFurniture.Data.Level - 1];
            _selectedFurniture.Data.Time = _staticDataService.GetFurnitureStaticData()[_selectedFurniture.Data.Type]
                .speed[_selectedFurniture.Data.Level - 1];
            gameData.furnitures[_selectedFurniture.Data.Id] = _selectedFurniture.Data;
            _persistantDataService.GameData = gameData;
            _uiUpdateService.ChangeData();
        }

        private async void Start()
        {

            foreach (var furniture in _persistantDataService.GameData.furnitures)
            {
                var newFurniture = await _furnitureFactory.Create(furniture.Type);
                newFurniture.Data.Level = furniture.Level;
                newFurniture.Data.Price = _staticDataService.GetFurnitureStaticData()[furniture.Type]
                    .avarageCost[furniture.Level - 1];
                newFurniture.Data.Time = _staticDataService.GetFurnitureStaticData()[furniture.Type].speed[furniture.Level - 1];
                newFurniture.Init(this, _staticDataService, _pointerOverUIService);
                Furniture.Add(newFurniture);
            }

            OnFurnitureCreated?.Invoke();
        }
        
        public void PressOnFurniture(FurnitureData furnitureData)
        {
            _selectedFurniture = GetFurnitureData(furnitureData);
            _cameraService.SetCameraPosition(_selectedFurniture.transform.position);
            OnFurnitureSelected?.Invoke(furnitureData);
        }
        
        public void SelectFurniture(FurnitureData furnitureData)
        {
            _selectedFurniture = GetFurnitureData(furnitureData);
            _cameraService.SetCameraPosition(_selectedFurniture.transform.position);
        }
        public Furniture GetFurniture(FurnitureType furnitureType)
        {
            return Furniture.FirstOrDefault(furniture => furniture.Data.Type == furnitureType);
        }
        
        public FurnitureData[] GetFurnitureArray(FurnitureType furnitureType)
        {
            return Furniture.Where(furniture => furniture.Data.Type == furnitureType).Select(furniture => furniture.Data).ToArray();
        }

        public void BuyNewFurniture(FurnitureType furnitureType)
        {
            _persistantDataService.GameData.furnitures.Add(new FurnitureData
            {
                Type = furnitureType,
                Level = 1,
                Time = 1,
                Id = _persistantDataService.GameData.furnitures.Count,
            });
        }

        private Furniture GetFurnitureData(FurnitureData furnitureData)
        {
            return Furniture.FirstOrDefault(furniture => furniture.Data == furnitureData);
        }
    }
}