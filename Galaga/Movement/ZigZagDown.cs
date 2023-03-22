using DIKUArcade.Entities;
using DIKUArcade.Math;
using System;

namespace Galaga.MovementStrategy{
    public class ZigZagDown : IMovementStrategy{
        public ZigZagDown(){}
        public void MoveEnemy(Enemy enemy){
            enemy.Move(new Vec2F (enemy.StartX + 0.05f * 
            (float)Math.Sin((2*Math.PI*(enemy.StartY - (enemy.GetShape().Position.Y - enemy.GetSpeed())))/0.045f),
            enemy.GetShape().Position.Y - enemy.GetSpeed()));
        }
        public void MoveEnemies(EntityContainer<Enemy> enemies){
            enemies.Iterate(MoveEnemy);
        }
    }
}