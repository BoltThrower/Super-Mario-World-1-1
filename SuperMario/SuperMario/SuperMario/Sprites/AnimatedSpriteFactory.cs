using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SuperMario
{
    public sealed class AnimatedSpriteFactory
    {
        // use the factory method
        private static readonly AnimatedSpriteFactory instance = new AnimatedSpriteFactory();

        public ContentManager content;

        private string smallMarioTextureName = "MarioSmallSheet";
        private string bigMarioTextureName = "MarioBigSheet";
        private string fireMarioTextureName = "MarioFireSheet";
        private string smallFlagMarioTextureName = "smallMarioFlagSlide";
        private string smallFlagEndMarioTextureName = "smallMarioFlagSlideEnd";
        private string bigFlagMarioTextureName = "bigMarioFlagSlide";
        private string bigFlagEndMarioTextureName = "bigMarioFlagSlideEnd";
        private string fireFlagMarioTextureName = "fireMarioFlagSlide";
        private string fireFlagEndMarioTextureName = "fireMarioFlagSlideEnd";
        private string brickBlockTextureName = "BlockBrick";
        private string brickBlockBlueTextureName = "BlockBrickBlue";
        private string questionBlockTextureName = "BlockQuestion";
        private string usedBlockTextureName = "BlockUsed";
        private string metalBlockTextureName = "BlockMetal";
        private string goombaTextureName = "EnemyGoomba";
        private string koopaTextureName = "EnemyKoopa";
        private string flippedGoombaTextureName = "EnemyFlippedGoomba";
        private string flippedKoopaShellTextureName = "EnemyFlippedKoopaShell";
        private string piranhaTextureName = "EnemyPiranhaPlant";
        private string floorTileTextureName = "FloorTile";
        private string floorTileBlueTextureName = "FloorTileBlue";
        private string pipeTextureName = "Pipe";
        private string fireFlowerTextureName = "ItemFireFlower";
        private string mushroomTextureName = "ItemMushroom";
        private string floatingCoinTextureName = "ItemFloatingCoin";
        private string rotatingCoinTextureName = "RotatingCoin";
        private string starTextureName = "ItemStar";
        private string cloudTextureName = "Cloud";
        private string bushTextureName = "Bush";
        private string hillTextureName = "Hill";
        private string brickBlockParticleTextureName = "BlockBrickParticle";
        private string castleTextureName = "Castle";
        private string flagpoleTextureName = "Flagpole";
        private string flagTextureName = "Flag";
        private string castleFlagTextureName = "CastleFlag";
        private string fireballTextureName = "Fireball";
        private string pipeHorizontalTextureName = "PipeHorizontal";
        private string pipeHorizontalConnectorTextureName = "PipeHorizontalConnector";
        private string smallMarioToBigMarioTextureName = "SmallToBigMario";
        private string crawfisLeftTextureName = "CrawfisLeft";
        private string crawfisRightTextureName = "CrawfisRight";
        private string crawfisDeadTextureName = "CrawfisDead";
        private string peachTextureName = "Peach";
        private string yoshiIdleTextureName = "YoshiIdle";
        private string yoshiGreenEggTextureName = "YoshiGreenEgg";
        private string yoshiGreenEggCrackedTextureName = "YoshiGreenEggCracked";
        private string yoshiSpawningTextureName = "YoshiSpawning";
        private string bowserLeftWalkingOpenTextureName = "BowserLeftWalkingOpen";
        private string bowserRightWalkingOpenTextureName = "BowserRightWalkingOpen";
        private string bowserLeftWalkingClosedTextureName = "BowserLeftWalkingClosed";
        private string bowserRightWalkingClosedTextureName = "BowserRightWalkingClosed";
        private string bowserDeadTextureName = "BowserDead";

        private string smallLeftIdleMarioYoshiTextureName = "SmallLeftIdleMarioYoshi";
        private string smallRightIdleMarioYoshiTextureName = "SmallRightIdleMarioYoshi";
        private string smallLeftWalkingMarioYoshiTextureName = "SmallLeftWalkingMarioYoshi";
        private string smallRightWalkingMarioYoshiTextureName = "SmallRightWalkingMarioYoshi";
        private string smallLeftJumpingMarioYoshiTextureName = "SmallLeftJumpingMarioYoshi";
        private string smallRightJumpingMarioYoshiTextureName = "SmallRightJumpingMarioYoshi";
        private string smallLeftTongueStartMarioYoshiTextureName = "SmallLeftTongueStartMarioYoshi";
        private string smallRightTongueStartMarioYoshiTextureName = "SmallRightTongueStartMarioYoshi";
        private string smallLeftTongueSmallMarioYoshiTextureName = "SmallLeftTongueSmallMarioYoshi";
        private string smallRightTongueSmallMarioYoshiTextureName = "SmallRightTongueSmallMarioYoshi";
        private string smallLeftTongueMediumMarioYoshiTextureName = "SmallLeftTongueMediumMarioYoshi";
        private string smallRightTongueMediumMarioYoshiTextureName = "SmallRightTongueMediumMarioYoshi";
        private string smallLeftTongueLargeMarioYoshiTextureName = "SmallLeftTongueLargeMarioYoshi";
        private string smallRightTongueLargeMarioYoshiTextureName = "SmallRightTongueLargeMarioYoshi";
        private string smallLeftTongueXLargeMarioYoshiTextureName = "SmallLeftTongueXLargeMarioYoshi";
        private string smallRightTongueXLargeMarioYoshiTextureName = "SmallRightTongueXLargeMarioYoshi";
        private string smallLeftFullMouthMarioYoshiTextureName = "SmallLeftFullMouthMarioYoshi";
        private string smallRightFullMouthMarioYoshiTextureName = "SmallRightFullMouthMarioYoshi";

        private string bigLeftIdleMarioYoshiTextureName = "BigLeftIdleMarioYoshi";
        private string bigRightIdleMarioYoshiTextureName = "BigRightIdleMarioYoshi";
        private string bigLeftWalkingMarioYoshiTextureName = "BigLeftWalkingMarioYoshi";
        private string bigRightWalkingMarioYoshiTextureName = "BigRightWalkingMarioYoshi";
        private string bigLeftJumpingMarioYoshiTextureName = "BigLeftJumpingMarioYoshi";
        private string bigRightJumpingMarioYoshiTextureName = "BigRightJumpingMarioYoshi";
        private string bigLeftTongueStartMarioYoshiTextureName = "BigLeftTongueStartMarioYoshi";
        private string bigRightTongueStartMarioYoshiTextureName = "BigRightTongueStartMarioYoshi";
        private string bigLeftTongueSmallMarioYoshiTextureName = "BigLeftTongueSmallMarioYoshi";
        private string bigRightTongueSmallMarioYoshiTextureName = "BigRightTongueSmallMarioYoshi";
        private string bigLeftTongueMediumMarioYoshiTextureName = "BigLeftTongueMediumMarioYoshi";
        private string bigRightTongueMediumMarioYoshiTextureName = "BigRightTongueMediumMarioYoshi";
        private string bigLeftTongueLargeMarioYoshiTextureName = "BigLeftTongueLargeMarioYoshi";
        private string bigRightTongueLargeMarioYoshiTextureName = "BigRightTongueLargeMarioYoshi";
        private string bigLeftTongueXLargeMarioYoshiTextureName = "BigLeftTongueXLargeMarioYoshi";
        private string bigRightTongueXLargeMarioYoshiTextureName = "BigRightTongueXLargeMarioYoshi";
        private string bigLeftFullMouthMarioYoshiTextureName = "BigLeftFullMouthMarioYoshi";
        private string bigRightFullMouthMarioYoshiTextureName = "BigRightFullMouthMarioYoshi";

        public static AnimatedSpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        public AnimatedSpriteFactory()
        {
        }


        private void NonLoopingAnimation(AnimatedSprite sprite, string textureName, int textureTotalFrames, int spritesFrameInTexture, Vector2 destinationPosition) 
        {
            sprite.Texture = content.Load<Texture2D>(textureName);

            int width = sprite.Texture.Width / textureTotalFrames;
            int height = sprite.Texture.Height;

            sprite.SpriteDestinationRectangle = new Rectangle((int)destinationPosition.X, (int)destinationPosition.Y, width, height);
            sprite.SpriteSourceRectangles.Add(CalculateRectangle(spritesFrameInTexture * width, 0, width, height));
        }

        private void LoopingAnimation(AnimatedSprite sprite, string textureName, int textureTotalFrames, int startingFrame, int animationTotalFrames, bool leftFacing, Vector2 destinationPosition)
        {
            sprite.Texture = content.Load<Texture2D>(textureName);

            int width = sprite.Texture.Width / textureTotalFrames;
            int height = sprite.Texture.Height;

            if (leftFacing == true)
            {
                for (int currentFrame = startingFrame; currentFrame >= animationTotalFrames || currentFrame >= 0 && currentFrame > (startingFrame - animationTotalFrames); currentFrame--)
                {
                    sprite.SpriteSourceRectangles.Add(CalculateRectangle(currentFrame * width, 0, width, height));
                }
                sprite.SpriteDestinationRectangle = new Rectangle((int)destinationPosition.X, (int)destinationPosition.Y, width, height);
            }
            else
            {
                for (int currentFrame = startingFrame; currentFrame < (animationTotalFrames + startingFrame); currentFrame++)
                {
                    sprite.SpriteSourceRectangles.Add(CalculateRectangle(width * currentFrame, 0, width, sprite.Texture.Height));
                }
                sprite.SpriteDestinationRectangle = new Rectangle((int)destinationPosition.X, (int)destinationPosition.Y, width, height);
            }
        }

        public Rectangle CalculateRectangle(int xPosition, int yPosition, int rectangleWidth, int rectangleHeight)
        {
            Rectangle calculatedRectangle = new Rectangle(xPosition, yPosition, rectangleWidth, rectangleHeight);
            return calculatedRectangle;
        }

        public AnimatedSprite BuildDeadMarioSprite()
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, smallMarioTextureName, GameValues.SpriteSmallMarioTotalTextureFrames, 0, Mario.Instance.Position);
            return sprite;
        }

