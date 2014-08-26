using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperMario.Interfaces
{
    public interface IPlayableObject : IDynamicObject 
    {
        IPlayableObjectState PlayableObjectState { get; set; }
        PlayableObjectStateTransitionMachine PlayableObjectStateTransitionMachine { get; set; }
        int Lives { get; set; }
        Fireball[] fireballs { get; set; }
        float MaxHorizontalVelocity { get; set; }
        bool InAir { get; set; }
        bool IsJumping { get; set; }
        bool StarPower { get; set; }
        bool IsSlidingOnPole { get; set; }
        bool IsEnteringPipe { get; set; }
        bool IsExitingPipe { get; set; }
        bool TakenDamageState { get; set; }
        bool InCoinRoom { get; set; }
        bool IsBig { get; set; }
        bool IsFire { get; set; }
        bool OnYoshi { get; set; }
        bool IsYoshiTongueLeft { get; set; }
        bool IsYoshiTongueRight { get; set; }
        bool IsYoshiEating { get; set; }
        bool IsYoshiFinishedEating { get; set; }
        bool ReverseYoshiSprites { get; set; }

        void ThrowFireball();
        void ResetPlayer();
    }
}
