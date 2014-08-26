using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SuperMario.Commands;
using SuperMario.Commands.MarioCollisionCommands;
using SuperMario.Interfaces;
using SuperMario.ItemStates;

namespace SuperMario
{
    public class PlayableObjectStateTransitionMachine
    {
        public bool finishedCollidingWithFlagPole { get; set; }
        private int PipeBuffer;
        private int flagSlideEndBuffer = GameValues.MarioStateMachineFlagSlideEndBuffer;

        //private bool finishedCollidingWithFlagPole = false;

        private IPlayableObject playableObject;

        public PlayableObjectStateTransitionMachine(IPlayableObject playableObject)
        {
            this.playableObject = playableObject;
            PipeBuffer = GameValues.MarioStateMachinePipeBuffer;
        }

        public void DynamicStateChange(String collisionDirection, String currentState, IDynamicObject dynamicObject)
        {
            if (dynamicObject.ToString() == "SuperMario.Block")
            {
                if (playableObject.IsSlidingOnPole)
                {
                    new MarioSlidesOnFlagPoleCommand(playableObject, dynamicObject).Execute();
                    finishedCollidingWithFlagPole = true;
                }

                else if(finishedCollidingWithFlagPole == true)
                {
                    if (flagSlideEndBuffer <= 0)
                    {
                        flagSlideEndBuffer = GameValues.MarioStateMachineFlagSlideEndBuffer;

                        new MarioFinishedCollidingWithFlagPoleCommand().Execute(playableObject);
                    }

                    else
                    {
                        flagSlideEndBuffer--;
                    }
                }

                else
                {
                    if (collisionDirection == GameValues.CollisionDirectionTop && !(playableObject.CollisionRectangle.Left >= dynamicObject.CollisionRectangle.Right - GameValues.MarioStateMachineDynamicObjectCollisionRectangleOffset) && !(playableObject.CollisionRectangle.Right <= dynamicObject.CollisionRectangle.Left + GameValues.MarioStateMachineDynamicObjectCollisionRectangleOffset))
                    {
                        playableObject.Position = new Vector2(playableObject.Position.X, dynamicObject.CollisionRectangle.Bottom);
                        playableObject.CollisionRectangle = new Rectangle((int)playableObject.Position.X, (int)playableObject.Position.Y, playableObject.CollisionRectangle.Width, playableObject.CollisionRectangle.Height);
                        //Reverse Y Velocity for bouncing effect.
                        playableObject.Velocity = new Vector2(playableObject.Velocity.X, playableObject.Velocity.Y * -1);
                        playableObject.Acceleration = new Vector2(playableObject.Acceleration.X, 0.0f);
                    }

                    else if (collisionDirection == GameValues.CollisionDirectionBottom && !(playableObject.CollisionRectangle.Left >= dynamicObject.CollisionRectangle.Right - GameValues.MarioStateMachineDynamicObjectCollisionRectangleOffset) && !(playableObject.CollisionRectangle.Right <= dynamicObject.CollisionRectangle.Left + GameValues.MarioStateMachineDynamicObjectCollisionRectangleOffset))
                    {
                        new MarioCollidesWithGroundCommand(playableObject).Execute();

                        playableObject.Position = new Vector2(playableObject.Position.X, dynamicObject.CollisionRectangle.Top - playableObject.CollisionRectangle.Height);
                        playableObject.CollisionRectangle = new Rectangle((int)playableObject.Position.X, (int)playableObject.Position.Y, playableObject.CollisionRectangle.Width, playableObject.CollisionRectangle.Height);
                    }

                    else if (collisionDirection == GameValues.CollisionDirectionRight)
                    {
                        playableObject.Position = new Vector2(dynamicObject.CollisionRectangle.Left - playableObject.CollisionRectangle.Width + 0.9f, playableObject.Position.Y);
                        playableObject.CollisionRectangle = new Rectangle((int)playableObject.Position.X, (int)playableObject.Position.Y, playableObject.CollisionRectangle.Width, playableObject.CollisionRectangle.Height);
                    
                        if (playableObject.Velocity.X > Vector2.Zero.X)
                        {
                            playableObject.Velocity = new Vector2(GameValues.MarioStateMachineCollisionNewXVelocity, playableObject.Velocity.Y);
                        }
                    }

                    else if (collisionDirection == GameValues.CollisionDirectionLeft)
                    {
                        playableObject.Position = new Vector2(dynamicObject.CollisionRectangle.Right, playableObject.Position.Y);
                        playableObject.CollisionRectangle = new Rectangle((int)playableObject.Position.X, (int)playableObject.Position.Y, playableObject.CollisionRectangle.Width, playableObject.CollisionRectangle.Height);

                        if (playableObject.Velocity.X < Vector2.Zero.X)
                        {
                            playableObject.Velocity = new Vector2(GameValues.MarioStateMachineCollisionNewXVelocity, playableObject.Velocity.Y);
                        }
                    }
                }
            }

            else if (dynamicObject.ToString() == "SuperMario.Item")
            {
                string itemType = "";
                string previousItemType = "";
                int itemScoreValue = GameValues.MarioStateMachineInitialItemScoreValue;


                foreach (Block block in Level.Instance.Blocks)
                {
                    if (block.Item.Position.X == dynamicObject.Position.X && block.Item.Position.Y == dynamicObject.Position.Y)
                    {
                        itemType = block.Item.ItemState.ToString();
                        previousItemType = block.Item.PreviousItemState.ToString();
                        block.Item.PreviousItemState = new NoItem();
                        itemScoreValue = block.Item.ItemState.ScoreValue;
                        break;
                    }
                }

                foreach (Item coin in Level.Instance.Coins)
                {
                    if (coin.Position.X == dynamicObject.Position.X && coin.Position.Y == dynamicObject.Position.Y)
                    {
                        itemType = coin.ItemState.ToString();
                        previousItemType = coin.PreviousItemState.ToString();
                        coin.PreviousItemState = new NoItem();
                        itemScoreValue = coin.ItemState.ScoreValue;
                    }
                }

                if (itemType == "SuperMario.ItemStates.PowerUp" || previousItemType == "SuperMario.ItemStates.PowerUp")
                {
                    if (!Level.Instance.PowerUpState)
                    {
                        new MarioCollidesWithMushroomCommand(playableObject).Execute();
                    }
                    else
                    {
                        new MarioCollidesWithFireFlower(playableObject).Execute();
                    }

                    SoundManager.Instance.GainPowerUp.Play();
                    HUD.Instance.ScoreHUDCounter += itemScoreValue;
                }

                else if (itemType == "SuperMario.ItemStates.Star" || previousItemType == "SuperMario.ItemStates.Star")
                {
                    new MarioCollidesWithStarCommand(playableObject).Execute();
                    HUD.Instance.ScoreHUDCounter += itemScoreValue;
                }

                else if (itemType == "SuperMario.ItemStates.Mushroom1Up" || previousItemType == "SuperMario.ItemStates.Mushroom1Up")
                {
                    SoundManager.Instance.GainOneUp.Play();
                    HUD.Instance.ScoreHUDCounter += itemScoreValue;
                    playableObject.Lives++;
                }

                else if (itemType == "SuperMario.ItemStates.FloatingCoin" || previousItemType == "SuperMario.ItemStates.FloatingCoin")
                {
                    SoundManager.Instance.Coin.Play();
                    HUD.Instance.CoinHUDCounter += 1;
                    HUD.Instance.ScoreHUDCounter += itemScoreValue;
                }

                else if (itemType == "SuperMario.ItemStates.YoshiIdle" || previousItemType == "SuperMario.ItemStates.YoshiIdle")
                {
                    playableObject.Position = new Vector2(dynamicObject.Position.X, dynamicObject.Position.Y - 10);
                    new MarioCollidesWithYoshi(playableObject).Execute();
                }
            }

            else if (dynamicObject.ToString() == "SuperMario.Enemy")
            {
                // Mario shouldn't react to a Dead Goomba.
                bool enemyIsDead = false;
                bool enemyIsStoppedShell = false;

                Enemy enemy = dynamicObject as Enemy;

                if (enemy.EnemyState.ToString() == "SuperMario.EnemyStates.DeadGoomba" || enemy.EnemyState.ToString() == "SuperMario.EnemyStates.NoEnemy")
                {
                    enemyIsDead = true;
                }

                if (enemy.EnemyState.ToString() == "SuperMario.EnemyStates.HidingInsideShellKoopa" || enemy.EnemyState.ToString() == "SuperMario.EnemyStates.ComingOutOfShellKoopa")
                {
                    if (!enemy.IsSlidingShell)
                    {
                        enemyIsStoppedShell = true;
                    }
                }

                if (!enemyIsDead)
                {
                    if (collisionDirection == GameValues.CollisionDirectionBottom || (playableObject.CollisionRectangle.Center.Y < dynamicObject.Position.Y - 1 && playableObject.PlayableObjectState.ToString().Substring(23, 5) == "Small"))
                    {
                        SoundManager.Instance.EnemyStomp.Play();
                        playableObject.Velocity = new Vector2(playableObject.Velocity.X, GameValues.MarioStateMachineMarioBounceYVelocity);
                    }

                    else if ((playableObject.IsYoshiTongueLeft && collisionDirection == GameValues.CollisionDirectionLeft) || (playableObject.IsYoshiTongueRight && collisionDirection == GameValues.CollisionDirectionRight))
                    {
                    // Don't let Mario collide with enemy.
                    }

                    else if (collisionDirection != null)
                    {
                        // Need to animate Mario if he goes from small to dead.
                        // Need to prevent Mario from getting hurt if this collision is a shell that is not already moving.

                        if (!enemyIsStoppedShell)
                        {
                            new MarioCollidesWithEnemyCommand(playableObject).Execute();
                        }
                    }
                }

                else
                {
                    if (collisionDirection == GameValues.CollisionDirectionBottom)
                    {
                        SoundManager.Instance.EnemyStomp.Play();
                        playableObject.Velocity = new Vector2(playableObject.Velocity.X, GameValues.MarioStateMachineMarioBounceYVelocity);
                    }
                }
            }

            else if (dynamicObject.ToString() == "SuperMario.Fireball")
            {
                Fireball fireballTemp = dynamicObject as Fireball;

                // If it is not an enemy fireball, then kill the enemy.
                if (fireballTemp.IsEnemyFireball)
                {
                    new MarioCollidesWithEnemyCommand(playableObject).Execute();
                }
            }

        }

