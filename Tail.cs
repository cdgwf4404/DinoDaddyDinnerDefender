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
    class Tail
    {
        public Texture2D tail;

        public Vector2 tailPosition;
        public Vector2 tailSpeed;
        public bool swip;
        public bool tailKey = false;

        public Tail(Texture2D texture)
        {
            tail = texture;
            swip = false;
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tail, tailPosition, null, Color.White, 0f, Vector2.Zero, 0.1f, SpriteEffects.None, 0f);
        }


    }
}
