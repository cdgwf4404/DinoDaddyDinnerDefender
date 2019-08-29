using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DDDD
{

    public class Swipe
    {
        public Texture2D swipe;
        public Vector2 swipePosition;

        public bool swiping;

        public int aniFrame;

        public float aniElapased;
        public float aniDelay = 50f; //speed of animation


        public Swipe(Texture2D texture, Vector2 vector2)
        {
            swipe = texture;
            swipePosition = vector2;

            swiping = false;
            aniFrame = 0;
        }



        public void Update(GameTime gameTime, Dino dino)
        {
            aniElapased += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (aniElapased >= aniDelay)
            {
                if (aniFrame >= 6)
                {
                    aniFrame = 0;
                    swiping = false;
                    dino.spaceBarPressed = false;
                }
                else
                {
                    aniFrame++;
                }
                aniElapased = 0;
            }

        }

        public void Draw(SpriteBatch spriteBatch, float dinoX, float dinoY)
        {
            spriteBatch.Draw(swipe, new Rectangle((int)dinoX, (int)dinoY - 147, 250, 147), new Rectangle(250 * aniFrame, 0, 250, 147), Color.White);
        }
    }
}

