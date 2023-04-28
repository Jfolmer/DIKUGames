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
                    switch (input.StringArg2){
                        case "Level1":
                            GameRunning.SetLevel(1);
                            break;
                        case "Level2":
                            GameRunning.SetLevel(2);
                            break;
                        case "Level3":
                            GameRunning.SetLevel(3);
                            break;
                        case "Wall":
                            GameRunning.SetLevel(4);
                            break;
                        case "Columns":
                            GameRunning.SetLevel(5);
                            break;
                        case "CentralMass":
                            GameRunning.SetLevel(6);
                            break;
                    }
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