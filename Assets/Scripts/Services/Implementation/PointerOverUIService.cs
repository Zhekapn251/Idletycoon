using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Services.Implementation
{
    public class PointerOverUIService : IPointerOverUIService
    {
        private bool IsTouchOverUI(int touchFingerId)
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = new Vector2(Input.GetTouch(touchFingerId).position.x, Input.GetTouch(touchFingerId).position.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            return results.Count > 0;
        }
        public bool PointerIsOverUI
        {
            get
            {
                if (Input.touchCount <= 0) return false;
                switch (Input.touchCount)
                {
                    case 1 when IsTouchOverUI(0):
                    case 2 when IsTouchOverUI(0) || IsTouchOverUI(1):
                        return true;
                }
                return false;
            }
        }
    }
}