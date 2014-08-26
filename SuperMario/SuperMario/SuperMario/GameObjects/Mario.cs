using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.GameStates;
using SuperMario.Interfaces;
using SuperMario.MarioStates;

namespace SuperMario
{
    public sealed class Mario : IPlayableObject
    {
        private static Mario instance;

        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 MaxVelocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public Rectangle CollisionRectangle { get; set; }
        public IPlayableObjectState PlayableObjectState { get; set; }  // no need to change the name here!
        public PlayableObjectStateTransitionMachine PlayableObjectStateTransitionMachine { get; set; }  // or here!
        public int Lives { get; set; }
        public Fireball[] fireballs { get; set; }
        public float MaxHorizontalVelocity { get; set; }
        public bool InAir { get; set; }
        public bool IsJumping { get; set; }
        public bool StarPower { get; set; }
        public bool IsSlidingOnPole { get; set; }
        public bool IsEnteringPipe { get; set; }
        public bool IsExitingPipe { get; set; }
        public bool TakenDamageState { get; set; }
        public bool InCoinRoom { get; set; }
        public bool IsBig { get; set; }
        public bool IsFire { get; set; }
        public bool OnYoshi { get; set; }
        public bool IsYoshiTongueLeft { get; set; }
        public bool IsYoshiTongueRight { get; set; }
        public bool IsYoshiEating { get; set; }
        public bool IsYoshiFinishedEating { get; set; }
        public bool ReverseYoshiSprites { get; set; }
        
        private int numberOfFireballs;
        private int deathBuffer;
        private int invisibleBuffer;
        private int visibleBuffer;
        private int takenDamageBuffer;
        private int starBuffer;

        public Mario()
        {
            MaxHorizontalVelocity = GameValues.MarioWalkingSpeed;
            Velocity = Vector2.Zero;
            MaxVelocity = new Vector2(MaxHorizontalVelocity, GameValues.PhysicsMaxYVelocity);
            Acceleration = Vector2.Zero;
            CollisionRectangle = new Rectangle((int)Position.X, (int)Position.Y, 0, 0);
            PlayableObjectStateTransitionMachine = new PlayableObjectStateTransitionMachine(this);
            Lives = GameValues.MarioStartingLives;

            numberOfFireballs = GameValues.MarioNumberOfFireBalls;
            fireballs = new Fireball[numberOfFireballs];
            fireballs[0] = new Fireball(Vector2.Zero, false, false);
            fireballs[1] = new Fireball(Vector2.Zero, false, false);

            deathBuffer = GameValues.MarioDeathBuffer;
            invisibleBuffer = GameValues.MarioInvisibleBuffer;
            visibleBuffer = GameValues.MarioVisibleBuffer;
            takenDamageBuffer = GameValues.MarioTakenDamageBuffer;
            starBuffer = GameValues.MarioStarBuffer;

            InAir = false;
            IsJumping = false;
            StarPower = false;
            IsSlidingOnPole = false;
            TakenDamageState = false;
            InCoinRoom = false;
            IsEnteringPipe = false;
            IsExitingPipe = false;
            IsBig = false;
            OnYoshi = false;
            ReverseYoshiSprites = false;
        }

