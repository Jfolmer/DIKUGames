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

        private Player? player;

        [SetUp]
        public void Setup() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            var shape = new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.2f)); 
            var image = new Image(Path.Combine("Assets", "Images", "Player.png"));
            player = new Player(shape, image);
        }

        [Test]
        public void TestRender(){
            Assert.DoesNotThrow(() => player?.Render());
        }

        [Test]
        public void TestMove(){
            var Position = player?.GetPosition();
            player?.GetShape().ChangeDirection(new Vec2F(0.30f, 0.0f));
            player?.Move();
            Assert.AreNotEqual(player?.GetPosition(), Position);
        }
        
        [Test]
        public void TestMoveAtBorder(){
            var Position = player?.GetPosition();
            player?.GetShape().ChangeDirection(new Vec2F(1.30f, 0.0f));
            player?.Move();
            Assert.AreEqual(player?.GetPosition(), Position);
        }

        [Test]
        public void TestRectangular(){
            Assert.AreNotEqual(player?.GetShape().Extent.Y, player?.GetShape().Extent.X);
        }
    }
}