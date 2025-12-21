using ProjectEventBus;
using ProjectGame.Players;

namespace ProjectGame
{
    public struct PlayerButtonEvent: IBusEvent
    {
        public IPlayer Player;
    }
}
