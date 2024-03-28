using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Tasks;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.AI;

namespace VisitorsRest
{
    public class Visitor: MonoBehaviour
    {
        public VisitorAnimation visitorAnimation;
        public bool Eated { get; set; }
        public event Action<Visitor> OnEndedTasks;
        private Queue<Task> _tasks;
        public bool isBusy = false;
        public NavMeshAgent agent;
        private bool isTask = false;
        private IInteractableVisitor currentTarget;
        private IVisitorService _visitorService;
        private Transform _respawnPoint;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            _tasks = new Queue<Task>();
        }
        public void AddTask(Task task)  
        {
            _tasks.Enqueue(task);
        }  
        private void Update()
        {
            if (!isBusy && _tasks.Count > 0) 
            {
                StartNewTask();    
            }
            if (currentTarget != null && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && !agent.isStopped )
            {
                OnReachedDestination();   
            }
        }
        public void SetRespawnPoint(Transform respawnPoint)
        {
            _respawnPoint = respawnPoint;
        }
        private void SetDestination(Vector3 destination, IInteractableVisitor target)
        {
            agent.SetDestination(destination);
            agent.isStopped = false;
            isBusy = true;
            visitorAnimation.Walk();
        }
        
        private async UniTask StartNewTask()
        {
            if (_tasks.Count > 0)
            {
                Task task = _tasks.Dequeue();
                isBusy = true;
                await PerformTask(task);
            }
        }
        private async UniTask PerformTask(Task task)
        {
            switch (task.TaskType)
            {
                case TaskType.Condition:
                    currentTarget = null;
                    //visitorAnimation.Sit();
                    await task.WaitCondition();
                    OnReachedDestination();
                    break;
                case TaskType.Destination:
                    SetDestination(task.Destination, task.InteractionObjectVisitor);
                    currentTarget = task.InteractionObjectVisitor;
                    break;
                case TaskType.Time:
                    currentTarget = null;
                    await UniTask.Delay(TimeSpan.FromSeconds(task.WaitTime.Value));
                    OnReachedDestination();
                    break;
            }
        }
        public void EnableAgent(bool enable)
        {
            agent.isStopped = enable;
        }
        private async void OnReachedDestination()
        {
            agent.isStopped = true;
            
            if (currentTarget != null)
            {
                visitorAnimation.Idle();
                await UniTask.Yield(PlayerLoopTiming.Update);
                await currentTarget.Interact(this);
            }
            isBusy = false;
            if (_tasks.Count == 0)
            {
                OnEndedTasks?.Invoke(this);
            }
                     
        }
        public async void DestroyVisitor()
        {
            agent.isStopped = false;
            agent.SetDestination(_respawnPoint.position);
            visitorAnimation.Walk();
            await UniTask.Delay(TimeSpan.FromSeconds(5));
            currentTarget = null;
            Destroy(gameObject);
        }
    }
}