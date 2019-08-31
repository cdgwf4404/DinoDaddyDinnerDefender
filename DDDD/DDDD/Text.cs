using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DDDD
{
    class Text
    {
        private Texture2D textTexture;
        public Vector2 textPosition;
        private Texture2D texture2D;


        public Text(Texture2D texture2D, Vector2 vector2)
        {
            textTexture = texture2D;
            textPosition = vector2;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textTexture, textPosition, Color.White);
        }
    }
}