#region Build Block Methods

        public AnimatedSprite BuildBrickBlockSprite(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, brickBlockTextureName, 1, 0, position);
            return sprite;
        }

        public AnimatedSprite BuildBrickBlockBlueSprite(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, brickBlockBlueTextureName, 1, 0, position);
            return sprite;
        }


        public AnimatedSprite BuildBrickBlockParticleSprite(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, brickBlockParticleTextureName, 1, 0, position);
            return sprite;
        }

        public AnimatedSprite BuildQuestionBlockSprite(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            LoopingAnimation(sprite, questionBlockTextureName, GameValues.SpriteQuestionBlockTotalTextureFrames, 0, GameValues.SpriteAnimationQuestionBlockTotalFrames, false, position);
            return sprite;
        }

        public AnimatedSprite BuildUsedBlockSprite(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, usedBlockTextureName, 1, 0, position);
            return sprite;
        }

        public AnimatedSprite BuildFloorTileSprite(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, floorTileTextureName, 1, 0, position);
            return sprite;
        }

        public AnimatedSprite BuildFloorTileBlueSprite(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, floorTileBlueTextureName, 1, 0, position);
            return sprite;
        }

        public AnimatedSprite BuildMetalBlockSprite(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, metalBlockTextureName, 1, 0, position);
            return sprite;
        }

