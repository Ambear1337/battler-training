using System;
using ProjectCore;
using ProjectEventBus;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

namespace ProjectGame
{
    public class MouseEventHandler: MonoBehaviour
    {
        private IClickable _currentClickableObject;
        
        [SerializeField] private LayerMask clickableLayerMask;

        public void MouseMove(InputAction.CallbackContext ctx)
        {
            if (!ctx.performed) return;

            LightDelightClickableObject();
        }

        public void MouseClick(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                HandleMouseEvent();
            }
        }

        private IClickable HandleMouseEvent()
        {
            Ray ray;
            
            if (Camera.main != null)
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            }
            else
            {
                Debug.LogError("There is no Main Camera!");
                return null;
            }
            
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);

            return Physics.Raycast(ray, out var hit, 1000f, clickableLayerMask) 
                ? hit.transform.gameObject.GetComponent<IClickable>()
                : null;
        }

        private void LightDelightClickableObject()
        {
            if (_currentClickableObject == null)
            {
                _currentClickableObject = HandleMouseEvent();
            }
            else
            {
                var clickable = HandleMouseEvent();
                if (clickable == null)
                {
                    _currentClickableObject.GFX.DelightObject();
                    _currentClickableObject = null;
                }
                else
                {
                    clickable?.GFX.LightObject();
                }
            }
        }
    }
}
