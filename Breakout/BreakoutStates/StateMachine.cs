using DIKUArcade.State;
using DIKUArcade.Events;
using BreakoutStates;

namespace Breakout.BreakoutStates {
    public class StateMachine : IGameEventProcessor {
        public IGameState ActiveState { get; private set; }

        public StateMachine() {
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, this);
            ActiveState = MainMenu.GetInstance();
            GameRunning.GetInstance();
            GamePaused.GetInstance();
            LevelSelector.GetInstance();
            GameOver.GetInstance();
            ScoreScreen.GetInstance();
        }
        
        /// <summary>
        /// Processes a GameEvent (Switching of states) sent through the GameBus
        /// </summary>
        /// <param>
        ///  A GameEvent
        /// </param>
        /// <returns>
        /// Void
        /// </returns>
        public void ProcessEvent(GameEvent input) {
            if (input.Message == "CHANGE_STATE"){
                if (input.StringArg1 == "GAME_RUNNING" && input.StringArg2 != ""){
                    GameRunning.SetInstance();
                    GameRunning.SetLevel(input.StringArg2);
                }
                SwitchState(StateTransformer.TransformStringToState(input.StringArg1));
            }
        }

        /// <summary>
        /// Switches the active state to the input GameStateType
        /// </summary>
        /// <param>
        ///  A GameStateType
        /// </param>
        /// <returns>
        /// Void
        /// </returns>
        private void SwitchState(GameStateType stateType) {
            switch (stateType) {
                case GameStateType.GameRunning:
                    GameRunning.SetTime();
                    GamePaused.SetInstance();
                    ActiveState = GameRunning.GetInstance();
                    break;
                case GameStateType.GamePaused:
                    ActiveState = GamePaused.GetInstance();
                    break;
                case GameStateType.MainMenu:
                    Points.ResetTally();
                    GameRunning.SetInstance();
                    ActiveState = MainMenu.GetInstance();
                    break;
                case GameStateType.LevelSelector:
                    Points.ResetTally();
                    GameRunning.SetInstance();
                    ActiveState = LevelSelector.GetInstance();
                    break;
                case GameStateType.GameOver:
                    GameRunning.SetInstance();
                    ActiveState = GameOver.GetInstance();
                    break;
                case GameStateType.ScoreScreen:
                    GameRunning.SetInstance();
                    ActiveState = ScoreScreen.GetInstance();
                    break;
            }
        }
    }
}