#endregion

#region Build Small Mario Methods

        public AnimatedSprite BuildSmallLeftIdleMarioSprite()
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, smallMarioTextureName, GameValues.SpriteSmallMarioTotalTextureFrames, 6, Mario.Instance.Position);
            return sprite;
        }
 
        public AnimatedSprite BuildSmallLeftJumpingMarioSprite()
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, smallMarioTextureName, GameValues.SpriteSmallMarioTotalTextureFrames, 1, Mario.Instance.Position);
            return sprite;
        }

        public AnimatedSprite BuildSmallLeftWalkingMarioSprite()
        {
            AnimatedSprite sprite = new AnimatedSprite();
            LoopingAnimation(sprite, smallMarioTextureName, GameValues.SpriteSmallMarioTotalTextureFrames, 5, GameValues.SpriteAnimationRunningMarioTotalFrames, true, Mario.Instance.Position);
            return sprite;
        }

        public AnimatedSprite BuildSmallLeftMarioToBigMarioSprite()
        {
            AnimatedSprite sprite = new AnimatedSprite();
            LoopingAnimation(sprite, smallMarioToBigMarioTextureName, GameValues.SpriteSmallMarioToBigTotalTextureFrames, 11, GameValues.SpriteAnimationSmallMarioToBigTotalFrames, true, Mario.Instance.Position);
            return sprite;
        }

        public AnimatedSprite BuildSmallRightIdleMarioSprite()
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, smallMarioTextureName, GameValues.SpriteSmallMarioTotalTextureFrames, 7, Mario.Instance.Position);
            return sprite;
        }

        public AnimatedSprite BuildSmallRightJumpingMarioSprite()
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, smallMarioTextureName, GameValues.SpriteSmallMarioTotalTextureFrames, 12, Mario.Instance.Position);
            return sprite;
        }

        public AnimatedSprite BuildSmallRightWalkingMarioSprite()
        {
            AnimatedSprite sprite = new AnimatedSprite();
            LoopingAnimation(sprite, smallMarioTextureName, GameValues.SpriteSmallMarioTotalTextureFrames, 8, GameValues.SpriteAnimationRunningMarioTotalFrames, false, Mario.Instance.Position);
            return sprite;
        }

        public AnimatedSprite BuildSmallRightMarioToBigMarioSprite()
        {
            AnimatedSprite sprite = new AnimatedSprite();
            LoopingAnimation(sprite, smallMarioToBigMarioTextureName, GameValues.SpriteSmallMarioToBigTotalTextureFrames, 12, GameValues.SpriteAnimationSmallMarioToBigTotalFrames, false, Mario.Instance.Position);
            return sprite;
        }

        public AnimatedSprite BuildSmallFlagSlideMarioSprite()
        {
            AnimatedSprite sprite = new AnimatedSprite();
            LoopingAnimation(sprite, smallFlagMarioTextureName, GameValues.SpriteSmallFlagMarioTotalTextureFrames, 0, GameValues.SpriteSmallFlagMarioTotalTextureFrames, false, Mario.Instance.Position);
            return sprite;
        }

        public AnimatedSprite BuildSmallFlagSlideEndMarioSprite()
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, smallFlagEndMarioTextureName, 1, 0, Mario.Instance.Position);
            return sprite;
        }

