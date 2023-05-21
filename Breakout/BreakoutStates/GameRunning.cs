using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Input;
using DIKUArcade.Physics;
using DIKUArcade.Timers;
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
        private EntityContainer<PowerUp> powerUps;
        private EntityContainer<RocketShot> rockets;
        private List<Image> explosionStrides = ImageStride.CreateStrides(8, Path.Combine("Assets", "Images", "Explosion.png"));
        private AnimationContainer Explosions;
        private int Lives;
        private int startTime;
        private int startTimePlusWidth;
        private int startTimePlusSpeed;
        private int startTimeMinusSpeed;
        private int startTimePlusBallWidth;
        public static string ActiveLevel;
        public GameRunning(){
            GameInit();
        }
        public void GameInit(){
            player = new Player(
                new DynamicShape(new Vec2F(0.415f, 0.02f), new Vec2F(0.17f, 0.02f)),
                new Image(Path.Combine("Assets", "Images", "player.png"))
            );

            ball = new Ball(
            new DynamicShape(new Vec2F(0.5f, 0.015f), new Vec2F(0.03f, 0.03f)),
            new Image(Path.Combine("Assets", "Images", "SofieBold.png"))
            );
            
            backgroundImage = new Entity(new StationaryShape(new Vec2F(0.0f,0.0f), new Vec2F(1.0f,1.0f)),
                new Image(Path.Combine("Assets", "Images", "SpaceBackground.png")));
            
            reader = new AsciiReader();
            loader = new LevelLoader();

            LevelChange(ActiveLevel);
            powerUps = new EntityContainer<PowerUp>();
            rockets = new EntityContainer<RocketShot>();
            Explosions = new AnimationContainer(blocks.CountEntities());
            Lives = 3;
            startTime = (int)StaticTimer.GetElapsedSeconds();
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
                    ball.Launch(new Vec2F(1.0f, 1.0f));
                    if (player.Rocket){
                        Vec2F pos = new Vec2F (player.GetShape().Position.X + player.GetShape().Extent.X / 2.0f,player.GetShape().Position.Y + player.GetShape().Extent.Y);
                        var rocketStrides = new ImageStride(100,ImageStride.CreateStrides(5,Path.Combine("Assets","Images","RocketLaunched.png")));
                        rockets.AddEntity(new RocketShot(pos,rocketStrides));
                        player.Rocket = false;
                    }
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
        public void AddExplosion(Vec2F position, Vec2F extent) {
            Explosions.AddAnimation(new StationaryShape(position,extent),
            500, new ImageStride (8, explosionStrides));
        }
        public void UpdateState() {
            player.Move();
            PreLaunch(ball);
            BlockCollisions();
            PlayerCollisions();
            ball.Move();
            MovePowerUp();
            MoveRockets();
            CheckGameWon();
            CheckTime();
            PowerUpTimers();
        }
        private void CheckTime(){
            if ((int)StaticTimer.GetElapsedSeconds() - startTime >= System.Int32.Parse(reader.GetMeta()["Time:"])){
                BreakoutBus.GetBus().RegisterEvent(
                    new GameEvent{EventType = GameEventType.GameStateEvent,
                        Message = "CHANGE_STATE",
                        StringArg1 = "GAME_OVER"
                    }
                );
            }
        }
        public void SpawnPowerUp(BaseBlock block){
            Vec2F spawn = new Vec2F((float)block.shape.Position.X - 0.015f + (float)block.shape.Extent.X / (float)2,block.shape.Position.Y);
            System.Random rnd = new System.Random();
            int rndNum = rnd.Next(1,11);
            if (block.BlockType != "Hardened" && block.BlockType != "Unbreakable" && !block.PowerUp){
                switch (rndNum){
                    case 1:
                        powerUps.AddEntity(new PowerUp(spawn,new Image(Path.Combine("Assets","Images", "HalfSpeedPowerUp.png")),"-Speed"));
                        break;
                    case 2:
                        powerUps.AddEntity(new PowerUp(spawn,new Image(Path.Combine("Assets","Images", "BombPickUp.png")),"Bomb"));
                        break;
                    default:
                        break;
                }
            }
            else if (block.PowerUp){
                switch (rndNum){
                    case 1: case 2:
                        powerUps.AddEntity(new PowerUp(spawn,new Image(Path.Combine("Assets","Images", "RocketPickUp.png")),"Rocket"));
                        break;
                    case 3: case 4:
                        powerUps.AddEntity(new PowerUp(spawn,new Image(Path.Combine("Assets","Images", "BigPowerUp.png")),"BigBallsBaby"));
                        break;
                    case 5: case 6:
                        powerUps.AddEntity(new PowerUp(spawn,new Image(Path.Combine("Assets","Images", "DoubleSpeedPowerUp.png")),"+Speed"));
                        break;
                    case 7: case 8: 
                        powerUps.AddEntity(new PowerUp(spawn,new Image(Path.Combine("Assets","Images", "WidePowerUp.png")),"+Width"));
                        break;
                    case 9: case 10:
                        powerUps.AddEntity(new PowerUp(spawn,new Image(Path.Combine("Assets","Images", "LifePickUp.png")),"+HP"));
                        break;
                }
            }
        }
        public void RenderState() {
            backgroundImage.RenderEntity();
            player.Render();
            blocks.RenderEntities();
            ball.Render();
            if (powerUps.CountEntities() != 0){
                powerUps.RenderEntities();
            }
            if (rockets.CountEntities() != 0){
                rockets.RenderEntities();
            }
            Explosions.RenderAnimations();
            RenderLives(Lives);
            RenderTime();
        }
        private void RenderTime(){
            int time = System.Int32.Parse(reader.GetMeta()["Time:"]) - (int)StaticTimer.GetElapsedSeconds() + startTime;
            Vec2F pos = new Vec2F(0.0f,-0.175f);
            Vec2F ext = new Vec2F(0.2f,0.2f);
            Text graphic = new Text("Time:" + time.ToString(), pos, ext);
            graphic.SetColor(new Vec3F(1.0f,1.0f,1.0f));
            graphic.RenderText();
        }
        private void RenderLives(int input){
            List<Entity> hearts = new List<Entity>();
            for (int i = 0; i < input; i++){
                if (i > 2){
                    hearts.Add(new Entity(new StationaryShape(new Vec2F(0.0f,0.05f + i * 0.05f), new Vec2F(0.05f, 0.05f)),
                        new Image(Path.Combine("Assets", "Images", "YellowHeart.png"))));
                }
                else {
                    hearts.Add(new Entity(new StationaryShape(new Vec2F(0.0f,0.05f +  i * 0.05f), new Vec2F(0.05f, 0.05f)),
                        new Image(Path.Combine("Assets", "Images", "heart_filled.png"))));
                }
            }
            if (hearts.Count < 3){
                for (int i = hearts.Count; i < 3; i++){
                    hearts.Add(new Entity(new StationaryShape(new Vec2F(0.0f,0.05f + i * 0.05f), new Vec2F(0.05f, 0.05f)),
                        new Image(Path.Combine("Assets", "Images", "heart_empty.png"))));
                }
            }
            for (int i = 0; i < hearts.Count; i++){
                hearts[i].RenderEntity();
            }
        }
        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            if (action == KeyboardAction.KeyPress){
                KeyPress(key);
            }
            else{
                KeyRelease(key);
            }
        }
        private void CheckGameWon(){
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
                    case "wall.txt": case "columns.txt": case "central-mass.txt":
                        BreakoutBus.GetBus().RegisterEvent(
                            new GameEvent{EventType = GameEventType.GameStateEvent,
                                Message = "CHANGE_STATE",
                                StringArg1 = "SCORE_SCREEN"
                            }
                        );
                        break;
                }
            } else if (ActiveLevel == "level3.txt" && blocks.CountEntities() > 21){
                BreakoutBus.GetBus().RegisterEvent(
                    new GameEvent{EventType = GameEventType.GameStateEvent,
                        Message = "CHANGE_STATE",
                        StringArg1 = "SCORE_SCREEN"
                    }
                );
            }
        }
        private void MoveRockets(){
            rockets.Iterate(rocket =>{ 
                if (rocket.Shape.Position.Y > 1){
                    rocket.DeleteEntity();
                }
                else {
                    rocket.Move();
                }});
        }
        private void PreLaunch(Ball input){
            if (!ball.launched){
                ball.shape.Position = new Vec2F((player.GetShape().Position.X + player.GetShape().Extent.X * (float)0.45), ((float)player.GetShape().Position.Y + player.GetShape().Extent.Y));
            }
        }
        private void MovePowerUp(){
            if (powerUps.CountEntities() != 0){
                powerUps.Iterate(powerup => { 
                    var col = CollisionDetection.Aabb(powerup.Shape.AsDynamicShape(),player.GetShape()).Collision;
                    if (col) {
                        switch (powerup.Type){
                            case "+Speed":
                                startTimePlusSpeed = (int) StaticTimer.GetElapsedSeconds();
                                player.ChangeSpeed(0.03f);
                                powerup.DeleteEntity();
                                break;
                            case "+Width":
                                startTimePlusWidth = (int) StaticTimer.GetElapsedSeconds();
                                player.GetShape().Extent.X = player.GetShape().Extent.X * 1.5f;
                                powerup.DeleteEntity();
                                break;
                            case "Rocket":
                                player.Rocket = true;
                                powerup.DeleteEntity();
                                break;
                            case "BigBallsBaby":
                                startTimePlusBallWidth= (int) StaticTimer.GetElapsedSeconds();
                                ball.Shape.Extent = new Vec2F(ball.Shape.Extent.X * 1.5f, ball.Shape.Extent.Y * 1.5f);
                                powerup.DeleteEntity();
                                break;
                            case "+HP":
                                Lives++;
                                powerup.DeleteEntity();
                                break;
                            case "Bomb":
                                AddExplosion(new Vec2F(0.0f,0.0f),new Vec2F(1.0f,1.0f));
                                if (Lives > 1){
                                    Lives--;
                                    player = new Player(
                                        new DynamicShape(new Vec2F(0.415f, 0.02f), new Vec2F(0.17f, 0.02f)),
                                        new Image(Path.Combine("Assets", "Images", "player.png"))
                                    );

                                    ball = new Ball(
                                        new DynamicShape(new Vec2F(0.5f, 0.015f), new Vec2F(0.03f, 0.03f)),
                                        new Image(Path.Combine("Assets", "Images", "SofieBold.png"))
                                    );
                                }
                                else{
                                    BreakoutBus.GetBus().RegisterEvent(
                                        new GameEvent{EventType = GameEventType.GameStateEvent,
                                            Message = "CHANGE_STATE",
                                            StringArg1 = "GAME_OVER"
                                        }
                                    );
                                }
                                powerup.DeleteEntity();
                                break;
                            case "-Speed":
                                startTimeMinusSpeed = (int) StaticTimer.GetElapsedSeconds();
                                player.ChangeSpeed(0.0075f);
                                powerup.DeleteEntity();
                                break;
                        }
                    }
                    powerup.Move();
                });
            }
        }
        private void PowerUpTimers(){
            int MinusSpeedTime = (int) StaticTimer.GetElapsedSeconds() - startTimeMinusSpeed;
            int PlusSpeedTime = (int) StaticTimer.GetElapsedSeconds() - startTimePlusSpeed;
            int PlusBallWidthTime = (int) StaticTimer.GetElapsedSeconds() - startTimePlusBallWidth;
            int PlusWidthTime = (int) StaticTimer.GetElapsedSeconds() - startTimePlusWidth;
            if (player.GetSpeed() != 0.015f && !(PlusSpeedTime <= 10) && !(MinusSpeedTime <= 10)){
                player.ChangeSpeed(0.015f); 
            }
            if (ball.shape.Extent != new Vec2F(0.03f,0.03f) && !(PlusBallWidthTime <= 10)){
                ball.shape.Extent = new Vec2F(0.03f,0.03f);
            }
            if (player.GetShape().Extent != new Vec2F(0.17f,0.02f) && !(PlusWidthTime <= 10)){
                player.GetShape().Extent = new Vec2F(0.17f,0.02f);
            }
        }
        private void PlayerCollisions(){
            var PlayerCol = CollisionDetection.Aabb(ball.shape, player.GetShape());
            if (PlayerCol.CollisionDir == CollisionDirection.CollisionDirUp || PlayerCol.CollisionDir == CollisionDirection.CollisionDirDown){
                ball.UpdateDirection(ball.AngleRandomizer((ball.shape).Direction.X),-ball.AngleRandomizer((ball.shape).Direction.Y));                  
            }
            else if (PlayerCol.CollisionDir == CollisionDirection.CollisionDirLeft || PlayerCol.CollisionDir == CollisionDirection.CollisionDirRight){
                ball.UpdateDirection(-ball.AngleRandomizer((ball.shape).Direction.X),ball.AngleRandomizer((ball.shape).Direction.Y));
            }
        }
        private void BlockCollisions(){
            blocks.Iterate(block => {
                var col = CollisionDetection.Aabb(ball.shape.AsDynamicShape(), block.shape).CollisionDir;
                if (col == CollisionDirection.CollisionDirDown 
                    || col == CollisionDirection.CollisionDirUp){
                    SpawnPowerUp(block);
                    block.Hit();
                    if (block.BlockType != "Unbreakable"){
                        Points.IncreaseTally();
                    }
                    ball.UpdateDirection(ball.AngleRandomizer((ball.shape).Direction.X),-ball.AngleRandomizer((ball.shape).Direction.Y));
                }
                else if (col == CollisionDirection.CollisionDirRight 
                    || col == CollisionDirection.CollisionDirLeft){
                    SpawnPowerUp(block);
                    block.Hit();
                    if (block.BlockType != "Unbreakable"){
                        Points.IncreaseTally();
                    }
                    ball.UpdateDirection(-ball.AngleRandomizer((ball.shape).Direction.X),ball.AngleRandomizer((ball.shape).Direction.Y));
                } else if (ball.IsDeleted()) {
                    if (Lives > 1){
                        Lives--;
                        player = new Player(
                            new DynamicShape(new Vec2F(0.415f, 0.02f), new Vec2F(0.17f, 0.02f)),
                            new Image(Path.Combine("Assets", "Images", "player.png"))
                        );

                        ball = new Ball(
                            new DynamicShape(new Vec2F(0.5f, 0.015f), new Vec2F(0.03f, 0.03f)),
                            new Image(Path.Combine("Assets", "Images", "SofieBold.png"))
                        );
                    }
                    else{
                        BreakoutBus.GetBus().RegisterEvent(
                        new GameEvent{EventType = GameEventType.GameStateEvent,
                            Message = "CHANGE_STATE",
                            StringArg1 = "GAME_OVER"
                        }
                    );
                    }
                }
                rockets.Iterate(rocket => {
                    var rocketcol = CollisionDetection.Aabb(rocket.Shape.AsDynamicShape(), block.shape).Collision;
                        if (rocket.Shape.Position.Y > 1.0f){
                            rocket.DeleteEntity();
                        }
                        else if (rocketcol){
                            AddExplosion(block.shape.Position,block.shape.Extent);
                            block.Hit();
                            SpawnPowerUp(block);
                            rocket.DeleteEntity();
                        }
                });
            });
        }
            
    }
}