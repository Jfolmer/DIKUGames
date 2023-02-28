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

    }
    
    public override void Render() {
        
        enemies.RenderEntities();

        player.Render();

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
                playerShots.AddEntity(new PlayerShot (new Vec2F ((player.GetShape().Position.X + player.GetShape().Extent.X/2),((player.GetShape().Position.Y + player.GetShape().Extent.Y))) ,playerShotImage));
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
            if ( shot.GetShape().Position.Y >= 1.0f ) {
                shot.DeleteEntity();
            } else {
                enemies.Iterate(enemy => {
                    // TODO: if collision btw shot and enemy -> delete both entities
                    if (enemy.GetShape().Position == shot.GetShape().Position){
                        shot.DeleteEntity();
                        enemy.DeleteEntity();
                    }
                });
            }
        });
    }

}