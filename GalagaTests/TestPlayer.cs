using NUnit.Framework;
using Galaga;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Input;

namespace GalagaTests {
    
    [TestFixture]
    public class TestPlayer {
        [SetUp]
        public void Setup() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            Testplayer = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "Player.png")));

            eventBus = new GameEventBus();
            
            eventBus.InitializeEventBus(new List<GameEventType> {GameEventType.PlayerEvent, GameEventType.InputEvent});
            
            eventBus.Subscribe(GameEventType.PlayerEvent, Testplayer);
        }
        private Player Testplayer;
        private GameEventBus eventBus;
        [Test]
        public void TestPlayerMovement(){
            Vec2F originalPos = Testplayer.GetPosition();
            
            eventBus.RegisterEvent(new GameEvent {EventType = GameEventType.PlayerEvent, To = Testplayer, Message = "LEFT"});

            this.eventBus.ProcessEventsSequentially();
            
            Testplayer.Move();

            Assert.AreNotEqual(originalPos, Testplayer.GetPosition());        
        }
        [Test]
        public void TestPlayerLeft(){            
            eventBus.RegisterEvent(new GameEvent {EventType = GameEventType.PlayerEvent, To = Testplayer, Message = "LEFT"});

            this.eventBus.ProcessEventsSequentially();
            
            Testplayer.Move();

            Assert.AreEqual(0.44f, Testplayer.GetPosition().X);   
        }
        [Test]
        public void TestPlayerRight(){            
            eventBus.RegisterEvent(new GameEvent {EventType = GameEventType.PlayerEvent, To = Testplayer, Message = "RIGHT"});

            this.eventBus.ProcessEventsSequentially();
            
            Testplayer.Move();

            Assert.GreaterOrEqual(0.46f, Testplayer.GetPosition().X);   
        }
        [Test]
        public void TestPlayerUp(){            
            eventBus.RegisterEvent(new GameEvent {EventType = GameEventType.PlayerEvent, To = Testplayer, Message = "UP"});

            this.eventBus.ProcessEventsSequentially();
            
            Testplayer.Move();

            Assert.AreEqual(0.11f, Testplayer.GetPosition().Y);   
        }
        [Test]
        public void TestPlayerDown(){            
            eventBus.RegisterEvent(new GameEvent {EventType = GameEventType.PlayerEvent, To = Testplayer, Message = "DOWN"});

            this.eventBus.ProcessEventsSequentially();
            
            Testplayer.Move();

            Assert.AreEqual(0.09f, Testplayer.GetPosition().Y);   
        }
    }
}