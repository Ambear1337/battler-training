using UnityEngine;
using UnityEngine.EventSystems;

namespace ProjectGame
{
    public abstract class MenuButtonLeftClickBase : MonoBehaviour, ISubmitHandler, IPointerClickHandler
    {
        public void OnSubmit(BaseEventData eventData)
        {
            FireEvent();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(eventData == null || eventData.selectedObject == null)
                return;
            
            if(!eventData.selectedObject.Equals(gameObject))
                return;
            
            if(eventData is { button: PointerEventData.InputButton.Left })
                FireEvent();
        }

        protected abstract void FireEvent();
    }
}