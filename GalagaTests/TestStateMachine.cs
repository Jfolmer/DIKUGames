using NUnit.Framework;
using Galaga.GalagaStates;
using System;
using DIKUArcade;
using DIKUArcade.Events;
using DIKUArcade.State;
using GalagaStates;


namespace GalagaTests {
    [TestFixture]
    public class StateMachineTesting {
    [SetUp]
    public void InitiateStateMachine() {
        DIKUArcade.GUI.Window.CreateOpenGLContext();

        StateMachine = new Statemachine();

        GalagaBus.GetBus().InitializeEventBus(new List<GameEventType> 
            {GameEventType.GameStateEvent, GameEventType.InputEvent});

        GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
        GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);
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