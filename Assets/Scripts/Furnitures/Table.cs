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
    public class Table: Furniture, IInteractable, IInteractableVisitor
    { 
        public GameObject _dinner;
        public Transform EmployeeTransform;
        public Transform VisitorTransform;
        public Transform SitPoint;
        public bool IsOccupied;
        private Visitor _visitor;
        public Transform interactTransform { get; private set; }
        public Transform interactVisitorTransform { get; private set; }
        private UniTaskCompletionSource<bool> visitorArrivedCompletionSource;
        private UniTaskCompletionSource<bool> dinnerReadyCompletionSource;
        private IFurnitureService _furnitureService;
        private IMoneyData _moneyData;
        private const float PriceMultiplier = 10f;


        [Inject]
        public void Construct(IMoneyData moneyData, IFurnitureService furnitureService)
        {
            _moneyData = moneyData;
            _furnitureService = furnitureService;
        }
        
        private void Start()
        {
            interactTransform = EmployeeTransform;
            interactVisitorTransform = VisitorTransform;  
        }

        public Func<UniTask> CreateWaitForVisitorTask()
        {
            visitorArrivedCompletionSource = new UniTaskCompletionSource<bool>();
            return async () =>
            {
                await visitorArrivedCompletionSource.Task;
                visitorArrivedCompletionSource = null;
            };
        }

        private Func<UniTask> CreateWaitForDinnerTask()
        {
           
            dinnerReadyCompletionSource = new UniTaskCompletionSource<bool>();
            return async () =>
            {
                await dinnerReadyCompletionSource.Task;
                dinnerReadyCompletionSource = null;
            };
        }
        public async UniTask Interact(Employee employee)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(
                StaticDataService.GetFurnitureStaticData()[FurnitureType.Table].speed[Data.Level - 1]));
            if (employee.IsDinner())
            {
                DinnerReady();
                PlaceDinner();
                employee.RemoveDinner();
            }
        }

        public async UniTask Interact(Visitor visitor)
        {
            if(!HasDinnerOnTable)VisitorArrived();
            visitor.transform.position = VisitorTransform.position;
            visitor.transform.forward = VisitorTransform.forward;
            visitor.visitorAnimation.Sit();
            // wait until dinner is ready
            Task task= new Task(CreateWaitForDinnerTask());
            await task.WaitCondition();
            await UniTask.Delay(TimeSpan.FromSeconds(3));
            RemoveDinner();
            float check = _furnitureService.DefineCheck()*PriceMultiplier;
            _moneyData.AddMoney(check); 
                visitor.Eated = true;
        }
        private void DinnerReady()
        {
            if (dinnerReadyCompletionSource != null)
            {
                dinnerReadyCompletionSource.TrySetResult(true);
            }
        }
        private void VisitorArrived()
        {
            if (visitorArrivedCompletionSource != null)
            {
                visitorArrivedCompletionSource.TrySetResult(true);
            }
        }



        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public void Occupy()
        {
            IsOccupied = true;
        }
        private void PlaceDinner()
        {
            _dinner.SetActive(true);
            HasDinnerOnTable = true;
        }

        public bool HasDinnerOnTable { get; set; }

        private void RemoveDinner()
        {
            _dinner.SetActive(false);
            HasDinnerOnTable = false;
        }
        public void Free()
        {
            IsOccupied = false;
        }
    }
}