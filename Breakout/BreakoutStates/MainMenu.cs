using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using System.IO;
using DIKUArcade.Math;
using DIKUArcade.Events;
using System;

namespace Breakout.BreakoutStates {
    public class MainMenu : IGameState {
        private static MainMenu instance = null;
        private Entity backGroundImage;
        private Text[] menuButtons;
        private static int activeMenuButton = 2;
        private static int maxMenuButtons = 3;
        private Random random;
        public MainMenu(){

            backGroundImage = new Entity(new StationaryShape(new Vec2F(0.0f,0.0f), new Vec2F(1.0f,1.0f)),
                new Image(Path.Combine("Assets", "Images", "BreakoutTitleScreen.png")));

            menuButtons = new []{
                new Text("Quit", new Vec2F(0.3f,0.1f),new Vec2F(0.3f,0.3f)),
                new Text("Level Select", new Vec2F(0.3f,0.2f),new Vec2F(0.3f,0.3f)),
                new Text("New Game", new Vec2F(0.3f,0.3f),new Vec2F(0.3f,0.3f))
            };
            
            menuButtons[0].SetColor(new Vec3F(1.0f,1.0f,1.0f));

            menuButtons[1].SetColor(new Vec3F(1.0f,1.0f,1.0f));
            
            activeMenuButton = 2;

            maxMenuButtons = 3;
        }
        public static MainMenu GetInstance() {
            if (MainMenu.instance == null) {
                MainMenu.instance = new MainMenu();
                MainMenu.instance.ResetState();
            }
            return MainMenu.instance;
        }
        public void ResetState(){
            activeMenuButton = 2;
        }
        public void UpdateState() {}
        public void RenderState() {

            random = new Random();

            if (random.Next(1,100) == 1){
                backGroundImage = new Entity(new StationaryShape(new Vec2F(0.0f,0.0f), new Vec2F(1.0f,1.0f)),
                new Image(Path.Combine("Assets", "Images", "shipit_titlescreen.png")));
            }

            backGroundImage.RenderEntity();

            menuButtons[activeMenuButton].SetColor(new Vec3F(0.0f,0.8f,0.8f));

            for (int i = 0; i <= menuButtons.Length - 1; i++) {
                menuButtons[i].RenderText();
            }
            backGroundImage = new Entity(new StationaryShape(new Vec2F(0.0f,0.0f), new Vec2F(1.0f,1.0f)),
                new Image(Path.Combine("Assets", "Images", "BreakoutTitleScreen.png")));
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
                    case KeyboardKey.Escape:
                        BreakoutBus.GetBus().RegisterEvent(
                            new GameEvent {EventType = GameEventType.WindowEvent, From = this, Message = "CLOSE"});
                        break;
                    case KeyboardKey.Enter:
                        switch (activeMenuButton){
                            case 0:
                                BreakoutBus.GetBus().RegisterEvent(
                                    new GameEvent {EventType = GameEventType.WindowEvent, From = this, Message = "CLOSE"});
                                break;
                            case 1:
                                BreakoutBus.GetBus().RegisterEvent(
                                    new GameEvent{EventType = GameEventType.GameStateEvent,
                                        Message = "CHANGE_STATE",
                                        StringArg1 = "LEVEL_SELECTOR"
                                    }
                                );
                                break;
                            case 2:
                                BreakoutBus.GetBus().RegisterEvent(
                                    new GameEvent{EventType = GameEventType.GameStateEvent,
                                        Message = "CHANGE_STATE",
                                        StringArg1 = "GAME_RUNNING",
                                        StringArg2 = "level1.txt"
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