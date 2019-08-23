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
       
        public Rectangle Rectangle
                {
                    get
                    {
                       
                        Rectangle rectangle = new Rectangle((int)_position.X, (int)_position.Y, _texture.Width, _texture.Height);
                        //Console.WriteLine("nestrectangle height " + rectangle.Height + "width " + rectangle.Width);
                        return rectangle;
                    }
         }

        public Nest(Texture2D texture)
        {
            _texture = texture;
          
        }

        

        public void Update()
        {
          
           //if collision meteor - dead
            //if collision food - count and grow
          

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
            //spriteBatch.Draw(_texture, destinationRectangle: new Rectangle(250, 200, 50, 50), Color.White);// scaling 50%

           
        }

        #region Collision
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
        #endregion 


    }
}
