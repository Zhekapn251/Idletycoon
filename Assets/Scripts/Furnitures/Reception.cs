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
    public class Reception: Furniture, IInteractable, IInteractableVisitor
    {
        public Transform TransformVisitor;
        public Transform TransformEmployee;
        public Transform interactTransform { get; private set; }
        public Transform interactVisitorTransform { get; private set; }
        private UniTaskCompletionSource<bool> visitorArrivedCompletionSource;
        private UniTaskCompletionSource<bool> employeeArrivedCompletionSource;

        private void VisitorArrived()
        {
            if (visitorArrivedCompletionSource != null)
            {
                visitorArrivedCompletionSource.TrySetResult(true);
            }
        }
        
        private void EmployeeArrived()
        {
            if (employeeArrivedCompletionSource != null)
            {
                employeeArrivedCompletionSource.TrySetResult(true);
            }
        }

        private void Start()
        {
            interactTransform = TransformEmployee;
            interactVisitorTransform = TransformVisitor;
        }

        public Func<UniTask> CreateWaitForVisitorTask()
        {
            visitorArrivedCompletionSource = new UniTaskCompletionSource<bool>();
            return async () =>
            {
                await visitorArrivedCompletionSource.Task;
            };
        }
        
        public Func<UniTask> CreateWaitForEmployeeTask()
        {
            employeeArrivedCompletionSource = new UniTaskCompletionSource<bool>();
            return async () =>
            {
                await employeeArrivedCompletionSource.Task;
            };
        }

        public async UniTask Interact(Employee employee)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(
                StaticDataService.GetFurnitureStaticData()[FurnitureType.Reception].speed[Data.Level - 1]));
            EmployeeArrived();
        }

        public async UniTask Interact(Visitor visitor)
        {
           VisitorArrived();
           await UniTask.Delay(TimeSpan.FromSeconds(1));
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }
    }
}