using NUnit.Framework;
using Galaga.GalagaStates;
using System;
using DIKUArcade;
using DIKUArcade.Events;
using DIKUArcade.State;
using Galaga;

namespace GalagaTests {
    [TestFixture]
    public class StateMachineTesting {
        [SetUp]
        public void InitiateStateMachine() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();

            Staten = new StateMachine();

        }
        private StateMachine Staten;

        [Test]
        public void TestInitialState() {
            Assert.That(Staten.ActiveState, Is.InstanceOf<MainMenu>());
        }
        [Test]
        public void TestEventGamePaused() {
            Staten.ProcessEvent(new GameEvent{
                    EventType = GameEventType.GameStateEvent,
                    Message = "CHANGE_STATE",
                    StringArg1 = "GAME_PAUSED"
                });
            Assert.That(Staten.ActiveState, Is.InstanceOf<GamePaused>());
        }
    }
}