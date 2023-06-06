using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
namespace Breakout;

public class RocketShot : Entity {

    private Vec2F extent = new Vec2F (0.05f,0.04f);

    private Vec2F direction = new Vec2F (0.0f,0.05f);

    private DynamicShape shape;

    public RocketShot(Vec2F position, IBaseImage image)
        : base(new DynamicShape (position, new Vec2F (0.05f,0.04f)), image) {
            this.shape = new DynamicShape (position, extent, direction);
            this.Shape.AsDynamicShape().ChangeDirection(direction);
        }
    /// <summary> moves the RocketShot </summary>
    /// <param> Null </param>
    /// <returns> void </returns>
    public void Move(){
        this.Shape.Move(direction);
    }
    /// <summary> Get the shape of RocketShot </summary>
    /// <param> Null </param>
    /// <returns> DynamicShape </returns>
    public void SetShape(DynamicShape input){
        shape = input;
    }

}