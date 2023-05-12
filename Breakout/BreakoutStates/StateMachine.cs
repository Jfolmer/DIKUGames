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
        }
        public void ProcessEvent(GameEvent input) {
            if (input.Message == "CHANGE_STATE"){
                if (input.StringArg1 == "GAME_RUNNING" && input.StringArg2 != ""){
                    GameRunning.SetInstance();
                    GameRunning.SetLevel(input.StringArg2);
                }
                SwitchState(StateTransformer.TransformStringToState(input.StringArg1));
            }
        }
        private void SwitchState(GameStateType stateType) {
            switch (stateType) {
                case GameStateType.GameRunning:
                    GamePaused.SetInstance();
                    ActiveState = GameRunning.GetInstance();
                    break;
                case GameStateType.GamePaused:
                    ActiveState = GamePaused.GetInstance();
                    break;
                case GameStateType.MainMenu:
                    GameRunning.SetInstance();
                    ActiveState = MainMenu.GetInstance();
                    break;
                case GameStateType.LevelSelector:
                    GameRunning.SetInstance();
                    ActiveState = LevelSelector.GetInstance();
                    break;
            }
        }
    }
}