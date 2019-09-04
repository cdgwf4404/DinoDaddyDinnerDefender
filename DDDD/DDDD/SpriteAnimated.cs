using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDD
{
    class SpriteAnimated
    {
        private Texture2D spriteTexture;
        public Vector2 spritePosition;

        public bool animated;

        public int aniFrame;

        public float aniElapased;
        public float aniDelay = 150f; //speed of animation

        public int frame;

        public Rectangle Rectangle
        {
            get
            {

                Rectangle rectangle = new Rectangle((int)spritePosition.X, (int)spritePosition.Y, spriteTexture.Width, spriteTexture.Height);
                return rectangle;
            }
        }

        public SpriteAnimated(Texture2D texture)
        {
            spriteTexture = texture;

        }

        public SpriteAnimated(Texture2D texture, Vector2 position)
        {
            spriteTexture = texture;
            spritePosition = position;
            animated = false;

            aniFrame = 0;
            frame = 0;

        }


        public void Update(GameTime gameTime, Vector2 position)
        {
            aniElapased += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            spritePosition = position;

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
            spriteBatch.Draw(spriteTexture, new Rectangle((int)spritePosition.X, (int)spritePosition.Y, 147, 159), new Rectangle(147 * aniFrame, 0, 147, 159), Color.White);
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
