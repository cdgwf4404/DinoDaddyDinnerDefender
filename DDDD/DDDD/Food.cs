﻿using System;
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
        Main main;

        public Texture2D food;
        public Vector2 foodPosition;
        public Vector2 foodSpeed;
        public bool foodSpawn;
        public int foodY;
        Random random = new Random();

        public Rectangle foodRec;
        GraphicsDeviceManager graphics;

        public Food(Texture2D texture, Vector2 vector, GraphicsDeviceManager gdm)
        {
            graphics = gdm;
            food = texture;
            foodPosition = vector;

            foodY = random.Next(2, 5); //randomize the falling speed of meteors between 1 and 3 

            foodSpeed = new Vector2(0f, foodY);

            foodRec = new Rectangle((int)foodPosition.X, (int)foodPosition.Y, (int)foodPosition.X + food.Width, (int)foodPosition.Y + food.Height);

            foodSpawn = true;

        }

        public void Update(GraphicsDevice graphicsDevice)
        {
            foodPosition += foodSpeed;

            if (foodPosition.Y > graphics.GraphicsDevice.DisplayMode.Height - 100) // make the meteors disappear when hit the ground
            {
                foodSpawn = false;
            }
            /*
            if (foodRec.Intersects(main.dino.dinoRec))
            {
                foodSpawn = false;
            }
            */
            //else if() TODO: get dinoRec from Main.cs
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(food, foodPosition, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}

