using Tasks;

namespace VisitorsRest
{
    public interface IVisitorService
    {
        void AssignTasksToVisitor(Visitor visitor);
        void RegisterVisitor(Visitor visitor);
        
        void RemoveVisitor(Visitor visitor);
    }
}