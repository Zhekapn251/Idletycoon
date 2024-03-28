using Cysharp.Threading.Tasks;
using UnityEngine;
using VisitorsRest;

namespace Tasks
{
    public interface IInteractableVisitor
    {
        Transform interactVisitorTransform { get; }
        UniTask Interact(Visitor visitor);
        Vector3 GetPosition();
    }
}