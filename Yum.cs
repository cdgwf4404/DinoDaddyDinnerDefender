using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DDDD
{
    class Yum
    {
        private Texture2D _texture;
        public Vector2 _position;

        public Yum(Texture2D texture)
        {
            _texture = texture;

        }

        public void Update()
        {


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(_texture, _postion, Color.White);
            spriteBatch.Draw(_texture, destinationRectangle: new Rectangle(250, 200, 50, 50), Color.White);

        }
    }
}
