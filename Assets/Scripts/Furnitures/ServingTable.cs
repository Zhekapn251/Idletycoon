using System;
using Cysharp.Threading.Tasks;
using Datas.Employeers;
using Datas.Furniture;
using Employees;
using Tasks;
using UnityEngine;

namespace Furnitures
{
    public class ServingTable: Furniture, IInteractable
    {
        [SerializeField] private GameObject _dinner;
        public Transform TransformEmployee;
        public Transform interactTransform { get; private set; }
        private bool HasDinnerOnTable;
        private UniTaskCompletionSource<bool> orderPlacedCompletionSource;
        private UniTaskCompletionSource<bool> isDinnerReadyCompletionSource;

        private void OrderPlaced()
        {
            if (orderPlacedCompletionSource != null)
            {
                orderPlacedCompletionSource.TrySetResult(true);
            }
        }
        
        private void IsDinnerReady()
        {
            if (isDinnerReadyCompletionSource != null)
            {
                isDinnerReadyCompletionSource.TrySetResult(true);
            }
        }
        private void Start()
        {
            interactTransform = TransformEmployee;
        }
            
        public Func<UniTask> CreateWaitForOrderTask()
        {
            orderPlacedCompletionSource = new UniTaskCompletionSource<bool>();
            return async () =>
            {
                await orderPlacedCompletionSource.Task;
            };
        }
        
        public Func<UniTask> CreateWaitForDinnerTask()
        {
            isDinnerReadyCompletionSource = new UniTaskCompletionSource<bool>();
            return async () =>
            {
                await isDinnerReadyCompletionSource.Task;
            };
        }

        public async UniTask Interact(Employee employee)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(
                StaticDataService.GetFurnitureStaticData()[FurnitureType.ServingTable].speed[Data.Level - 1]));
            
            if(employee is Chef)
            {
                IsDinnerReady(); 
                PlaceDinner();
            }
            else if(employee is Waiter)
            {
                if (HasDinnerOnTable)
                {
                    employee.TakeDinner();
                    RemoveDinner();
                }
                else
                {
                    OrderPlaced();
                }
                
            }
        }
        public bool HasDinner()
        {
            return HasDinnerOnTable;
        }

        private void PlaceDinner()
        {
            _dinner.SetActive(true);
            HasDinnerOnTable = true;
        }

        private void RemoveDinner()
        {
            _dinner.SetActive(false);
            HasDinnerOnTable = false;
        }
        public Vector3 GetPosition()
        {
            return transform.position;
        }
    }
}