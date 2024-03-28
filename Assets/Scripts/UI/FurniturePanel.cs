using Datas;
using Datas.Furniture;
using DefaultNamespace.UI;
using Factories.Interfaces;
using Services.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FurniturePanel : MonoBehaviour
    {
        [Header("Fridge")] [SerializeField] private Button _fridgeButton;
        [SerializeField] private TextMeshProUGUI _fridgeCount;

        [Header("Table")] [SerializeField] private Button _tableButton;
        [SerializeField] private TextMeshProUGUI _tableCount;

        [Header("Cupboard")] [SerializeField] private Button _cupboardButton;
        [SerializeField] private TextMeshProUGUI _cupboardCount;

        [Header("Cutting Table")] [SerializeField] private Button _cuttingTableButton;
        [SerializeField] private TextMeshProUGUI _cuttingTableCount;

        [Header("Oven")] [SerializeField] private Button _ovenButton;
        [SerializeField] private TextMeshProUGUI _ovenCount;

        [Header("Sink")] [SerializeField] private Button _sinkButton;
        [SerializeField] private TextMeshProUGUI _sinkCount;

        [Header("Toilet")] [SerializeField] private Button _toiletButton;
        [SerializeField] private TextMeshProUGUI _toiletCount;

        [Header("Serving Table")] [SerializeField]
        private Button _servingTableButton;

        [SerializeField] private TextMeshProUGUI _servingTableCount;

        [Header("Cleaning Shelves")] 
        [SerializeField] private Button _cleaningShelvesButton;
        [SerializeField] private TextMeshProUGUI _cleaningShelvesCount;
        
        [Header("Recetion")] 
        [SerializeField] private Button _receptionButton;
        [SerializeField] private TextMeshProUGUI _recetionCount;

        [SerializeField] private Button _closeButton;

        [SerializeField] private FurnitureItemPanel _furnitureItemPanel;
        private IUIFactory _uiFactory;
        private FurnitureStaticDataStructure[] _furnitureStaticDataStructures;
        private GameData _gameData;
        private IFurnitureService _furnitureService;
        private IStaticDataService _staticDataService;

        public void Init(IUIFactory uiFactory,
            GameData gameData,
            IFurnitureService furnitureService,
            IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
            _furnitureService = furnitureService;
            _gameData = gameData;
            _uiFactory = uiFactory;
        }

        private void Start()
        {
            _fridgeButton.onClick.AddListener(() => OnButtonClick(FurnitureType.Fridge));
            _tableButton.onClick.AddListener(() => OnButtonClick(FurnitureType.Table));
            _cupboardButton.onClick.AddListener(() => OnButtonClick(FurnitureType.CupBoard));
            _cuttingTableButton.onClick.AddListener(() => OnButtonClick(FurnitureType.CuttingTable));
            _ovenButton.onClick.AddListener(() => OnButtonClick(FurnitureType.Oven));
            _sinkButton.onClick.AddListener(() => OnButtonClick(FurnitureType.ToiletSink));
            _toiletButton.onClick.AddListener(() => OnButtonClick(FurnitureType.Toilet));
            _servingTableButton.onClick.AddListener(() => OnButtonClick(FurnitureType.ServingTable));
            _cleaningShelvesButton.onClick.AddListener(() => OnButtonClick(FurnitureType.CleaningShelves));
            _receptionButton.onClick.AddListener(()=> OnButtonClick(FurnitureType.Reception));
            _closeButton.onClick.AddListener(() => gameObject.SetActive(false));
            UpdateUI();
        }

        private void OnEnable()
        {
            UpdateUI();
        }

        private void OnButtonClick(FurnitureType furniture)
        {
            _furnitureItemPanel.SetType(furniture);
            _furnitureItemPanel.gameObject.SetActive(true);
        }

        private void UpdateUI()
        {
            _fridgeCount.text = _gameData.furnitures.FindAll(furniture => furniture.Type == FurnitureType.Fridge).Count
                .ToString();
            _tableCount.text = _gameData.furnitures.FindAll(furniture => furniture.Type == FurnitureType.Table).Count
                .ToString();
            _cupboardCount.text = _gameData.furnitures.FindAll(furniture => furniture.Type == FurnitureType.CupBoard)
                .Count.ToString();
            _cuttingTableCount.text = _gameData.furnitures
                .FindAll(furniture => furniture.Type == FurnitureType.CuttingTable).Count.ToString();
            _ovenCount.text = _gameData.furnitures.FindAll(furniture => furniture.Type == FurnitureType.Oven).Count
                .ToString();
            _sinkCount.text = _gameData.furnitures.FindAll(furniture => furniture.Type == FurnitureType.ToiletSink)
                .Count.ToString();
            _toiletCount.text = _gameData.furnitures.FindAll(furniture => furniture.Type == FurnitureType.Toilet).Count
                .ToString();
            _servingTableCount.text = _gameData.furnitures
                .FindAll(furniture => furniture.Type == FurnitureType.ServingTable).Count.ToString();
            _cleaningShelvesCount.text = _gameData.furnitures
                .FindAll(furniture => furniture.Type == FurnitureType.CleaningShelves).Count.ToString();
            _recetionCount.text = _gameData.furnitures.FindAll(furniture => furniture.Type == FurnitureType.Reception)
                .Count.ToString();
        }
    }
}