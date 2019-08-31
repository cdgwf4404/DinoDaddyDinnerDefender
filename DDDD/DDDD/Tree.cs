using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DDDD
{

    public class Tree
    {
        public Texture2D tree;

        public int aniFrame;

        public float aniElapased;
        public float aniDelay = 150f; //speed of animation


        public Tree(Texture2D texture)
        {
            tree = texture;
            aniFrame = 0;
        }



        public void Update(GameTime gameTime)
        {
            aniElapased += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (aniElapased >= aniDelay)
            {
                if (aniFrame >= 5)
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
            spriteBatch.Draw(tree, new Rectangle(0, 0, 1920, 1080), new Rectangle(1920 * aniFrame, 0, 1920, 1080), Color.White);
        }
    }
}