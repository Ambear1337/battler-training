using ProjectEventBus;

namespace ProjectGame
{
    public struct CharacterEvent: IBusEvent
    {
        public Character Character;
    }
}
