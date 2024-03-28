using System;
using Datas;
using Datas.Furniture;
using DefaultNamespace.Factories;
using Factories.Interfaces;
using Services.Interfaces;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace DefaultNamespace.UI
{
    public class FurnitureItemPanel : MonoBehaviour
    {
        [Header("Texts and Images")] 
        [SerializeField] private Image _furnitureImage;
        [SerializeField] private TextMeshProUGUI _furnitureTitle;
        [SerializeField] private TextMeshProUGUI _furnitureLevel;
        [SerializeField] private TextMeshProUGUI _furnitureTime;
        [SerializeField] private TextMeshProUGUI _upgradeCost;

        [Header("Buttons and Sprites")] 
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button[] _furnitureButtons;
        [SerializeField] private Sprite _selectedSprite;
        [SerializeField] private Sprite _unselectedSprite;

        private FurnitureData[] _furnitureArray;
        private FurnitureData _selectedFurniture;
        private IFurnitureService _furnitureService;

        private FurnitureType _furnitureType;
        private IUIFactory _uiFactory;
        private IStaticDataService _staticDataService;
        
        private int _selectedFurnitureIndex;
        private IUIUpdateEventService _uiUpdateEventService;

        [Inject]
        public void Construct(IFurnitureService furnitureService,
            IUIFactory uiFactory,
            IStaticDataService staticDataService, 
            IUIUpdateEventService uiUpdateEventService)
        {
            _uiUpdateEventService = uiUpdateEventService;
            _uiFactory = uiFactory;
            _staticDataService = staticDataService;
            _furnitureService = furnitureService;
        }


        public void SetType(FurnitureType furnitureType)
        {
            _furnitureType = furnitureType;
            SelectFurniture(0);
            SetSprite();
        }

        public void SetData(FurnitureData furnitureData)
        {
            gameObject.SetActive(true);
            _selectedFurniture = furnitureData;
            _furnitureType = furnitureData.Type;
            _selectedFurnitureIndex = Array.IndexOf(_furnitureArray, furnitureData);
            SetSprite();
            UpdateUI();
            SelectFurniture(0); 
        }
        

        private void OnEnable()
        {
            _furnitureArray = _furnitureService.GetFurnitureArray(_furnitureType);
            _uiUpdateEventService.OnDataChanged += UpdateUI;
        }
        
        private void OnDisable()
        {
            _uiUpdateEventService.OnDataChanged -= UpdateUI;
        }

        private void SelectFurniture(int index)
        {
            _furnitureArray = _furnitureService.GetFurnitureArray(_furnitureType);
            _furnitureService.SelectFurniture(_furnitureArray[index]);
            _selectedFurniture = _furnitureArray[index];
            _selectedFurnitureIndex = index;
            UpdateUI();
        }

        private void UpdateUI()
        {
            _furnitureTitle.text = _selectedFurniture.Type.ToString();
            _furnitureLevel.text = _selectedFurniture.Level.ToString();
            _furnitureTime.text = _selectedFurniture.Time.ToString();
            _upgradeCost.text = _staticDataService.GetUpgradeCost(_selectedFurniture.Type, _selectedFurniture.Level)
                .ToString();
            
            for (int i = 0; i < _furnitureButtons.Length; i++)
            {
                _furnitureButtons[i].image.sprite = i == _selectedFurnitureIndex ? _selectedSprite : _unselectedSprite;
            }
        }


        private void SetSprite()
        {
            _furnitureImage.sprite = _uiFactory.GetFurnitureIcon(_furnitureType);
        }

        private void Start()
        {
            _upgradeButton.onClick.AddListener(() => { _furnitureService.UpgradeFurniture(); });

            _closeButton.onClick.AddListener(() => { gameObject.SetActive(false); });
            _furnitureService.OnFurnitureSelected += SetData;

            for (int i = 0; i < _furnitureButtons.Length; i++)
            {
                int index = i;
                if(_furnitureArray.Length > i)
                {_furnitureButtons[i].onClick.AddListener(() =>
                {
                    _furnitureService.SelectFurniture(_furnitureArray[index]);
                });}
                else
                {
                    _furnitureButtons[i].gameObject.SetActive(false);
                }

            }
            
        }
    }
}