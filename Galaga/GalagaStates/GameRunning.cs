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
using Galaga.Squadron;
using Galaga.MovementStrategy;
using Galaga;
using DIKUArcade.State;

namespace Galaga.GalagaStates {
    public class GameRunning : IGameState {

        private static GameRunning instance = null;
        private Player player;

        private EntityContainer<Enemy> enemies;

        private EntityContainer<PlayerShot> playerShots;

        private IBaseImage playerShotImage;

        private List<Image> enemyStridesBlue;

        private List<Image> enemyStridesGreen;

        private List<Image> enemyStridesRed;

        private AnimationContainer enemyExplosions;

        private List<Image> explosionStrides;

        private const int EXPLOSION_LENGTH_MS = 500;

        private Score Points = new Score(0);

        private ISquadron Squad;

        private IMovementStrategy move;

        private Health HP;

        private bool GameOver;
        public GameRunning(){
            GameInit();
        }

        public void GameInit(){
            player = new Player(
        
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),

            new Image(Path.Combine("Assets", "Images", "Player.png")));

            HP = new Health(new Vec2F (0.0f,0.0f),new Vec2F(0.2f,0.3f));

            GameOver = false;

            HP.GetDisplay().SetColor(new Vec3F (1.0f,1.0f,1.0f));

            enemyStridesBlue = ImageStride.CreateStrides(4, Path.Combine("Assets",
                "Images", "BlueMonster.png"));

            enemyStridesGreen = ImageStride.CreateStrides(2, Path.Combine("Assets",
                "Images", "GreenMonster.png"));

            enemyStridesRed = ImageStride.CreateStrides(2, Path.Combine("Assets",
                "Images", "RedMonster.png"));

            System.Random rnd = new System.Random();

            int num = rnd.Next(1,6);

            int numMove = rnd.Next(1,4);

            switch (numMove){
                case 1:
                    move = new NoMove();
                    break;

                case 2:
                    move = new Down();
                    break;
                case 3:
                    move = new ZigZagDown();
                    break;
            }

            switch (num){
                case 1:
                    Squad = new ColSquad();
                    break;

                case 2:
                    Squad = new DiamondSquad();
                    break;

                case 3:
                    Squad = new DiagSquad();
                    break;

                case 4:
                    Squad = new BoxSquad();
                    break;

                case 5:
                    Squad = new ArrowSquad();
                    break;
            }

            enemies = Squad.Enemies;

            Squad.CreateEnemies(enemyStridesBlue,enemyStridesRed);

            playerShots = new EntityContainer<PlayerShot>();

            playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));

            enemyExplosions = new AnimationContainer(Squad.MaxEnemies);

            explosionStrides = ImageStride.CreateStrides(8, Path.Combine("Assets", "Images", "Explosion.png"));
        }
        public static void SetInstance(){
            instance = null;
        }
        private void KeyPress(KeyboardKey key) {
            switch (key){
                case KeyboardKey.Escape:
                    GalagaBus.GetBus().RegisterEvent(
                                new GameEvent{EventType = GameEventType.GameStateEvent,
                                Message = "CHANGE_STATE",
                                StringArg1 = "GAME_PAUSED"
                                }
                            );
                    break;

                case KeyboardKey.A:
                    GalagaBus.GetBus().RegisterEvent(new GameEvent {EventType = GameEventType.PlayerEvent, To = player, Message = "LEFT"});
                    break;
                
                case KeyboardKey.D:
                    GalagaBus.GetBus().RegisterEvent(new GameEvent {EventType = GameEventType.PlayerEvent, To = player, Message = "RIGHT"});
                    break;

                case KeyboardKey.Left:
                    GalagaBus.GetBus().RegisterEvent(new GameEvent {EventType = GameEventType.PlayerEvent, To = player, Message = "LEFT"});
                    break;
                
                case KeyboardKey.Right:
                    GalagaBus.GetBus().RegisterEvent(new GameEvent {EventType = GameEventType.PlayerEvent, To = player, Message = "RIGHT"});
                    break;

                case KeyboardKey.Space:
                    playerShots.AddEntity(new PlayerShot (new Vec2F ((player.GetShape().Position.X + player.GetShape().Extent.X/2),
                    ((player.GetShape().Position.Y + player.GetShape().Extent.Y))) ,playerShotImage));
                    break;

                case KeyboardKey.W:
                    GalagaBus.GetBus().RegisterEvent(new GameEvent {EventType = GameEventType.PlayerEvent, To = player, Message = "UP"});
                    break;

                case KeyboardKey.S:
                    GalagaBus.GetBus().RegisterEvent(new GameEvent {EventType = GameEventType.PlayerEvent, To = player, Message = "DOWN"});
                    break;
                case KeyboardKey.Up:
                    GalagaBus.GetBus().RegisterEvent(new GameEvent {EventType = GameEventType.PlayerEvent, To = player, Message = "UP"});
                    break;
                
                case KeyboardKey.Down:
                    GalagaBus.GetBus().RegisterEvent(new GameEvent {EventType = GameEventType.PlayerEvent, To = player, Message = "DOWN"});
                    break;

                default:
                    break;
            }
        }
        private void KeyRelease(KeyboardKey key) {
            switch (key){
                case KeyboardKey.A:
                    GalagaBus.GetBus().RegisterEvent(new GameEvent {EventType = GameEventType.PlayerEvent, To = player, Message = "NOTLEFT"});
                    break;

                case KeyboardKey.D:
                    GalagaBus.GetBus().RegisterEvent(new GameEvent {EventType = GameEventType.PlayerEvent, To = player, Message = "NOTRIGHT"});
                    break;

                case KeyboardKey.Left:
                    GalagaBus.GetBus().RegisterEvent(new GameEvent {EventType = GameEventType.PlayerEvent, To = player, Message = "NOTLEFT"});
                    break;

                case KeyboardKey.Right:
                    GalagaBus.GetBus().RegisterEvent(new GameEvent {EventType = GameEventType.PlayerEvent, To = player, Message = "NOTRIGHT"});
                    break;

                case KeyboardKey.W:
                    GalagaBus.GetBus().RegisterEvent(new GameEvent {EventType = GameEventType.PlayerEvent, To = player, Message = "NOTUP"});
                    break;

                case KeyboardKey.S:
                    GalagaBus.GetBus().RegisterEvent(new GameEvent {EventType = GameEventType.PlayerEvent, To = player, Message = "NOTDOWN"});
                    break;

                case KeyboardKey.Up:
                    GalagaBus.GetBus().RegisterEvent(new GameEvent {EventType = GameEventType.PlayerEvent, To = player, Message = "NOTUP"});
                    break;

                case KeyboardKey.Down:
                    GalagaBus.GetBus().RegisterEvent(new GameEvent {EventType = GameEventType.PlayerEvent, To = player, Message = "NOTDOWN"});
                    break;

                default:
                    break;

            }
        }
        private void IterateShots() {
        playerShots.Iterate(shot => {

            shot.Move();

            if (shot.Shape.Position.Y > 1.0f ) {
                
                shot.DeleteEntity();

            } else {

                enemies.Iterate(enemy => {

                    if (CollisionDetection.Aabb(shot.Shape.AsDynamicShape(), enemy.Shape).Collision){

                        enemy.Hit();

                        shot.DeleteEntity();
                        
                        if (enemy.GetHP() < 2){
                            enemy.Enrage();
                        }

                        if (enemy.GetHP() < 1){
                            enemy.DeleteEntity();

                            AddExplosion(enemy.Shape.Position,enemy.Shape.Extent);

                            Points.IncreaseTally();
                        }

                    }
                });
                
            }
        });
    }

    public void AddExplosion(Vec2F position, Vec2F extent) {

        enemyExplosions.AddAnimation(new StationaryShape(position,extent),
        EXPLOSION_LENGTH_MS, new ImageStride (8, explosionStrides));

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
            HP.UpdateHP();
            if (HP.GetHealth() < 1){
                GalagaBus.GetBus().RegisterEvent(new GameEvent {EventType = GameEventType.GameStateEvent,
                    Message = "CHANGE_STATE",
                    StringArg1 = "GAME_OVER"});
            }
            if (!GameOver){
                player.Move();
            }
            IterateShots();

            move.MoveEnemies(enemies);

            if (!GameOver){
                enemies.Iterate(enemy => {
                    if (enemy.GetShape().Position.Y < 0.2f){
                        enemy.DeleteEntity();
                        HP.LoseHealth();
                        if (HP.GetHealth() == 0 && !GameOver){
                            GameOver = true;
                        }
                    }
                });
            }

            if (enemies.CountEntities() < 1 && !GameOver){
                System.Random rnd = new System.Random();
                int num = rnd.Next(1,6);
                int numMove = rnd.Next(1,4);
                switch (numMove){
                    case 1:
                        move = new NoMove();
                        break;
                    case 2:
                        move = new Down();
                        break;
                    case 3:
                        move = new ZigZagDown();
                        break;
                }
                switch (num){
                    case 1:
                        Squad = new ColSquad();
                        break;
                    case 2:
                        Squad = new DiamondSquad();
                        break;
                    case 3:
                        Squad = new DiagSquad();
                        break;
                    case 4:
                        Squad = new BoxSquad();
                        break;
                    case 5:
                        Squad = new ArrowSquad();
                        break;
                }
                enemies = Squad.Enemies;
                Squad.CreateEnemies(enemyStridesBlue,enemyStridesRed);
                enemies.Iterate(enemy => {enemy.IncreaseSpeed();});
            }
        }
        public void RenderState() {
            if (!GameOver){
                enemies.RenderEntities();
            }
        
            if (!GameOver){
                player.Render();
            }
            enemyExplosions.RenderAnimations();

            playerShots.RenderEntities();

            HP.RenderHealth();
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