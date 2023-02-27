using DIKUArcade.Entities;

using DIKUArcade.Graphics;

using DIKUArcade.Math;

namespace Galaga {

    public class Player {
    
        private Entity entity;

        private DynamicShape shape;

        private float moveLeft = 0.0f;

        private float moveRight = 0.0f;

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
            shape.Position.X + shape.Extent.X + shape.Direction.X > 1.0f){
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

        private void UpdateDirection(){
            float sum = moveRight + moveLeft;
            Vec2F vec = new Vec2F (sum,0.0f);
            shape.ChangeDirection(vec);
        }
        

    }
}
