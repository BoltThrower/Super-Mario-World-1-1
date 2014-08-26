using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace SuperMario
{
    public sealed class SoundManager
    {
        private static SoundManager instance = new SoundManager();

        public ContentManager content { get; set; }
        public SoundEffect GainOneUp { get; set; }
        public SoundEffect GainPowerUp { get; set; }
        public SoundEffect Coin { get; set; }
        public SoundEffect CoinScore { get; set; }
        public SoundEffect PowerUpAppears { get; set; }
        public SoundEffect BlockBump { get; set; }
        public SoundEffect BlockBreaking { get; set; }
        public SoundEffect SmallJump { get; set; }
        public SoundEffect BigJump { get; set; }
        public SoundEffect PowerDown { get; set; }
        public SoundEffect EnemyStomp { get; set; }
        public SoundEffect EnemyKick { get; set; }
        public SoundEffect MarioDeath { get; set; }
        public SoundEffect Fireball { get; set; }
        public SoundEffect Flagpole { get; set; }
        public SoundEffect LevelClear { get; set; }
        public SoundEffect TimeWarning { get; set; }
        public SoundEffect TimeRunningOutOverworld { get; set; }
        public SoundEffect TimeRunningOutStar { get; set; }
        public SoundEffect WorldClear { get; set; }
        public SoundEffect Fireworks { get; set; }
        public SoundEffect GamePause { get; set; }
        public SoundEffect GameOver { get; set; }
        public SoundEffect LevelComplete { get; set; }
        public SoundEffect OverWorld { get; set; }
        public SoundEffect Star  { get; set; }
        public SoundEffect Underground { get; set; }
        public SoundEffect YourCodeSucks { get; set; }
        public SoundEffect Laugh { get; set; }
        public SoundEffect EvilLaugh { get; set; }
        public SoundEffect YoshiSound { get; set; }
        public SoundEffect EggHatchingSound { get; set; }
        public SoundEffect YoshiTongueSound { get; set; }
        public SoundEffect YoshiSwallowSound { get; set; }
        public SoundEffect Dubstep { get; set; }
        public SoundEffectInstance OverWorldInstance { get; set; }
        public SoundEffectInstance UndergroundInstance { get; set; }
        public SoundEffectInstance StarInstance { get; set; }
        public SoundEffectInstance TimeRunningOutOverworldInstance { get; set; }
        public SoundEffectInstance TimeRunningOutStarInstance { get; set; }
        public SoundEffectInstance TimeWarningInstance { get; set; }
        public SoundEffectInstance LevelCompleteInstance { get; set; }
        public SoundEffectInstance GameOverInstance { get; set; }
        public SoundEffectInstance DubstepInstance { get; set; }

        public bool gameOverPlayOnce { get; set; }

        private bool levelCompleteSoundOnce = false;

        public SoundManager()
        {
            gameOverPlayOnce = true;
        }

        public static SoundManager Instance
        {
            get
            {
                return instance;
            }
        }

        public void LoadSounds()
        {
            GainOneUp = content.Load<SoundEffect>("smb_1-up");
            GainPowerUp = content.Load<SoundEffect>("smb_powerup");
            Coin = content.Load<SoundEffect>("smb_coin");
            CoinScore = content.Load<SoundEffect>("smb_coin_score");
            PowerUpAppears = content.Load<SoundEffect>("smb_powerup_appears");
            BlockBump = content.Load<SoundEffect>("smb_bump");
            BlockBreaking = content.Load<SoundEffect>("smb_breakblock");
            SmallJump = content.Load<SoundEffect>("smb_jump-small");
            BigJump = content.Load<SoundEffect>("smb_jump-super");
            PowerDown = content.Load<SoundEffect>("smb_pipe");
            EnemyStomp = content.Load<SoundEffect>("smb_stomp");
            EnemyKick = content.Load<SoundEffect>("smb_kick");
            MarioDeath = content.Load<SoundEffect>("smb_mariodie");
            Fireball = content.Load<SoundEffect>("smb_fireball");
            Flagpole = content.Load<SoundEffect>("smb_flagpole");
            LevelClear = content.Load<SoundEffect>("smb_stage_clear");
            TimeWarning = content.Load<SoundEffect>("smb_warning");
            TimeRunningOutOverworld = content.Load<SoundEffect>("Overworld_Theme_Fast");
            TimeRunningOutStar = content.Load<SoundEffect>("Star_Theme_Fast");
            WorldClear = content.Load<SoundEffect>("smb_world_clear");
            Fireworks = content.Load<SoundEffect>("smb_fireworks");
            GamePause = content.Load<SoundEffect>("smb_pause");
            GameOver = content.Load<SoundEffect>("smb_gameover");
            LevelComplete = content.Load<SoundEffect>("smb_level_complete");
            Star = content.Load<SoundEffect>("Star Theme");
            OverWorld = content.Load<SoundEffect>("Overworld_Theme");
            Underground = content.Load<SoundEffect>("smb_underground");
            YourCodeSucks = content.Load<SoundEffect>("YourCodeSucks");
            Laugh = content.Load<SoundEffect>("NormalLaugh");
            EvilLaugh = content.Load<SoundEffect>("EvilLaugh");
            YoshiSound = content.Load<SoundEffect>("YoshiSound");
            EggHatchingSound = content.Load<SoundEffect>("EggHatchingSound");
            YoshiTongueSound = content.Load<SoundEffect>("YoshiTongueSound");
            YoshiSwallowSound = content.Load<SoundEffect>("YoshiSwallowSound");
            Dubstep = content.Load<SoundEffect>("Dubstep");
            UndergroundInstance = Underground.CreateInstance();
            OverWorldInstance = OverWorld.CreateInstance();
            StarInstance = Star.CreateInstance();
            TimeRunningOutOverworldInstance = TimeRunningOutOverworld.CreateInstance();
            TimeRunningOutStarInstance = TimeRunningOutStar.CreateInstance();
            TimeWarningInstance = TimeWarning.CreateInstance();
            LevelCompleteInstance = LevelComplete.CreateInstance();
            GameOverInstance = GameOver.CreateInstance();
            DubstepInstance = Dubstep.CreateInstance();

            TimeWarningInstance.IsLooped = false;
        }

        public void BackgroundMusic()
        {
            
            // create instance for star music
            if (GameStateMachine.Instance.GameState.ToString() != "SuperMario.GameStates.PlayingState" || Mario.Instance.PlayableObjectState.ToString() == "SuperMario.MarioStates.DeadMario")
            {
                StarInstance.Stop();
                OverWorldInstance.Stop();
                UndergroundInstance.Stop();
                TimeRunningOutOverworldInstance.Stop();
                TimeRunningOutStarInstance.Stop();
                DubstepInstance.Stop();

                if(!levelCompleteSoundOnce && GameStateMachine.Instance.GameState.ToString() == "SuperMario.GameStates.EndLevelState")
                {
                    LevelCompleteInstance.Play();
                    levelCompleteSoundOnce = true;
                }
            }
            /* Dubstep music for being on Yoshi.
            else if (Mario.Instance.OnYoshi && DubstepInstance.State != SoundState.Playing)
            {
                StarInstance.Stop();
                OverWorldInstance.Stop();
                UndergroundInstance.Stop();
                TimeRunningOutOverworldInstance.Stop();
                TimeRunningOutStarInstance.Stop();

                DubstepInstance.Play();
            }
            */
            else if(!Mario.Instance.OnYoshi)
            {
                if (DubstepInstance.State == SoundState.Playing)
                {
                    DubstepInstance.Stop();
                }

                if (Mario.Instance.InCoinRoom)
                {
                    if (UndergroundInstance.State != SoundState.Playing && !Mario.Instance.StarPower)
                    {
                        OverWorldInstance.Stop();
                        UndergroundInstance.Play();
                    }
                }

                else if (!Mario.Instance.IsSlidingOnPole)
                {
                    if (!Mario.Instance.InCoinRoom && UndergroundInstance.State == SoundState.Playing)
                    {
                        UndergroundInstance.Stop();
                        OverWorldInstance.Play();
                    }

                    else if (!Mario.Instance.StarPower && OverWorldInstance.State != SoundState.Playing && !HUD.Instance.TimeIsLow)
                    {
                        StarInstance.Stop();

                        if (!Mario.Instance.InCoinRoom)
                        {
                            OverWorldInstance.Play();
                        }
                    }
                    else if (Mario.Instance.StarPower && OverWorldInstance.State == SoundState.Playing && !HUD.Instance.TimeIsLow)
                    {
                        OverWorldInstance.Stop();
                        StarInstance.Play();
                    }

                    else if (HUD.Instance.TimeIsLow && TimeWarningInstance.State != SoundState.Playing)
                    {
                        if (Mario.Instance.StarPower && TimeRunningOutStarInstance.State != SoundState.Playing)
                        {
                            TimeRunningOutOverworldInstance.Stop();
                            TimeRunningOutStarInstance.Play();
                        }
                        else if (!Mario.Instance.StarPower && TimeRunningOutOverworldInstance.State != SoundState.Playing)
                        {
                            TimeRunningOutStarInstance.Stop();
                            TimeRunningOutOverworldInstance.Play();
                        }
                    }
                }
            }
        }

        public void PlayGameOverMusic()
        {
            if (GameStateMachine.Instance.GameState.ToString() == "SuperMario.GameStates.GameOverState" && this.GameOverInstance.State != SoundState.Playing)
            {
                if (gameOverPlayOnce)
                {
                    gameOverPlayOnce = false;
                    GameOverInstance.Play();
                }
            }
        }

        public void PlayTimeWarning()
        {
            if (OverWorldInstance.State == SoundState.Playing)
            {
                OverWorldInstance.Stop();
            }
            else if (StarInstance.State == SoundState.Playing)
            {
                StarInstance.Stop();
            }
            TimeWarningInstance.Play();
        }

        public void PlayPauseSound()
        {
            if (GameStateMachine.Instance.GameState.ToString() == "SuperMario.GameStates.PausedState")
            {
                if (OverWorldInstance.State == SoundState.Playing)
                {
                    OverWorldInstance.Pause();
                }

                else if (UndergroundInstance.State == SoundState.Playing)
                {
                    UndergroundInstance.Pause();
                }

                else if (StarInstance.State == SoundState.Playing)
                {
                    StarInstance.Pause();
                }

                else if (TimeRunningOutOverworldInstance.State == SoundState.Playing)
                {
                    TimeRunningOutOverworldInstance.Pause();
                }

                else if (TimeRunningOutStarInstance.State == SoundState.Playing)
                {
                    TimeRunningOutStarInstance.Pause();
                }
            }

            else if (GameStateMachine.Instance.GameState.ToString() == "SuperMario.GameStates.PlayingState")
            {
                if (OverWorldInstance.State == SoundState.Paused)
                {
                    OverWorldInstance.Resume();
                }

                else if (UndergroundInstance.State == SoundState.Paused)
                {
                    UndergroundInstance.Resume();
                }

                else if (StarInstance.State == SoundState.Paused)
                {
                    StarInstance.Resume();
                }

                else if (TimeRunningOutOverworldInstance.State == SoundState.Paused)
                {
                    TimeRunningOutOverworldInstance.Resume();
                }

                else if (TimeRunningOutStarInstance.State == SoundState.Paused)
                {
                    TimeRunningOutStarInstance.Resume();
                }
            }

            GamePause.Play();
        }

        public void PlayFireballSound()
        {
            Fireball.Play();
        }

        public void PlayFlagSlideSound()
        {
            Flagpole.Play();
        }

        public void PlayEndingScoreIncrementSound()
        {
            CoinScore.Play();
        }

        public void PlayPowerDownSound()
        {
            PowerDown.Play();
        }

        public void PlayYourCodeSucksSound()
        {
            YourCodeSucks.Play();
        }

        public void PlayLaughSound(bool isEvil)
        {
            if (isEvil)
            {
                EvilLaugh.Play();
            }

            else
            {
                Laugh.Play();
            }
        }

        public void PlayYoshiSound()
        {
            YoshiSound.Play();
        }

        public void PlayEggHatchingSound()
        {
            EggHatchingSound.Play();
        }

        public void PlayYoshiTongueSound()
        {
            YoshiTongueSound.Play();
        }

        public void PlayYoshiSwallowSound()
        {
            YoshiSwallowSound.Play();
        }
    }
}
