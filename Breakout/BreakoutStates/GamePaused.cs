using System.IO;
using DIKUArcade.State;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Entities;

namespace Breakout.BreakoutStates {
    public class GamePaused : IGameState {
        private static GamePaused instance = null;
        private Text[] menuButtons;
        private int activeMenuButton;
        private int maxMenuButtons;
        public GamePaused(){

            menuButtons = new []{
                new Text("Main Menu", new Vec2F(0.4f,0.1f),new Vec2F(0.3f,0.3f)),
                new Text("Continue", new Vec2F(0.4f,0.2f),new Vec2F(0.3f,0.3f))
            };
            
            for (int i = 0; i <= menuButtons.Length - 1; i++) {
                menuButtons[i].SetColor(new Vec3F(1.0f,1.0f,1.0f));
            }

            activeMenuButton = 1;

            maxMenuButtons = 2;
        }

        /// <summary>
        ///  Returns the local instance field or resets the instance of GamePaused
        /// </summary>
        /// <param>
        ///  Null
        /// </param>
        /// <returns>
        /// GamePaused
        /// </returns>
        public static GamePaused GetInstance() {
            if (GamePaused.instance == null) {
                GamePaused.instance = new GamePaused();
                GamePaused.instance.ResetState();
            }
            return GamePaused.instance;
        }

        /// <summary>
        ///  Sets the local instance field to null
        /// </summary>
        /// <param>
        ///  Null
        /// </param>
        /// <returns>
        /// void
        /// </returns>
        public static void SetInstance(){
            instance = null;
        }

        /// <summary>
        ///  Does noooothing, but is needed due to interface
        /// </summary>
        /// <param>
        ///  Null
        /// </param>
        /// <returns>
        /// Void
        /// </returns>
        public void ResetState(){}

        /// <summary>
        ///  Does noooothing, but is needed due to interface
        /// </summary>
        /// <param>
        ///  Null
        /// </param>
        /// <returns>
        /// Void
        /// </returns>
        public void UpdateState() {}

        /// <summary>
        ///  Renders the different elements in the GamePaused State
        /// </summary>
        /// <param>
        ///  Null
        /// </param>
        /// <returns>
        /// Void
        /// </returns>
        public void RenderState() {
            Entity backgroundImage = new Entity(new StationaryShape(new Vec2F(0.0f,0.0f), new Vec2F(1.0f,1.0f)),
                new Image(Path.Combine("Assets", "Images", "SpaceBackground.png")));
            backgroundImage.RenderEntity();
            menuButtons[activeMenuButton].SetColor(new Vec3F(0.0f,0.8f,0.8f));
            for (int i = 0; i <= menuButtons.Length - 1; i++) {
                menuButtons[i].RenderText();
            }
        }
        
        /// <summary>
        ///  Handles input from the player in the GamePaused State
        /// </summary>
        /// <param action>
        ///  A KeyboardAction. Wether or not a key was pressed or let go
        /// </param action>
        /// <param key>
        ///  A KeyboardKey. The key pressed by the user
        /// </param key>
        /// <returns>
        ///  Void
        /// </returns>
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
                                BreakoutBus.GetBus().RegisterEvent(
                                    new GameEvent {EventType = GameEventType.GameStateEvent,
                                        Message = "CHANGE_STATE",
                                        StringArg1 = "GAME_RUNNING",
                                        StringArg2 = ""
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