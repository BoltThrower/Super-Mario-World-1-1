using Microsoft.Xna.Framework;

namespace SuperMario
{
    public class InvisibleBarrier
    {
        public Vector2 Position { get; set; }
        public Rectangle CollisionRectangle { get; set; }

        private Vector2 startingPosition;

        public InvisibleBarrier(Vector2 position)
        {
            UpdatePosition(position);
            startingPosition = position;
        }

        public void Update()
        {
            if (!Mario.Instance.InCoinRoom)
            {
                if (GameStateMachine.Instance.GameState.ToString() == "SuperMario.GameStates.MarioRespawnState" || GameStateMachine.Instance.GameState.ToString() == "SuperMario.GameStates.GameOverState" || GameStateMachine.Instance.GameState.ToString() == "SuperMario.GameStates.TimeUpState")
                {
                    Position = startingPosition;
                    UpdatePosition(Position);
                }
                else
                {
                    if (Mario.Instance.Position.X > CollisionRectangle.Right + GameValues.InvisibleBarrierOffset && GameStateMachine.Instance.GameState.ToString() != "SuperMario.GameStates.MarioRespawnState")
                    {
                        Position = new Vector2(Mario.Instance.Position.X - GameValues.InvisibleBarrierOffset);
                        UpdatePosition(Position);
                    }
                }
            }
        }

        private void UpdatePosition(Vector2 position)
        {
            Position = new Vector2(position.X, startingPosition.Y);
            CollisionRectangle = new Rectangle((int)position.X, (int)startingPosition.Y, GameValues.InvisibleBarrierWidth, GameValues.InvisibleBarrierHeight);
        }
    }
}
