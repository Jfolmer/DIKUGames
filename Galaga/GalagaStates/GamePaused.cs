using DIKUArcade.State;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.Events;

namespace Galaga.GalagaStates {
    public class GamePaused : IGameState {
        private static GamePaused instance = null;
        private Text[] menuButtons;
        private int activeMenuButton;
        private int maxMenuButtons;
        public GamePaused(){

            menuButtons = new []{
                new Text("Main Menu", new Vec2F(0.5f,0.1f),new Vec2F(0.3f,0.3f)),
                new Text("Continue", new Vec2F(0.5f,0.2f),new Vec2F(0.3f,0.3f))
            };
            
            for (int i = 0; i <= menuButtons.Length - 1; i++) {
                menuButtons[i].SetColor(new Vec3F(1.0f,1.0f,1.0f));
            }

            activeMenuButton = 1;

            maxMenuButtons = 2;
        }
        public static GamePaused GetInstance() {
            if (GamePaused.instance == null) {
                GamePaused.instance = new GamePaused();
                GamePaused.instance.ResetState();
            }
            return GamePaused.instance;
        }
        public static void SetInstance(){
            instance = null;
        }
        public void ResetState(){}
        public void UpdateState() {}
        public void RenderState() {

            menuButtons[activeMenuButton].SetColor(new Vec3F(0.0f,0.8f,0.8f));

            for (int i = 0; i <= menuButtons.Length - 1; i++) {
                menuButtons[i].RenderText();
            }
        }
        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            if (action == KeyboardAction.KeyPress){
                switch (key) {
                    case KeyboardKey.Up:
                        if (activeMenuButton < maxMenuButtons - 1){
                            menuButtons[activeMenuButton].SetColor(new Vec3F(1.0f,1.0f,1.0f));
                            activeMenuButton++;
                        }
                        break;
                    case KeyboardKey.Down:
                        if (activeMenuButton > 0){
                            menuButtons[activeMenuButton].SetColor(new Vec3F(1.0f,1.0f,1.0f));
                            activeMenuButton--;
                        }
                        break;
                    case KeyboardKey.Enter:
                        switch (activeMenuButton){
                            case 1:
                                GalagaBus.GetBus().RegisterEvent(
                                    new GameEvent {EventType = GameEventType.GameStateEvent,
                                        Message = "CHANGE_STATE",
                                        StringArg1 = "GAME_RUNNING"
                                    }
                                );
                                break;
                            case 0:
                                GalagaBus.GetBus().RegisterEvent(new GameEvent{EventType = GameEventType.GameStateEvent,
                                        Message = "RESET_GAME",
                                    }
                                );
                                GalagaBus.GetBus().RegisterEvent(
                                    new GameEvent{EventType = GameEventType.GameStateEvent,
                                        Message = "CHANGE_STATE",
                                        StringArg1 = "MAIN_MENU"
                                    }
                                );
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}