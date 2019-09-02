using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DDDD
{
    class Infant
    {
        private Texture2D _texture;
        public Vector2 _position;

        public Rectangle Rectangle
        {
            get
            {

                Rectangle rectangle = new Rectangle((int)_position.X, (int)_position.Y, _texture.Width, _texture.Height);
                return rectangle;
            }
        }

        public Infant(Texture2D texture)
        {
            _texture = texture;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }
    }
}