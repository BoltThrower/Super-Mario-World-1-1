﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario.MarioStates
{
    class FireLeftFireballThrowingMario : IPlayableObjectState
    {
        private AnimatedSprite sprite;
        private int timer;
        private IPlayableObject playableObject;

        public FireLeftFireballThrowingMario(IPlayableObject playableObject)
        {
            this.playableObject = playableObject;
            sprite = AnimatedSpriteFactory.Instance.BuildFireLeftFireballThrowingMarioSprite();
            this.playableObject.CollisionRectangle = sprite.SpriteDestinationRectangle;
            
            this.playableObject.ThrowFireball();
            timer = GameValues.MarioStateFireThrowingTimer;
        }

        public void Update(GameTime gameTime)
        {
            // fire left fireball throwing mario only has one animation frame and doesn't need to call AdvanceFrame()

            sprite.UpdateSpritePosition(playableObject.Position);
            
            if (timer == 0)
            {
                if (playableObject.IsJumping)
                {
                    playableObject.PlayableObjectState = new FireLeftJumpingMario(playableObject);
                }
                else if (playableObject.Velocity.X > 0)
                {
                    playableObject.PlayableObjectState = new FireRightIdleMario(playableObject);
                }
                else if (playableObject.Velocity.X <= 0)
                {
                    playableObject.PlayableObjectState = new FireLeftIdleMario(playableObject);
                }
            }
            else
            {
                timer--;
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

        }

        public void RunButtonInput()
        {
            
        }

        public void JumpButtonInput()
        {

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