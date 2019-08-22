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
    public class Dino
    {
        public Texture2D dino;
        public Vector2 dinoPosition;
        public Vector2 dinoCenter;
        public Vector2 dinoJumpSpeed;
        public bool dinoJumpFlag;

        public double dinoAngle = 0;

        public Rectangle dinoRec;

        public Dino(Texture2D texture, Vector2 vector)
        {
            dino = texture;
            dinoPosition = vector;
            dinoCenter = new Vector2(dino.Width / 2, dino.Height); //(1000 / 2, 1000)
            dinoJumpFlag = true;
            dinoRec = new Rectangle((int)dinoPosition.X, (int)dinoPosition.Y, (int)dinoPosition.X + dino.Width, (int)dinoPosition.Y + dino.Height);
        }

        public void Update(GameTime gameTime)
        {
            //dino logic
            dinoPosition += dinoJumpSpeed;

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                dinoJumpSpeed.X = -3f;
                dinoAngle = Math.PI;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                dinoJumpSpeed.X = 3f;
                dinoAngle = 0f;
            }
            else
            {
                dinoJumpSpeed.X = 0f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W) && dinoJumpFlag == false)
            {
                dinoPosition.Y -= 5f;
                dinoJumpSpeed.Y = -7f; //the height a dino jumps
                dinoJumpFlag = true;
            }


            if (dinoJumpFlag == true) //dino in the air
            {
                dinoJumpSpeed.Y += 0.15f; // falling speed
            }

            if (dinoPosition.Y >= 480) //dino reaches floor
            {
                dinoJumpFlag = false;
            }

            if (dinoJumpFlag == false) //dino on the floor
            {
                dinoJumpSpeed.Y = 0f;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(dino, dinoPosition, null, Color.White, 0f, dinoCenter, 0.1f, SpriteEffects.None, 0f);
        }

    }
   
}
