using System;
using ProjectEventBus;
using ProjectGame.Players;
using TMPro;
using UnityEngine;

namespace ProjectGame
{
    public class UIPlayerTurnText: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _currentPlayerTurnText;
        
        private EventBinding<UIPlayerTurnTextEvent> _uiPlayerTextEventBinding;

        private void OnEnable()
        {
            _uiPlayerTextEventBinding = new EventBinding<UIPlayerTurnTextEvent>(ChangeCurrentPlayerPlayerTurnText);
            EventBus<UIPlayerTurnTextEvent>.Register(_uiPlayerTextEventBinding);
        }

        private void OnDisable()
        {
            EventBus<UIPlayerTurnTextEvent>.Deregister(_uiPlayerTextEventBinding);
        }

        private void ChangeCurrentPlayerPlayerTurnText(UIPlayerTurnTextEvent uiPlayerTurnTextEvent)
        {
            if (uiPlayerTurnTextEvent.Player is HumanPlayer)
            {
                _currentPlayerTurnText.text = "Turn: Human Player";
            }
            else
            {
                _currentPlayerTurnText.text = "Turn: AI Player";
            }
        }
    }
}
