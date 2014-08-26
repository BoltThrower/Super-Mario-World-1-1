using System;
using Microsoft.Xna.Framework;
using SuperMario.Commands.GenericCollisionCommands;
using SuperMario.Interfaces;

namespace SuperMario
{
    class FireballStateTransitionMachine
    {
        public FireballStateTransitionMachine()
        {
        }

        public void DynamicStateChange(String location, Fireball fireball, IDynamicObject dynamicObject)
        {
            if (dynamicObject.ToString() == "SuperMario.Block")
            {
                if (location == GameValues.CollisionDirectionTop || location == GameValues.CollisionDirectionBottom)
                {
                    new GenericTopOrBottomCollisionCommand(fireball).Execute();
                }

                else
                {
                    fireball.IsAlive = false;
                }
            }

            else if (dynamicObject.ToString() != "SuperMario.Mario")
            {
                fireball.IsAlive = false;
            }
        }

        public void StaticStateChange(String location, Fireball fireball, IStaticObject staticObject)
        {
            if (location == GameValues.CollisionDirectionTop || location == GameValues.CollisionDirectionBottom || staticObject.ToString() == "SuperMario.FloorTile")
            {
                fireball.Position = new Vector2(fireball.Position.X, staticObject.CollisionRectangle.Top - fireball.CollisionRectangle.Height);
                fireball.CollisionRectangle = new Rectangle((int)fireball.Position.X, (int)fireball.Position.Y, fireball.CollisionRectangle.Width, fireball.CollisionRectangle.Height);
                fireball.Velocity = new Vector2(fireball.Velocity.X, fireball.Velocity.Y * (-1));
            }

            else
            {
                fireball.IsAlive = false;
            }

        }
    }
}
