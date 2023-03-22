using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using DIKUArcade.Math;

namespace Galaga.Squadron{

public class ArrowSquad : ISquadron{

    public EntityContainer<Enemy> Enemies {get;}

    public int MaxEnemies {get;} = 8;

    public void CreateEnemies (List<Image> enemyStride, List<Image> alternativeEnemyStride){
        for (int i = 0; i < MaxEnemies; i++) {
            if (i <= 2){
                Enemies.AddEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.2f + (float)i * 0.1f, 0.9f - (float)i * 0.1f), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStride),
                    3));
            }
            if (i > 2 && i <= 4){
                Enemies.AddEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.2f + (float)i * 0.1f, 0.5f + (float)i * 0.1f), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStride),
                    3));
            }
            if (i == 5){
                Enemies.AddEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.3f, 0.9f), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStride),
                    3));
            }
            if (i == 6){
                Enemies.AddEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.5f, 0.9f), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStride),
                    3));
            }
            if (i== 7){
                Enemies.AddEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.4f, 0.8f), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStride),
                    3));
            }

        }
    }

    public ArrowSquad() {
        Enemies = new EntityContainer<Enemy>(MaxEnemies);
    }

    }

    

}