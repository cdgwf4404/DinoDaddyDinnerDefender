using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

//all 3 branches - platform, tail & nest
namespace DDDD
{

    public class Main : Game
    {
        public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public Dino dino;
        List<Meteor> meteors = new List<Meteor>();
        List<Food> foods = new List<Food>();
        List<Tail> tails = new List<Tail>();

        private Nest _nest1;
        private Yum _yum;
        private Full _full;
        public int foodIndex = 0;

        Random random = new Random();

        public Texture2D volcano;
        public Texture2D nest1Texture;
        public Texture2D yumTexture;
        public Texture2D fullTexture;

        public float meteorAmount = 0;
        public float foodAmount = 0;
        public int foodFromDaddy = 0;
        public Boolean receivedFood = false;//collision detected
        public Boolean babyIsfull = false;
        public int foodMax = 3;

        TimeSpan foodTimeout = TimeSpan.FromSeconds(3);


       
        

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);

            volcano = Content.Load<Texture2D>("volcano");

            dino = new Dino(Content.Load<Texture2D>("dino"), new Vector2(GraphicsDevice.DisplayMode.Width / 2, GraphicsDevice.DisplayMode.Height), graphics);

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
            randomFood(gameTime);

            dino.Update(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                swip();
            }
            tailAction();

            for (int i = 0; i < foods.Count; i++)
            //foreach (var food in foods)
            {
                Rectangle foodRectangle = new Rectangle((int)foods[i].foodPosition.X, (int)foods[i].foodPosition.Y, foods[i].food.Width, foods[i].food.Height);
                //Rectangle foodRectangle = new Rectangle((int)food.foodPosition.X, (int)food.foodPosition.Y, food.food.Width, food.food.Height);
                //check collision
                //if (foodRectangle.Intersects(_nest1.Rectangle))


                if (_nest1.Rectangle.Intersects(foodRectangle))
                {
                    receivedFood = true;//collision

                    foodIndex = i;
                }

            }

            base.Update(gameTime);


        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(volcano, new Rectangle(0, 0, GraphicsDevice.DisplayMode.Width/*800*/, GraphicsDevice.DisplayMode.Height/*480*/), Color.White); //background

            foreach(Meteor meteor in meteors)
            {
                meteor.Draw(spriteBatch);
            }

            foreach (Food food in foods)
            {
                food.Draw(spriteBatch);
            }

            foreach (Tail tail in tails)
            {
                tail.Draw(spriteBatch);
            }

            dino.Draw(spriteBatch);
            _nest1.Draw(spriteBatch);

            if (receivedFood)
            {
                _yum.Draw(spriteBatch);
                receivedFood = false;
                foods.RemoveAt(foodIndex);

                foodFromDaddy += 1;
                Console.WriteLine("test " + foodFromDaddy);
            }

            if (foodFromDaddy == foodMax)
            {
                babyIsfull = true;

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

         public void randomFood(GameTime gameTime) //spwan foods
        {
            int randomX = random.Next(0, GraphicsDevice.DisplayMode.Width);
            if (foodAmount > 1) // Spawn cool down (seconds)
            {
                foodAmount = 0;
                if (foods.Count < 10) // Amount of foods allowed on the screen
                {
                    foods.Add(new Food(Content.Load<Texture2D>("food"), new Vector2(randomX, -10), graphics));
                }
            }

            for (int i = 0; i < foods.Count; i++)
            {
                if (foods[i].foodSpawn == false) //TODO: Collision for foods and dino
                {
                    foods[i].foodSpeed = new Vector2(0f, 0f); //Stop food falling and begin countdown
                    foodTimeout -= gameTime.ElapsedGameTime;  
                    
                    if (foodTimeout <= TimeSpan.Zero) 
                    {
                        foods.RemoveAt(i); //Remove foods after time
                        i--;
                        foodTimeout = TimeSpan.FromSeconds(3); 
                    }

                    
                }

            }
        }

        public void tailAction() //generate a tail
        {
            foreach (Tail tail in tails.ToList())
            {
                tail.tailPosition += tail.tailSpeed;
                if (Vector2.Distance(tail.tailPosition, dino.dinoPosition) > 100) //length of the tail
                {
                    tail.swip = false;
                }

                for (int i = 0; i < tails.Count; i++)
                {
                    if(tails[i].swip == false)
                    {
                        tails.RemoveAt(i);
                        i--;
                    }

                }
            }
        }

        public void swip() //asign speed and position to the tail
        {
            Tail newtail = new Tail(Content.Load<Texture2D>("tail")); // draw only for testings 
            newtail.tailSpeed = new Vector2((float)Math.Cos(dino.dinoAngle), (float)Math.Sin(dino.dinoAngle)) * 5f + dino.dinoJumpSpeed;
            newtail.tailPosition = dino.dinoPosition + newtail.tailSpeed * 5;
            newtail.tailPosition.Y -= 50; //adjest tail position
            newtail.swip = true;

            if(tails.Count < 1)
            {
                tails.Add(newtail);
            }
        }
    }
}
