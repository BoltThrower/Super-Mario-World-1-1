using System;
using Microsoft.Xna.Framework;
using SuperMario.EnemyStates;
using SuperMario.Interfaces;

namespace SuperMario
{
    public class EnemyStateTransitionMachine
    {
        public EnemyStateTransitionMachine()
        {
        }

        public void DynamicStateChange(String location, Enemy enemy, IDynamicObject dynamicObject)
        {
            if (dynamicObject.ToString() == "SuperMario.Mario" && Mario.Instance.PlayableObjectState.ToString() != "SuperMario.MarioStates.DeadMario" )
            {
                if (Mario.Instance.StarPower)
                {
                    enemy.Velocity = Vector2.Zero;
                    enemy.FlipState(enemy.EnemyState.ToString());
                }

                else if ((location == "Top" || Mario.Instance.CollisionRectangle.Center.Y < dynamicObject.Position.Y) && enemy.EnemyState.ToString() != "SuperMario.EnemyStates.PiranhaPlant")
                {
                    // Mario lands on top of enemy and kills it.

                    if (enemy.EnemyState.ToString() != "SuperMario.EnemyStates.LeftWalkingKoopa" && enemy.EnemyState.ToString() != "SuperMario.EnemyStates.RightWalkingKoopa" && enemy.EnemyState.ToString() != "SuperMario.EnemyStates.HidingInsideShellKoopa" && enemy.EnemyState.ToString() != "SuperMario.EnemyStates.ComingOutOfShellKoopa")
                    {
                        enemy.Velocity = Vector2.Zero;

                        if (enemy.EnemyState.ToString() == "SuperMario.EnemyStates.WalkingGoomba")
                        {
                            enemy.Position = new Vector2(enemy.Position.X, enemy.Position.Y);
                            enemy.EnemyState = new DeadGoomba(enemy.Position, enemy);
                            enemy.EnemyState.ScoreSprite.ScoringOn = true;
                            HUD.Instance.ScoreHUDCounter += enemy.EnemyState.ScoreValue;

                        }

                        else if (enemy.EnemyState.ToString() == "SuperMario.EnemyStates.CrawfisLeft" || enemy.EnemyState.ToString() == "SuperMario.EnemyStates.CrawfisRight" || enemy.EnemyState.ToString() == "SuperMario.EnemyStates.BowserLeft" || enemy.EnemyState.ToString() == "SuperMario.EnemyStates.BowserRight")
                        {
                            enemy.FlipState(enemy.EnemyState.ToString());
                            enemy.EnemyState.ScoreSprite.ScoringOn = true;
                            HUD.Instance.ScoreHUDCounter += enemy.EnemyState.ScoreValue;
                        }

                        else if (enemy.EnemyState.ToString() != "SuperMario.EnemyStates.DeadGoomba" && enemy.EnemyState.ToString() != "SuperMario.EnemyStates.BowserDead")
                        {
                            enemy.EnemyState.ScoreSprite.ScoringOn = true;
                            enemy.EnemyState.ScoreSprite = new ScoreSprite(enemy.EnemyState.ScoreSprite.ScoreValue(), enemy.Position, true);
                            enemy.EnemyState = new NoEnemy(enemy);
                        }
                    }

                    else if (enemy.EnemyState.ToString() == "SuperMario.EnemyStates.HidingInsideShellKoopa" || enemy.EnemyState.ToString() == "SuperMario.EnemyStates.ComingOutOfShellKoopa")
                    {
                        if (enemy.IsSlidingShell)
                        {
                            enemy.IsSlidingShell = false;
                            enemy.Velocity = new Vector2(Vector2.Zero.X, enemy.Velocity.Y);
                        }

                        else
                        {
                            if (Mario.Instance.CollisionRectangle.Center.X > enemy.CollisionRectangle.Center.X)
                            {
                                enemy.Velocity = new Vector2(GameValues.EnemyStateMachineKoopaShellXVelocity, enemy.Velocity.Y);
                            }

                            else
                            {
                                enemy.Velocity = new Vector2(-GameValues.EnemyStateMachineKoopaShellXVelocity, enemy.Velocity.Y);
                            }
                            enemy.EnemyState = new HidingInsideShellKoopa(enemy.Position, enemy);
                            enemy.MaxVelocity = new Vector2(GameValues.EnemyStateMachineKoopaShellXVelocity, enemy.MaxVelocity.Y);
                            enemy.IsSlidingShell = true;
                        }
                    }

                    else if (enemy.EnemyState.ToString() == "SuperMario.EnemyStates.PiranhaPlant")
                    {
                        // If Mario hits the top of a piranha plant he should die, this will take place within MarioTransitionMachine.
                    }

                    else
                    {
                        // Turn the Koopa into a shell state.
                        HUD.Instance.ScoreHUDCounter += enemy.EnemyState.ScoreValue;
                        enemy.EnemyState.ScoreSprite.ScoringOn = true;
                        enemy.EnemyState = new HidingInsideShellKoopa(enemy.Position, enemy);
                        enemy.Velocity = Vector2.Zero;
                    }
                }

                else if ((location == "Left" || location == null) && Mario.Instance.IsYoshiTongueRight && !enemy.IsTongueCaptured)
                {
                    Mario.Instance.IsYoshiEating = true;
                    enemy.IsTongueCaptured = true;
                    enemy.Position = new Vector2(Mario.Instance.CollisionRectangle.Right - 10, Mario.Instance.CollisionRectangle.Top);
                }

                else if ((location == "Right" || location == null) && Mario.Instance.IsYoshiTongueLeft && !enemy.IsTongueCaptured)
                {
                    Mario.Instance.IsYoshiEating = true;
                    enemy.IsTongueCaptured = true;
                    enemy.Position = new Vector2(Mario.Instance.CollisionRectangle.Left + 10, Mario.Instance.CollisionRectangle.Top);
                }

                else if (enemy.IsTongueCaptured)
                {
                    if (Mario.Instance.IsYoshiFinishedEating)
                    {
                        HUD.Instance.ScoreHUDCounter += enemy.EnemyState.ScoreValue;
                        enemy.IsTongueCaptured = false;
                        enemy.EnemyState = new NoEnemy(enemy);
                    }

                    else if (Mario.Instance.IsYoshiTongueLeft)
                    {
                        enemy.Position = new Vector2(Mario.Instance.CollisionRectangle.Left + 16, Mario.Instance.CollisionRectangle.Top);
                    }

                    else if (Mario.Instance.IsYoshiTongueRight)
                    {
                       enemy.Position = new Vector2(Mario.Instance.CollisionRectangle.Right - 16, Mario.Instance.CollisionRectangle.Top);
                    }
                }

                else if ((enemy.EnemyState.ToString() == "SuperMario.EnemyStates.HidingInsideShellKoopa" || enemy.EnemyState.ToString() == "SuperMario.EnemyStates.ComingOutOfShellKoopa") && !enemy.IsSlidingShell)
                {
                    if (location == GameValues.CollisionDirectionLeft)
                    {
                        enemy.Velocity = new Vector2(GameValues.EnemyStateMachineKoopaShellXVelocity, enemy.Velocity.Y);
                        enemy.MaxVelocity = new Vector2(GameValues.EnemyStateMachineKoopaShellXVelocity, enemy.MaxVelocity.Y);
                        enemy.Position = new Vector2(enemy.Position.X + GameValues.EnemyStateMachineKoopaShellXPositionOffset, enemy.Position.Y);

                        enemy.EnemyState = new HidingInsideShellKoopa(enemy.Position, enemy);
                        enemy.IsSlidingShell = true;
                    }
                    else if (location == GameValues.CollisionDirectionRight)
                    {
                        enemy.Velocity = new Vector2(-GameValues.EnemyStateMachineKoopaShellXVelocity, enemy.Velocity.Y);
                        enemy.MaxVelocity = new Vector2(GameValues.EnemyStateMachineKoopaShellXVelocity, enemy.MaxVelocity.Y);
                        enemy.Position = new Vector2(enemy.Position.X - GameValues.EnemyStateMachineKoopaShellXPositionOffset, enemy.Position.Y);

                        enemy.EnemyState = new HidingInsideShellKoopa(enemy.Position, enemy);
                        enemy.IsSlidingShell = true;
                    }
                }
            }

            else if (dynamicObject.ToString() == "SuperMario.Block" || dynamicObject.ToString() == "SuperMario.Enemy" || dynamicObject.ToString() == "SuperMario.Fireball")
            {
                bool shellEnemyCollision = false;
                bool blockMovingCollision = false;

                if (dynamicObject.ToString() == "SuperMario.Enemy" && enemy.EnemyState.ToString() == "SuperMario.EnemyStates.HidingInsideShellKoopa")
                {
                    foreach (Enemy enemy2 in Level.Instance.Enemies)
                    {
                        if (enemy2.Position.X == dynamicObject.Position.X && enemy2.Position.Y == dynamicObject.Position.Y)
                        {
                            if (enemy2.EnemyState.ToString() == "SuperMario.EnemyStates.WalkingGoomba")
                            {
                                //Goomba death
                                shellEnemyCollision = true;
                            }

                            else if (enemy2.EnemyState.ToString() == "SuperMario.EnemyStates.LeftWalkingKoopa" || enemy2.EnemyState.ToString() == "SuperMario.EnemyStates.RightWalkingKoopa")
                            {
                                //Koopa death
                                shellEnemyCollision = true;
                            }
                        }
                    }
                }

                if (dynamicObject.ToString() == "SuperMario.Block")
                {
                    Block block = dynamicObject as Block;
                    
                    if (block.IsMoving)
                    {
                        enemy.FlipState(enemy.EnemyState.ToString());
                        blockMovingCollision = true;
                    }
                }

                if (shellEnemyCollision || dynamicObject.ToString() == "SuperMario.Fireball")
                {
                    if (dynamicObject.ToString() == "SuperMario.Fireball")
                    {
                        Fireball fireballTemp = dynamicObject as Fireball;

                        // If it is not an enemy fireball, then kill the enemy.
                        if (!fireballTemp.IsEnemyFireball)
                        {
                            enemy.FlipState(enemy.EnemyState.ToString());
                        }
                    }

                    else
                    {
                        enemy.FlipState(enemy.EnemyState.ToString());
                    }
                }

                if (!shellEnemyCollision && !blockMovingCollision)
                {
                    if (location == GameValues.CollisionDirectionTop)
                    {
                        enemy.Position = new Vector2(enemy.Position.X, dynamicObject.CollisionRectangle.Top - enemy.CollisionRectangle.Height);
                        enemy.CollisionRectangle = new Rectangle((int)enemy.Position.X, (int)enemy.Position.Y, enemy.CollisionRectangle.Width, enemy.CollisionRectangle.Height);
                    }

                    else if (location == GameValues.CollisionDirectionBottom)
                    {
                        enemy.Position = new Vector2(enemy.Position.X, dynamicObject.CollisionRectangle.Top - enemy.CollisionRectangle.Height);
                        enemy.CollisionRectangle = new Rectangle((int)enemy.Position.X, (int)enemy.Position.Y, enemy.CollisionRectangle.Width, enemy.CollisionRectangle.Height);
                    }

                    else if (location == GameValues.CollisionDirectionRight)
                    {
                        enemy.Position = new Vector2(dynamicObject.CollisionRectangle.Left - enemy.CollisionRectangle.Width, enemy.Position.Y);
                        enemy.CollisionRectangle = new Rectangle((int)enemy.Position.X, (int)enemy.Position.Y, enemy.CollisionRectangle.Width, enemy.CollisionRectangle.Height);
                        enemy.Velocity = new Vector2(enemy.Velocity.X * (-1), enemy.Velocity.Y);

                        enemy.ChangeState(enemy.EnemyState.ToString());
                    }

                    else if (location == GameValues.CollisionDirectionLeft)
                    {
                        enemy.Position = new Vector2(dynamicObject.CollisionRectangle.Right, enemy.Position.Y);
                        enemy.CollisionRectangle = new Rectangle((int)enemy.Position.X, (int)enemy.Position.Y, enemy.CollisionRectangle.Width, enemy.CollisionRectangle.Height);
                        enemy.Velocity = new Vector2(enemy.Velocity.X * (-1), enemy.Velocity.Y);

                        enemy.ChangeState(enemy.EnemyState.ToString());
                    }

                    else
                    {
                        enemy.Position = new Vector2(enemy.Position.X, dynamicObject.CollisionRectangle.Top - enemy.CollisionRectangle.Height);
                        enemy.CollisionRectangle = new Rectangle((int)enemy.Position.X, (int)enemy.Position.Y, enemy.CollisionRectangle.Width, enemy.CollisionRectangle.Height);
                    }
                }
            }

        }
        public void StaticStateChange(String location, IStaticObject staticObject, Enemy enemy)
        {
            if (location == GameValues.CollisionDirectionTop)
            {
                enemy.Position = new Vector2(enemy.Position.X, staticObject.CollisionRectangle.Top - enemy.CollisionRectangle.Height);
                enemy.CollisionRectangle = new Rectangle((int)enemy.Position.X, (int)enemy.Position.Y, enemy.CollisionRectangle.Width, enemy.CollisionRectangle.Height);
            }

            else if (location == GameValues.CollisionDirectionBottom)
            {
                enemy.Position = new Vector2(enemy.Position.X, staticObject.CollisionRectangle.Top - enemy.CollisionRectangle.Height);
                enemy.CollisionRectangle = new Rectangle((int)enemy.Position.X, (int)enemy.Position.Y, enemy.CollisionRectangle.Width, enemy.CollisionRectangle.Height);
                enemy.InAir = false;
            }

            else if (location == GameValues.CollisionDirectionRight)
            {
                enemy.Position = new Vector2(staticObject.CollisionRectangle.Left - enemy.CollisionRectangle.Width, enemy.Position.Y);
                enemy.CollisionRectangle = new Rectangle((int)enemy.Position.X, (int)enemy.Position.Y, enemy.CollisionRectangle.Width, enemy.CollisionRectangle.Height);
                enemy.Velocity = new Vector2(enemy.Velocity.X * (-1), enemy.Velocity.Y);

                enemy.ChangeState(enemy.EnemyState.ToString());
            }

            else if (location == GameValues.CollisionDirectionLeft)
            {
                enemy.Position = new Vector2(staticObject.CollisionRectangle.Right, enemy.Position.Y);
                enemy.CollisionRectangle = new Rectangle((int)enemy.Position.X, (int)enemy.Position.Y, enemy.CollisionRectangle.Width, enemy.CollisionRectangle.Height);
                enemy.Velocity = new Vector2(enemy.Velocity.X * (-1), enemy.Velocity.Y);

                enemy.ChangeState(enemy.EnemyState.ToString());
            }
        }
    }
}
