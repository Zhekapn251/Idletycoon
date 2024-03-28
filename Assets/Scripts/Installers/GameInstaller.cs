using System;
using System.Collections.Generic;
using Datas;
using Datas.Employeers;
using Datas.Furniture;
using DefaultNamespace;
using DefaultNamespace.Factories;
using DefaultNamespace.Services;
using Factories.Implementation;
using Factories.Interfaces;
using Services;
using Services.Implementation;
using Services.Interfaces;
using UnityEngine;
using VisitorsRest;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private SpawnPointsData[] _spawnPointsDatas;
    [SerializeField] private FurniturePrefabsSO _furniturePrefabsSo;
    [SerializeField] private StaticDataSO staticDataSo;
    [SerializeField] private EmployeePrefabsSO _employeePrefabsSo;
    [SerializeField] private UiIconsSO _uiIconsSo;
    [SerializeField] private VisitorPrefabSO _visitorPrefabSo;
    [SerializeField] private Transform _employeeSpawnPoint;

    private Dictionary<FurnitureType, FurniturePrefabStructure> _furniturePrefabStructures;
    private Dictionary<FurnitureType, SpawnPoint[]> _spawnPoints;
    public override void InstallBindings()
    {
        InitializeDictionaries();
        Container.BindInterfacesAndSelfTo<PersistantDataService>().AsSingle();
        Container.BindInterfacesAndSelfTo<UIUpdateService>().AsSingle(); 
        Container.Bind<IFurnitureFactory>().To<FurnitureFactory>().AsSingle().WithArguments(_furniturePrefabStructures, _spawnPoints);
        Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle().WithArguments(staticDataSo);
        Container.Bind<IEmployeeFactory>().To<EmployeeFactory>().AsSingle().WithArguments(_employeePrefabsSo, _employeeSpawnPoint);
        Container.Bind<IFurnitureService>().To<FurnitureService>().AsSingle();
        Container.Bind<IUIFactory>().To<UIFactory>().AsSingle().WithArguments(_uiIconsSo);
        Container.BindInterfacesAndSelfTo<CameraService>().AsSingle();
        Container.Bind<IStaffService>().To<StaffService>().AsSingle();
        Container.BindInterfacesAndSelfTo<TimeService>().AsSingle();
        Container.Bind<IVisitorFactory>().To<VisitorFactory>().AsSingle().WithArguments(_visitorPrefabSo);
        Container.BindInterfacesAndSelfTo<VisitorService>().AsSingle();
        Container.Bind<IPointerOverUIService>().To<PointerOverUIService>().AsSingle();
    }

    private void InitializeDictionaries()
    {
        _furniturePrefabStructures = new Dictionary<FurnitureType, FurniturePrefabStructure>();
        foreach (var furniture in _furniturePrefabsSo.Prefabs)
        {
            _furniturePrefabStructures.Add(furniture.FurnitureType, furniture);
        }
       
        _spawnPoints = new Dictionary<FurnitureType, SpawnPoint[]>();
        foreach (var spawnPointsData in _spawnPointsDatas)
        {
            _spawnPoints.Add(spawnPointsData.FurnitureType, spawnPointsData.SpawnPoints);
        }
    }
}


[Serializable]
public class SpawnPointsData
{
    public FurnitureType FurnitureType;
    public SpawnPoint[] SpawnPoints;
}