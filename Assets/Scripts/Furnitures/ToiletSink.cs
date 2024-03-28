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
    public class ToiletSink: Furniture, IInteractable, IInteractableVisitor
    {
        private const float PriceMultiplier = 2.5f;
        public Transform EmployeeTransform;
        public Transform VisitorTransform;
        private IMoneyData _moneyData;
        private IFurnitureService _furnitureService;
        public Transform interactTransform { get; private set; }

        public Transform interactVisitorTransform { get; private set; }
        
        [Inject]
        public void Construct(IMoneyData moneyData, IFurnitureService furnitureService)
        {
            _furnitureService = furnitureService;
            _moneyData = moneyData;
        }
        
        private void Start()
        {
            interactTransform = EmployeeTransform;
            interactVisitorTransform = VisitorTransform;
        }

        public async UniTask Interact(Employee employee)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(
                StaticDataService.GetFurnitureStaticData()[FurnitureType.ToiletSink].speed[Data.Level - 1]));
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