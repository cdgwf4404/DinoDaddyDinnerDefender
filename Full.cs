using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DDDD
{
    class Full
    {
        private Texture2D _texture;
        public Vector2 _position;

        public Rectangle Rectangle
        {
            get
            {

                Rectangle rectangle = new Rectangle((int)_position.X, (int)_position.Y, _texture.Width, _texture.Height);
                Console.WriteLine("nestrectangle height " + rectangle.Height + "width " + rectangle.Width);
                return rectangle;
            }
        }

        public Full(Texture2D texture)
        {
            _texture = texture;

        }

        public void Update()
        {

            //if collision meteor - dead
            //if collision food - count and grow


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
            //spriteBatch.Draw(_texture, destinationRectangle: new Rectangle(250, 200, 50, 50), Color.White);// scaling 50%


        }
    }
}