using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

//v2
namespace DDDD
{

    public class Main : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public Dino dino;
        List<Meteor> meteors = new List<Meteor>();
        List<Food> foods = new List<Food>();
        Random random = new Random();
        private Nest _nest1;
        private Yum _yum;
        private Full _full;

        

        public Texture2D volcano;
        public Texture2D nest1Texture;
        public Texture2D yumTexture;
        public Texture2D fullTexture;


        public float meteorAmount = 0;
        public float foodAmount = 0;
        public Boolean receivedFood = false;//collision detected
        public int foodFromDaddy = 0;
        public Boolean babyIsfull = false;
        public int foodMax = 20;
        public List<bool> fed = new List<bool>();


        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);

            volcano = Content.Load<Texture2D>("volcano");

            dino = new Dino(Content.Load<Texture2D>("dino"), new Vector2(GraphicsDevice.Viewport.Bounds.Width / 2, GraphicsDevice.Viewport.Bounds.Height)); //(800 / 2, 480)

            nest1Texture = Content.Load<Texture2D>("egg1");
            _nest1 = new Nest(nest1Texture);
            _nest1._position = new Vector2(100, 100);

            yumTexture = Content.Load<Texture2D>("yummy");
            _yum = new Yum(yumTexture);
            _yum._position = new Vector2(300, 300);

            fullTexture = Content.Load<Texture2D>("full");
            _full = new Full(fullTexture);
            _full._position = new Vector2(100, 100);
         
            
        }


        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        protected override void Update(GameTime gameTime)
        {

            meteorAmount += (float)gameTime.ElapsedGameTime.TotalSeconds;
            foreach(Meteor meteor in meteors)
            {
                meteor.Update(graphics.GraphicsDevice);
            }
            randomMeteor();

            foodAmount += (float)gameTime.ElapsedGameTime.TotalSeconds;
            foreach (Food food in foods)
            {
                food.Update(graphics.GraphicsDevice);
            }
            randomFood();

            dino.Update(gameTime);
           

            //_nest1.Update();

            //checking for collisions nest1 & food
            /*
            foreach (var food in foods)
            {
                if (_nest1.IsTouchingLeft(food) || _nest1.IsTouchingRight(food)) //&& tailswipe boolean - to know that the food comes from daddy)
                {
                    foodFromDaddy +=1;
                    if (foodFromDaddy == 20)
                    {
                        babyIsfull = true;
                    }
                }
            }
            */

           // for (int i=0; i<foods.Count; i++)
           foreach (var food in foods)
            {
                //Rectangle foodRectangle = new Rectangle((int)foods[i].foodPosition.X, (int)foods[i].foodPosition.Y, foods[i].food.Width, foods[i].food.Height);
                Rectangle foodRectangle = new Rectangle((int)food.foodPosition.X, (int)food.foodPosition.Y, food.food.Width, food.food.Height);
                //check collision
                //if (foodRectangle.Intersects(_nest1.Rectangle))
                
                if (_nest1.Rectangle.Intersects(foodRectangle))
                {
                    receivedFood = true;//collision
                   // fed.Add(true);
                    
                    //foodFromDaddy += 1;
 
                }
                
                
           
          
                /*

                if (_nest1.IsTouchingLeft(food))
                {
                    receivedFood = true;//collision
                    foodFromDaddy += 1;
                }
                */
                /*
                if (fed.Count > 20)
                    {
                        babyIsfull = true;

                    }
                */


                    
                

            }

         
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(volcano, new Rectangle(0, 0, 800, 480), Color.White); //background

            foreach(Meteor meteor in meteors)
            {
                meteor.Draw(spriteBatch);
            }

            foreach (Food food in foods)
            {
                food.Draw(spriteBatch);
            }

            dino.Draw(spriteBatch);
            _nest1.Draw(spriteBatch);
            

           if (receivedFood)
            {
                _yum.Draw(spriteBatch);
                receivedFood = false;
                foodFromDaddy += 1;
                Console.WriteLine("test " + foodFromDaddy);
            }
            
            

            if (foodFromDaddy == foodMax)
            {
               // babyIsfull = true;

            }


            //baby is full
            if (babyIsfull)
            {
                _full.Draw(spriteBatch);
            }
               

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void randomMeteor() //spawn meteors
        {
            int randomX = random.Next(0, 800);
            if (meteorAmount > 1) // Spawn cool down (seconds)
            {
                meteorAmount = 0;
                if (meteors.Count < 1) // Amount of meteors allowed on the screen
                {
                    meteors.Add(new Meteor(Content.Load<Texture2D>("meteor"), new Vector2(randomX, -10)));
                }
            }

            for(int i = 0; i < meteors.Count; i++)
            {
                if(meteors[i].meteorSpawn == false)
                {
                    meteors.RemoveAt(i); //Remove meteors when hit ground
                    i--;
                }
            }
        }

        public void randomFood() //spwan foods
        {
            int randomX = random.Next(0, 800);
            if (foodAmount > 1) // Spawn cool down (seconds)
            {
                foodAmount = 0;
                if (foods.Count < 5) // Amount of foods allowed on the screen
                {
                    foods.Add(new Food(Content.Load<Texture2D>("food"), new Vector2(randomX, -10)));
                }
            }

            for (int i = 0; i < foods.Count; i++)
            {
                if (foods[i].foodSpawn == false) //TODO: Collision for foods and dino
                {
                    foods.RemoveAt(i); //Remove foods when hit ground
                    i--;
                }

            }
        }
    }
}
