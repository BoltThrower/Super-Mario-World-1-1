﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario.MarioStates
{
    public class SmallLeftIdleMarioYoshi : IPlayableObjectState
    {
        private AnimatedSprite sprite;
        private IPlayableObject playableObject;

        public SmallLeftIdleMarioYoshi(IPlayableObject playableObject)
        {
            this.playableObject = playableObject;
            sprite = AnimatedSpriteFactory.Instance.BuildSmallLeftIdleMarioYoshiSprite(playableObject.Position);
            playableObject.CollisionRectangle = sprite.SpriteDestinationRectangle;
        }

        public void Update(GameTime gameTime)
        {
            sprite.UpdateSpritePosition(playableObject.Position);

            if (playableObject.Velocity.X > 0)
            {
                playableObject.PlayableObjectState = new SmallRightWalkingMarioYoshi(playableObject);
            }
            else if (playableObject.Velocity.X < 0)
            {
                playableObject.PlayableObjectState = new SmallLeftWalkingMarioYoshi(playableObject);
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!playableObject.StarPower)
            {
                sprite.Draw(spriteBatch, gameTime);
            }
            else
            {
                sprite.StarDraw(spriteBatch, gameTime);
            }
        }

        public void RightInput()
        {
            playableObject.Acceleration = new Vector2(GameValues.MarioHorizontalAcceleration, playableObject.Acceleration.Y);
        }

        public void LeftInput()
        {
            playableObject.Acceleration = new Vector2(-GameValues.MarioHorizontalAcceleration, playableObject.Acceleration.Y);
        }

        public void DownInput()
        {
            playableObject.Acceleration = Vector2.Zero;
        }

        public void RunButtonInput()
        {
            if (playableObject.Velocity.X == 0)
            {
                playableObject.PlayableObjectState = new SmallLeftTongueMarioYoshi(playableObject);
            }
        }

        public void JumpButtonInput()
        {
            playableObject.Acceleration = new Vector2(playableObject.Acceleration.X, GameValues.MarioVerticalAcceleration);
            playableObject.PlayableObjectState = new SmallLeftJumpingMarioYoshi(playableObject);
        }

        public void NoInput()
        {

        }

        public void TakeDamage()
        {
            playableObject.PlayableObjectState = new SmallLeftIdleMario(playableObject);
        }

        public void PickUpPowerup()
        {
            playableObject.Position = new Vector2(playableObject.Position.X, playableObject.Position.Y - 6);
            playableObject.PlayableObjectState = new BigLeftIdleMarioYoshi(playableObject);
        }

        public void PickUpStar()
        {
            playableObject.StarPower = true;
        }
    }
}
