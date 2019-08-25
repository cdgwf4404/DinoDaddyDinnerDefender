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
        List<Nest> nests = new List<Nest>();

        public int meteorIndex = 0;

        private Yum _yum;
        private Full _full;
        private Full _full2;

        public int foodIndex = 0;
        public int foodIndex2 = 0;

        Random random = new Random();

        public Texture2D volcano;

        public Texture2D nest1Texture;
        public Texture2D nest2Texture;

        public Texture2D yumTexture;
        public Texture2D fullTexture;
        public Texture2D fullTexture2;

        public float meteorAmount = 0;
        public float foodAmount = 0;
        

        public int foodMax = 2;

        TimeSpan foodTimeout = TimeSpan.FromSeconds(3);

        public bool foodHitDino = false;
        public bool meteorHitDino = false;

        private SpriteFont Ubuntu32;

        public int dinoHealth = 3;

        private void reload() //reset game values when win or fail
        {
            dinoHealth = 3;
            for (int i = 0; i < meteors.Count; i++)
            {
                meteors.RemoveAt(i);
            }
            for (int i = 0; i < foods.Count; i++)
            {
                foods.RemoveAt(i);
            }

            foreach (Nest nest in nests)
            {
                nest.foodFromDaddy = 0;
                nest.receivedFood = false;
                nest.babyIsfull = false;
            }



        }

        enum GameState
        {
            Menu,
            Win,
            Lose,
            Playing,
        }

        GameState currentGameState = GameState.Menu;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
        }


        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
        }


  
        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);
            volcano = Content.Load<Texture2D>("Background");
            dino = new Dino(Content.Load<Texture2D>("dino"), new Vector2(1920 / 2, 900), graphics);
            Ubuntu32 = Content.Load<SpriteFont>("Ubuntu32");

            //nest1Texture = Content.Load<Texture2D>("egg1");
            nest1Texture = Content.Load<Texture2D>("BabyDino");
            nests.Add(new Nest(nest1Texture));
            nests[0]._position = new Vector2(500, 900);
            // nests.Add(new Nest(Content.Load<Texture2D>("BabyDino2"), new Vector2(1200, 400)));

            nest2Texture = Content.Load<Texture2D>("BabyDino");
            nests.Add(new Nest(nest1Texture));
            nests[1]._position = new Vector2(1000, 900);

            yumTexture = Content.Load<Texture2D>("yummy");
            _yum = new Yum(yumTexture);
            _yum._position = new Vector2(300, 300);

            fullTexture = Content.Load<Texture2D>("fullSmall");
            _full = new Full(fullTexture);
            _full._position = new Vector2(500, 900);

            fullTexture2 = Content.Load<Texture2D>("fullSmall");
            _full2 = new Full(fullTexture);
            _full2._position = new Vector2(1000, 900);



        }


        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            else
            {

                switch (currentGameState)
                {
                    case GameState.Menu:
                        if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                        {
                            currentGameState = GameState.Playing;
                        }

                        break;

                    case GameState.Playing:
                        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                        {
                            Exit();
                        }
                        else
                        {
                            dino.Update(gameTime);

                            meteorAmount += (float)gameTime.ElapsedGameTime.TotalSeconds;

                            foreach (Meteor meteor in meteors)
                            {
                                meteor.Update(graphics.GraphicsDevice);
                            }
                            randomMeteor();

                            foodAmount += (float)gameTime.ElapsedGameTime.TotalSeconds;

                            //Rectangle dinoRectangle = new Rectangle((int)dino.dinoPosition.X - 250, (int)dino.dinoPosition.Y - 147, dino.dino.Width, dino.dino.Height);

                            //foreach (Food food in foods)
                            for (int i = 0; i < foods.Count; i++)
                            {
                                if (foods[i].Rectangle.Intersects(dino.Rectangle))//&& swipe
                                {
                                    foodHitDino = true;
                                }

                                foods[i].Update(graphics.GraphicsDevice, gameTime, dino.dinoAngle, foodHitDino);//and swipe== true
                                foodHitDino = false;
                            }

                            randomFood(gameTime);

                                                                                  
                            for (int i = 0; i < meteors.Count; i++)
                            {
                                if (meteors[i].Rectangle.Intersects(dino.Rectangle) && Keyboard.GetState().IsKeyDown(Keys.Space))
                                {
                                    //meteorHitDino = true;
                                    meteors.RemoveAt(i);

                                }
                                else if (meteors[i].Rectangle.Intersects(dino.Rectangle) && Keyboard.GetState().IsKeyUp(Keys.Space))
                                {
                                    //meteorHitDino = true;
                                    meteors.RemoveAt(i);
                                    dinoHealth -= 1;

                                }
                            }

                            if (dinoHealth <= 0)
                            {
                                reload();
                                currentGameState = GameState.Lose;
                            }

                            babyIsFed();
                            gameWon();

                            base.Update(gameTime);
                        }
                        break;

                    case GameState.Win:
                        if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                        {
                            currentGameState = GameState.Playing;
                        }
                        break;

                    case GameState.Lose:
                        if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                        {
                            currentGameState = GameState.Playing;
                        }
                        break;
                }
            }

        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            switch (currentGameState)
            {
                case GameState.Menu:
                    spriteBatch.Draw(volcano, new Rectangle(0, 0, GraphicsDevice.DisplayMode.Width/*800*/, GraphicsDevice.DisplayMode.Height/*480*/), Color.White); //background
                    spriteBatch.DrawString(Ubuntu32, "Press Enter to Begin", new Vector2(GraphicsDevice.DisplayMode.Width / 2, 100), Color.Black);
                    break;

                case GameState.Playing:
                    spriteBatch.Draw(volcano, new Rectangle(0, 0, GraphicsDevice.DisplayMode.Width/*800*/, GraphicsDevice.DisplayMode.Height/*480*/), Color.White); //background

                    foreach (Meteor meteor in meteors)
                    {
                        meteor.Draw(spriteBatch);
                    }

                    foreach (Food food in foods)
                    {
                        food.Draw(spriteBatch);
                    }



                    foreach (Nest nests in nests)
                    {
                        nests.Draw(spriteBatch);
                    }
                   
                    dino.Draw(spriteBatch);

                    foreach(Nest nests in nests)
                    {
                        nests.Draw(spriteBatch);
                    }


                    babyReceivedFood();
                    setBabyAsFull();
                    babyGrows();
                  

                    spriteBatch.DrawString(Ubuntu32, "HP: " + dinoHealth, new Vector2(100, 100), Color.Black);

                    break;

                case GameState.Win:
                    spriteBatch.Draw(volcano, new Rectangle(0, 0, GraphicsDevice.DisplayMode.Width/*800*/, GraphicsDevice.DisplayMode.Height/*480*/), Color.White); //background
                    spriteBatch.DrawString(Ubuntu32, "You Won! Press Enter to Retry", new Vector2(GraphicsDevice.DisplayMode.Width / 2, 100), Color.Black);
                    break;

                case GameState.Lose:
                    spriteBatch.Draw(volcano, new Rectangle(0, 0, GraphicsDevice.DisplayMode.Width/*800*/, GraphicsDevice.DisplayMode.Height/*480*/), Color.White); //background
                    spriteBatch.DrawString(Ubuntu32, "You Died... Press Enter to Retry", new Vector2(GraphicsDevice.DisplayMode.Width / 2, 100), Color.Black);
                    break;
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
                if (meteors.Count < 2) // Amount of meteors allowed on the screen
                {
                    // meteors.Add(new Meteor(Content.Load<Texture2D>("Meteor"), new Vector2(randomX, -10)));
                    meteors.Add(new Meteor(Content.Load<Texture2D>("Meteor"), new Vector2(randomX, -10), graphics));
                }
            }

            for (int i = 0; i < meteors.Count; i++)
            {
                if (meteors[i].meteorSpawn == false)
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
                    int foodType = random.Next(0, 3);
                    if (foodType == 0)
                    {
                        foods.Add(new Food(Content.Load<Texture2D>("Grapes"), new Vector2(randomX, -10), graphics));
                    }
                    else if (foodType == 1)
                    {
                        foods.Add(new Food(Content.Load<Texture2D>("Apples"), new Vector2(randomX, -10), graphics));
                    }
                    else if (foodType == 2)
                    {
                        foods.Add(new Food(Content.Load<Texture2D>("Carrots"), new Vector2(randomX, -10), graphics));
                    }
                }
            }

            for (int i = 0; i < foods.Count; i++)
            {
                if (foods[i].foodGround == true)
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
                else if (foods[i].foodOutside == true)
                {
                    foods.RemoveAt(i); //Remove foods when outside of screen
                    i--;
                }

            }
        }


        public void gameWon()
        {
            if (checkIfAllBabiesAreFull())
            {
                reload();
                currentGameState = GameState.Win;
            }

        }

        public Boolean checkIfAllBabiesAreFull()
        {
            int numberOfNests = nests.Count;
            int fullBabies = countFullBabies();
            Boolean full = false;

            if (fullBabies == numberOfNests)
            {
                full = true;
            }
            return full;

        }

        public int countFullBabies()
        {
            int totalFull = 0;

            foreach (Nest nest in nests)
            {
                if (nest.babyIsfull)
                    totalFull += 1;

            }
            return totalFull;

        }





        private void babyReceivedFood()
        {
            foreach (Nest nest in nests)
            {
                if (nest.receivedFood)
                {
                    nest.receivedFood = false;
                    foods.RemoveAt(foodIndex2);
                    nest.foodFromDaddy += 1;
                }
            }
        }

        //draw grown baby
        private void babyGrows()
        {
            if (nests[0].babyIsfull)
            {
                _full.Draw(spriteBatch);
            }
            if (nests[1].babyIsfull)
            {
                _full2.Draw(spriteBatch);
            }

        }



        private void setBabyAsFull()
        {
            foreach (Nest nest in nests)
            {
                if (nest.foodFromDaddy == foodMax)
                {
                    nest.babyIsfull = true;

                }
            }
        }

        private Boolean checkIfbabyIsFull(Nest nest)
        {
            Boolean full = false;
            if (nest.babyIsfull)
                full = true;
            return full;
        }



        private void babyIsFed()
        {

            for (int i = 0; i < foods.Count; i++)
            {
                Rectangle foodRectangle = new Rectangle((int)foods[i].foodPosition.X, (int)foods[i].foodPosition.Y, foods[i].food.Width, foods[i].food.Height);

                for (int j = 0; j < nests.Count; j++)
                {
                    if (nests[j].Rectangle.Intersects(foodRectangle) && foods[i].foodHit)
                    {
                        nests[j].receivedFood = true;
                        foodIndex = i;

                    }
                }

            }

        }


        private void setBabyHitbyMeteor()
        {

            for (int i = 0; i < meteors.Count; i++)
            {
                Rectangle meteorRectangle = new Rectangle((int)meteors[i].meteorPosition.X, (int)meteors[i].meteorPosition.Y, meteors[i].meteor.Width, meteors[i].meteor.Height);

                for (int j = 0; j < nests.Count; j++)
                {
                    if (nests[j].Rectangle.Intersects(meteorRectangle))
                    {
                        nests[j].hitByMeteor = true;
                        meteorIndex = i;

                    }
                }

            }

        }





    }
}
