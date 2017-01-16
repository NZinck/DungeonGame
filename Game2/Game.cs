using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Xml.Linq;
using TiledSharp;
using MonoGame;

namespace Game2
{
    /// <summary>
    /// This is the main type for your game..
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;

        TmxMap map;
        Texture2D tileset;
        Camera m_camera;

        int tileWidth;
        int tileHeight;
        int tilesWide;
        int tilesHigh;
        int tilesetTilesWide;
        int tilesetTilesHigh;

        SpriteBatch spriteBatch;

        // Keyboard states used to determine key presses
        KeyboardState keyboardState;
        MouseState mouseState;

        Player player;
        Sprite mouse;
        Vector2 relativeCursor = new Vector2();

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //Initialize Map

            //Initialize Camera
            m_camera = new Camera(new Rectangle(0, 0, (this.Window.ClientBounds.Width / 2), (this.Window.ClientBounds.Height / 2)));

            //Initialize Player
            player = new Player(Content.Load<Texture2D>("Graphics\\Player\\alienPink_hit"), new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height / 2));
            
            //Not sure what this does?
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Load the Map Resources
            map = new TmxMap("Content/Graphics/Maps/map.tmx");
            tileset = Content.Load<Texture2D>("Graphics/Tilesets/" + map.Tilesets[0].Name.ToString() + ".png");
            mouse = new Sprite(Content.Load<Texture2D>("cursor"));

            tileWidth = map.Tilesets[0].TileWidth;
            tileHeight = map.Tilesets[0].TileHeight;
                
            //Number of Tiles Wide and Hight the map is
            tilesWide = map.Width;
            tilesHigh = map.Height;

            tilesetTilesWide = tileset.Width / tileWidth;
            tilesetTilesHigh = tileset.Height / tileHeight;

            graphics.ApplyChanges();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //Update the keyboardState
            keyboardState = Keyboard.GetState();

            //Update the player
            player.Update(keyboardState);
            // Make sure that the player does not go out of bounds
            player.sprite.Position.X = MathHelper.Clamp(player.sprite.Position.X, 0, (tileWidth * tilesWide) - player.Width);
            player.sprite.Position.Y = MathHelper.Clamp(player.sprite.Position.Y, 0, (tileHeight * tilesHigh) - player.Height);

            //Update the Camera
            m_camera.Pos = new Vector2(player.sprite.Position.X, player.sprite.Position.Y);

            //Update the Mouse
            mouseState = Mouse.GetState();
            relativeCursor.X = mouseState.X + m_camera.Pos.X;
            relativeCursor.Y = mouseState.Y + m_camera.Pos.Y;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            // Start drawing
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, m_camera.viewMatrix);
            //spriteBatch.Begin();

            //Draw Background
            for (var i = 0; i < map.Layers[0].Tiles.Count; i++)
            {
                int gid = map.Layers[0].Tiles[i].Gid;

                // If empty tile, do nothing
                if (gid != 0){
                    int tileFrame = gid - 1;
                    int column = tileFrame % tilesetTilesWide;
                    int row = (int)Math.Floor((double)tileFrame / (double)tilesetTilesWide);

                    float x = (i % map.Width) * map.TileWidth;
                    float y = (float)Math.Floor(i / (double)map.Width) * map.TileHeight;

                    Rectangle tilesetRec = new Rectangle(tileWidth * column, tileHeight * row, tileWidth, tileHeight);

                    spriteBatch.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);
                }
            }

            // Draw the Player
            player.Draw(spriteBatch);

            // Draw any Objects

            //Draw the Mouse
            mouse.Position.X = relativeCursor.X;
            mouse.Position.Y = relativeCursor.Y;
            mouse.Draw(spriteBatch);

            // Stop drawing
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
