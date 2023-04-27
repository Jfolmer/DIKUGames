using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using System.IO;


namespace Breakout{
    public class Ball:Entity{

        public Ball(DynamicShape shape, IBaseImage image):base(shape,image){
            this.shape = shape;
            this.Image = new Image(Path.Combine("Assets", "Images", "ball.png"));
        }

        private DynamicShape shape;
        
    }
}