using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DDDD
{

    public class Hit
    {
        private Texture2D hit;
        public Vector2 hitPosition;

        public bool dinoHit;

        public int aniFrame;

        public int hitLeftFrame;
        public int hitRightFrame;

        public float aniElapased;
        public float aniDelay = 100f; //speed of animation


        public Hit(Texture2D texture, Vector2 vector2)
        {
            hit = texture;
            hitPosition = vector2;

            dinoHit = false;
            aniFrame = 0;
            hitRightFrame = 0;
            hitLeftFrame = 4;

        }



        public void Update(GameTime gameTime, double dinoAngle)
        {
            aniElapased += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if(dinoAngle == 0f)
            {
                if (aniElapased >= aniDelay)
                {
                    if (hitRightFrame >= 3)
                    {
                        hitRightFrame = 0;
                        dinoHit = false;
                    }
                    else
                    {
                        hitRightFrame++;
                    }
                    aniElapased = 0;

                    aniFrame = hitRightFrame;
                }
            }
            else if (dinoAngle == Math.PI)
            {
                if (aniElapased >= aniDelay)
                {
                    if (hitLeftFrame >= 7)
                    {
                        hitLeftFrame = 4;
                        dinoHit = false;
                    }
                    else
                    {
                        hitLeftFrame++;
                    }
                    aniElapased = 0;

                    aniFrame = hitLeftFrame;
                }
            }

        }

        public void Draw(SpriteBatch spriteBatch, float dinoX, float dinoY)
        {
            spriteBatch.Draw(hit, new Rectangle((int)dinoX, (int)dinoY - 147, 250, 147), new Rectangle(250 * aniFrame, 0, 250, 147), Color.White);
        }
    }
}
