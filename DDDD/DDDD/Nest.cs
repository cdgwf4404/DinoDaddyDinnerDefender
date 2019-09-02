using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DDDD
{
    class Nest
    {
        private Texture2D _texture;
        public Vector2 _position;
        public Boolean babyIsfull = false;
        public Boolean receivedFood = false;//collision detected
        public int foodFromDaddy;
        public Boolean hitByMeteor = false;
        public int babyHealth = 1;
        public bool grown;

        public bool animated;
        public int aniFrame;
        public float aniElapased;
        public float aniDelay = 80f; //speed of animation
        public int frame;


        public Rectangle Rectangle
        {
            get
            {

                Rectangle rectangle = new Rectangle((int)_position.X, (int)_position.Y, _texture.Width / 5, _texture.Height);
                //Console.WriteLine("nestrectangle height " + rectangle.Height + "width " + rectangle.Width);
                return rectangle;
            }
        }

        public Nest(Texture2D texture)
        {
            _texture = texture;
            foodFromDaddy = 0;
            grown = false;
        }

        public Nest(Texture2D texture, Vector2 position)
        {
            _texture = texture;
            _position = position;
            animated = false;

            aniFrame = 0;
            frame = 0;

        }


        public void Update(GameTime gameTime, Vector2 position)
        {
            aniElapased += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            _position = position;

            if (aniElapased >= aniDelay)
            {
                if (aniFrame >= frame)
                {
                    aniFrame = 0;
                    animated = false;
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
            //spriteBatch.Draw(_texture, _position, Color.White);
            spriteBatch.Draw(_texture, new Rectangle((int)_position.X, (int)_position.Y, 147, 147), new Rectangle(147 * foodFromDaddy, 0, 147, 147), Color.White);
            //spriteBatch.Draw(_texture, destinationRectangle: new Rectangle(250, 200, 50, 50), Color.White);// scaling 50%


        }

        
        public bool IsTouchingLeft(Food food)
        {
            return this.Rectangle.Right > food.foodRec.Left &&
                   this.Rectangle.Left < food.foodRec.Left &&
                   this.Rectangle.Bottom > food.foodRec.Top &&
                   this.Rectangle.Top < food.foodRec.Bottom;
        }

        public bool IsTouchingRight(Food food)
        {
            return this.Rectangle.Left < food.foodRec.Right &&
                   this.Rectangle.Right > food.foodRec.Right &&
                   this.Rectangle.Bottom > food.foodRec.Top &&
                   this.Rectangle.Top < food.foodRec.Bottom;
        }

        protected bool IsTouchingTop(Food food)
        {
            return this.Rectangle.Bottom > food.foodRec.Top &&
                   this.Rectangle.Top < food.foodRec.Top &&
                   this.Rectangle.Right > food.foodRec.Left &&
                   this.Rectangle.Left < food.foodRec.Right;
        }

        public bool IsTouchingBottom(Food food)
        {
            return this.Rectangle.Top < food.foodRec.Bottom &&
                   this.Rectangle.Bottom > food.foodRec.Bottom &&
                   this.Rectangle.Right > food.foodRec.Left &&
                   this.Rectangle.Left < food.foodRec.Right;
        }
        


    }
}