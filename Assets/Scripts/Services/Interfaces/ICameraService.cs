using UnityEngine;

namespace Services.Interfaces
{
    public interface ICameraService
    {
        void SetCameraPosition(Vector3 position);
        void SetCameraMode(CameraControl.CameraMode mode);
    }
}