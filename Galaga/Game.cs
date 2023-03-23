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
using Galaga.GalagaStates;


namespace Galaga;

public class Game : DIKUGame, IGameEventProcessor {

    private StateMachine stateMachine;
    
    public Game(WindowArgs windowArgs) : base(windowArgs) {

        GalagaBus.GetBus().InitializeEventBus(new List<GameEventType> {GameEventType.InputEvent,
            GameEventType.PlayerEvent, 
            GameEventType.WindowEvent, 
            GameEventType.GameStateEvent});

        window.SetKeyEventHandler(KeyHandler);

        GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);

        GalagaBus.GetBus().Subscribe(GameEventType.WindowEvent, this);

        stateMachine = new StateMachine();

    }
    
    public override void Render() {
        stateMachine.ActiveState.RenderState();  
    }
    public override void Update() {
        GalagaBus.GetBus().ProcessEventsSequentially();
        stateMachine.ActiveState.UpdateState();        
    }

    private void KeyHandler(KeyboardAction action, KeyboardKey key) {
        stateMachine.ActiveState.HandleKeyEvent(action,key);
    }
    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.WindowEvent){
            window.CloseWindow();
        }
    }
}