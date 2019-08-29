using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDD
{
    abstract class AnimatedSprite
    {
        protected Texture2D spriteTexture;
        protected Vector2 spritePosition;
        protected Vector2 spriteDirection = Vector2.Zero;//procected so that we can access it from the dino class
        private Rectangle[] spriteRectangles;
        private int frameIndex;
        private double timeElapsed;
        private double timeToUpdate;//how much time shall pass before we update - how fast the animation should run



        public int FramesPerSecond
        {
            set { timeToUpdate = (1f / value); } //value is set in sprite FramesPerSecond = 10
        }


        public AnimatedSprite(Vector2 position)
        {
            spritePosition = position;
        }



        public virtual void Update(GameTime gameTime)//we need to be able to override this because we want to call it from the dino first - therefore virtual
            //so when we call dino.update in the main, we want to call that dino update first, then within that dino update we call the base update
        {
            //frameIndex = 0;
            timeElapsed += gameTime.ElapsedGameTime.TotalSeconds;
            if (timeElapsed > timeToUpdate)
            {
                timeElapsed -= timeToUpdate; //steady framerate

                if (frameIndex < spriteRectangles.Length - 1)
                {
                    frameIndex++;

                }
                else
                {
                    frameIndex = 0; //loops the animation
                }

            }

            /*
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                AddAnimation(7);
            }
            */

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteTexture, spritePosition, spriteRectangles[frameIndex], Color.White);
        }

        
        public void AddAnimation(int frames)
        {
             //Width= 250single , 22 frames
            int width = spriteTexture.Width / frames;
            spriteRectangles = new Rectangle[frames];
            for (int i = 0; i < frames; i++)
            {
                spriteRectangles[i] = new Rectangle(i * width, 0, width, spriteTexture.Height);
            }
        }

            /*
        public void AddAnimation(int frames)
        {

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
//Width= 250single , 22 frames
            int width = spriteTexture.Width / frames;
            spriteRectangles = new Rectangle[frames];
            for (int i = 0; i < frames; i++)
            {
                spriteRectangles[i] = new Rectangle(i * width, 0, width, spriteTexture.Height);
            }
            }
            
        }
        */
    }
}
