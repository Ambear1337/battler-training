using System.Collections.Generic;

namespace ProjectGame.Players
{
    internal interface IPlayer
    {
        public void BeginTurn();
        public void EndTurn();
        public int CalculateCharacterWithMostInitative();
    }
}
