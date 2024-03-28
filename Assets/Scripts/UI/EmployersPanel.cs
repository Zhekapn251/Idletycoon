using System.Collections.Generic;
using Datas;
using DefaultNamespace.UI;
using Factories.Interfaces;
using Services.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class EmployersPanel:MonoBehaviour
    {
        [SerializeField] private EmployeeButton _buttonPrefab;
        [SerializeField] private EmployeePanel _employeePanel;
        [SerializeField] private Button _closeButton;  

        private List<EmployeeButton> _buttons = new List<EmployeeButton>();

        private IUIFactory _uiFactory;
        private int[] _employeeIndex = new int[3];
        private GameData _gameData;
        private IUIUpdateEventService _uiUpdateEventService;
        private IStaffService _staffService;

        private void Start()
        {
            _closeButton.onClick.AddListener(() =>
            {
                gameObject.SetActive(false);
            });
        }

        [Inject]
        private void Construct(IUIFactory uiFactory, 
            IUIUpdateEventService uiUpdateEventService, 
            IStaffService staffController, 
            IPersistantDataService persistantDataService)
        {
            _uiFactory = uiFactory;
            _uiUpdateEventService = uiUpdateEventService;
             _staffService = staffController;       
        }
        

        public void Init(GameData gameData)
        {
            _gameData = gameData;
            
            ButtonInit();
        }

        private void OnEnable()
        {
            ButtonInit();
        }

        private void ButtonInit()
        {
            DeleteButtons();
            
            for (int i = 0; i < _gameData.employees.Count; i++)
            {
                var button = Instantiate(_buttonPrefab, transform);
                button.Init(_uiFactory.GetEmployeeIcon(_gameData.employees[i].Type, 
                    _employeeIndex[(int)_gameData.employees[i].Type]),
                    i, OnButtonClick);
                _employeeIndex[(int)_gameData.employees[i].Type]++;
                _buttons.Add(button);
            }
        }
        private void DeleteButtons()
        {
            foreach (var button in _buttons)
            {
                Destroy(button.gameObject);
            }
            _buttons.Clear();
            _employeeIndex = new int[3];
        }
        

        private void OnButtonClick(int index)
        {
          _employeePanel.SetEmployeeData(_gameData.employees[index]); 
          _employeePanel.gameObject.SetActive(true);
        }
        
    }
}