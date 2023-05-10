using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.Blocks{
    public class UnbreakableBlock :  BaseBlock{
        public UnbreakableBlock(DynamicShape shape, IBaseImage image)
        : base(shape, image) {
            this.shape = shape;
            this.Image = image;
            this.BlockType = "Unbreakable";
        }
        public override Entity entity {get;}
        public override DynamicShape shape {get;}
        public override string BlockType {get;}
        public override void Hit(){}
    }
}