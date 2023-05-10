using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.Blocks{
    public class BaseBlock : Entity{
        public BaseBlock(DynamicShape shape, IBaseImage image)
        : base(shape, image) {
            this.shape = shape;
            this.Image = image;
            this.HP = 1;
            this.BlockType = "BaseBlock";
        }
        public virtual Entity entity {get;}
        public virtual DynamicShape shape {get;}
        public virtual int HP {get;set;}
        public virtual string BlockType {get;}
        public virtual bool PowerUp {get;set;}
        public virtual int GetHP(){
            return HP;
        }
        public virtual void Hit(){
            HP--;
            if (HP == 0){
                this.DeleteEntity();
            }
        }
    }
}