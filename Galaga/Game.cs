using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Input;
using System.Collections.Generic;


namespace Galaga;

public class Game : DIKUGame, IGameEventProcessor {

    private Player player;

    private GameEventBus eventBus;

    public Game(WindowArgs windowArgs) : base(windowArgs) {

        player = new Player(

        new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),

        new Image(Path.Combine("Assets", "Images", "Player.png")));

        eventBus = new GameEventBus();

        eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent });

        window.SetKeyEventHandler(KeyHandler);

        eventBus.Subscribe(GameEventType.InputEvent, this);

    }
    public override void Render() {
        
        player.Render();

    }
    public override void Update() {

        this.eventBus.ProcessEventsSequentially();

        player.Move();
        
    }

    private void KeyPress(KeyboardKey key) {
        switch (key){
            case KeyboardKey.Escape:
                window.CloseWindow();
                break;

            case KeyboardKey.A:
                player.SetMoveRight(false);
                player.SetMoveLeft(true);
                player.Move();
                break;
            
            case KeyboardKey.D:
                player.SetMoveLeft(false);
                player.SetMoveRight(true);
                player.Move();
                break;

            case KeyboardKey.Left:
                player.SetMoveRight(false);
                player.SetMoveLeft(true);
                player.Move();
                break;
            
            case KeyboardKey.Right:
                player.SetMoveLeft(false);
                player.SetMoveRight(true);
                player.Move();
                break;

            /* case KeyboardKey.Space:
                tbd */

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

            /* case KeyboardKey.Space:
                tbd */

            default:
                break;
                
        }
    }
    private void KeyHandler(KeyboardAction action, KeyboardKey key) {
        /* while (){
            this.KeyPress(key);
        }
        this.KeyRelease(key); */

    }
    public void ProcessEvent(GameEvent gameEvent) {
        // Leave this empty for now
    }
}