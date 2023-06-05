using NUnit.Framework;
using Breakout.BreakoutStates;
using System;
using DIKUArcade;
using DIKUArcade.Events;
using DIKUArcade.State;
using Breakout;

namespace Breakout.Tests {
    [TestFixture]
    public class PowerUpTesting {
        private StateMachine? Staten;
        private EntityContainer<PowerUp>? powerUps;
        private PowerUp? powerUp;
        [SetUp]
        public void InitiateStateMachine() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            Staten = new StateMachine();
            Staten.ProcessEvent(new GameEvent{
                    EventType = GameEventType.GameStateEvent,
                    Message = "CHANGE_STATE",
                    StringArg1 = "GAME_RUNNING",
                    StringArg2 = "wall.txt"
                });
            powerUps = new EntityContainer<PowerUp>();
        }
        [Test]
        public void TestPowerUpSpawn() {
            Vec2F spawn = new Vec2F (0.5f,0.5f);
            powerUps?.AddEntity(new PowerUp(spawn,new Image(Path.Combine("Assets","Images", "RocketPickUp.png")),"Rocket"));
            Assert.AreEqual(powerUps?.CountEntities(),1);
            powerUps?.AddEntity(new PowerUp(spawn,new Image(Path.Combine("Assets","Images", "BigPowerUp.png")),"BigBallsBaby"));
            Assert.AreEqual(powerUps?.CountEntities(),2);
        }
        [Test]
        public void TestPowerUpMovement() {
            Vec2F spawn = new Vec2F (0.5f,0.5f);
            powerUp = new PowerUp(spawn,new Image(Path.Combine("Assets","Images", "RocketPickUp.png")),"Rocket");
            Assert.AreEqual(powerUp.Shape.Position, spawn);
            powerUp.Move();
            Assert.AreNotEqual(powerUp.Shape.Position, spawn);
        }
/*         [Test]
        public void TestPowerUpOutOfBounds() {
            Vec2F spawn = new Vec2F (0.5f,0.5f);
            powerUp = new PowerUp(spawn,new Image(Path.Combine("Assets","Images", "RocketPickUp.png")),"Rocket");
            powerUps?.AddEntity(powerUp);
            Assert.AreEqual(powerUps?.CountEntities(),1);
            for (int i = 0; i < 100; i++) {
                powerUp.Move();
            }
            Assert.AreEqual(powerUps?.CountEntities(),0);
        } */
    }
}