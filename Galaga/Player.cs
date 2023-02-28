using DIKUArcade.Entities;

using DIKUArcade.Graphics;

using DIKUArcade.Math;

namespace Galaga {

    public class Player {
    
        private Entity entity;

        private DynamicShape shape;

        private float moveLeft = 0.0f;

        private float moveRight = 0.0f;

        private float moveUp = 0.0f;

        private float moveDown = 0.0f;

        private const float MOVEMENT_SPEED = 0.01f;

        public Player(DynamicShape shape, IBaseImage image) {
            entity = new Entity(shape, image);
            this.shape = shape;
        }
        
        public void Render() {
            entity.RenderEntity();
        }
        
        public void Move() {
            if (0.0f > shape.Position.X + shape.Direction.X ||
            shape.Position.X + shape.Extent.X + shape.Direction.X > 1.0f || 0.0f > shape.Position.Y + shape.Direction.Y
            || shape.Position.Y + shape.Extent.Y + shape.Direction.Y > 0.9f){
                return;
            }
            shape.Move();
        }

        public void SetMoveLeft(bool val) {
            if (val){
                moveLeft = moveLeft - MOVEMENT_SPEED;
                UpdateDirection();
            }
            else{
                moveLeft = 0.0f;
                UpdateDirection();
            }
        }

        public void SetMoveRight(bool val) {
            if (val){
                moveRight = moveRight + MOVEMENT_SPEED;
                UpdateDirection();
            }
            else{
                moveRight = 0.0f;
                UpdateDirection();
            }
        }

        public void SetMoveUp(bool val) {
            if (val){
                moveUp = moveUp + MOVEMENT_SPEED;
                UpdateDirection();
            }
            else{
                moveUp = 0.0f;
                UpdateDirection();
            }
        }

        public void SetMoveDown(bool val) {
            if (val){
                moveDown = moveDown - MOVEMENT_SPEED;
                UpdateDirection();
            }
            else{
                moveDown = 0.0f;
                UpdateDirection();
            }
        }

        private void UpdateDirection(){
            float sumX = moveRight + moveLeft ;
            float sumY = moveUp + moveDown;
            Vec2F vec = new Vec2F (sumX,sumY);
            shape.ChangeDirection(vec);
        }
        
            public DynamicShape GetShape(){
            return shape;
        }
    
        public void SetShape(DynamicShape input){
            shape = input;
        }
    }
}
