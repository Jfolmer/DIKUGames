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
    public class PointTests {

        [SetUp]
        public void Setup() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
        }
        [Test]
        public void TestInitPoints(){
            Points.ResetTally();
            Assert.AreEqual(Points.GetTally(),0);
        }
        [Test]
        public void TestIncreasePoints(){
            Points.ResetTally();
            Assert.AreEqual(Points.GetTally(),0);
            Points.IncreaseTally();
            Assert.AreEqual(Points.GetTally(),1);
            Points.IncreaseTally();
            Assert.AreEqual(Points.GetTally(),2);
        }
        [Test]
        public void TestResetPoints(){
            Points.ResetTally();
            Points.IncreaseTally();
            Assert.AreNotEqual(Points.GetTally(),0);
            Points.ResetTally();
            Assert.AreEqual(Points.GetTally(),0);
            Points.IncreaseTally();
            Assert.AreEqual(Points.GetTally(),1);
        }
    }
}