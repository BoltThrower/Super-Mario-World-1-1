using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace SuperMario
{
    static class GameValues
    {
        // General Game Values
        public static Rectangle EmptyCollisionRectangle { get { return new Rectangle(0, 0, 0, 0); } }
        public static string CollisionDirectionLeft { get { return "Left"; } }
        public static string CollisionDirectionRight { get { return "Right"; } }
        public static string CollisionDirectionTop { get { return "Top"; } }
        public static string CollisionDirectionBottom { get { return "Bottom"; } }

        // SuperMario.cs values
        public static int ScreenWidth { get { return 600; } }
        public static int ScreenHeight { get { return 600; } }
        public static Vector2 InitialHUDPositionOffset { get { return new Vector2(2, 25); } }

        public static string LevelName1 { get { return @"..\..\..\Levels\Level1-1.csv"; } }
        public static string LevelName2 { get { return @"..\..\..\Levels\Level1-2.csv"; } }
        public static string LevelName3 { get { return @"..\..\..\Levels\Level1-3.csv"; } }
        public static int StartingLevel { get { return 1; } }

        public static Dictionary<int, string> Levels 
        { 
            get 
            {
                Dictionary<int, string> loadLevels = new Dictionary<int, string>();
                loadLevels.Add(1, LevelName1);
                loadLevels.Add(2, LevelName2);
                loadLevels.Add(3, LevelName3);
                return loadLevels;
            }
        }

#region Levels Game Values

        // Level.cs Values
        public static int LevelCellSize { get { return 16; } }
        public static int KoopaPositionOffset { get { return 8; } }
        public static int FlagMovingStoppingHeight { get { return 25; } }
        public static Vector2 FlagPositionOffsetVector { get { return new Vector2(13, 10); } }

#endregion

#region Background Game Values

        // Background.cs Values
        public static int BackgroundBlueSpriteOffset { get { return 5; } }

        // Castle.cs Values
        public static Vector2 CastleFlagPositionOffset { get { return new Vector2(6, 11); } }
        public static int CastleNextLevelBuffer { get { return 100; } }

#endregion

#region Block States Game Values

        // BrickBlockExplode.cs Values
        public static Vector2 ExplodingBlockInitialVelocity { get { return new Vector2(4f, 4f); } }
        public static int ExplodingBlockScoreValue { get { return 50; } }
        public static int ExplodingBlockStoppingPositionOffset { get { return 4; } }
        public static int ExplodingBlockUpdateDelay { get { return 50; } }

        // QuestionBlock.cs Values
        public static int QuestionBlockUpdateDelay { get { return 100; } }

#endregion

#region Commands Game Values
        // BlockCommands Values
        public static int BlockCollisionCommandYVelocity { get { return -3; } }

        // MarioFinishedCollisingWithFlagPoleCommand.cs Values
        public static int FinishedCollisingWithFlagPoleYPositionOffset { get { return 5; } }

#endregion

#region EnemyState Game Values

        // ComingOutOfShellKoopa.cs Values
        public static int ComingOutOfShellKoopaScoreValue { get { return 0; } }

        // CrawfisDead.cs Values
        public static int CrawfisScoreValue { get { return 500; } }

        // CrawfisLeft.cs Values
        public static Vector2 CrawfisInitialVelocity { get { return new Vector2(5f, 0f); } }

        // BowserLeft.cs Values
        public static Vector2 BowserInitialVelocity { get { return new Vector2(3f, 0f); } }
        public static int BowserScoreValue { get { return 5000; } }
        public static int BowserUpdateDelay { get { return 100; } }
        public static int BowserChangeStateDelay { get { return 6000; } }

        // DeadGoomba.cs && FlippedGoomba Values
        public static int GoombaScoreValue { get { return 100; } }

        // FlippedKoopa.cs && HidingInsideShellKoopa.cs Values
        public static int KoopaShellScoreValue { get { return 400; } }

        // LeftWalkingKoopa.cs && RightWalkingKoopa.cs Values
        public static int KoopaScoreValue { get { return 100; } }
        public static Vector2 LeftWalkingKoopaInitialVelocity { get { return new Vector2(-5f, 0f); } }
        public static Vector2 RightWalkingKoopaInitialVelocity { get { return new Vector2(5f, 0f); } }
        public static int KoopaUpdateDelay { get { return 100; } }

        // PiranhaPlant.cs Values
        public static int PiranhaPlantUpdateDelay { get { return 100; } }
        public static Vector2 PiranhaPlantInitialVelocity { get { return new Vector2(0, 4f); } }
        public static int PiranhaPlantMaxRaisedTime { get { return 25; } }
        public static int PiranhaPlantMaxLoweredTime { get { return 10; } }
        public static int PiranhaPlantScoreValue { get { return 200; } }

        // WalkingGoomba.cs Values
        public static Vector2 GoombaWalkingInitialVelocity { get { return new Vector2(5f, 0f); } }
        public static int GoombaWalkingUpdateDelay { get { return 100; } }

#endregion

#region GameObject Game Values

        // Block.cs Values
        public static int BlockCoinBrickCoinAmount { get { return 9; } }
        public static int BlockRotatingCoinInitialXPositionOffset { get { return 4; } }
        public static float BlockItemInitialYAcceleration { get { return 1f; } }
        public static Vector2 BlockItemInitialSpawnVelocity { get { return new Vector2(0f, -1f); } }
        public static Vector2 BlockRotatingCoinSpawningVelocity { get { return new Vector2(0f, -8f); } }
        public static int BlockRotatingCoinStoppingYPosition { get { return 10; } }

        // Camera.cs Values
        public static float CameraZoom { get { return 2.4f; } }
        public static int InvisibleBarrierXPositionOffset { get { return 65; } }

        // Enemy.cs Values
        public static float EnemyMaxXVelocity { get { return 0.5f; } }
        public static float EnemyMaxYVelocity { get { return 0.5f; } }
        public static int EnemyKoopaShellTimeout { get { return 400; } }
        public static int EnemyDeadEnemyTimeout { get { return 100; } }
        public static Vector2 EnemyFlippedStateFallingVelocity { get { return new Vector2(0f, -8f); } }
        public static int EnemyAINotInAirYVelocity { get { return -3; } }
        public static int EnemyAIRandomBufferMax { get { return 60; } }
        public static int EnemyKoopaShellUpdateDelay { get { return 10; } }
        public static float EnemyFlippedYAcceleration { get { return 0.5f; } }

        // FireBall.cs Values
        public static int FireBallExpirationBuffer { get { return 100; } }
        public static Vector2 FireBallVelocity { get { return new Vector2(2, 0); } }
        public static Vector2 FireBallMaxVelocity { get { return new Vector2(2, 2); } }
        public static int FireBallLeftMaxVisiblePosition { get { return 350; } }
        public static int FireBallRightMaxVisiblePosition { get { return 225; } }

        // Flag.cs Values
        public static float FlagInitialYVelocity { get { return 1.5f; } }
        
        // Flagpole.cs Values
        public static int FlagpoleDrawFlagScoreOffset { get { return 25; } }
        public static string FlagpoleMinScoreSprite { get { return "200"; } }
        public static string FlagpoleMaxScoreSprite { get { return "5000"; } }
        public static string FlagpoleYPosScoreSprite1 { get { return "400"; } }
        public static string FlagpoleYPosScoreSprite2 { get { return "600"; } }
        public static string FlagpoleYPosScoreSprite3 { get { return "800"; } }
        public static string FlagpoleYPosScoreSprite4 { get { return "1000"; } }
        public static string FlagpoleYPosScoreSprite5 { get { return "2000"; } }
        public static string FlagpoleYPosScoreSprite6 { get { return "3000"; } }
        public static string FlagpoleYPosScoreSprite7 { get { return "4000"; } }
        public static int FlagpoleYPosition1 { get { return 15; } }
        public static int FlagpoleYPosition2 { get { return 30; } }
        public static int FlagpoleYPosition3 { get { return 45; } }
        public static int FlagpoleYPosition4 { get { return 60; } }
        public static int FlagpoleYPosition5 { get { return 75; } }
        public static int FlagpoleYPosition6 { get { return 90; } }
        public static int FlagpoleYPosition7 { get { return 105; } }
        public static int FlagpoleYPosition8 { get { return 120; } }
        public static int FlagpoleYPosition9 { get { return 15; } }
        public static int FlagpoleYPosition10 { get { return 15; } }

        // HUD.cs Values
        public static int HUDTimerLife { get { return 400; } }
        public static int HUDUpdateDelay { get { return 200; } }
        public static int HUDDrawDelay { get { return 500; } }
        public static int HUDTimeWarningAmount { get { return 300; } }
        public static string HUDEmptyTimeCounter { get { return "000"; } }
        public static int HUDScoreMaxLength { get { return 6; } }
        public static int HUDCoinMaxLength { get { return 2; } }
        public static int HUDTimerMaxLength { get { return 3; } }

        // InvisibleBarrier.cs Values
        public static int InvisibleBarrierWidth { get { return 1; } }
        public static int InvisibleBarrierHeight { get { return 800; } }
        public static int InvisibleBarrierOffset { get { return 65; } }

        // Item.cs Values
        public static float ItemInitialMaxXVelocity { get { return 0.75f; } }
        public static int ItemRotatingCoinInitialYPositionOffset { get { return 3; } }

        // Mario.cs Values
        public static int MarioStartingLives { get { return 3; } }
        public static int MarioNumberOfFireBalls { get { return 2; } }
        public static int MarioDeathBuffer { get { return 300; } }
        public static int MarioInvisibleBuffer { get { return 10; } }
        public static int MarioVisibleBuffer { get { return 0; } }
        public static int MarioTakenDamageBuffer { get { return 260; } }
        public static int MarioStarBuffer { get { return 750; } }
        public static int MarioDeathYPosition { get { return 500; } }

        public static float MarioHorizontalAcceleration { get { return 0.1f; } }
        public static float MarioVerticalAcceleration { get { return (-(GameValues.PhysicsMaxYVelocity + 1)); } }
        public static float MarioWalkingSpeed { get { return 1.5f; } }
        public static float MarioRunningSpeed { get { return 2.5f; } }

#endregion

#region Item States Game Values

        // FloatingCoin.cs Values
        public static int FloatingCoinScoreValue { get { return 200; } }
        public static int FloatCoinUpdateDelay { get { return 100; } }

        // Mushroom1Up.cs Values
        public static int Mushroom1UPScoreValue { get { return 0; } }
        public static string Mushroom1UPScoreSpriteName { get { return "1 UP"; } }
        public static int Mushroom1UPUpdateDelay { get { return 100; } }

        // PowerUp.cs Values
        public static int PowerUpScoreValue { get { return 1000; } }

        // RotatingCoin.cs Values
        public static int RotatingCoinScoreValue { get { return 200; } }
        public static int RotatingCoinUpdateDelay { get { return 100; } }

        // Star.cs Values
        public static int StarScoreValue { get { return 1000; } }
        public static int StarUpdateDelay { get { return 100; } }

        // YoshiGreenEgg.cs Values
        public static int YoshiGreenEggScoreValue { get { return 0; } }

        // YoshiIdle.cs Values
        public static int YoshiIdleUpdateDelay { get { return 200; } }

        // YoshiSpawning.cs Values
        public static int YoshiSpawningUpdateDelay { get { return 200; } }





#endregion

#region Mario States & Mario Yoshi States Game Values

        // Mario State Jumping Mario Values
        public static int MarioStateJumperTimer { get { return 10; } }
        public static int MarioStateAccelerationOffset { get { return 4; } }
        public static int MarioStateWalkingUpdateDelay { get { return 100; } }

        // Mario State Flag Sldiing Mario Values
        public static int MarioStateFlagSlideUpdateDelay { get { return 200; } }

        // Mario State Fire Ball Throwing Values
        public static int MarioStateFireThrowingTimer { get { return 10; } }

        // Mario Small States Values
        public static int MarioStatePowerupSmallToBigYOffset { get { return 16; } }

        // Mario States Small to Big Values
        public static int MarioStateSmallToBigGrowthBuffer { get { return 5; } }
        public static int MarioStateSmallToBigMaxSpriteFrames { get { return 11; } }

#endregion

#region Physics Game Values

        // CollisionManager.cs Values
        public static int CollisionManagerDetectCollisionMax { get { return 256; } }

        // Physics.cs Values
        public static float PhysicsMaxYVelocity { get { return 4.0f; } }
        public static float PhysicsGravity { get { return 0.2f; } }

#endregion

#region Sprites Game Values

        // AnimatedSprite.cs Values
        public static int AnimatedSpriteStarCountMin { get { return 0; } }
        public static int AnimatedSpriteStarCounterMax1 { get { return 2; } }
        public static int AnimatedSpriteStarCounterMax2 { get { return 4; } }
        public static int AnimatedSpriteStarCounterMax3 { get { return 6; } }

        // AnimatedSpriteFactory.cs Values
        public static int SpriteSmallMarioTotalTextureFrames { get { return 13; } }
        public static int SpriteBigMarioTotalTextureFrames { get { return 14; } }
        public static int SpriteFireMarioTotalTextureFrames { get { return 16; } }
        public static int SpriteSmallMarioToBigTotalTextureFrames { get { return 24; } }
        public static int SpriteSmallFlagMarioTotalTextureFrames { get { return 2; } }
        public static int SpriteBigFlagMarioTotalTextureFrames { get { return 2; } }
        public static int SpriteFireFlagMarioTotalTextureFrames { get { return 2; } }
        public static int SpriteQuestionBlockTotalTextureFrames { get { return 4; } }
        public static int SpriteGoombaTotalTextureFrames { get { return 3; } }
        public static int SpriteKoopaTotalTextureFrames { get { return 6; } }
        public static int SpritePiranhaTotalTextureFrames { get { return 2; } }
        public static int SpritePipeTotalTextureFrames { get { return 2; } }
        public static int SpriteMushroomTotalTextureFrames { get { return 2; } }
        public static int SpriteHillTotalTextureFrames { get { return 2; } }
        public static int SpriteCloudTotalTextureFrames { get { return 3; } }
        public static int SpriteBushTotalTextureFrames { get { return 3; } }
        public static int SpriteFireBallTotalTextureFrames { get { return 4; } }
        public static int SpriteYoshiIdleTotalTextureFrames { get { return 2; } }
        public static int SpriteYoshiSpawningTotalTextureFrames { get { return 2; } }
        public static int SpriteSmallLeftWalkingMarioYoshiTotalTextureFrames { get { return 2; } }
        public static int SpriteSmallRightWalkingMarioYoshiTotalTextureFrames { get { return 2; } }
        public static int SpriteBigLeftWalkingMarioYoshiTotalTextureFames { get { return 3; } }
        public static int SpriteBigRightWalkingMarioYoshiTotalTextureFames { get { return 3; } }
        public static int SpriteBowserWalkingTotalTextureFrames { get { return 2; } }

        public static int SpriteAnimationRunningMarioTotalFrames { get { return 3; } }
        public static int SpriteAnimationQuestionBlockTotalFrames { get { return 4; } }
        public static int SpriteAnimationGoombaTotalFrames { get { return 2; } }
        public static int SpriteAnimationKoopaTotalFrames { get { return 2; } }
        public static int SpriteAnimationPiranhaTotalFrames { get { return 2; } }
        public static int SpriteAnimationFireBallTotalFrames { get { return 4; } }
        public static int SpriteAnimationSmallMarioToBigTotalFrames { get { return 12; } }
        public static int SpriteAnimationYoshiIdleTotalFrames { get { return 2; } }
        public static int SpriteAnimationYoshiSpawningTotalFrames { get { return 2; } }
        public static int SpriteAnimationSmallLeftWalkingMarioYoshiTotalFrames { get { return 2; } }
        public static int SpriteAnimationSmallRightWalkingMarioYoshiTotalFrames { get { return 2; } }
        public static int SpriteAnimationBigLeftWalkingMarioYoshiTotalFrames { get { return 3; } }
        public static int SpriteAnimationBigRightWalkingMarioYoshiTotalFrames { get { return 3; } }
        public static int SpriteAnimationBowserWalkingTotalFrames { get { return 2; } }

        // ScoreSprite.cs Values
        public static int ScoreSpriteInitialYPositionOffset { get { return 50; } }
        public static int ScoreSpriteScoreBuffer { get { return 100; } }
        public static int ScoreSpriteScoreOriginOffset { get { return 5; } }
        public static int ScoreSpriteDrawFlagScoreYOffset { get { return 15; } }
        public static int ScoreSpriteDrawFlagScoreDropOffet { get { return 2; } }

#endregion

#region Transition Machines Game Values

        // EnemyStateTransitionMachine.cs Values
        public static float EnemyStateMachineKoopaShellXVelocity { get { return 3f; } }
        public static int EnemyStateMachineKoopaShellXPositionOffset { get { return 2; } }

        // MarioStateTransitionMachine.cs Values
        public static int MarioStateMachineFlagSlideEndBuffer { get { return 50; } }
        public static int MarioStateMachinePipeBuffer { get { return 0; } }
        public static int MarioStateMachineDynamicObjectCollisionRectangleOffset { get { return 2; } }
        public static float MarioStateMachineCollisionRectangleWidthOffset { get { return 0.9f; } }
        public static float MarioStateMachineCollisionNewXVelocity { get { return 0.1f; } }
        public static int MarioStateMachineInitialItemScoreValue { get { return 0; } }
        public static float MarioStateMachineMarioBounceYVelocity { get { return -15f; } }
        public static float MarioStateMachineFlagpoleMarioYVelocity { get { return 1.25f; } }
        public static int MarioStateMachineFlagPoleCollisionRectangleWidthOffset { get { return 4; } }
        public static int MarioStateMachinePipeBufferMin { get { return 20; } }
        public static int MarioStateMachineEnteringPipeYPositionOffset { get { return 2; } }
        public static int MarioStateMachineExitingPipeCollisionRectangleHeightOffset { get { return 5; } }
        public static float MarioStateMachineExitingPipeNewYVelocity { get { return -1.05f; } }


#endregion

#region GameStateValues

        public static int GameStateMarioDeathBuffer { get { return 150; } }
        public static int GameStateRespawnBuffer { get { return 300; } }
        public static int GameStateGameOverBuffer { get { return 300; } }
        public static int GameStateTimeUpBuffer { get { return 250; } }
        public static int GameStateGameStatsBuffer { get { return 500; } }
        
#endregion
    }
}
