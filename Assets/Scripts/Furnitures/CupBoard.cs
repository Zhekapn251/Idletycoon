using System;
using Cysharp.Threading.Tasks;
using Datas.Furniture;
using Employees;
using Tasks;
using UnityEngine;

namespace Furnitures
{
    public class CupBoard:Furniture, IInteractable
    {
        public Transform TransformEmployee;
        public Transform interactTransform { get; set; }

        private void Start()
        {
            interactTransform = TransformEmployee;
        }

        public async UniTask Interact(Employee employee)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(StaticDataService.GetFurnitureStaticData()
                [FurnitureType.CupBoard].speed[Data.Level - 1]));
        }
        
        public Vector3 GetPosition()
        {
            return transform.position;
        }
    }
}