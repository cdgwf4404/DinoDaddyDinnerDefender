using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DDDD
{
    public class Piece
    {
        private Texture2D piece;
        public Vector2 piecePosition;

        public bool destoried;

        public float aniElapased;
        public float aniDelay = 800f; //speed of animation


        public Piece(Texture2D texture, Vector2 vector2)
        {
            piece = texture;
            piecePosition = vector2;
            destoried = false;

        }



        public void Update(GameTime gameTime)
        {
            aniElapased += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (aniElapased >= aniDelay)
            {
                destoried = false;
                aniElapased = 0;
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(destoried == true)
            {
                spriteBatch.Draw(piece, piecePosition, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }
        }
    }
}
