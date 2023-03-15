using DIKUArcade.Entities;
using DIKUArcade.Math;

namespace Galaga.MovementStrategy{
    public class Down : IMovementStrategy{
        public Down(){}
        public void MoveEnemy(Enemy enemy){
            enemy.GetShape().ChangeDirection(new Vec2F (0.0f, -enemy.GetSpeed()));
            enemy.GetShape().Move();
            /* if (enemy.GetShape().Position.Y < 0.2f){
                enemy.DeleteEntity();
            } */
        }
        public void MoveEnemies(EntityContainer<Enemy> enemies){
            enemies.Iterate(MoveEnemy);
        }
    }
}