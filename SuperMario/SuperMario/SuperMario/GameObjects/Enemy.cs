using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.EnemyStates;
using SuperMario.Interfaces;

namespace SuperMario
{
    public class Enemy : IDynamicObject
    {
        public IEnemyState EnemyState { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 MaxVelocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public Rectangle CollisionRectangle { get; set; }
        public bool InAir { get; set; }
        public EnemyStateTransitionMachine EnemyStateTransitionMachine { get; set; }
        public bool IsFlipped { get; set; }
        public bool IsSlidingShell { get; set; }
        public bool IsTongueCaptured { get; set; }
        public Fireball Fireball { get; set; }

        private int koopaShellTimeout;
        private int deadGoombaTimeout;
        private Vector2 FallingVelocity;
        private Random rand;
        private int randBuffer;

        public Enemy(Vector2 position, string enemyState)
        {
            Position = position;
            MaxVelocity = new Vector2(GameValues.EnemyMaxXVelocity, GameValues.PhysicsMaxYVelocity);
            Acceleration = Vector2.Zero;
            EnemyStateTransitionMachine = new EnemyStateTransitionMachine();
            IsFlipped = false;
            IsSlidingShell = false;
            IsTongueCaptured = false;
            rand = new Random();
            randBuffer = 0;
            Fireball = new Fireball(Vector2.Zero, false, true);
            
            koopaShellTimeout = GameValues.EnemyKoopaShellTimeout;
            deadGoombaTimeout = GameValues.EnemyDeadEnemyTimeout;


            if (enemyState == "WalkingGoomba")
            {
                EnemyState = new WalkingGoomba(position, this);
                Velocity = new Vector2(-MaxVelocity.X, 0);
            }

            else if (enemyState == "RightWalkingKoopa")
            {
                EnemyState = new RightWalkingKoopa(position, this);
                Velocity = new Vector2(MaxVelocity.X, 0);
            }

            else if (enemyState == "LeftWalkingKoopa")
            {
                EnemyState = new LeftWalkingKoopa(position, this);
                Velocity = new Vector2(-MaxVelocity.X, 0);
            }

            else if (enemyState == "PiranhaPlant")
            {
                EnemyState = new PiranhaPlant(position, this);
            }

            else if (enemyState == "HidingInsideShellKoopa")
            {
                EnemyState = new HidingInsideShellKoopa(position, this);
            }

            else if (enemyState == "ComingOutOfShellKoopa")
            {
                EnemyState = new ComingOutOfShellKoopa(position, this);
            }

            else if (enemyState == "CrawfisLeft")
            {
                EnemyState = new CrawfisLeft(position, this);
                Velocity = new Vector2(-MaxVelocity.X, 0);
            }

            else if (enemyState == "BowserLeft")
            {
                EnemyState = new BowserLeft(position, this);
                Velocity = new Vector2(-MaxVelocity.X, 0);
            }

            else if (enemyState == "NoEnemy")
            {
                EnemyState = new NoEnemy(this);
            }

            CollisionRectangle = EnemyState.CollisionRectangle;
        }

        public void HandleDynamicCollision(string collisionDirection, IDynamicObject dynamicObject)
        {
            EnemyStateTransitionMachine.DynamicStateChange(collisionDirection, this, dynamicObject);
        }

        public void HandleStaticCollision(string collisionLocation, IStaticObject staticObject)
        {
            EnemyStateTransitionMachine.StaticStateChange(collisionLocation, staticObject, this);
        }

        public void ChangeState(string state)
        {
            if (state == "SuperMario.EnemyStates.LeftWalkingKoopa")
            {
                EnemyState = new RightWalkingKoopa(Position, this);
            }

            else if (state == "SuperMario.EnemyStates.RightWalkingKoopa")
            {
                EnemyState = new LeftWalkingKoopa(Position, this);
            }

            else if (state == "SuperMario.EnemyStates.CrawfisLeft")
            {
                EnemyState = new CrawfisRight(Position, this);
            }

            else if (state == "SuperMario.EnemyStates.CrawfisRight")
            {
                EnemyState = new CrawfisLeft(Position, this);
            }

            else if (state == "SuperMario.EnemyStates.BowserLeft")
            {
                EnemyState = new BowserRight(Position, this);
            }

            else if (state == "SuperMario.EnemyStates.BowserRight")
            {
                EnemyState = new BowserLeft(Position, this);
            }
        }

        public void FlipState(string state)
        {
            FallingVelocity = GameValues.EnemyFlippedStateFallingVelocity;

            if (state == "SuperMario.EnemyStates.WalkingGoomba")
            {
                EnemyState = new FlippedGoomba(Position, this);
            }

            else if (state == "SuperMario.EnemyStates.LeftWalkingKoopa" || state == "SuperMario.EnemyStates.RightWalkingKoopa" || state == "SuperMario.EnemyStates.HidingInsideShellKoopa" || state == "SuperMario.EnemyStates.ComingOutOfShellKoopa")
            {
                EnemyState = new FlippedKoopaShell(Position, this);
            }

            else if (state == "SuperMario.EnemyStates.CrawfisLeft" || state == "SuperMario.EnemyStates.CrawfisRight")
            {
                EnemyState = new CrawfisDead(Position, this);
                CollisionRectangle = GameValues.EmptyCollisionRectangle;
            }

            else if (state == "SuperMario.EnemyStates.BowserLeft" || state == "SuperMario.EnemyStates.BowserRight")
            {
                EnemyState = new BowserDead(Position, this);
                CollisionRectangle = GameValues.EmptyCollisionRectangle;
            }
        }

        public void AI()
        {
            if (randBuffer >= GameValues.EnemyAIRandomBufferMax)
            {
                randBuffer = 0;

                // Select random case from 1 to max - 1 (eg .Next(1, 4) -> random number 1, 2, or 3)
                int caseNum = rand.Next(1, 7);

                switch (caseNum)
                {
                    case 1:
                        // Reverse direction.
                        Velocity = new Vector2(-Velocity.X, Velocity.Y);
                        ChangeState(EnemyState.ToString());
                        break;

                    case 2:
                        // Make it jump if not in air.
                        if (!InAir)
                        {
                            InAir = true;
                            Velocity = new Vector2(Velocity.X, GameValues.EnemyAINotInAirYVelocity);
                            Physics.Move(this);
                        }

                        else
                        {
                            Physics.Move(this);
                        }
                        break;

                    case 3:
                        // Shoot fireball
                        if (!Fireball.IsAlive)
                        {
                            if (EnemyState.ToString().Substring(30, 4) == "Left")
                            {
                                Fireball = new Fireball(new Vector2(this.CollisionRectangle.Left - 8, this.CollisionRectangle.Center.Y), false, true);
                            }

                            else
                            {
                                Fireball = new Fireball(new Vector2(this.CollisionRectangle.Right, this.CollisionRectangle.Center.Y), true, true);
                            }

                            Fireball.IsAlive = true;
                            SoundManager.Instance.PlayFireballSound();
                        }
                        break;

                    case 4:
                        // Play SoundClip
                        SoundManager.Instance.PlayYourCodeSucksSound();
                        break;

                    case 5:
                        // Play Laugh
                        SoundManager.Instance.PlayLaughSound(false);
                        break;
                    case 6:
                        // Play Evil Laugh
                        SoundManager.Instance.PlayLaughSound(true);
                        break;

                }
            }

            else
            {
                randBuffer++;
            }
        }

        public void Update(GameTime gameTime)
        {
            if (IsTongueCaptured)
            {
                Vector2 previousPosition = new Vector2(Position.X, Position.Y);
                Physics.Move(this);
                Position = previousPosition;
            }
            if (!IsTongueCaptured)
            {
                Physics.Move(this);
            }

            if (!IsSlidingShell)
            {
                if (EnemyState.ToString() == "SuperMario.EnemyStates.HidingInsideShellKoopa")
                {
                    koopaShellTimeout--;

                    if (koopaShellTimeout <= 0)
                    {
                        koopaShellTimeout = GameValues.EnemyKoopaShellTimeout;
                        EnemyState = new LeftWalkingKoopa(Position, this);
                        Velocity = new Vector2(-MaxVelocity.X, 0);
                    }

                    else if (koopaShellTimeout % GameValues.EnemyKoopaShellUpdateDelay == 0)
                    {
                        EnemyState = new ComingOutOfShellKoopa(Position, this);
                        EnemyState.ScoreSprite.ScoringOn = false;
                    }
                }

                else if (EnemyState.ToString() == "SuperMario.EnemyStates.ComingOutOfShellKoopa")
                {
                    koopaShellTimeout--;

                    if (koopaShellTimeout <= 0)
                    {
                        koopaShellTimeout = GameValues.EnemyKoopaShellTimeout;
                        EnemyState = new LeftWalkingKoopa(Position, this);
                        Velocity = new Vector2(-MaxVelocity.X, 0);
                    }

                    else if (koopaShellTimeout % GameValues.EnemyKoopaShellUpdateDelay == 0)
                    {
                        EnemyState = new HidingInsideShellKoopa(Position, this);
                        EnemyState.ScoreSprite.ScoringOn = false;

                    }
                }

                else if (this.EnemyState.ToString() == "SuperMario.EnemyStates.DeadGoomba")
                {
                    deadGoombaTimeout--;

                    if (deadGoombaTimeout <= 0)
                    {
                        EnemyState = new NoEnemy(this);
                    }
                }
            }

            if (EnemyState.ToString() == "SuperMario.EnemyStates.NoEnemy" || EnemyState.ToString() == "SuperMario.EnemyStates.DeadGoomba" || EnemyState.ToString() == "SuperMario.EnemyStates.FlippedGoomba" || EnemyState.ToString() == "SuperMario.EnemyStates.FlippedKoopaShell")
            {
                CollisionRectangle = GameValues.EmptyCollisionRectangle;
            }

            if (EnemyState.ToString() == "SuperMario.EnemyStates.FlippedGoomba" || EnemyState.ToString() == "SuperMario.EnemyStates.FlippedKoopaShell")
            {
                float yAcceleration = GameValues.EnemyFlippedYAcceleration;
                Position = new Vector2(Position.X, Position.Y + FallingVelocity.Y);
                FallingVelocity = new Vector2(FallingVelocity.X, FallingVelocity.Y + yAcceleration);
            }

            if (EnemyState.ToString() == "SuperMario.EnemyStates.CrawfisLeft" || EnemyState.ToString() == "SuperMario.EnemyStates.CrawfisRight")
            {
                AI();
            }

            Fireball.Update(gameTime);
            EnemyState.Update(gameTime, Position);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Fireball.Draw(spriteBatch, gameTime);
            EnemyState.Draw(spriteBatch, gameTime);
        }
    }
}
