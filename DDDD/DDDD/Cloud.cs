using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DDDD
{

    public class Cloud
    {
        public Texture2D cloud;
        public Rectangle cloudRec;

        public int aniFrame;

        public float aniElapased;
        public float aniDelay = 150f; //speed of animation


        public Cloud(Texture2D texture, Rectangle rectangle)
        {
            cloud = texture;
            cloudRec = rectangle;
            aniFrame = 0;
        }



        public void Update(GameTime gameTime)
        {
            cloudRec.X -= 1;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(cloud, cloudRec, Color.White);
        }
    }
}
