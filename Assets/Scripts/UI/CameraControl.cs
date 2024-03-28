using System.Collections.Generic;
using Cinemachine;
using Services.Implementation;
using Services.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class CameraControl : MonoBehaviour
{

    [SerializeField] private CinemachineVirtualCamera _camera;
    private const float touchZoomSpeed = 1f;
    private Vector2 _previousDistance;
    private CameraMode _mode;
    private ICameraEventService _cameraEventService;
    private IPointerOverUIService _pointerOverUIService;

    [Inject]
    public void Construct(ICameraEventService cameraEventService, IPointerOverUIService pointerOverUIService)
    {
        _pointerOverUIService = pointerOverUIService;
        _cameraEventService = cameraEventService;
        _cameraEventService.OnCameraModeChanged += SetMode;
        _cameraEventService.OnCameraPositionChanged += Center;
    }

    private void Update()
    {
        switch (_mode)
        {
            case CameraMode.Centered:
                break;
            case CameraMode.Free:
                FreeMode();
                break;
        }
    }

    private void FreeMode()
    {
        if (_pointerOverUIService.PointerIsOverUI) return;
        if (Input.touchCount > 0)
        {
            switch (Input.touchCount)
            {
                case 1:
                {
                    Touch touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Moved)
                    {
                        Vector2 delta = touch.deltaPosition;
                        transform.Translate(-delta.x * Time.deltaTime, -delta.y * Time.deltaTime, 0);
                    }

                    break;
                }
                case 2:
                {
                    Touch touchZero = Input.GetTouch(0);
                    Touch touchOne = Input.GetTouch(1);

                    Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                    Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                    float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                    float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                    float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                    _camera.m_Lens.OrthographicSize += deltaMagnitudeDiff * Time.deltaTime * touchZoomSpeed;
                    break;
                }
                
            }
        }
    }

    private bool IsTouchOverUI(int touchFingerId)
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = new Vector2(Input.GetTouch(touchFingerId).position.x, Input.GetTouch(touchFingerId).position.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            Debug.Log("Number of raycasted objects " + results.Count);
            return results.Count > 0;
        }

        private void SetMode(CameraMode mode)
        {
            this._mode = CameraMode.Free;
        }

        public void Center(Vector3 targetPosition) =>
            transform.position = targetPosition;

    public enum CameraMode
    {
        Free,
        Centered,
    }
}