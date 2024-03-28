using Factories.Interfaces;
using UnityEngine;
using VisitorsRest;

namespace Factories.Implementation
{
    public class VisitorFactory : IVisitorFactory
    {
        private VisitorPrefabSO visitorPrefabSO;
        
        public VisitorFactory(VisitorPrefabSO visitorPrefabSO)
        {
            this.visitorPrefabSO = visitorPrefabSO;
        }
        
        public Visitor CreateVisitor()
        {
            return Object.Instantiate(GetRandomPrefab());
        }

        private Visitor GetRandomPrefab()
        {
            return visitorPrefabSO.visitorPrefabs[Random.Range(0, visitorPrefabSO.visitorPrefabs.Length)];
        }
    }
}