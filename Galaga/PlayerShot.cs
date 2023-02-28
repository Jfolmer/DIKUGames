using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
namespace Galaga;

public class PlayerShot : Entity {

    private Vec2F extent = new Vec2F (0.008f,0.021f);

    private Vec2F direction = new Vec2F (0.0f,0.1f);

    private DynamicShape shape;

    public PlayerShot(Vec2F position, IBaseImage image)
        : base(new DynamicShape (position, new Vec2F (0.008f,0.021f)), image) {
            this.shape = new DynamicShape (position, new Vec2F (0.008f,0.021f));
        }
    
    public void Move(){
        shape.Position += direction; 
    }

    public DynamicShape GetShape(){
        return shape;
    }
    
    public void SetShape(DynamicShape input){
        shape = input;
    }

}