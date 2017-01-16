using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Xml.Linq;
using TiledSharp;
using MonoGame;

namespace Game2
{
    public class Sprite
    {
        public Texture2D _texture;
        public Vector2 Position;
        
         public Sprite(Texture2D texture)
        {
            _texture = texture;
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, Color.White);
        }
    }
}
