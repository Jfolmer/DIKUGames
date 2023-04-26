using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.IO;
using DIKUArcade.Math;
using DIKUArcade.Events;
using Breakout;

namespace Breakout.Blocks{
    public class BaseBlock :  Entity, Block{
        public BaseBlock(DynamicShape shape, IBaseImage image, int startHP)
        : base(shape, image) {
            this.shape = shape;
            this.Image = image;
            this.HP = 2;
            this.X = shape.Position.X;
            this.Y = shape.Position.Y;
        }
        public Entity entity {get;}
        public DynamicShape shape {get;}
        public float Y {get;}
        public float X {get;}
        public int HP {get;set;}
        public void Hit(){
            HP--;
            if (HP == 0){
                entity.DeleteEntity();
            }
        }
    }
}