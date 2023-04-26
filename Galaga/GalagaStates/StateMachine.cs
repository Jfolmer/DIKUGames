using DIKUArcade.State;
using DIKUArcade.Events;
using GalagaStates;

namespace Galaga.GalagaStates {
    public class StateMachine : IGameEventProcessor {
        public IGameState ActiveState { get; private set; }

        public StateMachine() {
            GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);
            ActiveState = MainMenu.GetInstance();
            GameOver.GetInstance();
            GameRunning.GetInstance();
            GamePaused.GetInstance();
        }
        public void ProcessEvent(GameEvent input) {
            if (input.Message == "CHANGE_STATE"){
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
                case GameStateType.GameOver:
                    GameRunning.SetInstance();
                    ActiveState = GameOver.GetInstance();
                    break;
                default: break;
            }
        }
    }
}