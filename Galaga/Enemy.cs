using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.IO;
namespace Galaga;
public class Enemy : Entity {
    public Enemy(DynamicShape shape, IBaseImage image, int startHP)
        : base(shape, image) {
            this.shape = shape;
            this.HP = startHP;
        }
    
    private DynamicShape shape;

    private int HP;

    private int Speed;

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

    public int GetSpeed(){
        return this.Speed;
    }
}