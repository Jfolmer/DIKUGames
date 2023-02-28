using DIKUArcade.Entities;
using DIKUArcade.Graphics;
namespace Galaga;
public class Enemy : Entity {
    public Enemy(DynamicShape shape, IBaseImage image)
        : base(shape, image) {
            this.shape = shape;
        }
    
    private DynamicShape shape;

    public void SetShape(DynamicShape input){
        shape = input;
    }
}