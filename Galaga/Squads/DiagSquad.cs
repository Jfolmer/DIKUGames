using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using DIKUArcade.Math;

namespace Galaga.Squadron{

public class DiagSquad : ISquadron{

    public EntityContainer<Enemy> Enemies {get;}

    public int MaxEnemies {get;} = 8;

    public void CreateEnemies (List<Image> enemyStride, List<Image> alternativeEnemyStride){
        for (int i = 0; i < MaxEnemies/4; i++) {
            for (int a = 0; a < MaxEnemies/2; a++){
                if (i == 0 || i == 2){
                    Enemies.AddEntity(new Enemy(
                        new DynamicShape(new Vec2F(0.1f + (float)i * 0.1f + (float)a*0.1f, 0.8f - (float)a*0.1f), new Vec2F(0.1f, 0.1f)),
                        new ImageStride(80, enemyStride),
                        3));
                }
                else{
                    Enemies.AddEntity(new Enemy(
                        new DynamicShape(new Vec2F(0.1f + (float)i * 0.1f + (float)a*0.1f, 0.9f - (float)a*0.1f), new Vec2F(0.1f, 0.1f)),
                        new ImageStride(80, enemyStride),
                        3));
                }
            }
        }
    }

    public DiagSquad() {
        Enemies = new EntityContainer<Enemy>(MaxEnemies);
    }

    }

    

}