#endregion

#region Build Big Mario Methods

        public AnimatedSprite BuildBigLeftIdleMarioSprite()
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, bigMarioTextureName, GameValues.SpriteBigMarioTotalTextureFrames, 6, Mario.Instance.Position);
            return sprite;
        }

        public AnimatedSprite BuildBigLeftJumpingMarioSprite()
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, bigMarioTextureName, GameValues.SpriteBigMarioTotalTextureFrames, 1, Mario.Instance.Position);
            return sprite;
        }

        public AnimatedSprite BuildBigLeftWalkingMarioSprite()
        {
            AnimatedSprite sprite = new AnimatedSprite();
            LoopingAnimation(sprite, bigMarioTextureName, GameValues.SpriteBigMarioTotalTextureFrames, 5, GameValues.SpriteAnimationRunningMarioTotalFrames, true, Mario.Instance.Position);
            return sprite;
        }

        public AnimatedSprite BuildBigLeftCrouchingMarioSprite()
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, bigMarioTextureName, GameValues.SpriteBigMarioTotalTextureFrames, 0, Mario.Instance.Position);
            return sprite;
        }

        public AnimatedSprite BuildBigRightIdleMarioSprite()
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, bigMarioTextureName, GameValues.SpriteBigMarioTotalTextureFrames, 7, Mario.Instance.Position);
            return sprite;
        }

        public AnimatedSprite BuildBigRightJumpingMarioSprite()
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, bigMarioTextureName, GameValues.SpriteBigMarioTotalTextureFrames, 12, Mario.Instance.Position);
            return sprite;
        }

        public AnimatedSprite BuildBigRightWalkingMarioSprite()
        {
            AnimatedSprite sprite = new AnimatedSprite();
            LoopingAnimation(sprite, bigMarioTextureName, GameValues.SpriteBigMarioTotalTextureFrames, 8, GameValues.SpriteAnimationRunningMarioTotalFrames, false, Mario.Instance.Position);
            return sprite;
        }

        public AnimatedSprite BuildBigRightCrouchingMarioSprite()
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, bigMarioTextureName, GameValues.SpriteBigMarioTotalTextureFrames, 13, Mario.Instance.Position);
            return sprite;
        }

        public AnimatedSprite BuildBigFlagSlideMarioSprite()
        {
            AnimatedSprite sprite = new AnimatedSprite();
            LoopingAnimation(sprite, bigFlagMarioTextureName, GameValues.SpriteBigFlagMarioTotalTextureFrames, 0, GameValues.SpriteBigFlagMarioTotalTextureFrames, false, Mario.Instance.Position);
            return sprite;
        }

        public AnimatedSprite BuildBigFlagSlideEndMarioSprite()
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, bigFlagEndMarioTextureName, 1, 0, Mario.Instance.Position);
            return sprite;
        }

#endregion

#region Build Fire Mario Methods

        public AnimatedSprite BuildFireLeftIdleMarioSprite()
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, fireMarioTextureName, GameValues.SpriteFireMarioTotalTextureFrames, 7, Mario.Instance.Position);
            return sprite;
        }

        public AnimatedSprite BuildFireLeftJumpingMarioSprite()
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, fireMarioTextureName, GameValues.SpriteFireMarioTotalTextureFrames, 1, Mario.Instance.Position);
            return sprite;
        }

        public AnimatedSprite BuildFireLeftWalkingMarioSprite()
        {
            AnimatedSprite sprite = new AnimatedSprite();
            LoopingAnimation(sprite, fireMarioTextureName, GameValues.SpriteFireMarioTotalTextureFrames, 6, GameValues.SpriteAnimationRunningMarioTotalFrames, true, Mario.Instance.Position);
            return sprite;
        }

        public AnimatedSprite BuildFireLeftCrouchingMarioSprite()
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, fireMarioTextureName, GameValues.SpriteFireMarioTotalTextureFrames, 0, Mario.Instance.Position);
            return sprite;
        }

        public AnimatedSprite BuildFireLeftFireballThrowingMarioSprite()
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, fireMarioTextureName, GameValues.SpriteFireMarioTotalTextureFrames, 3, Mario.Instance.Position);
            return sprite;
        }

        public AnimatedSprite BuildFireRightIdleMarioSprite()
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, fireMarioTextureName, GameValues.SpriteFireMarioTotalTextureFrames, 8, Mario.Instance.Position);
            return sprite;
        }

        public AnimatedSprite BuildFireRightJumpingMarioSprite()
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, fireMarioTextureName, GameValues.SpriteFireMarioTotalTextureFrames, 14, Mario.Instance.Position);
            return sprite;
        }

        public AnimatedSprite BuildFireRightWalkingMarioSprite()
        {
            AnimatedSprite sprite = new AnimatedSprite();
            LoopingAnimation(sprite, fireMarioTextureName, GameValues.SpriteFireMarioTotalTextureFrames, 9, GameValues.SpriteAnimationRunningMarioTotalFrames, false, Mario.Instance.Position);
            return sprite;
        }

        public AnimatedSprite BuildFireRightCrouchingMarioSprite()
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, fireMarioTextureName, GameValues.SpriteFireMarioTotalTextureFrames, 15, Mario.Instance.Position);
            return sprite;
        }

        public AnimatedSprite BuildFireRightFireballThrowingMarioSprite()
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, fireMarioTextureName, GameValues.SpriteFireMarioTotalTextureFrames, 12, Mario.Instance.Position);
            return sprite;
        }

        public AnimatedSprite BuildFireFlagSlideMarioSprite()
        {
            AnimatedSprite sprite = new AnimatedSprite();
            LoopingAnimation(sprite, fireFlagMarioTextureName, GameValues.SpriteFireFlagMarioTotalTextureFrames, 0, GameValues.SpriteFireFlagMarioTotalTextureFrames, false, Mario.Instance.Position);
            return sprite;
        }

        public AnimatedSprite BuildFireFlagSlideEndMarioSprite()
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, fireFlagEndMarioTextureName, 1, 0, Mario.Instance.Position);
            return sprite;
        }

