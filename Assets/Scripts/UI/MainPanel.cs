using System;
using DefaultNamespace.Factories;
using DefaultNamespace.Services;
using Factories.Interfaces;
using Restaurant;
using Services.Interfaces;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace DefaultNamespace.UI
{
    public class MainPanel : MonoBehaviour
    {
        [SerializeField] private Button _employersButton;
        [SerializeField] private Button _furnitureButton;
        [SerializeField] private EmployersPanel _employersPanel;
        [SerializeField] private FurniturePanel _furniturePanel;
        [SerializeField] private Button _closeButton;


        private IUIFactory _uiFactory;
        private IPersistantDataService _persistantDataService;
        private IStaticDataService _staticDataService;
        private IUIUpdateEventService _uiUpdateEventService;
        private IStaffService _staffService;
        private ICameraService _cameraService;
        private IFurnitureService _furnitureService;


        [Inject]
        public void Construct(IUIFactory uiFactory,
            IStaticDataService staticDataService,
            IPersistantDataService persistantDataService, 
            IUIUpdateEventService uiUpdateEventService,
            IStaffService staffService, 
            ICameraService cameraService,
            IFurnitureService furnitureService)
        {
            _furnitureService = furnitureService;
            _cameraService = cameraService;
            _staffService = staffService;
            _uiUpdateEventService = uiUpdateEventService;
            _staticDataService = staticDataService;
            _persistantDataService = persistantDataService;
            _uiFactory = uiFactory;
        }
 

        private void Start()
        {
            _employersButton.onClick.AddListener(OnEmployersButtonClick);
            _furnitureButton.onClick.AddListener(OnFurnitureButtonClick);
            _closeButton.onClick.AddListener(OnCloseButtonClick);
        }

        private void OnEnable()
        {
            _cameraService.SetCameraMode(CameraControl.CameraMode.Centered);
        }

        private void OnDisable()
        {
            _cameraService.SetCameraMode(CameraControl.CameraMode.Free);
        }

        private void OnFurnitureButtonClick()
        {
            _furniturePanel.Init(_uiFactory, _persistantDataService.GameData, _furnitureService, _staticDataService);
            _furniturePanel.gameObject.SetActive(true);
        }

        private void OnEmployersButtonClick()
        {
            _employersPanel.Init(
                _persistantDataService.GameData);
            _employersPanel.gameObject.SetActive(true);
        }

        private void OnCloseButtonClick() => 
            gameObject.SetActive(false);
    }
}