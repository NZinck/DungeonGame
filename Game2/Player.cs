using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Xml.Linq;
using TiledSharp;
using MonoGame;

namespace Game2
{
    class Player
    {
        public Sprite sprite;
        private float Speed = 8f;

        // Get the width of the player ship
        public int Width
        {
            get { return sprite._texture.Width; }
        }

        // Get the height of the player ship
        public int Height
        {
            get { return sprite._texture.Height; }
        }

        public Player(Texture2D texture, Vector2 position)
        {
            sprite = new Sprite(texture);
            sprite.Position = position;
        }

        public void Update(KeyboardState state)
        {
            // Determine Movement
            if (state.IsKeyDown(Keys.A))
            {
                sprite.Position.X -= Speed;
            }
            if (state.IsKeyDown(Keys.D))
            {
                sprite.Position.X += Speed;
            }
            if (state.IsKeyDown(Keys.W))
            {
                sprite.Position.Y -= Speed;
            }
            if (state.IsKeyDown(Keys.S))
            {
                sprite.Position.Y += Speed;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch);
        }
    }
}
