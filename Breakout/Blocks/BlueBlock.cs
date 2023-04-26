using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.IO;
using DIKUArcade.Math;
using DIKUArcade.Events;
using Breakout;

namespace Breakout.Blocks{
    public class BlueBlock : Entity, Block{
        public BlueBlock(DynamicShape shape, IBaseImage image, int startHP)
        : base(shape, image) {
            this.shape = shape;
            this.Image = new Image(Path.Combine("Assets", "Images", "blue-block.png"));
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
            if (HP == 1){
                this.Image = new Image(Path.Combine("Assets", "Images", "blue-block-damaged.png"));
            }
            else if (HP == 0){
                entity.DeleteEntity();
            }
        }
    }
}