using System;
using ProjectCore;
using ProjectEventBus;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.XR;

namespace ProjectGame
{
    public class MouseEventHandler: MonoBehaviour
    {
        private IClickable _currentClickableObject;
        
        [SerializeField] private LayerMask _clickableLayerMask;
        [SerializeField] private InputActionReference _mouseClick;

        private void OnEnable()
        {
            if (_mouseClick != null && _mouseClick.action != null)
            {
                _mouseClick.action.canceled += MouseClick;
            }
        }

        private void OnDisable()
        {
            if (_mouseClick != null && _mouseClick.action != null)
            {
                _mouseClick.action.canceled -= MouseClick;
            }
        }

        public void MouseClick(InputAction.CallbackContext ctx)
        {
            LightDelightClickableObject();
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

            return Physics.Raycast(ray, out var hit, 1000f, _clickableLayerMask) 
                ? hit.transform.gameObject.GetComponent<IClickable>()
                : null;
        }

        private void LightDelightClickableObject()
        {
            _currentClickableObject?.GFX.DelightObject();

            _currentClickableObject = HandleMouseEvent();

            _currentClickableObject?.GFX.LightObject();
        }
    }
}
