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
using Breakout.BreakoutStates;


namespace Breakout;

public class Game : DIKUGame, IGameEventProcessor {

    private StateMachine stateMachine;
    
    public Game(WindowArgs windowArgs) : base(windowArgs) {

        BreakoutBus.GetBus().InitializeEventBus(new List<GameEventType> {GameEventType.InputEvent,
            GameEventType.PlayerEvent, 
            GameEventType.WindowEvent, 
            GameEventType.GameStateEvent});

        window.SetKeyEventHandler(KeyHandler);

        BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, this);

        BreakoutBus.GetBus().Subscribe(GameEventType.WindowEvent, this);

        stateMachine = new StateMachine();

    }
    
    public override void Render() {
        stateMachine.ActiveState.RenderState();  
    }
    public override void Update() {
        BreakoutBus.GetBus().ProcessEventsSequentially();
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