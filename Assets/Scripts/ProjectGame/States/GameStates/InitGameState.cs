using System;
using UnityEngine;

namespace ProjectGame.States.GameStates
{
    internal class InitGameState: MonoBehaviour, IState
    {
        public event ChangeState ChangeState;

        private void Awake()
        {
            EnterState();
        }

        public void EnterState()
        {
            
            // Происходит спавн персонажей на поле
            
            // Происходит расчет инициативы у персонажей, у кого самая большая инициатива, тот и ходит
            // Если максимальная инициатива у персонажей у игрока и у ИИ одинаковая, то берется рандом
            throw new System.NotImplementedException();
        }

        public void ExitState()
        {
            // Переход в HumanPlayerTurnGameState или в AIPlayerTurnGameState в зависимости от инициативы
            throw new System.NotImplementedException();
        }
    }
}
