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
    public class Food
    {
        //Main main;

        public Texture2D food;
        public Vector2 foodPosition;
        public Vector2 foodSpeed;
        public int foodY;
        public bool foodHit;
        public bool foodOutside;
        public bool foodGround;
        Random random = new Random();

        public Color[] foodTextureData;

        public Rectangle foodRec;
        GraphicsDeviceManager graphics;

        public bool foodHitDino;


        public Rectangle Rectangle
        {
            get
            {

                Rectangle rectangle = new Rectangle((int)foodPosition.X, (int)foodPosition.Y, food.Width, food.Height);
                return rectangle;
            }
        }

        public Food(Texture2D texture, Vector2 vector, GraphicsDeviceManager gdm)
        {
            foodHitDino = false;
            graphics = gdm;
            food = texture;
            foodPosition = vector;

            foodY = random.Next(2, 5); //randomize the falling speed of meteors between 1 and 3 

            foodSpeed = new Vector2(0f, foodY);

            //foodRec = new Rectangle((int)foodPosition.X, (int)foodPosition.Y, (int)foodPosition.X + food.Width, (int)foodPosition.Y + food.Height);

            foodHit = false;

            foodOutside = false;

            foodGround = false;

            foodTextureData = new Color[food.Width * food.Height];
            food.GetData(foodTextureData);

        }

        public void Update(GraphicsDevice graphicsDevice, GameTime gameTime, double dinoAngle, /*bool foodHitDino,*/ Dino dino)
        {
            //foodRec = new Rectangle((int)foodPosition.X - food.Width/2, (int)foodPosition.Y - food.Height/2, food.Width, food.Height);
            //if (Keyboard.GetState().IsKeyDown(Keys.Space))
            //{
            if (foodHitDino == true /*&& spinFlag*//*&& foodHit == false*/)
                {
                    if (dinoAngle == 0f)
                    {
                        foodSpeed.Y = 0;
                        foodSpeed.X = 15;
                        foodHit = true;
                    }
                    else if (dinoAngle == Math.PI)
                    {
                        foodSpeed.Y = 0;
                        foodSpeed.X = -15;
                        foodHit = true;
                    }
                }
            //}

            foodPosition += foodSpeed;
           

            foodRec = new Rectangle((int)foodPosition.X - food.Width / 2, (int)foodPosition.Y - food.Height / 2, food.Width, food.Height);

            if (foodPosition.Y >= graphics.GraphicsDevice.DisplayMode.Height-40) // make the meteors disappear when hit the ground
            {
                foodGround = true;
            }
            else if (foodPosition.X >= graphics.GraphicsDevice.DisplayMode.Width && foodHit == true)
            {
                foodOutside = true;
            }
            else if (foodPosition.X <= 0 && foodHit == true)
            {
                foodOutside = true;
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(food, foodPosition, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}

