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
            GameRunning.GetInstance();
            GamePaused.GetInstance();
        }
        public void ProcessEvent(GameEvent input) {
        }
        private void SwitchState(GameStateType stateType) {
            switch (stateType) {
                case GameStateType.GameRunning: ActiveState = GameRunning.GetInstance();
                break;

                case GameStateType.GamePaused: ActiveState = GamePaused.GetInstance();
                break;

                case GameStateType.MainMenu: ActiveState = MainMenu.GetInstance();
                break;

                default: break;
            }
        }
    }
}