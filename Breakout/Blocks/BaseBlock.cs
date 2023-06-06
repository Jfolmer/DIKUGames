using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.Blocks{
    public class BaseBlock : Entity{
        public BaseBlock(DynamicShape shape, IBaseImage image, bool powerUp)
        : base(shape, image) {
            this.shape = shape;
            this.Image = image;
            this.HP = 1;
            this.BlockType = "BaseBlock";
            this.PowerUp = powerUp;
        }
        public virtual Entity entity {get;}
        public virtual DynamicShape shape {get;}
        public virtual int HP {get;set;}
        public virtual string BlockType {get;}
        public virtual bool PowerUp {get;set;}

        /// <summary>
        /// Returns the local HP field
        /// </summary>
        /// <param>
        ///  Null
        /// </param>
        /// <returns>
        /// An integer
        /// </returns>
        public virtual int GetHP(){
            return HP;
        }

        /// <summary>
        /// Damages the block and deletes the block if relavant
        /// </summary>
        /// <param>
        ///  A GameEvent
        /// </param>
        /// <returns>
        /// Void
        /// </returns>
        public virtual void Hit(){
            HP--;
            if (HP == 0){
                this.DeleteEntity();
            }
        }
    }
}