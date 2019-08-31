using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DDDD
{
    public class NestDead
    {
        private Texture2D dead;
        public Vector2 deadPosition;
        //public Vector2 fallingSpeed;

        GraphicsDeviceManager graphics;

        public bool dying;

        public int aniFrame;

        public float aniElapased;
        public float aniDelay = 80f; //speed of animation


        public NestDead(Texture2D texture, Vector2 vector2, GraphicsDeviceManager gdm)
        {
            dead = texture;
            deadPosition = vector2;
            dying = false;

            aniFrame = 0;
            graphics = gdm;

        }



        public void Update(GameTime gameTime, Vector2 dinoPosition)
        {
            aniElapased += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (aniFrame == 0)
            {
                deadPosition = dinoPosition;
            }
            /*
            else
            {
                if (deadPosition.Y < graphics.GraphicsDevice.DisplayMode.Height - 120)
                {
                    float index = 4;
                    fallingSpeed.Y += 0.15f * index;
                }
                else
                {
                    fallingSpeed.Y = 0f;
                }
            }
            */

            //deadPosition += fallingSpeed;

            if (aniElapased >= aniDelay)
            {
                if (aniFrame >= 20)
                {
                    aniFrame = 0;
                    dying = false;
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
            /*
            if(dying == true)
            {
                spriteBatch.Draw(dead, new Rectangle((int)deadPosition.X, (int)deadPosition.Y - 147, 250, 147), new Rectangle(250 * aniFrame, 0, 250, 147), Color.White);
            }
            else
            {
                spriteBatch.Draw(dead, new Rectangle((int)deadPosition.X, (int)deadPosition.Y - 147, 250, 147), new Rectangle(250 * 17, 0, 250, 147), Color.White);
            }
            */
            spriteBatch.Draw(dead, new Rectangle((int)deadPosition.X, (int)deadPosition.Y - 147, 250, 147), new Rectangle(250 * aniFrame, 0, 250, 147), Color.White);
        }
    }
}

