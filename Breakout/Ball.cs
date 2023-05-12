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
        private DynamicShape shape;
        private Vec2F velocity;
        private const float speed = 0.04f;
        private bool launched;
        public Ball(DynamicShape shape, IBaseImage image) : base(shape, image) {
            this.shape = shape;
            this.Image = new Image(Path.Combine("Assets", "Images", "ball.png"));
            this.velocity = new Vec2F(0.012f, 0.012f);
            this.launched = true;
            this.entity = this;
        }
        public void Render() {
            entity.RenderEntity();
        }
        public void UpdateDirection(float xDirection, float yDirection) {
            (shape.AsDynamicShape()).ChangeDirection(new Vec2F(xDirection, yDirection));
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
                shape.Move(velocity);

                if (shape.Position.Y + shape.Extent.Y > 1.0f) { // toppen
                    float overlap = shape.Position.Y + shape.Extent.Y - 1.0f;
                    shape.Position = new Vec2F(shape.Position.X, 1.0f - shape.Extent.Y - overlap);
                    velocity = new Vec2F(velocity.X, -velocity.Y);

                } else if (shape.Position.X < 0.0f) { // venstre
                    shape.Position = new Vec2F(0.0f, shape.Position.Y);
                    velocity = new Vec2F(-velocity.X, velocity.Y);

                } else if (shape.Position.X + shape.Extent.X > 1.0f) { // højre
                    shape.Position = new Vec2F(1.0f - shape.Extent.X, shape.Position.Y);
                    velocity = new Vec2F(-velocity.X, velocity.Y);

/*                 } else if (CollisionDetection.Aabb(shape.AsDynamicShape(), other.AsDynamicShape())) { // player
                    velocity = new Vec2F(velocity.X, velocity.Y); */

                } else if (shape.Position.Y + shape.Extent.Y < 0.0f) { // bunden
                    this.DeleteEntity();
                }
            }
        }
    }
}