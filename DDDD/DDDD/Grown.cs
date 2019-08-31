using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;

namespace DDDD
{
    public class Grown
    {
        public Texture2D grown;
        public Vector2 grownPosition;
        public Vector2 grownSpeed;

        GraphicsDeviceManager graphics;

        public bool grownJumpFlag;
        public bool spwan;

        public int aniFrame = 0;

        public int aniFrameRight = 0;
        public int aniFrameLeft = 0;

        public float aniElapased;
        public float aniDelay = 150f; //speed of animation

        public int hp;

        public Rectangle Rectangle
        {
            get
            {

                Rectangle rectangle = new Rectangle((int)grownPosition.X, (int)grownPosition.Y, grown.Width / 17, grown.Height);
                return rectangle;
            }
        }

        public Grown(Texture2D texture, GraphicsDeviceManager gdm)
        {
            grown = texture;
            graphics = gdm;
            grownJumpFlag = true;
            spwan = false;
            hp = 1;
        }

        public void Update(GameTime gameTime, Vector2 dinoPosition, bool onPlatform, Rectangle meteorRec)
        {
            /*
            if(spwan == true)
            {
                grownPosition = nestPosition;
            }
            */
            grownPosition += grownSpeed;
            /*
            if(dinoPosition.Y < grownPosition.Y && grownJumpFlag == false)
            {
                grownJumpFlag = true;
                grownPosition.Y -= 5f;
                grownSpeed.Y = -16; // jump height
            }
            */
            if(dinoPosition.X + 50 < grownPosition.X)
            {
                grownSpeed.X = -3f; // dino's walking speed
  
                aniElapased += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (aniElapased >= aniDelay)
                {
                    if (aniFrameLeft >= 16)
                    {
                        aniFrameLeft = 0;
                    }
                    else
                    {
                        aniFrameLeft++;
                    }
                    aniElapased = 0;
                }
                aniFrame = aniFrameLeft;
            }

            else if(dinoPosition.X - 50 > grownPosition.X)
            {
                grownSpeed.X = 3f; // dino's walking speed

                aniElapased += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (aniElapased >= aniDelay)
                {
                    if (aniFrameRight >= 16)
                    {
                        aniFrameRight = 0;
                    }
                    else
                    {
                        aniFrameRight++;
                    }
                    aniElapased = 0;
                }
                aniFrame = aniFrameRight;
            }
            else
            {
                grownSpeed.X = 0f;
            }

            if (grownJumpFlag == true) // in the air
            {
                float index = 3;
                grownSpeed.Y += 0.15f * index; // falling speed
            }
            else if (onPlatform == false && grownPosition.Y < graphics.GraphicsDevice.DisplayMode.Height - 130)
            {
                float index = 3;
                grownSpeed.Y += 0.15f * index;
                grownJumpFlag = true;
            }

            if (grownPosition.Y >= graphics.GraphicsDevice.DisplayMode.Height / 2 + 290) //reaches floor
            {
                grownJumpFlag = false;
                grownSpeed.Y = 0f;
            }

            if (onPlatform == true)
            {
                grownJumpFlag = false;
            }

            if (meteorRec.Intersects(Rectangle))
            {
                hp -= 1;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(grown, new Rectangle((int)grownPosition.X, (int)grownPosition.Y, 237, 121), new Rectangle(237 * aniFrame, 0, 237, 121), Color.White);
        }

    }
}
