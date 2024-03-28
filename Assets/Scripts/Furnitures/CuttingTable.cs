using System;
using Cysharp.Threading.Tasks;
using Datas.Furniture;
using Employees;
using Tasks;
using UnityEngine;

namespace Furnitures
{
    public class CuttingTable:Furniture, IInteractable
    {
        public Transform TransformEmployee;
        public Transform interactTransform { get; private set; }

        private void Start()
        {
            interactTransform = TransformEmployee;
        }

        public async UniTask Interact(Employee employee)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(
                StaticDataService.GetFurnitureStaticData()[FurnitureType.CuttingTable].speed[Data.Level - 1]));
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }
    }
}