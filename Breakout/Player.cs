using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;


namespace Breakout {

    public class Player: IGameEventProcessor {
    
        private Entity entity;

        private DynamicShape shape;
        private float moveLeft = 0.0f;
        private float moveRight = 0.0f;
        public bool Rocket;
        private float MOVEMENT_SPEED = 0.015f;
        public Player(DynamicShape shape, IBaseImage image) {
            entity = new Entity(shape, image);
            this.shape = shape;
            this.Rocket = false;
            BreakoutBus.GetBus().Subscribe(GameEventType.PlayerEvent, this);
        }
        /// <summary> Render the player </summary>
        /// <param> Null </param>
        /// <returns> void </returns>
        public void Render() {
            entity.RenderEntity();
        }
        /// <summary> Move the player in x direction without our borders </summary>
        /// <param> Null</param>
        /// <returns> void </returns>
        public void Move() {
            if (0.0f > shape.Position.X + shape.Direction.X){ // venstre
                shape.Position.X = 0.0001f;
            }
            else if (shape.Position.X + shape.Extent.X + shape.Direction.X > 1.0f){ //højre
                shape.Position.X = 0.9999f - shape.Extent.X;
            }
            else {
                shape.Move();
            }
        }
        /// <summary> Change the speed of the player </summary>
        /// <param> float input </param>
        /// <returns> void </returns>
        public void ChangeSpeed(float input){
            MOVEMENT_SPEED = input;
        }
        /// <summary> Get the movement speed of the player </summary>
        /// <param> Null </param>
        /// <returns> float </returns>
        public float GetSpeed(){
            return MOVEMENT_SPEED;
        }
        /// <summary> Makes it possible to move the player left </summary>
        /// <param> bool val </param>
        /// <returns> void </returns>
        private void SetMoveLeft(bool val) {
            if (val){
                moveLeft = moveLeft - MOVEMENT_SPEED;
            }
            else{
                moveLeft = 0.0f;
            }
            UpdateDirection();
        }
        /// <summary> Makes it possible to move the player right </summary>
        /// <param> bool val </param>
        /// <returns> void </returns>
        private void SetMoveRight(bool val) {
            if (val){
                moveRight = moveRight + MOVEMENT_SPEED;
            }
            else{
                moveRight = 0.0f;
            }
            UpdateDirection();
        }
        /// <summary> Update the direction of the player </summary>
        /// <param> Null </param>
        /// <returns> void </returns>
        private void UpdateDirection(){
            float sumX = moveRight + moveLeft ;
            Vec2F vec = new Vec2F (sumX,0.0f);
            shape.ChangeDirection(vec);
        }
        /// <summary> Gets player shape </summary>
        /// <param> Null </param>
        /// <returns> DynamicShape </returns>
        public DynamicShape GetShape(){
            return shape;
        }
        /// <summary> Gets player position </summary>
        /// <param> Null </param>
        /// <returns> Vec2F </returns>
        public Vec2F GetPosition(){
            return shape.Position;
        }
        /// <summary> Proccess different events from player move </summary>
        /// <param> GameEvent gameEvent </param>
        /// <returns> void </returns>
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
                default:
                    break;
            }
        }
    }
}