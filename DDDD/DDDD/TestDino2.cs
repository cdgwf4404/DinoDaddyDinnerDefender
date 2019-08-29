using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DDDD
{
    class TestDino2 : AnimatedSprite
    {

        public bool spaceBarPressed = false;
        public int angle = 0;
        public bool spinFlag = false;
        float spriteSpeed = 100;

        public TestDino2(Vector2 position) : base(position)
        {
            FramesPerSecond = 7;
        }

        public void LoadContent(ContentManager content)
        {
            spriteTexture = content.Load<Texture2D>("DinoSwipeRightRed");
            AddAnimation(7);

        }

        private void HandleInput(KeyboardState keystate)
        {
            if (keystate.IsKeyDown(Keys.W))
            {
                //up
                //vectors for direction in AnimatedSprite class 
                spriteDirection += new Vector2(0, -1);//coordinate system is up side down - going up is minus on Y
            }
            if (keystate.IsKeyDown(Keys.A))
            {
                //left
                spriteDirection += new Vector2(-1, 0);
            }
            if (keystate.IsKeyDown(Keys.S))
            {
                //down
                spriteDirection += new Vector2(0, 1);
            }
            if (keystate.IsKeyDown(Keys.D))
            {
                //right
                spriteDirection += new Vector2(1, 0);
            }
        }

        public override void Update(GameTime gameTime) //the update() in AnimatedSprite makes sure that we run thru our animation from frame 1 to the end of the respective sprite
        {//this update() needs to be called as well and needs to check the input

            spriteDirection = Vector2.Zero;//we want to reset it to check for the new input
            HandleInput(Keyboard.GetState());//tells my handleinput function that I pressed A
           
            //calculating delta time makes the movement frame rate independent
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            spriteDirection *= spriteSpeed;

            //now adding direction to current position
            spritePosition += (spriteDirection * deltaTime);


            
            //we don't only want to check the input but also update
            base.Update(gameTime);


        }
    }
}
