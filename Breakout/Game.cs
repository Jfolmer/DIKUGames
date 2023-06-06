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
    /// <summary> renders the games active state </summary>
    /// <param> Null </param>
    /// <returns> void </returns>
    public override void Render() {
        stateMachine.ActiveState.RenderState();  
    }
    /// <summary> updates the games active state </summary>
    /// <param> Null </param>
    /// <returns> void </returns>
    public override void Update() {
        BreakoutBus.GetBus().ProcessEventsSequentially();
        stateMachine.ActiveState.UpdateState();        
    }
    /// <summary> handles the keyboard input </summary>
    /// <param> KeyboardAction action, KeyboardKey key </param>
    /// <returns> void </returns>
    private void KeyHandler(KeyboardAction action, KeyboardKey key) {
        stateMachine.ActiveState.HandleKeyEvent(action,key);
    }
    /// <summary> processes the events </summary>
    /// <param>GameEvent gameEvent </param>
    /// <returns> void </returns>
    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.WindowEvent){
            window.CloseWindow();
        }
    }
}