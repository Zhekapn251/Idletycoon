using System;
using Cysharp.Threading.Tasks;
using Datas.Furniture;
using Employees;
using Services.Interfaces;
using Tasks;
using UnityEngine;
using VisitorsRest;
using Zenject;

namespace Furnitures
{
    public class Toilet: Furniture, IInteractable, IInteractableVisitor
    {
        public Transform TransformEmployee;
        public Transform TransformVisitor;
        private IMoneyData _moneyData;
        private IFurnitureService _furnitureService;
        public Transform interactTransform { get; private set; }

        public Transform interactVisitorTransform { get; private set; }
        private const float PriceMultiplier = 5f;
        [Inject]
        public void Construct(IMoneyData moneyData, IFurnitureService furnitureService)
        {
            _moneyData = moneyData;
            _furnitureService = furnitureService;
        }
        
        private void Start()
        {
            interactTransform = TransformEmployee;
            interactVisitorTransform = TransformVisitor;
        }

        public async UniTask Interact(Employee employee)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(
                StaticDataService.GetFurnitureStaticData()[FurnitureType.Toilet].speed[Data.Level - 1]));
        }

        public async UniTask Interact(Visitor visitor)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(5));
            float check = _furnitureService.DefineCheck()*PriceMultiplier;
            _moneyData.AddMoney(check); 
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }
    }
}