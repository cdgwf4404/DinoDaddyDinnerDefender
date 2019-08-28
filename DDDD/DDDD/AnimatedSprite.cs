using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        private Vector2 spritePosition;
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



        public void Update (GameTime gameTime) 
        {
            timeElapsed += gameTime.ElapsedGameTime.TotalSeconds;
            if (timeElapsed > timeToUpdate)
            {
                timeElapsed -= timeToUpdate; //steady framerate

                if (frameIndex < spriteRectangles.Length-1)
                {
                    frameIndex++;
                
                }
                else
                {
                    frameIndex = 0; //loops the animation
                }

            }


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteTexture, spritePosition, spriteRectangles[frameIndex], Color.White);
        }

        public void AddAnimation (int frames)
        {
            //Width= 250single , 22 frames
            int width = spriteTexture.Width / 22;
            spriteRectangles = new Rectangle[frames];
            for (int i = 0; i < frames; i++)
            {
                spriteRectangles[i] = new Rectangle(i * width, 0, width, spriteTexture.Height);
            }

            
        }
    }
}
