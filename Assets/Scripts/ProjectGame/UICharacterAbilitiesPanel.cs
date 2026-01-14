using ProjectEventBus;
using ProjectGame.Abilities;
using ProjectGame.Players;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectGame
{
    public sealed class UICharacterAbilitiesPanel: MonoBehaviour
    {
        [SerializeField] private GameObject _humanPlayerUI;

        [SerializeField] private Button[] _abilitiesButtons =  new Button[5];
        
        private EventBinding<CharacterEvent> _characterEventBinding;

        private void OnEnable()
        {
            _characterEventBinding = new EventBinding<CharacterEvent>(HandleCharacterEvent);
            EventBus<CharacterEvent>.Register(_characterEventBinding);
        }

        private void OnDisable()
        {
            EventBus<CharacterEvent>.Deregister(_characterEventBinding);
        }

        private void HandleCharacterEvent(CharacterEvent characterEvent)
        {
            if (characterEvent.Character.PlayerOwner is HumanPlayer)
            {
                _humanPlayerUI.SetActive(true);
                UpdateUI(characterEvent);
            }
            else
            {
                _humanPlayerUI.SetActive(false);
            }
        }

        private void UpdateUI(CharacterEvent characterEvent)
        {
            for (int i = 0; i < _abilitiesButtons.Length; i++)
            {
                var ability = characterEvent.Character.CharacterDescription.Abilities[i].Value;
                _abilitiesButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = ability.AbilityName;
                
                if (ability is NullCharacterAbility)
                {
                    _abilitiesButtons[i].interactable = false;
                }
                else
                {
                    _abilitiesButtons[i].interactable = true;
                    _abilitiesButtons[i].GetComponent<UseAbilityButton>().SetupAbilityButton(ability);
                }
            }
        }
    }
}
