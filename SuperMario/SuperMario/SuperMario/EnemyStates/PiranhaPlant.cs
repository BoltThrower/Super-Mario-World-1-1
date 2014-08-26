using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario.EnemyStates
{
    class PiranhaPlant : IEnemyState
    {
        private AnimatedSprite sprite;
        private Vector2 startingPosition, updatePosition;
        private int pauseTime;
        private bool raisePlant;
        private Enemy parent;

        public Vector2 Velocity { get; set; }
        public Rectangle CollisionRectangle { get; set; }
        public int ScoreValue { get; set; }
        public ScoreSprite ScoreSprite { get; set; }

        public PiranhaPlant(Vector2 position, Enemy parent)
        {
            this.parent = parent;
            startingPosition = new Vector2(position.X - GameValues.LevelCellSize / 2, position.Y + GameValues.LevelCellSize / 4);
            updatePosition = startingPosition;
            parent.Position = startingPosition;
            ScoreValue = GameValues.PiranhaPlantScoreValue;
            sprite = AnimatedSpriteFactory.Instance.BuildEnemyPiranhaPlant(position);
            CollisionRectangle = sprite.SpriteDestinationRectangle;
        }

        public void Update(GameTime gameTime, Vector2 position)
        {
            if (gameTime.TotalGameTime.Milliseconds % GameValues.PiranhaPlantUpdateDelay == 0)
            {

                if (updatePosition.Y < (startingPosition.Y + sprite.Texture.Height) && raisePlant == true)
                {
                    updatePosition.Y += 1;
                }
                else if (updatePosition.Y == (startingPosition.Y + sprite.Texture.Height) && pauseTime < GameValues.PiranhaPlantMaxRaisedTime)
                {
                    pauseTime++;
                    if (raisePlant == true)
                    {
                        raisePlant = false;
                    }
                }
                else if (updatePosition.Y > startingPosition.Y && raisePlant == false)
                {
                    pauseTime = 0;
                    updatePosition.Y -= 1;
                }
                else if (updatePosition.Y == startingPosition.Y && pauseTime < GameValues.PiranhaPlantMaxLoweredTime)
                {
                    pauseTime++;
                }
                else if (updatePosition.Y == startingPosition.Y && raisePlant == false)
                {
                    pauseTime = 0;
                    raisePlant = true;
                }
                sprite.AdvanceFrame();
                sprite.UpdateSpritePosition(updatePosition);

            }
            parent.CollisionRectangle = CollisionRectangle;
            parent.Acceleration = Vector2.Zero;
            parent.Velocity = Vector2.Zero;
            parent.MaxVelocity = Vector2.Zero;
            parent.Position = updatePosition;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            sprite.Draw(spriteBatch, gameTime);
        }
    }
}
