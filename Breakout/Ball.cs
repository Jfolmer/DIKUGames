using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Physics;
using System;
using Breakout.Blocks;
using System.IO;
using System.Collections.Generic;


namespace Breakout {
    public class Ball : Entity {
        private Entity entity;
        public DynamicShape ballshape;
        public Vec2F velocity;
        public const float speed = 0.04f;
        public bool launched;

        public Ball(DynamicShape shape, IBaseImage image) : base(shape, image) {
            this.ballshape = shape;
            this.Image = new Image(Path.Combine("Assets", "Images", "ball.png"));
            this.velocity = new Vec2F(0.012f, 0.012f);
            this.launched = true;
            this.entity = this;
        }
        public void Render() {
            entity.RenderEntity();
        }
        public void UpdateDirection(float xDirection, float yDirection) {
            (ballshape.AsDynamicShape()).ChangeDirection(new Vec2F(xDirection, yDirection));
        }

        public void Launch(Vec2F direction) {
            if (!launched) {
                if (direction.Y < 0.0f) {
                    direction = new Vec2F(direction.X, -direction.Y);
                }
                float length = (float)System.Math.Sqrt(direction.X * direction.X + direction.Y * direction.Y);
                this.velocity = direction / length * speed;
                this.launched = true;
            }
        }
        public void Move() {
            if (launched) {
                ballshape.Move(velocity);

                if (ballshape.Position.Y + ballshape.Extent.Y > 1.0f) { // toppen
                    float overlap = ballshape.Position.Y + ballshape.Extent.Y - 1.0f;
                    ballshape.Position = new Vec2F(ballshape.Position.X, 1.0f - ballshape.Extent.Y - overlap);
                    velocity = new Vec2F(velocity.X, -velocity.Y);

                } else if (ballshape.Position.X < 0.0f) { // venstre
                    ballshape.Position = new Vec2F(0.0f, ballshape.Position.Y);
                    velocity = new Vec2F(-velocity.X, velocity.Y);
                
                } else if (ballshape.Position.X + ballshape.Extent.X > 1.0f) { // højre
                    ballshape.Position = new Vec2F(1.0f - ballshape.Extent.X, ballshape.Position.Y);
                    velocity = new Vec2F(-velocity.X, velocity.Y);

                } else if (ballshape.Position.Y + ballshape.Extent.Y < 0.0f) { // bunden
                    this.DeleteEntity();
                }
            }
        }
    }
}