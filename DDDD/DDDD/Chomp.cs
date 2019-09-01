using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DDDD
{
    public class Chomp
    {
        public Texture2D chomp;
        public Vector2 chompPosition;

        public bool chomping;

        public int aniFrame;

        public float aniElapased;
        public float aniDelay = 80f; //speed of animation

        public int frame;

        public Chomp(Texture2D texture, Vector2 vector2)
        {
            chomp = texture;
            chompPosition = vector2;

            chomping = false;

            aniFrame = 0;
            frame = 0;
        }



        public void Update(GameTime gameTime, Vector2 babyPosition)
        {
            aniElapased += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            chompPosition = babyPosition;

            if (aniElapased >= aniDelay)
            {
                if (aniFrame >= frame)
                {
                    aniFrame = 0;
                    chomping = false;
                }
                else
                {
                    aniFrame++;
                }
                aniElapased = 0;
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(chomp, new Rectangle((int)chompPosition.X, (int)chompPosition.Y, 147, 159), new Rectangle(147 * aniFrame, 0, 147, 159), Color.White);
        }
    }
}