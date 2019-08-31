using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DDDD
{

    public class Win
    {
        public Texture2D win;
        public Vector2 winPosition;
        public int winWidth;
        public int winHeight;

        public int aniFrame;

        public float aniElapased;
        public float aniDelay = 120f; //speed of animation


        public Win(Texture2D texture, Vector2 vector2, int width, int height)
        {
            win = texture;
            winPosition = vector2;
            winWidth = width;
            winHeight = height;
            aniFrame = 0;
        }



        public void Update(GameTime gameTime)
        {
            aniElapased += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (aniElapased >= aniDelay)
            {
                if (aniFrame >= 16)
                {
                    aniFrame = 0;
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
            spriteBatch.Draw(win, new Rectangle((int)winPosition.X, (int)winPosition.Y, winWidth, winHeight), new Rectangle(winWidth * aniFrame, 0, winWidth, winHeight), Color.White);
        }
    }
}
