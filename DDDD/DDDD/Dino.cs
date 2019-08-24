using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DDDD
{
    public class Dino
    {
        public Texture2D dino;
        public Vector2 dinoPosition;
        public Vector2 dinoCenter;
        public Vector2 dinoJumpSpeed;
        public bool dinoJumpFlag;

        public double dinoAngle = 0f;
        GraphicsDeviceManager graphics;
        public Rectangle dinoRec;

        public Color[] dinoTextureData;

        public float aniElapased;
        public float aniDelay = 150f; //speed of animation
        public float swipeDelay = 100f;

        public int aniFrame = 0;
        public int aniFrameRight = 0;
        public int aniFrameLeft = 4;

        public int swipeRight = 12;
        public int swipeLeft = 17;


        //public Rectangle aniStart = new Rectangle(1920 / 2 - 250, 1080 - 147, 250, 147);
        public Rectangle aniStart;

        public Rectangle Rectangle
        {
            get
            {

                Rectangle rectangle = new Rectangle((int)dinoPosition.X, (int)dinoPosition.Y - 147, dino.Width/22, dino.Height);
                //Console.WriteLine("nestrectangle height " + rectangle.Height + "width " + rectangle.Width);
                return rectangle;
            }
        }

        public Dino(Texture2D texture, Vector2 vector, GraphicsDeviceManager gdm)
        {
            graphics = gdm;
            dino = texture;
            dinoPosition = vector;
            //dinoCenter = new Vector2(dino.Width / 2, dino.Height); //(1000 / 2, 1000)
            dinoJumpFlag = true;
            //dinoRec = new Rectangle((int)dinoPosition.X, (int)dinoPosition.Y, dino.Width, dino.Height);
            dinoTextureData = new Color[dino.Width * dino.Height];
            dino.GetData(dinoTextureData);
        }

        public void Update(GameTime gameTime)
        {
            //dino logic
            //dinoRec = new Rectangle((int)dinoPosition.X, (int)dinoPosition.Y, dino.Width, dino.Height);

            dinoPosition += dinoJumpSpeed;

            dinoRec = new Rectangle((int)dinoPosition.X /*+ 250 + (dino.Width/2)*/, (int)dinoPosition.Y - 147 - (dino.Height/2), dino.Width, dino.Height);

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                dinoJumpSpeed.X = -7f; // dino's walking speed
                dinoAngle = Math.PI;
                aniElapased += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if(aniElapased >= aniDelay)
                {
                    if (aniFrameLeft >= 7)
                    {
                        aniFrameLeft = 4;
                    }
                    else
                    {
                        aniFrameLeft++;
                    }
                    aniElapased = 0;
                }
                aniFrame = aniFrameLeft;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                dinoJumpSpeed.X = 7f; // dino's walking speed
                dinoAngle = 0f;
                aniElapased += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (aniElapased >= aniDelay)
                {
                    if (aniFrameRight >= 3)
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
                dinoJumpSpeed.X = 0f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && dinoAngle == Math.PI)
            {
                if (aniElapased >= swipeDelay)
                {
                    if (swipeLeft >= 21)
                    {
                        swipeLeft = 17;
                    }
                    else
                    {
                        swipeLeft++;
                    }
                    aniElapased = 0;
                }
                aniFrame = swipeLeft;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Space) && dinoAngle == 0f)
            {
                if (aniElapased >= swipeDelay)
                {
                    if (swipeRight >= 16)
                    {
                        swipeRight = 12;
                    }
                    else
                    {
                        swipeRight++;
                    }
                    aniElapased = 0;
                }
                aniFrame = swipeRight;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W) && dinoJumpFlag == false)
            {
                dinoPosition.Y -= 5f;
                dinoJumpSpeed.Y = -13f; //the height a dino jumps
                dinoJumpFlag = true;
            }


            if (dinoJumpFlag == true) //dino in the air
            {
                float index = 2;
                dinoJumpSpeed.Y += 0.15f * index; // falling speed
            }

            if (dinoPosition.Y >= graphics.GraphicsDevice.DisplayMode.Height - 120) //dino reaches floor
            {
                dinoJumpFlag = false;
            }

            if (dinoJumpFlag == false) //dino on the floor
            {
                dinoJumpSpeed.Y = 0f;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(dino, dinoPosition, null, Color.White, 0f, dinoCenter, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(dino, new Rectangle((int)dinoPosition.X, (int)dinoPosition.Y - 147, 250, 147), new Rectangle(250 * aniFrame, 0, 250, 147), Color.White);
            //spriteBatch.Draw(dinoRec, new Rectangle((int)dinoPosition.X, (int)dinoPosition.Y - 147, 250, 147), new Rectangle(250 * aniFrame, 0, 250, 147), Color.White);
        }

    }
   
}
