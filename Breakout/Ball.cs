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
        /// <summary> Render the ball </summary>
        /// <param> Null </param>
        /// <returns> void </returns>
        public void Render() {
            entity.RenderEntity();
        }
        /// <summary> update the ball direction </summary>
        /// <param> float xDirection, float yDirection </param>
        /// <returns> void </returns>
        public void UpdateDirection(float xDirection, float yDirection) {
            (shape.AsDynamicShape()).ChangeDirection(new Vec2F(xDirection, yDirection));
        }
        /// <summary> Small adjustment angle to the ball's direction </summary>
        /// <param> float input </param>
        /// <returns> float </returns>
        public float AngleRandomizer(float input){
            Random rnd = new Random();
            return (input + rnd.Next(-1,1) * 0.001f);
        }
        /// <summary> ensures the ball is only launched when we want it to be launched </summary>
        /// <param> Vec2F direction </param>
        /// <returns> void </returns>
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
        /// <summary> Move the ball, within our borders, besides the bottom </summary>
        /// <param> Null </param>
        /// <returns> void </returns>
        public void Move() {
            if (launched) {
                Vec2F prepos = this.shape.Position;
                shape.Move(shape.Direction);
                if (shape.Position.Y + shape.Extent.Y > 1.0f) { // toppen
                    this.shape.Direction = new Vec2F(AngleRandomizer(shape.Direction.X), -AngleRandomizer(shape.Direction.Y));
                    this.shape.Position.Y = 0.9999f - shape.Extent.Y;
                } else if (shape.Position.X <= 0.0f) { // venstre
                    this.shape.Direction = new Vec2F(-AngleRandomizer(shape.Direction.X), AngleRandomizer(shape.Direction.Y));
                    this.shape.Position.X = 0.0001f;
                }
                else if (shape.Position.X + shape.Extent.X >= 1.0f) { // højre
                    this.shape.Direction = new Vec2F(-AngleRandomizer(shape.Direction.X), AngleRandomizer(shape.Direction.Y));
                    this.shape.Position.X = 0.9999f - shape.Extent.X;
                } else if (shape.Position.Y + shape.Extent.Y < 0.0f) { // bunden
                    this.DeleteEntity();
                }
                float length = (float)System.Math.Sqrt(shape.Direction.X * shape.Direction.X + shape.Direction.Y * shape.Direction.Y);
                this.shape.Direction = shape.Direction / length * speed;
            }
        }
    }
}