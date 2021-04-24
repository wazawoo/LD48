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

        //player
        Texture2D playerTexture;
        Vector2 playerPosition;
        float playerSpeed;

        //output
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        //input
        private KeyboardState keyboardState;

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
            playerPosition = new Vector2(
                gameScreenSize.X / 2,
                gameScreenSize.Y / 2
            );
            playerSpeed = 100f;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            playerTexture = Content.Load<Texture2D>("player");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            //grab input
            keyboardState = Keyboard.GetState();

            //update entities

            var kstate = Keyboard.GetState();
            var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(Keys.Up)) {
                playerPosition.Y -= playerSpeed * dt;
            }

            if (kstate.IsKeyDown(Keys.Down)) {
                playerPosition.Y += playerSpeed * dt;
            }

            if (kstate.IsKeyDown(Keys.Left))
                playerPosition.X -= playerSpeed * dt;

            if (kstate.IsKeyDown(Keys.Right))
                playerPosition.X += playerSpeed * dt;

            var width = gameScreenSize.X;
            var height = gameScreenSize.Y;

            // restrict to bounds
            if (playerPosition.X > width - playerTexture.Width / 2)
                playerPosition.X = width - playerTexture.Width / 2;
            else if (playerPosition.X < playerTexture.Width / 2)
                playerPosition.X = playerTexture.Width / 2;

            if (playerPosition.Y > height - playerTexture.Height / 2)
                playerPosition.Y = height - playerTexture.Height / 2;
            else if (playerPosition.Y < playerTexture.Height / 2)
                playerPosition.Y = playerTexture.Height / 2;

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

            spriteBatch.Draw(
                playerTexture,
                playerPosition,
                null,
                Color.White,
                0f,
                new Vector2(playerTexture.Width / 2, playerTexture.Height / 2),
                Vector2.One,
                SpriteEffects.None,
                0f
            );

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
