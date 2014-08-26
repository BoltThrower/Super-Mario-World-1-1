using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SuperMario.Commands.GameStateCommands;
using SuperMario.Commands.MarioInputCommands;
using SuperMario.Interfaces;
using System.Collections.Generic;

namespace SuperMario
{
    class GamePadController : IController
    {
        private Dictionary<Buttons, ICommand> buttonMap;
        private GamePadState gamePadState;
        private Game game;
        private bool startPressed, jumpPressed;
        private IPlayableObject playableObject;

        public GamePadController(Game game, IPlayableObject playableObject)
        {
            this.playableObject = playableObject;
            buttonMap = new Dictionary<Buttons, ICommand> { };

            buttonMap.Add(Buttons.DPadRight, new MoveMarioRightCommand(playableObject));
            buttonMap.Add(Buttons.LeftThumbstickRight, new MoveMarioRightCommand(playableObject));
            buttonMap.Add(Buttons.DPadLeft, new MoveMarioLeftCommand(playableObject));
            buttonMap.Add(Buttons.LeftThumbstickLeft, new MoveMarioLeftCommand(playableObject));
            buttonMap.Add(Buttons.DPadDown, new MarioCrouchCommand(playableObject));
            buttonMap.Add(Buttons.LeftThumbstickDown, new MarioCrouchCommand(playableObject));
            buttonMap.Add(Buttons.Back, new ExitGameCommand(game));

            this.game = game;
            startPressed = false;
            jumpPressed = false;
        }

        public void Update()
        {
            gamePadState = GamePad.GetState(PlayerIndex.One);
            bool foundInput = false;
         
            if (gamePadState.IsButtonDown(Buttons.Start) && !startPressed)
            {
                startPressed = true;
                new StartButtonCommand().Execute();
            }
            else if (gamePadState.IsButtonUp(Buttons.Start) && startPressed)
            {
                startPressed = false;
            }

            if (playableObject.IsEnteringPipe || playableObject.IsExitingPipe)
            {
                new MarioNoInputCommand(playableObject).Execute();
            }
            else
            {
                if (gamePadState.IsButtonDown(Buttons.A) && !jumpPressed)
                {
                    jumpPressed = true;
                    new MarioJumpCommand(playableObject).Execute();
                    foundInput = true;
                }
                else if (gamePadState.IsButtonDown(Buttons.A))
                {
                    foundInput = true;
                }
                else if (gamePadState.IsButtonUp(Buttons.A) && jumpPressed)
                {
                    jumpPressed = false;
                }

                if (gamePadState.IsButtonDown(Buttons.B))
                {
                    playableObject.MaxHorizontalVelocity = GameValues.MarioRunningSpeed;
                    new MarioRunCommand(playableObject).Execute();
                }
                else
                {
                    playableObject.MaxHorizontalVelocity = GameValues.MarioWalkingSpeed;
                }

                foreach (KeyValuePair<Buttons, ICommand> item in buttonMap)
                {
                    if (gamePadState.IsButtonDown(item.Key))
                    {
                        item.Value.Execute();
                        foundInput = true;
                    }
                }
                if (!foundInput)
                {
                    new MarioNoInputCommand(playableObject).Execute();
                }
            }
        }
    }
}
