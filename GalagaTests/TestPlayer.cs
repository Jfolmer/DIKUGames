using NUnit.Framework;
using Galaga;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Input;
using Galaga;

namespace GalagaTests {
    
    [TestFixture]
    public class TestPlayer {
        [SetUp]
        public void Setup() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            Testplayer = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "Player.png")));

            GalagaBus.GetBus().Subscribe(GameEventType.PlayerEvent, Testplayer);
        }
        private Player Testplayer;
        [Test]
        public void TestPlayerMovement(){
            Vec2F originalPos = Testplayer.GetPosition();
            
            GalagaBus.GetBus().RegisterEvent(new GameEvent {EventType = GameEventType.PlayerEvent, To = Testplayer, Message = "LEFT"});

            GalagaBus.GetBus().ProcessEventsSequentially();
            
            Testplayer.Move();

            Assert.AreNotEqual(originalPos, Testplayer.GetPosition());        
        }
        [Test]
        public void TestPlayerLeft(){            
            GalagaBus.GetBus().RegisterEvent(new GameEvent {EventType = GameEventType.PlayerEvent, To = Testplayer, Message = "LEFT"});

            GalagaBus.GetBus().ProcessEventsSequentially();
            
            Testplayer.Move();

            Assert.LessOrEqual(0.44f, Testplayer.GetPosition().X);   
        }
        [Test]
        public void TestPlayerRight(){            
            GalagaBus.GetBus().RegisterEvent(new GameEvent {EventType = GameEventType.PlayerEvent, To = Testplayer, Message = "RIGHT"});

            GalagaBus.GetBus().ProcessEventsSequentially();
            
            Testplayer.Move();

            Assert.GreaterOrEqual(0.46f, Testplayer.GetPosition().X);   
        }
    }
}