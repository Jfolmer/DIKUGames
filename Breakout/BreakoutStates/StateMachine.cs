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
        }
        public void ProcessEvent(GameEvent input) {
            if (input.Message == "CHANGE_STATE"){
                SwitchState(StateTransformer.TransformStringToState(input.StringArg1));
            }
        }
        private void SwitchState(GameStateType stateType) {
            switch (stateType) {
                case GameStateType.GameRunning:
                    ActiveState = GameRunning.GetInstance();
                    break;
                case GameStateType.MainMenu:
                    GameRunning.SetInstance();
                    ActiveState = MainMenu.GetInstance();
                    break;
            }
        }
    }
}