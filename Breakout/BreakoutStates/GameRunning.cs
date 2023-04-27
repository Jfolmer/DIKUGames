using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Input;
using DIKUArcade.Physics;
using System.Collections.Generic;
using Breakout;
using DIKUArcade.State;

namespace Breakout.BreakoutStates{

    public class GameRunning : IGameState{
        private static GameRunning instance = null;
        private Player player;
        private Entity backgroundImage;
        
        public GameRunning(){
            GameInit();
        }
        public void GameInit(){
            player = new Player(
        
            new DynamicShape(new Vec2F(0.425f, 0.05f), new Vec2F(0.15f, 0.02f)),

            new Image(Path.Combine("Assets", "Images", "player.png")));

            backgroundImage = new Entity(new StationaryShape(new Vec2F(0.0f,0.0f), new Vec2F(1.0f,1.0f)),
                new Image(Path.Combine("Assets", "Images", "Overlay.png")));
            
            
        }
        public static void SetInstance(){
            instance = null;
        }
        private void KeyPress(KeyboardKey key) {
            switch (key){
                case KeyboardKey.A:
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent {EventType = GameEventType.PlayerEvent, To = player, Message = "LEFT"});
                    break;
                
                case KeyboardKey.D:
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent {EventType = GameEventType.PlayerEvent, To = player, Message = "RIGHT"});
                    break;

                case KeyboardKey.Left:
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent {EventType = GameEventType.PlayerEvent, To = player, Message = "LEFT"});
                    break;
                
                case KeyboardKey.Right:
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent {EventType = GameEventType.PlayerEvent, To = player, Message = "RIGHT"});
                    break;

                default:
                    break;
            }
        }
        private void KeyRelease(KeyboardKey key) {
            switch (key){
                case KeyboardKey.A:
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent {EventType = GameEventType.PlayerEvent, To = player, Message = "NOTLEFT"});
                    break;

                case KeyboardKey.D:
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent {EventType = GameEventType.PlayerEvent, To = player, Message = "NOTRIGHT"});
                    break;

                case KeyboardKey.Left:
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent {EventType = GameEventType.PlayerEvent, To = player, Message = "NOTLEFT"});
                    break;

                case KeyboardKey.Right:
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent {EventType = GameEventType.PlayerEvent, To = player, Message = "NOTRIGHT"});
                    break;

                default:
                    break;

            }
        }
        public static GameRunning GetInstance() {
            if (GameRunning.instance == null) {
                GameRunning.instance = new GameRunning();
                GameRunning.instance.ResetState();
            }
            return GameRunning.instance;
        }
        public void ResetState(){
            GameInit();
        }
        public void UpdateState() {
            player.Move();
        }
        public void RenderState() {
            backgroundImage.RenderEntity();
            player.Render();
        }
        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            if (action == KeyboardAction.KeyPress){
                KeyPress(key);
            }
            else{
                KeyRelease(key);
            }
        }
    }
}