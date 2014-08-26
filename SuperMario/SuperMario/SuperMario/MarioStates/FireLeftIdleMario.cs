﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario.MarioStates
{
    class FireLeftIdleMario : IPlayableObjectState
    {
        private AnimatedSprite sprite;
        private IPlayableObject playableObject;

        public FireLeftIdleMario(IPlayableObject playableObject)
        {
            this.playableObject = playableObject;
            sprite = AnimatedSpriteFactory.Instance.BuildFireLeftIdleMarioSprite();
            this.playableObject.CollisionRectangle = sprite.SpriteDestinationRectangle;
        }

        public void Update(GameTime gameTime)
        {
            // fire left idle mario only has one animation frame and doesn't need to call AdvanceFrame()
            sprite.UpdateSpritePosition(playableObject.Position);

            if (playableObject.Velocity.X > 0)
            {
                playableObject.PlayableObjectState = new FireRightWalkingMario(playableObject);
            }
            else if (playableObject.Velocity.X < 0)
            {
                playableObject.PlayableObjectState = new FireLeftWalkingMario(playableObject);
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
            if (playableObject.Acceleration.X != 0)
            {
                playableObject.Acceleration = new Vector2(0.0f, playableObject.Acceleration.Y);
            }
            else
            {
                playableObject.PlayableObjectState = new FireLeftCrouchingMario(playableObject);
            }
        }

        public void RunButtonInput()
        {
            playableObject.PlayableObjectState = new FireLeftFireballThrowingMario(playableObject);
        }

        public void JumpButtonInput()
        {
            playableObject.Acceleration = new Vector2(playableObject.Acceleration.X, GameValues.MarioVerticalAcceleration);
            playableObject.PlayableObjectState = new FireLeftJumpingMario(playableObject);
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

        }

        public void PickUpStar()
        {
            playableObject.StarPower = true;
        }
    }
}