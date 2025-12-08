namespace ProjectGame.States.GameStates
{
    internal class GameState: IState
    {
        public event ChangeState ChangeState;
        public void EnterState()
        {
            // Происходит расчет инициативы у персонажей, у кого самая большая инициатива, тот и ходит
            // Если максимальная инициатива у персонажей у игрока и у ИИ одинаковая, то берется рандом
            throw new System.NotImplementedException();
        }

        public void ExitState()
        {
            // Переход
            throw new System.NotImplementedException();
        }
    }
}
