using System;
using Services.Interfaces;
using UnityEngine;

namespace Services.Implementation
{
    public class CameraService : ICameraEventService, ICameraService
    {
        public event Action<CameraControl.CameraMode> OnCameraModeChanged;
        public event Action<Vector3> OnCameraPositionChanged;

        public void SetCameraPosition(Vector3 position)
        {
            OnCameraPositionChanged?.Invoke(position);
        }
        
        public void SetCameraMode(CameraControl.CameraMode mode)
        {
            OnCameraModeChanged?.Invoke(mode);
        }
        
    }
}