using UnityEngine;

namespace Employees
{
    public class EmployeeAnimation: MonoBehaviour
    {
    
            [SerializeField] private Animator _animator;
            
            
            public void Walk()
            {
                _animator.SetBool("Walk", true);
            }
            
            public void Idle()
            {
                _animator.SetBool("Walk", false);
            }
        
    }
}