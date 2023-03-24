using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;


namespace Galaga {

    public class Player: IGameEventProcessor {
    
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

            GalagaBus.GetBus().Subscribe(GameEventType.PlayerEvent, this);

        }

        public void DeletePlayer(){
            entity.Shape.Position = new Vec2F (0.0f,0.0f);
            entity.Shape.Extent = new Vec2F (0.0f,0.0f);
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

        private void SetMoveLeft(bool val) {
            if (val){
                moveLeft = moveLeft - MOVEMENT_SPEED;
                UpdateDirection();
            }
            else{
                moveLeft = 0.0f;
                UpdateDirection();
            }
        }

        private void SetMoveRight(bool val) {
            if (val){
                moveRight = moveRight + MOVEMENT_SPEED;
                UpdateDirection();
            }
            else{
                moveRight = 0.0f;
                UpdateDirection();
            }
        }

        private void SetMoveUp(bool val) {
            if (val){
                moveUp = moveUp + MOVEMENT_SPEED;
                UpdateDirection();
            }
            else{
                moveUp = 0.0f;
                UpdateDirection();
            }
        }

        private void SetMoveDown(bool val) {
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

        public Vec2F GetPosition(){
            return shape.Position;
        }

        public void ProcessEvent(GameEvent gameEvent) {
            switch (gameEvent.Message){

                case "LEFT":
                    this.SetMoveLeft(true);
                    break;

                case "NOTLEFT":
                    this.SetMoveLeft(false);
                    break;
                
                case "RIGHT":
                    this.SetMoveRight(true);
                    break;

                case "NOTRIGHT":
                    this.SetMoveRight(false);
                    break;

                case "UP":
                    this.SetMoveUp(true);
                    break;

                case "NOTUP":
                    this.SetMoveUp(false);
                    break;

                case "DOWN":
                    this.SetMoveDown(true);
                    break;

                case "NOTDOWN":
                    this.SetMoveDown(false);
                    break;
                
                default:
                    break;
                

            }
        }
    }
}
