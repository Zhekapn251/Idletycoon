using System;
using System.Collections.Generic;
using Services;
using Services.Implementation;
using Services.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace DefaultNamespace
{
    public class Room: MonoBehaviour
    {
        private ICameraService _cameraService;
        private IPointerOverUIService _pointerOverUIService;

        [Inject]
        public void Construct(ICameraService cameraService, IPointerOverUIService pointerOverUIService)
        {
            _pointerOverUIService = pointerOverUIService;
            _cameraService = cameraService;
        }
        private void OnMouseDown()
        {
            if (_pointerOverUIService.PointerIsOverUI) return;
            _cameraService.SetCameraPosition(transform.position);
        }
        
    }
}