#endregion

#region Build Enemy Methods
        public AnimatedSprite BuildEnemyWalkingGoombaSprite(Vector2 enemyPosition)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            LoopingAnimation(sprite, goombaTextureName, GameValues.SpriteGoombaTotalTextureFrames, 0, GameValues.SpriteAnimationGoombaTotalFrames, false, enemyPosition);
            return sprite;
        }
        public AnimatedSprite BuildEnemyDeadGoombaSprite(Vector2 enemyPosition)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, goombaTextureName, GameValues.SpriteGoombaTotalTextureFrames, 2, enemyPosition);
            return sprite;
        }
        public AnimatedSprite BuildEnemyLeftWalkingKoopaSprite(Vector2 enemyPosition)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            LoopingAnimation(sprite, koopaTextureName, GameValues.SpriteKoopaTotalTextureFrames, 1, GameValues.SpriteAnimationKoopaTotalFrames, true, enemyPosition);
            return sprite;
        }
        public AnimatedSprite BuildEnemyRightWalkingKoopaSprite(Vector2 enemyPosition)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            LoopingAnimation(sprite, koopaTextureName, GameValues.SpriteKoopaTotalTextureFrames, 2, GameValues.SpriteAnimationKoopaTotalFrames, false, enemyPosition);
            return sprite;
        }
        public AnimatedSprite BuildEnemyComingOutOfShellKoopaSprite(Vector2 enemyPosition)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, koopaTextureName, GameValues.SpriteKoopaTotalTextureFrames, 4, enemyPosition);
            return sprite;
        }
        public AnimatedSprite BuildEnemyHidingInsideShellKoopaSprite(Vector2 enemyPosition)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, koopaTextureName, GameValues.SpriteKoopaTotalTextureFrames, 5, enemyPosition);
            return sprite;
        }
        public AnimatedSprite BuildEnemyPiranhaPlant(Vector2 enemyPosition)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            LoopingAnimation(sprite, piranhaTextureName, GameValues.SpritePiranhaTotalTextureFrames, 0, GameValues.SpriteAnimationPiranhaTotalFrames, false, enemyPosition);
            return sprite;
        }
        public AnimatedSprite BuildEnemyFlippedGoombaSprite(Vector2 enemyPosition)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, flippedGoombaTextureName, 1, 0, enemyPosition);
            return sprite;
        }
        public AnimatedSprite BuildEnemyFlippedKoopaShellSprite(Vector2 enemyPosition)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, flippedKoopaShellTextureName, 1, 0, enemyPosition);
            return sprite;
        }
#endregion

#region Build Pipe Methods
        public AnimatedSprite BuildPipeTopSprite(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, pipeTextureName, GameValues.SpritePipeTotalTextureFrames, 0, position);
            return sprite;
        }

        public AnimatedSprite BuildPipeBaseSprite(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, pipeTextureName, GameValues.SpritePipeTotalTextureFrames, 1, position);
            return sprite;
        }

        public AnimatedSprite BuildPipeHorizontalSprite(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, pipeHorizontalTextureName, 1, 0, position);
            return sprite;
        }

        public AnimatedSprite BuildPipeHorizontalConnectorSprite(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, pipeHorizontalConnectorTextureName, 1, 0, position);
            return sprite;
        }
#endregion

