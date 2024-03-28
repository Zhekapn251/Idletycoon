using System;
using UnityEngine;
using UnityEngine.UI;

public class EmployeeButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _icon;
    
    
    public void Init(Sprite icon, int index, Action<int> onClick)
    {
        _icon.sprite = icon;
        _button.onClick.AddListener(() => onClick?.Invoke(index));
    }

}
