using NUnit.Framework;
using Breakout;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Input;
using System;
using System.IO;

namespace Breakout.Tests {
    [TestFixture]
    public class PlayerTests {

        private Player player;

        [SetUp]
        public void Setup() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            var shape = new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)); 
            var image = new Image(Path.Combine("Assets", "Images", "Player.png"));
            player = new Player(shape, image);
        }

        [Test]
        public void TestRender(){
            Assert.DoesNotThrow(() => player.Render());
        }

        [Test]
        public void TestMove(){
            var Position = player.GetPosition();
            player.Move();
            var newPosition = player.GetPosition();
            Assert.That(Position, Is.Not.EqualTo(newPosition));
        }
    }
}