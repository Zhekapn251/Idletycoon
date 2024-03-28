using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Datas;
using Datas.Furniture;
using Factories.Interfaces;
using Furnitures;
using Services;
using Services.Interfaces;
using UnityEngine;
using Zenject;

namespace DefaultNamespace.Factories
{
    public class FurnitureFactory : IFurnitureFactory
    {
        private Dictionary<FurnitureType, FurniturePrefabStructure> _furniturePrefabs;
        private Dictionary<FurnitureType, SpawnPoint[]> _spawnPoints;
        private readonly IFurnitureService _furnitureService;
        private DiContainer _container;

        public FurnitureFactory(Dictionary<FurnitureType, FurniturePrefabStructure> furniturePrefabs,
            Dictionary<FurnitureType, SpawnPoint[]> spawnPoints, DiContainer container)
        {
            _furniturePrefabs = furniturePrefabs;
            _spawnPoints = spawnPoints;
            _container = container;
        }

        public async UniTask<Furniture> Create(FurnitureType furnitureType)
        {
          
            GameObject prefab = await ResourcesLoader.LoadResourceAsync<GameObject>(_furniturePrefabs[furnitureType].PrefabPath);
            SpawnPoint spawnPoint = GetFreeSpawnPoint(furnitureType);
            GameObject furniture = _container.InstantiatePrefab(prefab);
            furniture.transform.position = spawnPoint.transform.position;
            furniture.transform.parent = spawnPoint.transform;
            spawnPoint.IsOccupied = true;
            InitializeFurniture(furniture, furnitureType);
            Furniture furnitureComponent = furniture.GetComponent<Furniture>();
            return furnitureComponent;
        }

        private void InitializeFurniture(GameObject furniture, FurnitureType furnitureType)
        {   
            Furniture furnitureComponent = furniture.GetComponent<Furniture>();
            
            if (furnitureComponent == null)
            {
                Debug.LogError("Furniture component not found");
                return;
            }
            
            furnitureComponent.Data.Type = furnitureType;
            furnitureComponent.Data.Level = 1;
            furnitureComponent.LevelPrefab = _furniturePrefabs[furnitureType].ModelsPath;
            furnitureComponent.LoadModel();
        }

        public bool CheckSpawnPoint(FurnitureType furnitureType)
        {
            SpawnPoint[] spawnPoints = _spawnPoints[furnitureType];

            foreach (var point in spawnPoints)
            {
                if (!point.IsOccupied)
                    return true;
            }

            return false;
        }

        private SpawnPoint GetFreeSpawnPoint(FurnitureType furnitureType)
        {
            SpawnPoint[] spawnPoints = _spawnPoints[furnitureType];

            foreach (var point in spawnPoints)
            {
                if (!point.IsOccupied)
                    return point;
            }

            return null;
        }

    }
}