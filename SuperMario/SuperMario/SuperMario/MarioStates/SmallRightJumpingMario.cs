﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario.MarioStates
{
    class SmallRightJumpingMario : IPlayableObjectState
    {
        private AnimatedSprite sprite;
        private bool jumpPressed;
        private int jumpTimer;
        private IPlayableObject playableObject;

        public SmallRightJumpingMario(IPlayableObject playableObject)
        {
            this.playableObject = playableObject;
            if (!this.playableObject.IsJumping)
            {
                SoundManager.Instance.SmallJump.Play();
            }

            this.playableObject.IsJumping = true;
            this.playableObject.InAir = true;
            jumpPressed = true;
            jumpTimer = GameValues.MarioStateJumperTimer;

            sprite = AnimatedSpriteFactory.Instance.BuildSmallRightJumpingMarioSprite();
            this.playableObject.CollisionRectangle = sprite.SpriteDestinationRectangle;
        }

        public void Update(GameTime gameTime)
        {
            // small right jumping mario only has one animation frame and doesn't need to call AdvanceFrame()
            sprite.UpdateSpritePosition(playableObject.Position);

            jumpTimer--;
            if (jumpTimer <= 0)
            {
                jumpPressed = false;
            }
            if (!jumpPressed)
            {
                playableObject.Acceleration = new Vector2(playableObject.Acceleration.X, 0.0f);
            }

            if (!playableObject.InAir)
            {
                if (playableObject.Velocity.X > 0)
                {
                    playableObject.PlayableObjectState = new SmallRightWalkingMario(playableObject);
                }
                else if (playableObject.Velocity.X < 0)
                {
                    playableObject.PlayableObjectState = new SmallLeftWalkingMario(playableObject);
                }
                else
                {
                    playableObject.PlayableObjectState = new SmallRightIdleMario(playableObject);
                }
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
            playableObject.Acceleration = new Vector2(GameValues.MarioHorizontalAcceleration / GameValues.MarioStateAccelerationOffset, playableObject.Acceleration.Y);
        }

        public void LeftInput()
        {
            playableObject.Acceleration = new Vector2(-GameValues.MarioHorizontalAcceleration / GameValues.MarioStateAccelerationOffset, playableObject.Acceleration.Y);
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
