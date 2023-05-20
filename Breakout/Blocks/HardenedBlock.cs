using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System;

namespace Breakout.Blocks{
    public class HardenedBlock :  BaseBlock{
        public HardenedBlock(DynamicShape shape, IBaseImage image, bool powerUp)
        : base(shape, image, powerUp) {
            this.shape = shape;
            this.Image = image;
            this.HP = 2;
            this.BlockType = "Hardened";
            this.PowerUp = powerUp;
        }
        public override Entity entity {get;}
        public override DynamicShape shape {get;}
        public override int HP {get;set;}
        public override string BlockType {get;}
        public override bool PowerUp {get;set;}
        public string ImagePath;
        public override void Hit(){
            HP--;
            if (HP == 1){
                ImagePath = ImagePath.Replace(".png","-damaged.png");
                this.Image = new Image(ImagePath);
            }
            else if (HP == 0){
                this.DeleteEntity();
            }
        }
    }
}