        public static Mario Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Mario();
                }
                return instance;
            }
        }

        public void HandleDynamicCollision(string collisionDirection, IDynamicObject dynamicObjectState)
        {
            if (Mario.Instance.PlayableObjectState.ToString() != "SuperMario.MarioStates.DeadMario")
            {
                PlayableObjectStateTransitionMachine.DynamicStateChange(collisionDirection, PlayableObjectState.ToString(), dynamicObjectState);
            }
        }

        public void HandleStaticCollision(string collisionDirection, IStaticObject staticObjectState)
        {
            if (Mario.Instance.PlayableObjectState.ToString() != "SuperMario.MarioStates.DeadMario")
            {
                PlayableObjectStateTransitionMachine.StaticStateChange(collisionDirection, staticObjectState);
            }
        }

        public void ThrowFireball()
        {
            // Make sure this can only run when Mario is Fire.
            if (this.PlayableObjectState.ToString().Substring(23, 4) == "Fire")
            {
                if (!fireballs[0].IsAlive)
                {
                    if(this.PlayableObjectState.ToString().Substring(27, 4) == "Left")
                    {
                        fireballs[0] = new Fireball(new Vector2(this.CollisionRectangle.Left, this.CollisionRectangle.Center.Y), false, false);
                    }

                    else if(this.PlayableObjectState.ToString().Substring(27, 5) == "Right")
                    {
                        fireballs[0] = new Fireball(new Vector2(this.CollisionRectangle.Right, this.CollisionRectangle.Center.Y), true, false);
                    }

                    fireballs[0].IsAlive = true;
                    SoundManager.Instance.PlayFireballSound();
                }
                else if (!fireballs[1].IsAlive)
                {
                    if(this.PlayableObjectState.ToString().Substring(27, 4) == "Left")
                    {
                        fireballs[1] = new Fireball(new Vector2(this.CollisionRectangle.Left, this.CollisionRectangle.Center.Y), false, false);
                    }

                    else if(this.PlayableObjectState.ToString().Substring(27, 5) == "Right")
                    {
                        fireballs[1] = new Fireball(new Vector2(this.CollisionRectangle.Right, this.CollisionRectangle.Center.Y), true, false);
                    }

                    fireballs[1].IsAlive = true;
                    SoundManager.Instance.PlayFireballSound();
                }
            }
        }

        public void ResetPlayer()
        {
            // Used for when the Level.Instance.Reset() method is called.
            TakenDamageState = false;
            IsSlidingOnPole = false;
            Acceleration = Vector2.Zero;
            Velocity = Vector2.Zero;
            OnYoshi = false;
            IsFire = false;
            IsBig = false;
            IsYoshiTongueLeft = false;
            IsYoshiTongueRight = false;
            Mario.Instance.PlayableObjectStateTransitionMachine.finishedCollidingWithFlagPole = false;
        }

        public void Update(GameTime gameTime)
        {

            if (this.PlayableObjectState.ToString() != "SuperMario.MarioStates.SmallMarioFlagSlideEnd" && this.PlayableObjectState.ToString() != "SuperMario.MarioStates.BigMarioFlagSlideEnd" && this.PlayableObjectState.ToString() != "SuperMario.MarioStates.FireMarioFlagSlideEnd")
            {
                MaxVelocity = new Vector2(MaxHorizontalVelocity, GameValues.PhysicsMaxYVelocity);
            }
            else
            {
                MaxVelocity = new Vector2(MaxHorizontalVelocity, 0);
            }

            if (Position.Y > GameValues.MarioDeathYPosition)
            {
                if (PlayableObjectState.ToString() != "SuperMario.MarioStates.DeadMario")
                {
                    PlayableObjectState = new DeadMario(this);
                }

                Lives--;
                if (Lives >= 1)
                {
                    GameStateMachine.Instance.GameState = new MarioRespawnState();
                }
                else
                {
                    if (deathBuffer > 0)
                    {
                        deathBuffer--;
                    }
                    else
                    {
                        deathBuffer = GameValues.MarioDeathBuffer;
                        GameStateMachine.Instance.GameState = new GameOverState();
                    }
                }
            }

            if (GameStateMachine.Instance.GameState.ToString() != "SuperMario.GameStates.MarioFreezeGameAnimationState")
            {
                Physics.Move(this);
            }

            PlayableObjectStateTransitionMachine.InvisibleBarrierCollision();
            Level.Instance.InvisibleBarrier.Update();

            for (int i = 0; i < fireballs.Length; i++)
            {
                fireballs[i].Update(gameTime);
            }

            if (StarPower)
            {
                if (starBuffer > 0)
                {
                    starBuffer--;
                }
                else
                {
                    starBuffer = GameValues.MarioStarBuffer;
                    StarPower = !StarPower;
                }
            }

            PlayableObjectState.Update(gameTime);      
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (TakenDamageState && !Mario.Instance.StarPower)
            {
                if (takenDamageBuffer <= 0)
                {
                    invisibleBuffer = GameValues.MarioInvisibleBuffer;
                    visibleBuffer = GameValues.MarioVisibleBuffer;
                    takenDamageBuffer = GameValues.MarioTakenDamageBuffer;

                    TakenDamageState = false;
                }

                else if (invisibleBuffer > 0 && visibleBuffer == 0)
                {
                    invisibleBuffer--;
                }

                else if (invisibleBuffer <= 0)
                {
                    invisibleBuffer = 0;

                    if (visibleBuffer < GameValues.MarioInvisibleBuffer)
                    {
                        PlayableObjectState.Draw(spriteBatch, gameTime);
                        visibleBuffer++;
                    }

                    else
                    {
                        invisibleBuffer = GameValues.MarioInvisibleBuffer;
                        visibleBuffer = GameValues.MarioVisibleBuffer;
                    }
                }

                takenDamageBuffer--;
            }

            else
            {
                PlayableObjectState.Draw(spriteBatch, gameTime);
            }

            for (int i = 0; i < fireballs.Length; i++)
            {
                fireballs[i].Draw(spriteBatch, gameTime);
            }
        }
    }
}
