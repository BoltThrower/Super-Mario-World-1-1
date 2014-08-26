using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SuperMario.Commands.GameStateCommands;
using SuperMario.Commands.MarioInputCommands;
using SuperMario.Interfaces;
using System.Collections.Generic;

namespace SuperMario
{
    class KeyboardController : IController
    {
        private Dictionary<Keys, ICommand> keyMap;
        private KeyboardState keyboardState;
        private bool startPressed, jumpPressed;
        private IPlayableObject playableObject;

        public KeyboardController(Game game, IPlayableObject playableObject)
        {
            this.playableObject = playableObject;

            keyMap = new Dictionary<Keys, ICommand> { };

            keyMap.Add(Keys.Left, new MoveMarioLeftCommand(playableObject));
            keyMap.Add(Keys.Right, new MoveMarioRightCommand(playableObject));
            keyMap.Add(Keys.Escape, new ExitGameCommand(game));

            startPressed = false;
            jumpPressed = false;
        }

        public void Update()
        {
            // Left Arrow - move left, change mario to appropriate left moving sprite
            // Right Arrow - move right, change mario to appropriate right moving sprite
            // Down Arrow - change mario to appropriate crouching state, if possible
            // Up Arrow - climb vine
            // Z Key - run, throw fireball if fire mario
            // X Key - jump, change mario to appropriate jumping state
            
            bool foundInput = false;
            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Enter) && !startPressed)
            {
                startPressed = true;
                new StartButtonCommand().Execute(); 
            }
            else if (keyboardState.IsKeyUp(Keys.Enter) && startPressed)
            {
                startPressed = false;
            }
            if (playableObject.IsEnteringPipe || playableObject.IsExitingPipe)
            {
                new MarioNoInputCommand(playableObject).Execute();
            }

            else
            {

                if (keyboardState.IsKeyDown(Keys.X) && !jumpPressed)
                {
                    jumpPressed = true;
                    new MarioJumpCommand(playableObject).Execute();
                    foundInput = true;
                }
                else if (keyboardState.IsKeyDown(Keys.X))
                {
                    foundInput = true;
                }
                else if (keyboardState.IsKeyUp(Keys.X) && jumpPressed)
                {
                    jumpPressed = false;
                }

                if (keyboardState.IsKeyDown(Keys.Z))
                {
                    playableObject.MaxHorizontalVelocity = GameValues.MarioRunningSpeed;
                    new MarioRunCommand(playableObject).Execute();
                }
                else
                {
                    playableObject.MaxHorizontalVelocity = GameValues.MarioWalkingSpeed;
                }

                Keys[] keysPressed = keyboardState.GetPressedKeys();

                for (int i = 0; i < keysPressed.Length; i++)
                {
                    if (keyMap.ContainsKey(keysPressed[i]))
                    {
                        keyMap[keysPressed[i]].Execute();
                        foundInput = true;
                    }
                }

                if (keyboardState.IsKeyDown(Keys.Down))
                {
                    new MarioCrouchCommand(playableObject).Execute();
                    foundInput = true;
                }

                if (!foundInput)
                {
                    new MarioNoInputCommand(playableObject).Execute();
                }
            }
        }
    }
}