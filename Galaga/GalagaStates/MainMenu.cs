using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using System.IO;
using DIKUArcade.Math;
using DIKUArcade.Events;

namespace Galaga.GalagaStates {
    public class MainMenu : IGameState {
        private static MainMenu instance = null;
        private Entity backGroundImage;
        private Text[] menuButtons;
        private static int activeMenuButton = 1;
        private static int maxMenuButtons = 2;
        public MainMenu(){

            backGroundImage = new Entity(new StationaryShape(new Vec2F(0.0f,0.0f), new Vec2F(1.0f,1.0f)) ,new Image(Path.Combine("Assets", "Images", "TitleImage.png")));

            menuButtons = new []{
                new Text("Quit", new Vec2F(0.5f,0.1f),new Vec2F(0.3f,0.3f)),
                new Text("New Game", new Vec2F(0.5f,0.2f),new Vec2F(0.3f,0.3f))
            };
            
            activeMenuButton = 2;

            maxMenuButtons = 2;
        }
        public static MainMenu GetInstance() {
            if (MainMenu.instance == null) {
                MainMenu.instance = new MainMenu();
                MainMenu.instance.ResetState();
            }
            return MainMenu.instance;
        }
        public void ResetState(){
            activeMenuButton = 1;
        }
        public void UpdateState() {}
        public void RenderState() {

            backGroundImage.RenderEntity();

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
                            menuButtons[activeMenuButton].SetColor(new Vec3F(0.0f,0.0f,0.0f));
                            activeMenuButton++;
                        }
                        break;
                    case KeyboardKey.Down:
                        if (activeMenuButton > 0){
                            menuButtons[activeMenuButton].SetColor(new Vec3F(0.0f,0.0f,0.0f));
                            activeMenuButton--;
                        }
                        break;
                    case KeyboardKey.Enter:
                        switch (activeMenuButton){
                            case 0:
                                GalagaBus.GetBus().RegisterEvent(
                                    new GameEvent {EventType = GameEventType.WindowEvent, From = this, Message = "CLOSE"});
                                break;
                            case 1:
                                GalagaBus.GetBus().RegisterEvent(
                                    new GameEvent{EventType = GameEventType.GameStateEvent,
                                        Message = "CHANGE_STATE",
                                        StringArg1 = "GAME_RUNNING"
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