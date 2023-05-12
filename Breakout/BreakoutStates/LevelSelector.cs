using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using System.IO;
using DIKUArcade.Math;
using DIKUArcade.Events;
using System;

namespace Breakout.BreakoutStates {
    public class LevelSelector : IGameState {
        private static LevelSelector instance = null;
        private Entity backGroundImage;
        private Text[] menuButtons;
        private static int activeMenuButton = 5;
        private static int maxMenuButtons = 6;
        private Random random;
        public LevelSelector(){

            backGroundImage = new Entity(new StationaryShape(new Vec2F(0.0f,0.0f), new Vec2F(1.0f,1.0f)),
                new Image(Path.Combine("Assets", "Images", "BreakoutTitleScreen.png")));

            menuButtons = new []{
                new Text("Wall", new Vec2F(0.1f,0.1f),new Vec2F(0.3f,0.3f)),
                new Text("Central Mass", new Vec2F(0.1f,0.2f),new Vec2F(0.3f,0.3f)),
                new Text("Columns", new Vec2F(0.1f,0.3f),new Vec2F(0.3f,0.3f)),
                new Text("Level 3", new Vec2F(0.4f,0.1f),new Vec2F(0.3f,0.3f)),
                new Text("Level 2", new Vec2F(0.4f,0.2f),new Vec2F(0.3f,0.3f)),
                new Text("Level 1", new Vec2F(0.4f,0.3f),new Vec2F(0.3f,0.3f)),
            };
            
            menuButtons[0].SetColor(new Vec3F(1.0f,1.0f,1.0f));

            menuButtons[1].SetColor(new Vec3F(1.0f,1.0f,1.0f));

            menuButtons[2].SetColor(new Vec3F(1.0f,1.0f,1.0f));

            menuButtons[3].SetColor(new Vec3F(1.0f,1.0f,1.0f));

            menuButtons[4].SetColor(new Vec3F(1.0f,1.0f,1.0f));

            menuButtons[5].SetColor(new Vec3F(1.0f,1.0f,1.0f));
            
            activeMenuButton = 5;

            maxMenuButtons = 6;
        }
        public static LevelSelector GetInstance() {
            if (LevelSelector.instance == null) {
                LevelSelector.instance = new LevelSelector();
                LevelSelector.instance.ResetState();
            }
            return LevelSelector.instance;
        }
        public void ResetState(){
            activeMenuButton = 1;
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
                    case KeyboardKey.Right:
                        if (activeMenuButton < 3){
                            menuButtons[activeMenuButton].SetColor(new Vec3F(1.0f,1.0f,1.0f));
                            activeMenuButton = activeMenuButton + 3;
                        }
                        break;
                    case KeyboardKey.Left:
                        if (activeMenuButton >= 3){
                            menuButtons[activeMenuButton].SetColor(new Vec3F(1.0f,1.0f,1.0f));
                            activeMenuButton = activeMenuButton - 3;
                        }
                        break;
                    case KeyboardKey.Escape:
                        BreakoutBus.GetBus().RegisterEvent(
                                    new GameEvent{EventType = GameEventType.GameStateEvent,
                                        Message = "CHANGE_STATE",
                                        StringArg1 = "MAIN_MENU"
                                    }
                                );
                                break;
                    case KeyboardKey.Enter:
                        switch (activeMenuButton){
                            case 0:
                                BreakoutBus.GetBus().RegisterEvent(
                                    new GameEvent{EventType = GameEventType.GameStateEvent,
                                        Message = "CHANGE_STATE",
                                        StringArg1 = "GAME_RUNNING",
                                        StringArg2 = "wall.txt"
                                    }
                                );
                                break;
                            case 1:
                                BreakoutBus.GetBus().RegisterEvent(
                                    new GameEvent{EventType = GameEventType.GameStateEvent,
                                        Message = "CHANGE_STATE",
                                        StringArg1 = "GAME_RUNNING",
                                        StringArg2 = "central-mass.txt"
                                    }
                                );
                                break;
                            case 2:
                                BreakoutBus.GetBus().RegisterEvent(
                                    new GameEvent{EventType = GameEventType.GameStateEvent,
                                        Message = "CHANGE_STATE",
                                        StringArg1 = "GAME_RUNNING",
                                        StringArg2 = "columns.txt"
                                    }
                                );
                                break;
                            case 3:
                                BreakoutBus.GetBus().RegisterEvent(
                                    new GameEvent{EventType = GameEventType.GameStateEvent,
                                        Message = "CHANGE_STATE",
                                        StringArg1 = "GAME_RUNNING",
                                        StringArg2 = "level3.txt"
                                    }
                                );
                                break;
                            case 4:
                                BreakoutBus.GetBus().RegisterEvent(
                                    new GameEvent{EventType = GameEventType.GameStateEvent,
                                        Message = "CHANGE_STATE",
                                        StringArg1 = "GAME_RUNNING",
                                        StringArg2 = "level2.txt"
                                    }
                                );
                                break;
                            case 5:
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