#region Build Items/Powerups

        public AnimatedSprite BuildFireFlowerSprite(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            LoopingAnimation(sprite, fireFlowerTextureName, 4, 0, 4, false, position);
            return sprite;
        }

        public AnimatedSprite BuildFloatingCoinSprite(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            LoopingAnimation(sprite, floatingCoinTextureName, 4, 0, 4, false, position);
            return sprite;
        }

        public AnimatedSprite BuildRotatingCoinSprite(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            LoopingAnimation(sprite, rotatingCoinTextureName, 4, 0, 4, false, position);
            return sprite;
        }

        public AnimatedSprite BuildMushroomSprite(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, mushroomTextureName, GameValues.SpriteMushroomTotalTextureFrames, 1, position);
            return sprite;
        }

        public AnimatedSprite BuildMushroom1UpSprite(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, mushroomTextureName, GameValues.SpriteMushroomTotalTextureFrames, 0, position);
            return sprite;
        }

        public AnimatedSprite BuildStarSprite(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            LoopingAnimation(sprite, starTextureName, 4, 0, 4, false, position);
            return sprite;
        }

        public AnimatedSprite BuildPeachSprite(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, peachTextureName, 1, 0, position);
            return sprite;
        }
#endregion  

#region Build Background Features

        public AnimatedSprite BuildBackgroundBlueSprite(Vector2 position, int screenWidth, int screenHeight)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            sprite.Texture = content.Load<Texture2D>("Background");
            sprite.SpriteSourceRectangles.Add(new Rectangle(0, 0, screenWidth, screenHeight));
            sprite.SpriteDestinationRectangle = new Rectangle((int)position.X, 0, screenWidth, screenHeight);
            return sprite;
        }

        public AnimatedSprite BuildBackgroundBlackSprite(Vector2 position, int screenWidth, int screenHeight)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            sprite.Texture = content.Load<Texture2D>("BackgroundBlack");
            sprite.SpriteSourceRectangles.Add(new Rectangle(0, 0, screenWidth, screenHeight));
            sprite.SpriteDestinationRectangle = new Rectangle((int)position.X, 0, screenWidth, screenHeight);
            return sprite;
        }

        public AnimatedSprite BuildBackgroundHillSprite(Vector2 position, int hillFrame)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, hillTextureName, GameValues.SpriteHillTotalTextureFrames, hillFrame, position);
            return sprite;
        }

        public AnimatedSprite BuildBackgroundBushSprite(Vector2 position, int bushFrame)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, bushTextureName, GameValues.SpriteBushTotalTextureFrames, bushFrame, position);
            return sprite;
        }

        public AnimatedSprite BuildBackgroundCloudSprite(Vector2 position, int cloudFrame)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, cloudTextureName, GameValues.SpriteCloudTotalTextureFrames, cloudFrame, position);
            return sprite;
        }

        public AnimatedSprite BuildBackgroundCastleSprite(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, castleTextureName, 1, 0, position);
            return sprite;
        }

#endregion

        public AnimatedSprite BuildFlagpoleSprite(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, flagpoleTextureName, 1, 0, position);
            return sprite;            
        }

        public AnimatedSprite BuildFlagSprite(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, flagTextureName, 1, 0, position);
            return sprite;
        }

        public AnimatedSprite BuildFireballSprite(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            LoopingAnimation(sprite, fireballTextureName, GameValues.SpriteFireBallTotalTextureFrames, 0, GameValues.SpriteAnimationFireBallTotalFrames, false, position);
            return sprite;
        }

        public AnimatedSprite BuildCastleFlag(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, castleFlagTextureName, 1, 0, position);
            return sprite;
        }

#region Game Transition Screen Sprites

        public AnimatedSprite BuildMarioLifeSprite(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, smallMarioTextureName, GameValues.SpriteSmallMarioTotalTextureFrames, 7, position);
            return sprite;
        }

#endregion

#region Crawfis Sprites
        public AnimatedSprite BuildCrawfisLeftSprite(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, crawfisLeftTextureName, 1, 0, position);
            return sprite;
        }

        public AnimatedSprite BuildCrawfisRightSprite(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, crawfisRightTextureName, 1, 0, position);
            return sprite;
        }

        public AnimatedSprite BuildCrawfisDeadSprite(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, crawfisDeadTextureName, 1, 0, position);
            return sprite;
        }
#endregion

#region Yoshi Sprites
        
        public AnimatedSprite BuildYoshiIdleSprite(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            LoopingAnimation(sprite, yoshiIdleTextureName, GameValues.SpriteYoshiIdleTotalTextureFrames, 0, GameValues.SpriteAnimationYoshiIdleTotalFrames, false, position);
            return sprite;
        }

        public AnimatedSprite BuildYoshiGreenEggSprite(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, yoshiGreenEggTextureName, 1, 0, position);
            return sprite;
        }

        public AnimatedSprite BuildYoshiGreenEggCrackedSprite(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, yoshiGreenEggCrackedTextureName, 1, 0, position);
            return sprite;
        }

        public AnimatedSprite BuildYoshiSpawningSprite(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            LoopingAnimation(sprite, yoshiSpawningTextureName, GameValues.SpriteYoshiSpawningTotalTextureFrames, 0, GameValues.SpriteAnimationYoshiSpawningTotalFrames, false, position);
            return sprite;
        }
