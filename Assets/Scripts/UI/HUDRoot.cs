using System;
using System.Collections;
using System.Collections.Generic;
using Datas.Employeers;
using Datas.Furniture;
using DefaultNamespace.UI;
using DG.Tweening;
using Services.Implementation;
using Services.Interfaces;
using UnityEngine;
using TMPro;
using UI;
using UnityEngine.UI;
using Zenject;


public class HUDRoot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _money;
    [SerializeField] private TextMeshProUGUI _time;
    [SerializeField] private TextMeshProUGUI _moneyTextAnimation;
    
    [SerializeField] private MainPanel _mainPanel;
    [SerializeField] private FurniturePanel _furniturePanel;
    [SerializeField] private EmployersPanel _employersPanel;
    [SerializeField] private FurnitureItemPanel _furnitureItemPanel;
    [SerializeField] private EmployeePanel _employeePanel;
    
    [SerializeField] private Button _menuButton;
    private IFurnitureService _furnitureService;
    private IStaffService _staffService;
    private IPersistantDataService _persistantDataService;
    private ITimeService _timeService;
    private int _hour;
    private int _minutes;
    float _moneyValue;
    private IUIUpdateEventService _uiUpdateEventService;

    private void Start()
    {
        _menuButton.onClick.AddListener(() =>
        {
            _mainPanel.gameObject.SetActive(true);
            _furniturePanel.gameObject.SetActive(false);
            _employersPanel.gameObject.SetActive(false);
            _furnitureItemPanel.gameObject.SetActive(false);
            _employeePanel.gameObject.SetActive(false);
        });
        
        _timeService.OnHourChanged += OnHourChanged;
        _timeService.OnMinutesChanged += OnMinutesChanged;
    }

    private void OnMinutesChanged(float obj)
    {
        _minutes = (int) obj;
        _time.text = _hour.ToString("D2") + ":" + _minutes.ToString("D2");

    }

    private void OnHourChanged(float obj)
    {
        _hour = (int )obj;
        _time.text = _hour.ToString("D2") + ":" + _minutes.ToString("D2");
 
    }

    [Inject]
    public void Construct(IFurnitureService furnitureService,
        IStaffService staffService,
        IPersistantDataService persistantDataService,
        ITimeService timeService,
        IUIUpdateEventService uiUpdateEventService)
    {
        _uiUpdateEventService = uiUpdateEventService;
        _timeService = timeService;
        _persistantDataService = persistantDataService;
        _staffService = staffService;
        _furnitureService = furnitureService;
        
        _staffService.OnEmployeeSelected += OnEmployeeSelected;
        _furnitureService.OnFurnitureSelected += OnFurnitureSelected;
        _uiUpdateEventService.OnMoneyChanged += RenewMoney;
        
    }

    private void OnFurnitureSelected(FurnitureData obj)
    {
        _furnitureItemPanel.gameObject.SetActive(true);
        _furnitureItemPanel.SetData(obj);
    }

    private void OnEmployeeSelected(EmployeeData obj)
    {
        _employeePanel.gameObject.SetActive(true);
        _employeePanel.SetEmployeeData(obj);
    }

    public void RenewMoney(float money)
    {
        if (_moneyValue < money)
        {
            AnimateAddMoney(money - _moneyValue);
        }
        else
        {
            AnimateSubtractMoney(_moneyValue - money);
        }

        _money.text = money.ToString("F2");
        _moneyValue = money;
    }

    private void AnimateSubtractMoney(float moneyValue)
    {
        _moneyTextAnimation.text = "-" + moneyValue;
        Vector3 startPosition = _moneyTextAnimation.transform.position;
        _moneyTextAnimation.color = Color.red;
        _moneyTextAnimation.DOFade(0, 0);
        DOTween.Sequence()
            .Append(_moneyTextAnimation.DOFade(1, 0.3f))
            .AppendCallback(()=>_moneyTextAnimation.transform.DOMoveY(startPosition.y + 50, 1f))
            .AppendInterval(0.3f)
            .Append(_moneyTextAnimation.DOFade(0, 0.3f))
            .AppendCallback(()=>_moneyTextAnimation.transform.position = startPosition);
        
    }

    private void AnimateAddMoney(float money)
    {
        _moneyTextAnimation.text = "+" + money;
        Vector3 startPosition = _moneyTextAnimation.transform.position;
        _moneyTextAnimation.color = Color.green;
        _moneyTextAnimation.DOFade(0, 0);
        DOTween.Sequence()
            .Append(_moneyTextAnimation.DOFade(1, 0.3f))
            .AppendCallback(()=>_moneyTextAnimation.transform.DOMoveY(startPosition.y + 50, 1f))
            .AppendInterval(0.3f)
            .Append(_moneyTextAnimation.DOFade(0, 0.3f))
            .AppendCallback(()=>_moneyTextAnimation.transform.position = startPosition);
    }
}
