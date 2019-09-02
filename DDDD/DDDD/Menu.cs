using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDD
{
    class Menu
    {
        public Texture2D menuTexture;
        public Vector2 menuPosition;


        public Menu(Texture2D texture, Vector2 position)
        {
            menuTexture = texture;
            menuPosition = position;
       
        }



        public void Update(GameTime gameTime, Vector2 babyPosition)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(menuTexture, menuPosition, Color.White);
        }
    }
}