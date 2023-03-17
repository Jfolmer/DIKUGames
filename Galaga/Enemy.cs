using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.IO;
using DIKUArcade.Math;
namespace Galaga;
public class Enemy : Entity {
    public Enemy(DynamicShape shape, IBaseImage image, int startHP)
        : base(shape, image) {
            this.shape = shape;
            this.HP = startHP;
            this.StartX = shape.Position.X;
            this.StartY = shape.Position.Y;
        }
    
    private DynamicShape shape;

    public float StartY;

    public float StartX;


    private int HP;

    private float Speed = 0.003f;

    public void Move(Vec2F place){
        shape.Position = place;
    }
    public DynamicShape GetShape(){
        return shape;
    }
    public void SetShape(DynamicShape input){
        shape = input;
    }

    public void Enrage(){
        this.Image = new ImageStride(80, ImageStride.CreateStrides(2, Path.Combine("Assets",
            "Images", "RedMonster.png")));
        this.Speed += Speed;
    }
    public void Hit(){
        HP--;
    }

    public int GetHP(){
        return this.HP;
    }

    public float GetSpeed(){
        return this.Speed;
    }

    public void IncreaseSpeed(){
        this.Speed += 0.003f;
    }
}