#endregion

#region Small MarioYoshi Sprites
        public AnimatedSprite BuildSmallLeftIdleMarioYoshiSprite(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, smallLeftIdleMarioYoshiTextureName, 1, 0, position);
            return sprite;
        }

        public AnimatedSprite BuildSmallRightIdleMarioYoshiSprite(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, smallRightIdleMarioYoshiTextureName, 1, 0, position);
            return sprite;
        }

        public AnimatedSprite BuildSmallLeftWalkingMarioYoshiSprite(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            LoopingAnimation(sprite, smallLeftWalkingMarioYoshiTextureName, GameValues.SpriteSmallLeftWalkingMarioYoshiTotalTextureFrames, 0, GameValues.SpriteAnimationSmallLeftWalkingMarioYoshiTotalFrames, false, position);
            return sprite;
        }

        public AnimatedSprite BuildSmallRightWalkingMarioYoshiSprite(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            LoopingAnimation(sprite, smallRightWalkingMarioYoshiTextureName, GameValues.SpriteSmallRightWalkingMarioYoshiTotalTextureFrames, 0, GameValues.SpriteAnimationSmallRightWalkingMarioYoshiTotalFrames, false, position);
            return sprite;
        }

        public AnimatedSprite BuildSmallLeftJumpingMarioYoshiSprite(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, smallLeftJumpingMarioYoshiTextureName, 1, 0, position);
            return sprite;
        }

        public AnimatedSprite BuildSmallRightJumpingMarioYoshiSprite(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, smallRightJumpingMarioYoshiTextureName, 1, 0, position);
            return sprite;
        }

        public AnimatedSprite BuildSmallLeftTongueStartMarioYoshi(Vector2 position, int frame)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, smallLeftTongueStartMarioYoshiTextureName, 2, frame, position);
            return sprite;
        }

        public AnimatedSprite BuildSmallRightTongueStartMarioYoshi(Vector2 position, int frame)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, smallRightTongueStartMarioYoshiTextureName, 2, frame, position);
            return sprite;
        }

        public AnimatedSprite BuildSmallLeftTongueSmallMarioYoshi(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, smallLeftTongueSmallMarioYoshiTextureName, 1, 0, position);
            return sprite;
        }

        public AnimatedSprite BuildSmallRightTongueSmallMarioYoshi(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, smallRightTongueSmallMarioYoshiTextureName, 1, 0, position);
            return sprite;
        }

        public AnimatedSprite BuildSmallLeftTongueMediumMarioYoshi(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, smallLeftTongueMediumMarioYoshiTextureName, 1, 0, position);
            return sprite;
        }

        public AnimatedSprite BuildSmallRightTongueMediumMarioYoshi(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, smallRightTongueMediumMarioYoshiTextureName, 1, 0, position);
            return sprite;
        }

        public AnimatedSprite BuildSmallLeftTongueLargeMarioYoshi(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, smallLeftTongueLargeMarioYoshiTextureName, 1, 0, position);
            return sprite;
        }

        public AnimatedSprite BuildSmallRightTongueLargeMarioYoshi(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, smallRightTongueLargeMarioYoshiTextureName, 1, 0, position);
            return sprite;
        }

        public AnimatedSprite BuildSmallLeftTongueXLargeMarioYoshi(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, smallLeftTongueXLargeMarioYoshiTextureName, 1, 0, position);
            return sprite;
        }

        public AnimatedSprite BuildSmallRightTongueXLargeMarioYoshi(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, smallRightTongueXLargeMarioYoshiTextureName, 1, 0, position);
            return sprite;
        }

        public AnimatedSprite BuildSmallLeftFullMouthMarioYoshi(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, smallLeftFullMouthMarioYoshiTextureName, 1, 0, position);
            return sprite;
        }

        public AnimatedSprite BuildSmallRightFullMouthMarioYoshi(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, smallRightFullMouthMarioYoshiTextureName, 1, 0, position);
            return sprite;
        }
#endregion

