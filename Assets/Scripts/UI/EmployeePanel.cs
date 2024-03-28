using Datas.Employeers;
using Datas.Furniture;
using Services.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class EmployeePanel : MonoBehaviour
    {
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private TextMeshProUGUI _employeeTitle;
        [SerializeField] private TextMeshProUGUI _employeeSalary;
        [SerializeField] private TextMeshProUGUI _employeeLevel;
        [SerializeField] private TextMeshProUGUI _upgradeCost;
        [SerializeField] private Button _closeButton;

        private EmployeeData _employeeData; 
        private EmployeeStaticDataStructure _employeeStaticData;
        
        private IUIUpdateEventService _uiUpdateEventService;
        private IStaffService _staffService;
        private IStaticDataService _staticDataService;

        [Inject]
        public void Construct(IStaffService staffService, 
            IUIUpdateEventService uiUpdateEventService, 
            IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
            _uiUpdateEventService = uiUpdateEventService;
            _staffService = staffService;
        }

        private void Start()
        {
            _upgradeButton.onClick.AddListener(() =>
            {
                _staffService.PromoteStaff();
            });
            
            _closeButton.onClick.AddListener(() =>
            {
                gameObject.SetActive(false);
            });
            
            _staffService.OnEmployeeSelected += SetEmployeeData;
        }

        public void SetEmployeeData(EmployeeData employeeData)
        {
            _employeeData = employeeData;
            _employeeStaticData = GetEmployeeStaticData(employeeData);
            gameObject.SetActive(true);
        }

        private EmployeeStaticDataStructure GetEmployeeStaticData(EmployeeData employeeData)
        {
            EmployeeStaticDataStructure[] employeeStaticDataStructures = _staticDataService.GetEmployeeStaticData();
            foreach (var staticDataStructure in employeeStaticDataStructures)
            {
                if(staticDataStructure.Type == employeeData.Type)
                    return staticDataStructure;
            }

            return null;
        }

        private void OnEnable()
        {
            _uiUpdateEventService.OnDataChanged += UpdateData;
            UpdateData();
        }

        private void OnDisable()
        {
            _uiUpdateEventService.OnDataChanged -= UpdateData;
            _staffService.UnSelectStaff();
        }

        private void UpdateData()
        {
            _staffService.SelectStaff(_employeeData);
            SetEmployeeTitle(_employeeData.Type.ToString());
            SetEmployeeSalary(_employeeData.Salary);
            SetEmployeeLevel(_employeeData.Level);
            SetUpgradeCost(_employeeStaticData.UpgradeCost[_employeeData.Level]);
            SetUpgradeButtonActive(_employeeStaticData.UpgradeCost.Length > _employeeData.Level + 1);
        }

        private void SetEmployeeTitle(string title) =>
            _employeeTitle.text = title;

        private void SetEmployeeSalary(float salary) =>
            _employeeSalary.text = "<color=red>" + salary + "$</color>/ day";
 
        private void SetEmployeeLevel(int level) =>
            _employeeLevel.text = "Level " + level;

        private void SetUpgradeCost(float cost) =>
            _upgradeCost.text = cost.ToString();

        private void SetUpgradeButtonActive(bool active) =>
            _upgradeButton.gameObject.SetActive(active);
    }
}