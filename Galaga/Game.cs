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


namespace Galaga;

public class Game : DIKUGame, IGameEventProcessor {

    private Player player;

    private GameEventBus eventBus;

    private EntityContainer<Enemy> enemies;

    private EntityContainer<PlayerShot> playerShots;

    private IBaseImage playerShotImage;

    private AnimationContainer enemyExplosions;

    private List<Image> explosionStrides;

    private const int EXPLOSION_LENGTH_MS = 500;

    private Score Points = new Score(0);

    public Game(WindowArgs windowArgs) : base(windowArgs) {

        player = new Player(

        new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),

        new Image(Path.Combine("Assets", "Images", "Player.png")));

        eventBus = new GameEventBus();

        eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent });

        window.SetKeyEventHandler(KeyHandler);

        eventBus.Subscribe(GameEventType.InputEvent, this);

        List<Image> images = ImageStride.CreateStrides
            (4, Path.Combine("Assets", "Images", "BlueMonster.png"));

        const int numEnemies = 8;

        enemies = new EntityContainer<Enemy>(numEnemies);

        for (int i = 0; i < numEnemies; i++) {
            enemies.AddEntity(new Enemy(
                new DynamicShape(new Vec2F(0.1f + (float)i * 0.1f, 0.9f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, images)));
        }

        playerShots = new EntityContainer<PlayerShot>();

        playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));

        enemyExplosions = new AnimationContainer(numEnemies);

        explosionStrides = ImageStride.CreateStrides(8, Path.Combine("Assets", "Images", "Explosion.png"));

    }
    
    public override void Render() {
        
        enemies.RenderEntities();

        player.Render();

        enemyExplosions.RenderAnimations();

        playerShots.RenderEntities();

    }
    public override void Update() {

        this.eventBus.ProcessEventsSequentially();

        player.Move();

        IterateShots();

    }

    private void KeyPress(KeyboardKey key) {
        switch (key){
            case KeyboardKey.Escape:
                System.Console.WriteLine("The player scored {0} points!", Points.tally );
                window.CloseWindow();
                break;

            case KeyboardKey.A:
                player.SetMoveLeft(true);
                break;
            
            case KeyboardKey.D:
                player.SetMoveRight(true);
                break;

            case KeyboardKey.Left:
                player.SetMoveLeft(true);
                break;
            
            case KeyboardKey.Right:
                player.SetMoveRight(true);
                break;

            case KeyboardKey.Space:
                playerShots.AddEntity(new PlayerShot (new Vec2F ((player.GetShape().Position.X + player.GetShape().Extent.X/2),
                ((player.GetShape().Position.Y + player.GetShape().Extent.Y))) ,playerShotImage));
                break;

            case KeyboardKey.W:
                player.SetMoveUp(true);
                break;

            case KeyboardKey.S:
                player.SetMoveDown(true);
                break;

            default:
                break;
        }
    }
    private void KeyRelease(KeyboardKey key) {
        switch (key){
            case KeyboardKey.A:
                player.SetMoveLeft(false);
                break;
            
            case KeyboardKey.D:
                player.SetMoveRight(false);
                break;

            case KeyboardKey.Left:
                player.SetMoveLeft(false);
                break;
            
            case KeyboardKey.Right:
                player.SetMoveRight(false);
                break;

            case KeyboardKey.W:
                player.SetMoveUp(false);
                break;

            case KeyboardKey.S:
                player.SetMoveDown(false);
                break;

            default:
                break;
                
        }
    }
    private void KeyHandler(KeyboardAction action, KeyboardKey key) {

        if (action == KeyboardAction.KeyPress){

            KeyPress(key);

        }
        else{

            KeyRelease(key);

        }
    }
    public void ProcessEvent(GameEvent gameEvent) {
        
    }

    
    private void IterateShots() {
        playerShots.Iterate(shot => {

            shot.Move();

            if (shot.Shape.Position.Y > 1.0f ) {
                
                shot.DeleteEntity();

            } else {

                enemies.Iterate(enemy => {

                    if (CollisionDetection.Aabb(shot.Shape.AsDynamicShape(), enemy.Shape).Collision){

                        enemy.DeleteEntity();
                        
                        AddExplosion(enemy.Shape.Position,enemy.Shape.Extent);
                        
                        shot.DeleteEntity();

                        Points.IncreaseTally();

                    }
                });
            }
        });
    }

    public void AddExplosion(Vec2F position, Vec2F extent) {

        enemyExplosions.AddAnimation(new StationaryShape(position,extent),
        EXPLOSION_LENGTH_MS, new ImageStride (8, explosionStrides));

    }

}