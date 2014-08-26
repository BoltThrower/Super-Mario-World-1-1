﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario.MarioStates
{
    class SmallLeftWalkingMario : IPlayableObjectState
    {
        private AnimatedSprite sprite;
        private IPlayableObject playableObject;

        public SmallLeftWalkingMario(IPlayableObject playableObject)
        {
            this.playableObject = playableObject;
            sprite = AnimatedSpriteFactory.Instance.BuildSmallLeftWalkingMarioSprite();
            this.playableObject.CollisionRectangle = sprite.SpriteDestinationRectangle;
        }

        public void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.Milliseconds % GameValues.MarioStateWalkingUpdateDelay == 0)
            {
                sprite.AdvanceFrame();
            }
            sprite.UpdateSpritePosition(playableObject.Position);

            if (playableObject.Velocity.X == 0)
            {
                playableObject.Acceleration = Vector2.Zero;
                playableObject.PlayableObjectState = new SmallLeftIdleMario(playableObject);
            }
            else if (playableObject.Velocity.X > 0)
            {
                playableObject.Acceleration = Vector2.Zero;
                playableObject.PlayableObjectState = new SmallRightWalkingMario(playableObject);
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
            playableObject.Acceleration = new Vector2(GameValues.MarioHorizontalAcceleration, playableObject.Acceleration.Y);
            NoInput();
        }

        public void RunButtonInput()
        {
            playableObject.MaxHorizontalVelocity = GameValues.MarioRunningSpeed;
        }

        public void JumpButtonInput()
        {
            playableObject.Acceleration = new Vector2(0.0f, GameValues.MarioVerticalAcceleration);
            playableObject.PlayableObjectState = new SmallLeftJumpingMario(playableObject);
        }

        public void NoInput()
        {
            if (playableObject.Velocity.X + playableObject.Acceleration.X >= 0)
            {
                playableObject.Velocity = new Vector2(0.0f, playableObject.Velocity.Y);
                playableObject.Acceleration = new Vector2(0.0f, playableObject.Acceleration.Y);
            }
            else
            {
                playableObject.Acceleration = new Vector2(GameValues.MarioHorizontalAcceleration, playableObject.Acceleration.Y);
            }
        }

        public void TakeDamage()
        {
            playableObject.PlayableObjectState = new DeadMario(playableObject);
        }

        public void PickUpPowerup()
        {
            playableObject.PlayableObjectState = new SmallLeftToBig(playableObject);
            playableObject.Position = new Vector2(playableObject.Position.X, playableObject.Position.Y - GameValues.MarioStatePowerupSmallToBigYOffset);
        }

        public void PickUpStar()
        {
            playableObject.StarPower = true;
        }
    }
}