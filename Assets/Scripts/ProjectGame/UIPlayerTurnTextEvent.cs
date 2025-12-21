using ProjectEventBus;
using ProjectGame.Players;

namespace ProjectGame
{
    public struct UIPlayerTurnTextEvent: IBusEvent
    {
        public IPlayer Player;
    }
}
