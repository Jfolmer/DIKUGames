using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using System.IO;
using DIKUArcade.Math;
using DIKUArcade.Events;

namespace Galaga.GalagaStates {
    public class GameOver : IGameState {
        private static GameOver instance = null;
        private Text Button;
        private Text Loser;
        private int activeMenuButton;
        private int maxMenuButtons;
        public GameOver(){

            Button = new Text("Main Menu", new Vec2F(0.3f,0.1f),new Vec2F(0.3f,0.3f));

            Button.SetColor(new Vec3F(1.0f,1.0f,1.0f));

            Loser = new Text("Game Over!", new Vec2F(0.3f,0.3f),new Vec2F(0.4f,0.4f));

            Loser.SetColor(new Vec3F(198f/255f,30f/255f,30f/255f));
        }
        public static GameOver GetInstance() {
            if (GameOver.instance == null) {
                GameOver.instance = new GameOver();
                GameOver.instance.ResetState();
            }
            return GameOver.instance;
        }
        public void ResetState(){}
        public void UpdateState() {}
        public void RenderState() {

            Loser.RenderText();

            Button.RenderText();

        }
        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            if (action == KeyboardAction.KeyPress){
                switch (key) {
                    case KeyboardKey.Enter:
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
            }
        }
    }
}