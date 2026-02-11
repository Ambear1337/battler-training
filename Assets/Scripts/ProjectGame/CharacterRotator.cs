using ProjectCore;
using UnityEngine;

namespace ProjectGame
{
    [AllowCreateInstance]
    public class CharacterRotator: SceneSingleton<CharacterRotator>
    {
        public void RotateCharacter(Character character)
        {
            var currentCharacterRotation = character.transform.localRotation.eulerAngles;

            currentCharacterRotation.y += 180;

            character.transform.localRotation = Quaternion.Euler(currentCharacterRotation);
        }
    }
}