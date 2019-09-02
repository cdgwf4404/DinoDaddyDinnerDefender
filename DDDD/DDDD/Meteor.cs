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
    public class Meteor
    {
        public Texture2D meteor;
        public Vector2 meteorPosition;
        public Vector2 meteorSpeed;
        public bool meteorSpawn;
        public int meteorY;
        //Random random = new Random();

        GraphicsDeviceManager graphics;

        public Rectangle Rectangle
        {
            get
            {

                Rectangle rectangle = new Rectangle((int)meteorPosition.X, (int)meteorPosition.Y, meteor.Width, meteor.Height);
                //Console.WriteLine("nestrectangle height " + rectangle.Height + "width " + rectangle.Width);
                return rectangle;
            }
        }

        public Meteor(Texture2D texture, Vector2 vector, GraphicsDeviceManager gdm)
        {
            graphics = gdm;
            meteor = texture;
            meteorPosition = vector;

             

            meteorSpeed = new Vector2(0f, 2);

            meteorSpawn = true;

        }

        public void Update(GraphicsDevice graphicsDevice)
        {
            meteorPosition += meteorSpeed;

            //if(meteorPosition.Y > 500) // make the meteors disappear when hit the ground
            //if (meteorPosition.Y > graphics.GraphicsDevice.DisplayMode.Height - 100) // make the meteors disappear when hit the ground
                if (meteorPosition.Y > graphics.GraphicsDevice.DisplayMode.Height)
                {
                meteorSpawn = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(meteor, meteorPosition, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}
