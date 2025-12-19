using System;
using Codice.Client.BaseCommands.Import;
using ProjectCore;
using ProjectGame.Players;
using ProjectGame.States;
using ProjectGame.States.GameStates;
using UnityEngine;

namespace ProjectGame
{
    public class AIBrain: SceneSingleton<AIBrain>
    {
        private IState _currentState;

        [SerializeField] private InitGameState _initGameState;
        [SerializeField] private HumanPlayerTurnGameState _humanPlayerTurnGameState;
        [SerializeField] private AIPlayerTurnGameState _aiPlayerTurnGameState;
        [SerializeField] private ResultsGameState _resultsGameState;

        private IPlayer _humanPlayer;
        private IPlayer _aiPlayer;

        private void Start()
        {
            _initGameState.ChangeState += OnChangeStateRequired;
            _humanPlayerTurnGameState.ChangeState += OnChangeStateRequired;
            _aiPlayerTurnGameState.ChangeState += OnChangeStateRequired;
            _resultsGameState.ChangeState += OnChangeStateRequired;
            
            _currentState = _initGameState;
            _currentState.EnterState();
        }

        private void OnDisable()
        {
            _initGameState.ChangeState -= OnChangeStateRequired;
            _humanPlayerTurnGameState.ChangeState -= OnChangeStateRequired;
            _aiPlayerTurnGameState.ChangeState -= OnChangeStateRequired;
            _resultsGameState.ChangeState -= OnChangeStateRequired;
        }

        private void OnChangeStateRequired(IState nextState)
        {
            _currentState.ExitState(nextState);

            _currentState = nextState;
            _currentState.EnterState();
        }
    }
}
