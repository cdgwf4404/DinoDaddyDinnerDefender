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
    public class Sprite
    {
        //Main main;

        private Texture2D platformTexture;

        public Vector2 position;
        //public Rectangle box;

        public Color[] platformTextureData;

        public Rectangle Rectangle
        {
            get
            {
                Rectangle rectangle = new Rectangle((int)position.X, (int)position.Y, platformTexture.Width, platformTexture.Height);
                return rectangle;
            }
        }

        public Platform(Texture2D texture)
        {
            platformTexture = texture;
            //position = vector;
            platformTextureData = new Color[platformTexture.Width * platformTexture.Height];
            platformTexture.GetData(platformTextureData);
        }

        public void Update(GraphicsDevice graphicsDevice, GameTime gameTime, bool platformHitDino)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(platformTexture, position, Color.White);
        }

        public bool IsTouchingLeft(Dino dino)
        {
            return this.Rectangle.Right > dino.dinoRec.Left &&
                   this.Rectangle.Left < dino.dinoRec.Left &&
                   this.Rectangle.Bottom > dino.dinoRec.Top &&
                   this.Rectangle.Top < dino.dinoRec.Bottom;
        }

        public bool IsTouchingRight(Dino dino)
        {
            return this.Rectangle.Left < dino.dinoRec.Right &&
                   this.Rectangle.Right > dino.dinoRec.Right &&
                   this.Rectangle.Bottom > dino.dinoRec.Top &&
                   this.Rectangle.Top < dino.dinoRec.Bottom;
        }

        public bool IsTouchingTop(Dino dino)
        {
            return this.Rectangle.Bottom > dino.dinoRec.Top &&
                   this.Rectangle.Top < dino.dinoRec.Top &&
                   this.Rectangle.Right > dino.dinoRec.Left &&
                   this.Rectangle.Left < dino.dinoRec.Right;
        }

        public bool IsTouchingBottom(Dino dino)
        {
            return this.Rectangle.Top < dino.dinoRec.Bottom &&
                   this.Rectangle.Bottom > dino.dinoRec.Bottom &&
                   this.Rectangle.Right > dino.dinoRec.Left &&
                   this.Rectangle.Left < dino.dinoRec.Right;
        }
    }
}
