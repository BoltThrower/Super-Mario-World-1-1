﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario.MarioStates
{
    class SmallRightIdleMario : IPlayableObjectState
    {
        private AnimatedSprite sprite;
        private IPlayableObject playableObject;

        public SmallRightIdleMario(IPlayableObject playableObject)
        {
            this.playableObject = playableObject;
            sprite = AnimatedSpriteFactory.Instance.BuildSmallRightIdleMarioSprite();
            this.playableObject.CollisionRectangle = sprite.SpriteDestinationRectangle;
        }

        public void Update(GameTime gameTime)
        {
            // small right idle mario only has one animation frame and doesn't need to call AdvanceFrame()
            sprite.UpdateSpritePosition(playableObject.Position);
            
            if (playableObject.Velocity.X > 0)
            {
                playableObject.PlayableObjectState = new SmallRightWalkingMario(playableObject);
            }
            else if (playableObject.Velocity.X < 0)
            {
                playableObject.PlayableObjectState = new SmallLeftWalkingMario(playableObject);
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

        }

        public void JumpButtonInput()
        {
            playableObject.Acceleration = new Vector2(playableObject.Acceleration.X, GameValues.MarioVerticalAcceleration);
            playableObject.PlayableObjectState = new SmallRightJumpingMario(playableObject);
        }

        public void NoInput()
        {
            
        }

        public void TakeDamage()
        {
            playableObject.PlayableObjectState = new DeadMario(playableObject);
        }

        public void PickUpPowerup()
        {
            playableObject.PlayableObjectState = new SmallRightToBig(playableObject);
            playableObject.Position = new Vector2(playableObject.Position.X, playableObject.Position.Y - GameValues.MarioStatePowerupSmallToBigYOffset);
        }

        public void PickUpStar()
        {
            playableObject.StarPower = true;
        }
    }
}