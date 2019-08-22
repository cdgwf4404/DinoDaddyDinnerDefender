using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace DDDD
{

    public class Main : Game
    {
        public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public Dino dino;
        public Platform platform;
        List<Meteor> meteors = new List<Meteor>();
        List<Food> foods = new List<Food>();
        Random random = new Random();

        public Texture2D volcano;

        public float meteorAmount = 0;
        public float foodAmount = 0;
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

            dino = new Dino(Content.Load<Texture2D>("dino"), new Vector2(GraphicsDevice.DisplayMode.Width / 2, GraphicsDevice.DisplayMode.Height), graphics); //(800 / 2, 480)

            //platform = new Platform(Content.Load<Texture2D>("platform"), new Vector2(GraphicsDevice.DisplayMode.Width / 2, GraphicsDevice.DisplayMode.Height));

            //ScaleArea();
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
            /*
            if (dinoRec.Intersects(foodRec))
            {

            }
            */
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(volcano, new Rectangle(0, 0, GraphicsDevice.DisplayMode.Width/*800*/, GraphicsDevice.DisplayMode.Height/*480*/), Color.White); //background
            //spriteBatch.Draw(platform, new Rectangle(GraphicsDevice.DisplayMode.Width / 2, GraphicsDevice.DisplayMode.Height / 2,  ),Color.White)

            foreach(Meteor meteor in meteors)
            {
                meteor.Draw(spriteBatch);
            }

            foreach (Food food in foods)
            {
                food.Draw(spriteBatch);
            }

            dino.Draw(spriteBatch);
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

       
    }
}
