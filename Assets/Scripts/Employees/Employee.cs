using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Datas.Employeers;
using DefaultNamespace.UI;
using Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Employees
{
    public abstract class Employee: MonoBehaviour
    {
        public event Action<Employee> OnEmployeeStateChanged; 
        public EmployeeAnimation EmployeeAnimation;
        [SerializeField] private EmployeeUI _employeeUI;
        [SerializeField] private GameObject _dinner;
        public EmployeeData EmployeeData;
        public EmployeeType EmployeeType { get; set; }

        public bool IsDinner()
        {
            return HasDinner;}

        private Queue<Task> _tasks;
        public bool isBusy = false;
        private bool HasDinner;
        public NavMeshAgent agent;
        private bool isTask = false;
        private IInteractable currentTarget;
        private void Awake()
        {
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
                StartNextTask();  
            }
            else if(_tasks.Count == 0 && !isBusy)
            {
                OnEmployeeStateChanged?.Invoke(this);
            }
            if (!agent.pathPending && (agent.remainingDistance - agent.stoppingDistance) <= 0.2f && !agent.isStopped && currentTarget != null)
            {
                OnReachedDestination();
            }
        }
        private void SetDestination(Vector3 destination, IInteractable target)
        {
            agent.SetDestination(destination);
            agent.isStopped = false;
            isBusy = true;
            EmployeeAnimation?.Walk();
        }
        
        private async UniTaskVoid StartNextTask()
        {
            if (_tasks.Count > 0)
            {
                var task = _tasks.Dequeue();
                isBusy = true;

                switch (task.TaskType)
                {
                    case TaskType.Condition:
                        currentTarget = null;
                        await task.WaitCondition();
                        OnReachedDestination();
                        break;
                    case TaskType.Destination:
                        SetDestination(task.Destination, task.InteractionObject);
                        currentTarget = task.InteractionObject;
                        break;
                    case TaskType.Time:
                        currentTarget = null;
                        await UniTask.Delay(TimeSpan.FromSeconds(task.WaitTime.Value));
                        OnReachedDestination();
                        break;
                }
            }
            else
            {
                OnEmployeeStateChanged?.Invoke(this);
            }
        }
        public void TakeDinner()
        {
            HasDinner = true;
            _dinner.SetActive(true);
        }
        
        public void RemoveDinner()
        {
            HasDinner = false;
            _dinner.SetActive(false);
        }
        private async void OnReachedDestination()
        {
            if (currentTarget != null)
            {
                EmployeeAnimation.Idle();
                agent.isStopped = true;
                await _employeeUI.AnimateWaiting(1f);
                await currentTarget.Interact(this);
                isBusy = false;
            }
            else
            {
                EmployeeAnimation.Idle();
                agent.isStopped = true;
                isBusy = false;
            }
        }
    }
}