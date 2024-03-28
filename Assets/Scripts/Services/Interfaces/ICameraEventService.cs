using System;
using UnityEngine;

namespace Services.Interfaces
{
    public interface ICameraEventService
    {
        event Action<CameraControl.CameraMode> OnCameraModeChanged;
        event Action<Vector3> OnCameraPositionChanged;
    }
}