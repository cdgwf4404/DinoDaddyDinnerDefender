using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDD
{
    class Sprite
    {
        private Texture2D spriteTexture;
        public Vector2 spritePosition;

        public Rectangle Rectangle
        {
            get
            {

                Rectangle rectangle = new Rectangle((int)spritePosition.X, (int)spritePosition.Y, spriteTexture.Width, spriteTexture.Height);
                return rectangle;
            }
        }

        public Sprite(Texture2D texture)
        {
            spriteTexture = texture;

        }

        public Sprite(Texture2D texture, Vector2 position)
        {
            spriteTexture = texture;
            spritePosition = position;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteTexture, spritePosition, Color.White);
        }
    }
}