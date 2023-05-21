using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.State;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.Events;

namespace Breakout.BreakoutStates {
    public class GameOver : IGameState {
        private static GameOver instance = null;
        private Text[] menuButtons;
        private Text[] text;
        private int activeMenuButton;
        private int maxMenuButtons;
        public GameOver(){
            text = new[]{ 
                new Text("Game Over", new Vec2F(0.2f,0.4f), new Vec2F(0.4f,0.4f)),
                new Text(string.Format("{0} points", Points.GetTally()), new Vec2F(0.2f,0.3f), new Vec2F(0.4f,0.4f)),
            };
            text[0].SetColor(new Vec3F(1.0f,0.0f,0.0f));
            text[1].SetColor(new Vec3F(1.0f,0.0f,0.0f));
            menuButtons = new []{
                new Text("Main Menu", new Vec2F(0.2f,0.0f),new Vec2F(0.4f,0.4f)),
                new Text("Restart Game", new Vec2F(0.2f,0.1f),new Vec2F(0.4f,0.4f))
            };
            
            for (int i = 0; i <= menuButtons.Length - 1; i++) {
                menuButtons[i].SetColor(new Vec3F(1.0f,1.0f,1.0f));
            }

            activeMenuButton = 1;

            maxMenuButtons = 2;
        }
        public static GameOver GetInstance() {
            if (GameOver.instance == null) {
                GameOver.instance = new GameOver();
                GameOver.instance.ResetState();
            }
            return GameOver.instance;
        }
        public static void SetInstance(){
            instance = null;
        }
        public void ResetState(){}
        public void UpdateState() {}
        public void RenderState() {
            text[1] = new Text(string.Format("{0} points", Points.GetTally()), new Vec2F(0.2f,0.3f), new Vec2F(0.4f,0.4f));
            text[1].SetColor(new Vec3F(1.0f,0.0f,0.0f));
            Entity backgroundImage = new Entity(new StationaryShape(new Vec2F(0.0f,0.0f), new Vec2F(1.0f,1.0f)),
                new Image(Path.Combine("Assets", "Images", "SpaceBackground.png")));
            backgroundImage.RenderEntity();

            for (int i = 0; i <= text.Length - 1; i++) {
                text[i].RenderText();
            }
            
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
                                Points.ResetTally();
                                BreakoutBus.GetBus().RegisterEvent(
                                    new GameEvent {EventType = GameEventType.GameStateEvent,
                                        Message = "CHANGE_STATE",
                                        StringArg1 = "GAME_RUNNING"
                                    }
                                );
                                break;
                            case 0:
                                BreakoutBus.GetBus().RegisterEvent(new GameEvent{EventType = GameEventType.GameStateEvent,
                                        Message = "RESET_GAME",
                                    }
                                );
                                BreakoutBus.GetBus().RegisterEvent(
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