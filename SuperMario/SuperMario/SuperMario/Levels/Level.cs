using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;
using SuperMario.MarioStates;

namespace SuperMario
{
    public sealed class Level
    {
        private static Level instance;

        public List<Block> Blocks { get; set; }
        public List<Item> Coins { get; set; }
        public List<Pipe> Pipes { get; set; }
        public List<Enemy> Enemies { get; set; }
        public Castle Castle { get; set; }
        public List<Flagpole> Flagpoles { get; set; }
        public List<CheckPoint> Checkpoints { get; set; }
        public Flag Flag { get; set; }
        public Vector2 CoinRoomPosition { get; set; }

        public InvisibleBarrier InvisibleBarrier { get; set; }

        public List<IDynamicObject> DynamicObjects { get; set; }
        public List<IStaticObject> StaticObjects { get; set; }
        public List<IBackgroundItem> BackgroundItems { get; set; }

        public bool PowerUpState { get; set; } // false = mushroom, true = fire flower
        public bool IsOnePlayer { get; set; }

        private List<string[]> levelData;
        private static int levelCellSize = GameValues.LevelCellSize;
        private string fileName;
        private int currentLevel;
        private Dictionary<int, string> levelDictionary;

        public Level()
        {
            Blocks = new List<Block>();
            Coins = new List<Item>();
            Pipes = new List<Pipe>();
            Enemies = new List<Enemy>();
            Flagpoles = new List<Flagpole>();
            Checkpoints = new List<CheckPoint>();

            Flag = new Flag(Vector2.Zero);
            CoinRoomPosition = Vector2.Zero;
            InvisibleBarrier = new InvisibleBarrier(Vector2.Zero);

            BackgroundItems = new List<IBackgroundItem>();
            DynamicObjects = new List<IDynamicObject>();
            StaticObjects = new List<IStaticObject>();

            levelDictionary = GameValues.Levels;
            currentLevel = GameValues.StartingLevel;
            fileName = GameValues.LevelName1;


            levelData = ParseLevelCSV(fileName);
            BuildLevel(levelData);

            PowerUpState = false;
            IsOnePlayer = true;
        }