        public void StaticStateChange(String collisionDirection, IStaticObject staticObject)
        {
            if (staticObject.ToString() == "SuperMario.Flagpole" && !finishedCollidingWithFlagPole)
            {
                if (playableObject.PlayableObjectState.ToString() != "SuperMario.MarioStates.SmallMarioFlagSlide" && playableObject.PlayableObjectState.ToString() != "SuperMario.MarioStates.BigMarioFlagSlide" && playableObject.PlayableObjectState.ToString() != "SuperMario.MarioStates.FireMarioFlagSlide")
                {
                    playableObject.Velocity = Vector2.Zero;
                    playableObject.Acceleration = Vector2.Zero;
                    playableObject.MaxVelocity = new Vector2(playableObject.MaxVelocity.X, GameValues.MarioStateMachineFlagpoleMarioYVelocity);
                    SoundManager.Instance.OverWorldInstance.Stop();
                    SoundManager.Instance.PlayFlagSlideSound();
                }

                if (playableObject.Acceleration.Y < Vector2.Zero.Y)
                {
                    playableObject.Acceleration = Vector2.Zero;
                }

                new MarioCollidesWithFlagPoleCommand(playableObject).Execute();

                Level.Instance.Flagpoles[0].SetScoreValue(playableObject.Position.Y);
                playableObject.Position = new Vector2(staticObject.CollisionRectangle.Left - playableObject.CollisionRectangle.Width + GameValues.MarioStateMachineFlagPoleCollisionRectangleWidthOffset, playableObject.Position.Y);
                staticObject.CollisionRectangle = GameValues.EmptyCollisionRectangle;
                playableObject.CollisionRectangle = new Rectangle((int)playableObject.Position.X, (int)playableObject.Position.Y, playableObject.CollisionRectangle.Width, playableObject.CollisionRectangle.Height);
            }

            else
            {
                if (collisionDirection == GameValues.CollisionDirectionTop)
                {

                }

                else if (collisionDirection == GameValues.CollisionDirectionBottom)
                {
                    new MarioCollidesWithGroundCommand(playableObject).Execute();

                    if (!playableObject.IsEnteringPipe && !playableObject.IsExitingPipe)
                    {
                        playableObject.Position = new Vector2(playableObject.Position.X, staticObject.CollisionRectangle.Top - playableObject.CollisionRectangle.Height);
                        playableObject.CollisionRectangle = new Rectangle((int)playableObject.Position.X, (int)playableObject.Position.Y, playableObject.CollisionRectangle.Width, playableObject.CollisionRectangle.Height);
                    }

                    KeyboardState keyboardState =  Keyboard.GetState();
                    GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);

                    if ((keyboardState.IsKeyDown(Keys.Down) || gamePadState.IsButtonDown(Buttons.LeftThumbstickDown) || gamePadState.IsButtonDown(Buttons.DPadDown)) && staticObject.ToString() == "SuperMario.Pipe" && playableObject.CollisionRectangle.Left > staticObject.CollisionRectangle.Left && playableObject.CollisionRectangle.Right < staticObject.CollisionRectangle.Right)
                    {
                        Pipe pipe = staticObject as Pipe;

                        if (pipe.IsWarpPipe && !playableObject.IsEnteringPipe)
                        {
                            playableObject.IsEnteringPipe = true;
                            SoundManager.Instance.PlayPowerDownSound();       
                        }

                    }
                }

                else if (collisionDirection == GameValues.CollisionDirectionRight)
                {
                    Vector2 pipeExitPosition = Vector2.Zero;

                    if (staticObject.ToString() == "SuperMario.Pipe")
                    {
                        Pipe pipe = staticObject as Pipe;

                        if (pipe.IsHorizontalPipe)
                        {
                            playableObject.IsEnteringPipe = true;
                            //command for pipe animation
                            SoundManager.Instance.PlayPowerDownSound();
                            playableObject.InCoinRoom = false;

                            foreach (Pipe pipeTemp in Level.Instance.Pipes)
                            {
                                if(pipeTemp.Position.X > pipeExitPosition.X && pipeTemp.Position.X != pipe.Position.X && pipeTemp.Position.X < pipe.Position.X)
                                {
                                    pipeExitPosition = new Vector2(pipeTemp.Position.X, pipeTemp.Position.Y);
                                }
                            }
                        }
                    }

                    if (pipeExitPosition.X != Vector2.Zero.X && pipeExitPosition.Y != Vector2.Zero.Y)
                    {
                        playableObject.IsExitingPipe = true;
                        playableObject.Position = new Vector2(pipeExitPosition.X, pipeExitPosition.Y);
                    }

                    else
                    {
                        playableObject.Position = new Vector2(staticObject.CollisionRectangle.Left - playableObject.CollisionRectangle.Width + GameValues.MarioStateMachineCollisionRectangleWidthOffset, playableObject.Position.Y);
                    }

                    playableObject.CollisionRectangle = new Rectangle((int)playableObject.Position.X, (int)playableObject.Position.Y, playableObject.CollisionRectangle.Width, playableObject.CollisionRectangle.Height);
                    if (playableObject.Velocity.X > Vector2.Zero.X)
                    {
                        playableObject.Velocity = new Vector2(GameValues.MarioStateMachineCollisionNewXVelocity, playableObject.Velocity.Y);
                    }
                }

                else if (collisionDirection == GameValues.CollisionDirectionLeft)
                {
                    playableObject.Position = new Vector2(staticObject.CollisionRectangle.Right, playableObject.Position.Y);
                    playableObject.CollisionRectangle = new Rectangle((int)playableObject.Position.X, (int)playableObject.Position.Y, playableObject.CollisionRectangle.Width, playableObject.CollisionRectangle.Height);
                    if (playableObject.Velocity.X < Vector2.Zero.X)
                    {
                        playableObject.Velocity = new Vector2(-GameValues.MarioStateMachineCollisionNewXVelocity, playableObject.Velocity.Y);
                    }
                }

                if (playableObject.IsEnteringPipe && !playableObject.IsExitingPipe)
                {
                    if (PipeBuffer >= GameValues.MarioStateMachinePipeBufferMin)
                    {
                        PipeBuffer = GameValues.MarioStateMachinePipeBuffer;
                        playableObject.IsEnteringPipe = false;
                        playableObject.MaxVelocity = new Vector2(playableObject.MaxVelocity.X, GameValues.PhysicsMaxYVelocity);
                        playableObject.Position = new Vector2(Level.Instance.CoinRoomPosition.X + GameValues.LevelCellSize, (Level.Instance.CoinRoomPosition.Y / GameValues.LevelCellSize) + GameValues.MarioStateMachineEnteringPipeYPositionOffset);
                        playableObject.InCoinRoom = true;
                    }

                    else
                    {
                        PipeBuffer++;
                        //playableObject.Position = new Vector2(playableObject.Position.X, playableObject.Position.Y + 0.1f);
                        playableObject.MaxVelocity = new Vector2(playableObject.MaxVelocity.X, GameValues.MarioStateMachineCollisionNewXVelocity);
                        playableObject.Acceleration = Vector2.Zero;
                    }
                }

                else if (playableObject.IsExitingPipe)
                {
                    if (staticObject.ToString() == "SuperMario.Pipe")
                    {
                        if (playableObject.Position.Y <= staticObject.Position.Y - playableObject.CollisionRectangle.Height + GameValues.MarioStateMachineExitingPipeCollisionRectangleHeightOffset)
                        {
                            playableObject.IsEnteringPipe = false;
                            playableObject.IsExitingPipe = false;
                            playableObject.Position = new Vector2(playableObject.Position.X, staticObject.Position.Y - playableObject.CollisionRectangle.Height);
                        }

                        else
                        {
                            playableObject.Velocity = new Vector2(playableObject.Velocity.X, GameValues.MarioStateMachineExitingPipeNewYVelocity);
                        }
                    }
                }
            }
        }

        public void InvisibleBarrierCollision()
        {
            if (playableObject.CollisionRectangle.Intersects(Level.Instance.InvisibleBarrier.CollisionRectangle))
            {
                playableObject.Position = new Vector2(Level.Instance.InvisibleBarrier.CollisionRectangle.Right, playableObject.Position.Y);
                playableObject.CollisionRectangle = new Rectangle((int)playableObject.Position.X, (int)playableObject.Position.Y, playableObject.CollisionRectangle.Width, playableObject.CollisionRectangle.Height);
            }
        }
    }
}
