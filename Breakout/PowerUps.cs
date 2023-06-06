using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
namespace Breakout;

public class PowerUp : Entity {

    private Vec2F extent = new Vec2F (0.03f,0.03f);

    private Vec2F direction = new Vec2F (0.0f,-0.005f);

    private DynamicShape shape;
    public string Type;

    public PowerUp(Vec2F position, IBaseImage image, string type)
        : base(new DynamicShape (position, new Vec2F (0.008f,0.021f)), image) {
            this.Shape = new DynamicShape (position, extent, direction);
            this.Shape.AsDynamicShape().ChangeDirection(direction);
            this.Type = type;
        }
    
    public void Move(){
        this.Shape.Move(direction);
    }

    public void SetShape(DynamicShape input){
        shape = input;
    }
}