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
using Breakout.Loader;
using Breakout.Blocks;

namespace Breakout.BreakoutStates{

    public class GameRunning : IGameState{
        private static GameRunning instance = null;
        private Ball ball;
        private Player player;
        private Entity backgroundImage;
        private AsciiReader reader;
        private LevelLoader loader;
        private EntityContainer<BaseBlock> blocks;
        public static string ActiveLevel;
        public GameRunning(){
            GameInit();
        }
        public void GameInit(){
            player = new Player(
                new DynamicShape(new Vec2F(0.425f, 0.02f), new Vec2F(0.17f, 0.02f)),
                new Image(Path.Combine("Assets", "Images", "player.png"))
            );

            ball = new Ball(
            new DynamicShape(new Vec2F(0.5f, 0.01f), new Vec2F(0.03f, 0.03f)),
            new Image(Path.Combine("Assets", "Images", "ball.png"))
            );
            
            backgroundImage = new Entity(new StationaryShape(new Vec2F(0.0f,0.0f), new Vec2F(1.0f,1.0f)),
                new Image(Path.Combine("Assets", "Images", "Overlay.png")));
            
            reader = new AsciiReader();
            loader = new LevelLoader();

            LevelChange(ActiveLevel);
        }
        public static void SetLevel(string lvl){
            ActiveLevel = lvl;
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
                case KeyboardKey.Space:
                    blocks.Iterate(block => block.Hit());
                    break;
                
                case KeyboardKey.Escape:
                    BreakoutBus.GetBus().RegisterEvent(
                        new GameEvent{EventType = GameEventType.GameStateEvent,
                            Message = "CHANGE_STATE",
                            StringArg1 = "GAME_PAUSED"
                        }
                    );
                    break;

                default:
                    break;
            }
        }
        private void LevelChange(string Lvl){
            if (Lvl != null){
                reader.Read(Path.Combine("Breakout","Assets", "Levels", Lvl));
            }
            else {
                reader.Read(Path.Combine("Breakout","Assets", "Levels", "level1.txt"));
            }
            blocks = loader.ReadLevel(reader.GetMap(),reader.GetMeta(),reader.GetLegend());
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
            ball.Move();
            blocks.Iterate(block =>
                block.GetHP()
            );
            if (ActiveLevel != "level3.txt" && blocks.CountEntities() == 0){
                switch (ActiveLevel){
                    case "level1.txt":
                        SetLevel("level2.txt");
                        ResetState();
                        break;
                    case "level2.txt":
                        SetLevel("level3.txt");
                        ResetState();
                        break;
                }
            } else if (ActiveLevel == "level3.txt" && blocks.CountEntities() == 20){
                SetLevel("level1.txt");
                ResetState();
            }
        }
        public void RenderState() {
            player.Render();
            blocks.RenderEntities();
            ball.Render();
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