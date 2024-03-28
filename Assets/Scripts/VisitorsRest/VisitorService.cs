using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Datas.Furniture;
using Furnitures;
using Services.Interfaces;
using Tasks;
using Zenject;

namespace VisitorsRest
{
    public class VisitorService : IVisitorService
    {
        private readonly IFurnitureService _furnitureService;
        private Queue<Task> taskQueue = new Queue<Task>();
        private List<Visitor> visitors = new List<Visitor>();


        public VisitorService(IFurnitureService furnitureService)
        {
            _furnitureService = furnitureService;
            _furnitureService.OnFurnitureCreated += Initialize;
        }

        public void AssignTasksToVisitor(Visitor visitor)
        {
            foreach (Task task in taskQueue)
            {
                visitor.AddTask(task);
            }
        }
        
        public void RegisterVisitor(Visitor visitor)
        {
            visitors.Add(visitor);
            visitor.OnEndedTasks += RemoveVisitor;
        }

        public void RemoveVisitor(Visitor visitor)
        {
            visitors.Remove(visitor);
            visitor.DestroyVisitor();
        }

        public void Initialize()
        {
            taskQueue.Enqueue(new Task(_furnitureService.GetFurniture(FurnitureType.Reception)
                .GetComponent<IInteractableVisitor>()));
            taskQueue.Enqueue(new Task(_furnitureService.GetFurniture(FurnitureType.Reception).GetComponent<Reception>()
                .CreateWaitForEmployeeTask()));
            taskQueue.Enqueue(new Task(_furnitureService.GetFurniture(FurnitureType.Table)  
                .GetComponent<IInteractableVisitor>()));
            taskQueue.Enqueue(new Task(_furnitureService.GetFurniture(FurnitureType.Toilet)
                .GetComponent<IInteractableVisitor>()));
            taskQueue.Enqueue(new Task(_furnitureService.GetFurniture(FurnitureType.ToiletSink)
                .GetComponent<IInteractableVisitor>()));
        }
    }
}   