        public static Level Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Level();
                }
                return instance;
            }
        }

        private void BuildLevel(List<string[]> levelData)
        {
            // iterate through lines
            for (int i = 0; i < levelData.Count(); i++)
            {
                switch (levelData[i][0])
                {
                    case "00":
                        // mario
                        Mario.Instance.PlayableObjectState = new SmallRightIdleMario(Mario.Instance);
                        Mario.Instance.Position = new Vector2(StringToInt(i,1) * levelCellSize, StringToInt(i,2) * levelCellSize);
                    break;
                    case "01":
                        // floor tiles
                        StaticObjects.Add(new FloorTile(new Vector2(StringToInt(i, 1) * levelCellSize, StringToInt(i, 2) * levelCellSize), new Vector2(StringToInt(i, 3), StringToInt(i, 4)), levelData[i][5]));
                    break;
                    case "02":
                        // blocks
                        Blocks.Add(new Block(new Vector2(StringToInt(i, 1) * levelCellSize, StringToInt(i, 2) * levelCellSize), levelData[i][3], levelData[i][4]));
                    break;
                    case "03":
                        // floating coins
                        Coins.Add(new Item(new Vector2((StringToInt(i, 1) * levelCellSize) + (levelCellSize/4), StringToInt(i, 2) * levelCellSize), "FloatingCoin"));
                    break;
                    case "04":
                        // pipes
                        Pipes.Add(new Pipe(new Vector2(StringToInt(i, 1) * levelCellSize, StringToInt(i, 2) * levelCellSize), StringToInt(i, 3), Convert.ToBoolean(levelData[i][4]), levelData[i][5]));
                    break;
                    case "05":
                        // enemies
                        Enemies.Add(new Enemy(new Vector2(StringToInt(i, 1) * levelCellSize, StringToInt(i, 2) * levelCellSize - 10), levelData[i][3]));
                    break;
                    case "06":
                        // hills
                        BackgroundItems.Add(new Hill(new Vector2(StringToInt(i, 1) * levelCellSize, StringToInt(i, 2) * levelCellSize), StringToInt(i, 3)));
                    break;
                    case "07":
                        // bushes
                        BackgroundItems.Add(new Bush(new Vector2(StringToInt(i, 1) * levelCellSize, StringToInt(i, 2) * levelCellSize), StringToInt(i, 3)));
                    break;
                    case "08":
                        // clouds
                        BackgroundItems.Add(new Cloud(new Vector2(StringToInt(i, 1) * levelCellSize, StringToInt(i, 2) * levelCellSize), StringToInt(i, 3)));
                    break;
                    case "09":
                        // castle
                        Castle = new Castle(new Vector2(StringToInt(i, 1) * levelCellSize, StringToInt(i,2) * levelCellSize));
                    break;
                    case "10":
                        // flag pole
                        Flagpoles.Add(new Flagpole(new Vector2(StringToInt(i, 1) * levelCellSize, StringToInt(i, 2) * levelCellSize), levelCellSize));
                    break;
                    case "11":
                        // check point
                        Checkpoints.Add(new CheckPoint(new Vector2(StringToInt(i, 1) * levelCellSize, StringToInt(i, 2) * levelCellSize)));
                    break;
                    case "12":
                        // coin room
                        CoinRoomPosition = new Vector2(StringToInt(i, 1) * levelCellSize + (levelCellSize/8), StringToInt(i, 2) * levelCellSize);
                    break;
                }
            }

            DynamicObjects.Add(Mario.Instance);

            foreach (Block block in Blocks)
            {
                DynamicObjects.Add(block);
                DynamicObjects.Add(block.Item); // Add items in each block too.
            }

            foreach (Item coin in Coins)
            {
                DynamicObjects.Add(coin);
            }

            foreach (Enemy enemy in Enemies)
            {
                // Added such that the Koopa spawns in a correct position before any updates are made, otherwise he spawns in the floor to start.
                if (enemy.EnemyState.ToString() == "SuperMario.EnemyStates.LeftWalkingKoopa" || enemy.EnemyState.ToString() == "SuperMario.EnemyStates.RightWalkingKoopa")
                {
                    enemy.Position = new Vector2(enemy.Position.X, enemy.Position.Y - GameValues.KoopaPositionOffset);
                }
                DynamicObjects.Add(enemy);
            }

            foreach (Pipe pipe in Pipes)
            {
                StaticObjects.Add(pipe);
            }

            foreach (Flagpole flagpole in Flagpoles)
            {
                StaticObjects.Add(flagpole);
            }

            if (Flagpoles.Count > 0)
            {
                Flag.Position = new Vector2(Flagpoles[0].CollisionRectangle.X - GameValues.FlagPositionOffsetVector.X, Flagpoles[0].CollisionRectangle.Y + GameValues.FlagPositionOffsetVector.Y);
            }
        }

        private List<string[]> ParseLevelCSV(string fileName)
        {
            List<string[]> parsedData = new List<string[]>();
            this.fileName = fileName;

            using (StreamReader readFile = new StreamReader(fileName))
            {
                string line;
                string[] stringRow;

                while ((line = readFile.ReadLine()) != null)
                {
                    stringRow = line.Split(',');
                    parsedData.Add(stringRow);
                }
            }
            return parsedData;
        }

        private int StringToInt(int row, int column)
        {
            return Convert.ToInt32(levelData[row][column]);
        }

        private bool IsOnScreen(float xPosition)
        {
            bool onScreen = false;

            if (Math.Abs(xPosition - Mario.Instance.Position.X) <= levelCellSize * levelCellSize)
            {
                onScreen = true;
            }

            return onScreen;
        }

        public void Update(GameTime gameTime)
        {

            Mario.Instance.Update(gameTime);
            SoundManager.Instance.BackgroundMusic();

            foreach (Item coin in Coins)
            {
                if (IsOnScreen(coin.Position.X))
                {
                    coin.Update(gameTime);
                }
            }

            foreach (Enemy enemy in Enemies)
            {
                if (IsOnScreen(enemy.Position.X))
                {
                    enemy.Update(gameTime);
                }
            }

            foreach (Block block in Blocks)
            {
                block.Update(gameTime);
            }

            foreach (Flagpole flagpole in Flagpoles)
            {
                if (IsOnScreen(flagpole.Position.X))
                {
                    flagpole.Update(gameTime);
                }
            }

            foreach (CheckPoint checkpoint in Checkpoints)
            {
                if (Mario.Instance.Position.X >= checkpoint.Position.X)
                {
                    checkpoint.CheckPointEnabled = true;
                }
            }

            if (Flag.IsMoving)
            {
                if (Flag.Position.Y >= Flagpoles[0].Position.Y + Flagpoles[0].CollisionRectangle.Height - GameValues.FlagMovingStoppingHeight)
                {
                    Flag.IsMoving = false;
                }
            }

            Flag.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (IBackgroundItem backgroundItem in BackgroundItems)
            {
                if (IsOnScreen(backgroundItem.Position.X))
                {
                    backgroundItem.Draw(spriteBatch, gameTime);
                }
            }

            if (IsOnScreen(Castle.Position.X))
            {
                Castle.Draw(spriteBatch, gameTime);
            }

            Mario.Instance.Draw(spriteBatch, gameTime);

            foreach (Item coin in Coins)
            {
                if (IsOnScreen(coin.Position.X))
                {
                    coin.Draw(spriteBatch, gameTime);
                }
            }

            foreach (Enemy enemy in Enemies)
            {
                if (IsOnScreen(enemy.Position.X))
                {
                    enemy.Draw(spriteBatch, gameTime);
                }
            }

            foreach (IStaticObject staticObject in StaticObjects)
            {
                staticObject.Draw(spriteBatch, gameTime);
            }

            foreach (Block block in Blocks)
            {
                block.Draw(spriteBatch, gameTime);
            }

            Flag.Draw(spriteBatch, gameTime);

        }

        // Reloads the level and reinitializes Mario to his default state.
        public void Reset()
        {
            Enemies.Clear();
            Blocks.Clear();
            Pipes.Clear();
            Coins.Clear();
            Checkpoints.Clear();
            Flagpoles.Clear();
            levelData.Clear();

            InvisibleBarrier = new InvisibleBarrier(Vector2.Zero);
            CoinRoomPosition = Vector2.Zero;

            DynamicObjects.Clear();
            StaticObjects.Clear();

            PowerUpState = false;

            Mario.Instance.ResetPlayer();

            SoundManager.Instance.gameOverPlayOnce = true;

            levelData = ParseLevelCSV(fileName);
            BuildLevel(levelData);
        }

        public void LoadNewLevel()
        {
            currentLevel++;
            if (!levelDictionary.ContainsKey(currentLevel))
            {
                currentLevel = GameValues.StartingLevel;
            }
            fileName = levelDictionary[currentLevel];
            HUD.Instance.CurrentStage = currentLevel;
            Reset();
        }

    }
}
