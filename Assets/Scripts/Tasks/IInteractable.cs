using Cysharp.Threading.Tasks;
using Employees;
using UnityEngine;

namespace Tasks
{
    public interface IInteractable
    {
        Transform interactTransform { get; }
        UniTask Interact(Employee employee);
        Vector3 GetPosition();
    }
}