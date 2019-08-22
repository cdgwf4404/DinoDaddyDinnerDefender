using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DDDD
{
    public class Platform
    {
        public Texture2D platform;
        public Vector2 platformPosition;
        //public Rectangle box;

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)platformPosition.X, (int)platformPosition.Y, platform.Width, platform.Height);
            }
        }

        public Platform(Texture2D texture, Vector2 position)
        {
            platformPosition = position;
            platform = texture;
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(platform, platformPosition, Color.White);
        }
    }
}
