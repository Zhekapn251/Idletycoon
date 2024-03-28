using Cysharp.Threading.Tasks;
using UnityEngine;

namespace VisitorsRest
{
    public class VisitorAnimation: MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private static readonly int SitHash = Animator.StringToHash("Sit");
        private static readonly int IdleHash = Animator.StringToHash("Idle");
        private static readonly int WalkHash = Animator.StringToHash("Walk");

        public void Walk()
        {
            _animator.SetTrigger(WalkHash);
        }

        public void Idle()
        {
            _animator.SetTrigger(IdleHash);
        }

        public void Sit()
        {
            _animator.SetTrigger(SitHash);
        }
    }
}