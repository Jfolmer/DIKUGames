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
        public DynamicShape shape;
        public const float speed = 0.02f;
        public bool launched;

        public Ball(DynamicShape shape, IBaseImage image) : base(shape, image) {
            this.shape = shape;
            this.Image = image;
            this.launched = false;
            this.entity = this;
        }
        public void Render() {
            entity.RenderEntity();
        }
        public void UpdateDirection(float xDirection, float yDirection) {
            (shape.AsDynamicShape()).ChangeDirection(new Vec2F(xDirection, yDirection));
        }
        public float AngleRandomizer(float input){
            Random rnd = new Random();
            return (input + rnd.Next(-1,1) * 0.001f);
        }
        public void Launch(Vec2F direction) {
            if (!launched) {
                if (direction.Y < 0.0f) {
                    direction = new Vec2F(direction.X, -direction.Y);
                }
                this.shape.Direction = direction;
                float length = (float)System.Math.Sqrt(direction.X * direction.X + direction.Y * direction.Y);
                this.shape.Direction = direction / length * speed;
                this.launched = true;
            }
        }
        public void Move() {
            if (launched) {
                if (shape.Position.Y + shape.Extent.Y > 1.0f) { // toppen
                    this.shape.Direction = new Vec2F(AngleRandomizer(shape.Direction.X), -AngleRandomizer(shape.Direction.Y));
                } else if (shape.Position.X < 0.0f || shape.Position.X + shape.Extent.X > 1.0f) { // venstre
                    this.shape.Direction = new Vec2F(-AngleRandomizer(shape.Direction.X), AngleRandomizer(shape.Direction.Y));
                } else if (shape.Position.Y + shape.Extent.Y < 0.0f) { // bunden
                    this.DeleteEntity();
                }
                float length = (float)System.Math.Sqrt(shape.Direction.X * shape.Direction.X + shape.Direction.Y * shape.Direction.Y);
                this.shape.Direction = shape.Direction / length * speed;
                shape.Move(shape.Direction);
            }
        }
    }
}