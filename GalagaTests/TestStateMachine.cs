using NUnit.Framework;
using Galaga.GalagaStates;
using System;
using DIKUArcade;
using DIKUArcade.Events;
using DIKUArcade.State;
using GalagaStates;
using Galaga;


namespace GalagaTests {
    [TestFixture]
    public class StateMachineTesting {
    [SetUp]
    public void InitiateStateMachine() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();

        stateMachine = new StateMachine();

        GalagaBus.GetBus().InitializeEventBus(new List<GameEventType> 
            {GameEventType.GameStateEvent, GameEventType.InputEvent});

        GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateMachine);
        GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, stateMachine);
    }
    private StateMachine stateMachine;
    [Test]
    public void TestInitialState() {
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<MainMenu>());
    }
    [Test]
    public void TestEventGamePaused() {
        GalagaBus.GetBus().RegisterEvent(
            new GameEvent{
                EventType = GameEventType.GameStateEvent,
                Message = "CHANGE_STATE",
                StringArg1 = "GAME_PAUSED"
            }
        );
        GalagaBus.GetBus().ProcessEventsSequentially();
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<GamePaused>());
    }
    }
}