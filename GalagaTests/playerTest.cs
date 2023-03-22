using NUnit.Framework;
using System;
using Galaga.Player;
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
using Galaga.Squadron;
using Galaga.MovementStrategy;
using Galaga.Player;
using galagaTests;

namespace galagaTests{
    [TestFixture]
    public class PlayerTest {

        [SetUp]
        private Player player;
        private GameEventBus eventBus;
        public void Setup() {
            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "Player.png")));

            eventBus = new GameEventBus();
            
            eventBus.InitializeEventBus(new List<GameEventType> {GameEventType.PlayerEvent, GameEventType.InputEvent});
            
            eventBus.Subscribe(GameEventType.PlayerEvent, player);
        }
        [Test]
        public void TestPlayerMovement(){
            Vec2F originalPos = player.GetPosition();
            
            eventBus.RegisterEvent(new GameEvent {EventType = GameEventType.PlayerEvent, To = player, Message = "LEFT"});

            this.eventBus.ProcessEventsSequentially();
            
            player.move();

            Assert.AreNotEqual(originalPos, player.GetPosition());
            
            
        }

    }
}