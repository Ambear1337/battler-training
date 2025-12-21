using System.Collections.Generic;

namespace ProjectGame.Players
{
    public interface IPlayer
    {
        public void BeginTurn();
        public void EndTurn();
    }
}
