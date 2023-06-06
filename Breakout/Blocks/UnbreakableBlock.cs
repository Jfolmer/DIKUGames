using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.Blocks{
    public class UnbreakableBlock :  BaseBlock{
        public UnbreakableBlock(DynamicShape shape, IBaseImage image, bool powerUp)
        : base(shape, image, powerUp) {
            this.shape = shape;
            this.Image = image;
            this.BlockType = "Unbreakable";
        }
        public override Entity entity {get;}
        public override DynamicShape shape {get;}
        public override string BlockType {get;}

        /// <summary>
        ///  Does noooothing, but is needed due to interface
        /// </summary>
        /// <param>
        ///  Null
        /// </param>
        /// <returns>
        /// Void
        /// </returns>
        public override void Hit(){}
    }
}