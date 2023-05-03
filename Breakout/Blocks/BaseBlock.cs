using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.IO;
using DIKUArcade.Math;
using DIKUArcade.Events;
using Breakout;

namespace Breakout.Blocks{
    public class BaseBlock :  Entity{
        public BaseBlock(DynamicShape shape, IBaseImage image)
        : base(shape, image) {
            this.shape = shape;
            this.Image = image;
            this.HP = 1;
        }
        public Entity entity {get;}
        public DynamicShape shape {get;}

        public int HP {get;set;}
        public void Hit(){
            HP--;
            if (HP == 0){
                this.DeleteEntity();
            }
        }
    }
}