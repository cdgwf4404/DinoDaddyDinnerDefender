using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DDDD
{
    public class DeadBaby
    {
        private Texture2D deadBaby;
        public Vector2 deadBabyPosition;
        public Vector2 fallingSpeed;

        GraphicsDeviceManager graphics;

        public bool babyDying;

        public int aniFrame;

        public float aniElapased;
        public float aniDelay = 100f; //speed of animation


        public DeadBaby(Texture2D texture, Vector2 vector2, GraphicsDeviceManager gdm)
        {
            deadBaby = texture;
            deadBabyPosition = vector2;

            babyDying = false;

            aniFrame = 0;
            graphics = gdm;

        }



        public void Update(GameTime gameTime, Vector2 babyPosition)
        {
            aniElapased += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (aniFrame == 0)
            {
                deadBabyPosition = babyPosition;
            }
            else
            {
                /*
                if (deadBabyPosition.Y < graphics.GraphicsDevice.DisplayMode.Height / 2 + 262)
                {
                    float index = 4;
                    fallingSpeed.Y += 0.15f * index;
                }
                else
                {
                    fallingSpeed.Y = 0f;
                }
                */
            }

            deadBabyPosition += fallingSpeed;

            if (aniElapased >= aniDelay)
            {
                if (aniFrame >= 19)
                {
                    aniFrame = 0;
                    babyDying = false;
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
            spriteBatch.Draw(deadBaby, new Rectangle((int)deadBabyPosition.X, (int)deadBabyPosition.Y, 147, 159), new Rectangle(147 * aniFrame, 0, 147, 159), Color.White);
        }
    }
}