#region Big MarioYoshi Sprites
        public AnimatedSprite BuildBigLeftIdleMarioYoshiSprite(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, bigLeftIdleMarioYoshiTextureName, 1, 0, position);
            return sprite;
        }

        public AnimatedSprite BuildBigRightIdleMarioYoshiSprite(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, bigRightIdleMarioYoshiTextureName, 1, 0, position);
            return sprite;
        }

        public AnimatedSprite BuildBigLeftWalkingMarioYoshiSprite(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            LoopingAnimation(sprite, bigLeftWalkingMarioYoshiTextureName, GameValues.SpriteBigLeftWalkingMarioYoshiTotalTextureFames, 0, GameValues.SpriteAnimationBigLeftWalkingMarioYoshiTotalFrames, false, position);
            return sprite;
        }

        public AnimatedSprite BuildBigRightWalkingMarioYoshiSprite(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            LoopingAnimation(sprite, bigRightWalkingMarioYoshiTextureName, GameValues.SpriteBigRightWalkingMarioYoshiTotalTextureFames, 0, GameValues.SpriteAnimationBigRightWalkingMarioYoshiTotalFrames, false, position);
            return sprite;
        }

        public AnimatedSprite BuildBigLeftJumpingMarioYoshiSprite(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, bigLeftJumpingMarioYoshiTextureName, 1, 0, position);
            return sprite;
        }

        public AnimatedSprite BuildBigRightJumpingMarioYoshiSprite(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, bigRightJumpingMarioYoshiTextureName, 1, 0, position);
            return sprite;
        }

        public AnimatedSprite BuildBigLeftTongueStartMarioYoshi(Vector2 position, int frame)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, bigLeftTongueStartMarioYoshiTextureName, 2, frame, position);
            return sprite;
        }

        public AnimatedSprite BuildBigRightTongueStartMarioYoshi(Vector2 position, int frame)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, bigRightTongueStartMarioYoshiTextureName, 2, frame, position);
            return sprite;
        }

        public AnimatedSprite BuildBigLeftTongueSmallMarioYoshi(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, bigLeftTongueSmallMarioYoshiTextureName, 1, 0, position);
            return sprite;
        }

        public AnimatedSprite BuildBigRightTongueSmallMarioYoshi(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, bigRightTongueSmallMarioYoshiTextureName, 1, 0, position);
            return sprite;
        }

        public AnimatedSprite BuildBigLeftTongueMediumMarioYoshi(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, bigLeftTongueMediumMarioYoshiTextureName, 1, 0, position);
            return sprite;
        }

        public AnimatedSprite BuildBigRightTongueMediumMarioYoshi(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, bigRightTongueMediumMarioYoshiTextureName, 1, 0, position);
            return sprite;
        }

        public AnimatedSprite BuildBigLeftTongueLargeMarioYoshi(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, bigLeftTongueLargeMarioYoshiTextureName, 1, 0, position);
            return sprite;
        }

        public AnimatedSprite BuildBigRightTongueLargeMarioYoshi(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, bigRightTongueLargeMarioYoshiTextureName, 1, 0, position);
            return sprite;
        }

        public AnimatedSprite BuildBigLeftTongueXLargeMarioYoshi(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, bigLeftTongueXLargeMarioYoshiTextureName, 1, 0, position);
            return sprite;
        }

        public AnimatedSprite BuildBigRightTongueXLargeMarioYoshi(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, bigRightTongueXLargeMarioYoshiTextureName, 1, 0, position);
            return sprite;
        }

        public AnimatedSprite BuildBigLeftFullMouthMarioYoshi(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, bigLeftFullMouthMarioYoshiTextureName, 1, 0, position);
            return sprite;
        }

        public AnimatedSprite BuildBigRightFullMouthMarioYoshi(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, bigRightFullMouthMarioYoshiTextureName, 1, 0, position);
            return sprite;
        }
#endregion

#region Bowser Sprites
        public AnimatedSprite BuildBowserLeftWalkingOpenSprite(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            LoopingAnimation(sprite, bowserLeftWalkingOpenTextureName, GameValues.SpriteBowserWalkingTotalTextureFrames, 0, GameValues.SpriteAnimationBowserWalkingTotalFrames, false, position);
            return sprite;
        }

        public AnimatedSprite BuildBowserRightWalkingOpenSprite(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            LoopingAnimation(sprite, bowserRightWalkingOpenTextureName, GameValues.SpriteBowserWalkingTotalTextureFrames, 0, GameValues.SpriteAnimationBowserWalkingTotalFrames, false, position);
            return sprite;
        }

        public AnimatedSprite BuildBowserLeftWalkingClosedSprite(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            LoopingAnimation(sprite, bowserLeftWalkingClosedTextureName, GameValues.SpriteBowserWalkingTotalTextureFrames, 0, GameValues.SpriteAnimationBowserWalkingTotalFrames, false, position);
            return sprite;
        }

        public AnimatedSprite BuildBowserRightWalkingClosedSprite(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            LoopingAnimation(sprite, bowserRightWalkingClosedTextureName, GameValues.SpriteBowserWalkingTotalTextureFrames, 0, GameValues.SpriteAnimationBowserWalkingTotalFrames, false, position);
            return sprite;
        }

        public AnimatedSprite BuildBowserDeadSprite(Vector2 position)
        {
            AnimatedSprite sprite = new AnimatedSprite();
            NonLoopingAnimation(sprite, bowserDeadTextureName, 1, 0, position);
            return sprite;
        }
#endregion
    }
}