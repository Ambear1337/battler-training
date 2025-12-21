using ProjectEventBus;
using ProjectGame.Players;

namespace ProjectGame.Buttons
{
    internal class SpawnCharacterButton: MenuButtonLeftClickBase
    {
        public HumanPlayer HumanPlayer;
        
        protected override void FireEvent()
        {
            EventBus<PlayerButtonEvent>.Raise(new PlayerButtonEvent
            {
                Player = HumanPlayer
            });
        }
    }
}