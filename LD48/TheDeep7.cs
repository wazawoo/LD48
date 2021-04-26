using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LD48
{
    public class TheDeep7 : Microsoft.Xna.Framework.Game
    {
        //game size and scale
        Vector2 gameScreenSize = new Vector2(320, 240);
        Vector2 tileSize = new Vector2(10, 10);

        readonly float scaleFactor = 3f;
        Matrix scaleTransormation;

        //entities
        Player player;
        Dog dog;

        TileSet tileSet;

        //output
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        //input
        private KeyboardState keyboardState;

        //init
        public TheDeep7()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // store the scale transformation that will be used for all drawing
            Vector3 scaleVector3 = new Vector3(scaleFactor, scaleFactor, 1);
            scaleTransormation = Matrix.CreateScale(scaleVector3);

            // TODO: Add your initialization logic here

            //graphics init
            graphics.PreferredBackBufferWidth = (int) (gameScreenSize.X * scaleFactor);
            graphics.PreferredBackBufferHeight = (int) (gameScreenSize.Y * scaleFactor);
            graphics.ApplyChanges();

            //tiles init (set size, then set tiles
            var width =  (int) (gameScreenSize.X / tileSize.X);
            var height = (int) (gameScreenSize.Y / tileSize.Y);

            tileSet = new TileSet(tileSize, width, height);

            player = new Player(
                position: new Vector2(
                    gameScreenSize.X / 2,
                    gameScreenSize.Y / 2),
                size: tileSize);

            dog = new Dog(
                position: new Vector2(
                    gameScreenSize.X / 2,
                    gameScreenSize.Y - 5),
                size: tileSize);

            //need a cleaner way to add behaviors
            //also, should be able to add a behavior without needing to specify owner
            //owner should always be the class that the behaviors are being added to
            //maybe behaviors cant be edited directly
            //and put an add behavior function in the entity class?

            //player behaviors
            player.behaviors.Add(
                new Gravity(
                    acceleration: new Vector2(0, 10),
                    owner: player,
                    enabled: true));

            player.behaviors.Add(
                new Friction(
                    owner: player,
                    enabled: true));

            player.behaviors.Add(
                new Move(
                    lateralAcceleration: 16f,
                    owner: player,
                    enabled: true));

            player.behaviors.Add(
                new Jump(
                    velocity: new Vector2(0, -150),
                    owner: player,
                    enabled: true));

            //dog behaviors
            dog.behaviors.Add(
                new Gravity(
                    acceleration: new Vector2(0, 10),
                    owner: dog,
                    enabled: true));

            dog.behaviors.Add(
                new Friction(
                    owner: dog,
                    enabled: true));

            dog.behaviors.Add(
                new Follow(
                    target: player,
                    lateralAcceleration: 5f,
                    owner: dog,
                    enabled: true));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            //load tiles
            string levelPath = "Content/Levels/level1.txt";
            using (Stream fileStream = TitleContainer.OpenStream(levelPath))
                tileSet.LoadTiles(Services, fileStream);

            //load entity data

            //load player data
            player.texture = Content.Load<Texture2D>("player-standing-forward");
            dog.texture = Content.Load<Texture2D>("pet-alien");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            //grab input
            keyboardState = Keyboard.GetState();

            //update tiles

            //update entities

            //update player
            player.Update(gameTime, gameScreenSize, keyboardState, tileSet);
            dog.Update(gameTime, gameScreenSize, keyboardState, tileSet);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin(
                SpriteSortMode.Immediate,
                null,
                SamplerState.PointClamp,
                null,
                null,
                null,
                scaleTransormation
            );

            //draw tiles
            tileSet.Draw(gameTime, spriteBatch);

            //draw entities

            //draw player
            player.Draw(gameTime, spriteBatch, gameScreenSize);
            dog.Draw(gameTime, spriteBatch, gameScreenSize);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
