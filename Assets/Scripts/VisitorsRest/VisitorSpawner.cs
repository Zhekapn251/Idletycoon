using System.Collections;
using Factories.Interfaces;
using UnityEngine;
using Zenject;

namespace VisitorsRest
{
    public class VisitorSpawner: MonoBehaviour
    {
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Transform despawnPoint;
        private IVisitorFactory _visitorFactory;
        private IVisitorService _visitorService;

        [Inject]
        public void Construct(IVisitorFactory visitorFactory,
            IVisitorService visitorService)
        {
            _visitorService = visitorService;
            _visitorFactory = visitorFactory;
        }
        
        private void Start()
        {
            StartCoroutine(StartSpawn());
        }
        
        private IEnumerator StartSpawn()
        {
            while (true)
            {
                yield return new WaitForSeconds(20);
                SpawnVisitor();
                yield return new WaitForSeconds(80);
            }
        }

        private void SpawnVisitor()
        {
            var visitor = _visitorFactory.CreateVisitor();
            visitor.transform.position = spawnPoint.position;
            visitor.SetRespawnPoint(despawnPoint);
            _visitorService.RegisterVisitor(visitor);
            _visitorService.AssignTasksToVisitor(visitor);
        }
            
        public void RespawnVisitor(Visitor visitor)
        {
            visitor.transform.position = spawnPoint.position;
            _visitorService.RegisterVisitor(visitor);
        }
        

    }
}