using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperMario
{
    public class Camera
    {
        public Vector2 Position { get; set; }
        public Matrix Transform { get; set; }
        public Vector2 StartingPosition { get; set; }

        public Camera()
        {
           Position = Mario.Instance.Position;
           StartingPosition = Mario.Instance.Position;
        }

        public void Update(GameTime gameTime)
        {
            if (GameStateMachine.Instance.GameState.ToString() == "SuperMario.GameStates.MarioRespawnState" || GameStateMachine.Instance.GameState.ToString() == "SuperMario.GameStates.GameOverState" || GameStateMachine.Instance.GameState.ToString() == "SuperMario.GameStates.TimeUp")
            {
                Position = StartingPosition;
            }
            else
            {
                if (!Mario.Instance.InCoinRoom)
                {
                    if ((Mario.Instance.Position.X > Position.X && GameStateMachine.Instance.GameState.ToString() != "SuperMario.GameStates.MarioRespawn") || (Position.X > Level.Instance.Castle.Position.X))
                    {
                        Position = Mario.Instance.Position;
                        Level.Instance.InvisibleBarrier.Position = new Vector2(Position.X - GameValues.InvisibleBarrierXPositionOffset, 0);
                        Transform = Matrix.CreateTranslation(new Vector3(-(Position.X - GameValues.InvisibleBarrierXPositionOffset), 0, 0))
                                            * Matrix.CreateScale(new Vector3(GameValues.CameraZoom, GameValues.CameraZoom, 1f));
                    }
                }
                else
                {
                    Position = Level.Instance.CoinRoomPosition;
                    Transform = Matrix.CreateTranslation(new Vector3(-(Position.X), 0, 0))
                                        * Matrix.CreateScale(new Vector3(GameValues.CameraZoom, GameValues.CameraZoom, 1f));
                }
            }
        }
    }
}
