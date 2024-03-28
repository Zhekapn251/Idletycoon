using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Tasks
{
    public class Task
    {
        public Vector3 Destination { get; }
        public IInteractable InteractionObject { get; }
        
        public IInteractableVisitor InteractionObjectVisitor { get; }

        public TaskType TaskType { get; }
        public float? WaitTime { get; }
        public Func<UniTask> WaitCondition { get; } 
        
        public Task( IInteractable interactionObject)
        {
            Destination = interactionObject.interactTransform.position;
            InteractionObject = interactionObject;
            TaskType = TaskType.Destination;
        }
        public Task( IInteractableVisitor interactionObject)
        {
            Destination = interactionObject.interactVisitorTransform.position;
            InteractionObjectVisitor = interactionObject;
            TaskType = TaskType.Destination;
        }
        
        public Task(float waitTime)
        {
            WaitTime = waitTime;
            TaskType = TaskType.Time;
        }
        
        public Task(Func<UniTask> waitCondition)
        {
            WaitCondition = waitCondition;
            TaskType = TaskType.Condition;
        }
    }
}