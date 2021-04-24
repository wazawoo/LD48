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
        readonly float scaleFactor = 2f;
        Matrix scaleTransormation;

        //entities
        Player player;

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
            graphics.PreferredBackBufferWidth = (int)(gameScreenSize.X * scaleFactor);
            graphics.PreferredBackBufferHeight = (int)(gameScreenSize.Y * scaleFactor);
            graphics.ApplyChanges();

            //player init
            player = new Player(
                position: new Vector2(
                    gameScreenSize.X / 2,
                    gameScreenSize.Y / 2
                    ),
                speed: 100f
                );

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            //load entity data

            //load player data
            player.texture = Content.Load<Texture2D>("player");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            //grab input
            keyboardState = Keyboard.GetState();

            //update entities
            //... movement, collision

            //update player
            player.Update(gameTime, keyboardState, gameScreenSize);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin(
                SpriteSortMode.Immediate,
                null,
                null,
                null,
                null,
                null,
                scaleTransormation
            );

            //draw entities

            //draw player
            player.Draw(gameTime, spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
