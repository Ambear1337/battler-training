using ProjectGame.Players;

namespace ProjectGame
{
    internal class SpawnCharacterButton: MenuButtonLeftClickBase
    {
        protected override void FireEvent()
        {
            var humanPlayer = FindFirstObjectByType<HumanPlayer>();
            
            humanPlayer.SpawnRandomCharacter();
        }
    }
}