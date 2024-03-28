using System;
using UnityEngine;

namespace DefaultNamespace.UI
{
    public class FaceCamera : MonoBehaviour
    {
        private Camera targetCamera;

        private void Start()
        {
            targetCamera = Camera.main;
        }

        void Update()
        {
            if (targetCamera == null)
            {
                targetCamera = Camera.main;
            }
            
            if (targetCamera != null)
            {
                Vector3 targetPosition = targetCamera.transform.position;
                targetPosition.y = transform.position.y;

                transform.LookAt(targetPosition);
                transform.rotation = Quaternion.LookRotation(transform.position - targetPosition);
            }
        }
    }
}