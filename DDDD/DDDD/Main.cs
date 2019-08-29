using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
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
        public Hit hit;
        public Dead dead;
        public Piece piece;
        public Swipe swipe;

        List<Text> texts = new List<Text>();
        List<Meteor> meteors = new List<Meteor>();
        List<Food> foods = new List<Food>();
        List<Food> menuFoods = new List<Food>();
        List<Nest> nests = new List<Nest>();
        List<Platform> platforms = new List<Platform>();

        public int meteorIndex = 0;
        public int prevRandom = -1;
        public int currRandom = -1;

        private Yum _yum;

        public int foodIndex = 0;
        public int foodIndex2 = 0;

        Random random = new Random();

        public Texture2D volcano;

        public Texture2D nest1Texture;
        public Texture2D nest2Texture;
        public Texture2D nest3Texture;

        public Texture2D yumTexture;
        public Texture2D fullTexture;
        public Texture2D fullTexture2;

        public Texture2D infantTexture;

        public float meteorAmount = 0;
        public float foodAmount = 0;
        public float menuFoodAmount = 0;

        public bool onPlatform = false;
        

        public int foodMax = 5;

        TimeSpan foodTimeout = TimeSpan.FromSeconds(3);

        //public bool foodHitDino = false;
        public bool meteorHitDino = false;

        private SpriteFont Ubuntu32;

        public int dinoHealth = 3;

        public bool babyHit = false;

        public string failText;

        public bool exitLoop = false;

        public bool spinFlag = false;

        Song gameplaySong;
        Song failSong;

        private void reload() //reset game values when win or fail
        {
            dinoHealth = 3;
            for (int i = 0; i < meteors.Count; i++)
            {
                meteors.RemoveAt(i);
            }

            meteors = new List<Meteor>();

            for (int i = 0; i < foods.Count; i++)
            {
                foods.RemoveAt(i);
            }

            foreach (Nest nest in nests)
            {
                nest.foodFromDaddy = 0;
                nest.receivedFood = false;
                nest.babyIsfull = false;
                nest.babyHealth = 1;
            }
            hit.dinoHit = false;
            dead.dying = false;
            swipe.swiping = false;
            exitLoop = false;

            dino.dinoPosition.X = 1920 / 2;
            dino.dinoPosition.Y = 900;
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
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();
        }


  
        protected override void LoadContent()
        {
            gameplaySong = Content.Load<Song>("DDDD_BgMusic");
            MediaPlayer.Play(gameplaySong);
            MediaPlayer.IsRepeating = true;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            volcano = Content.Load<Texture2D>("Background");
            Rectangle textRec = new Rectangle(0, 0, GraphicsDevice.DisplayMode.Width, GraphicsDevice.DisplayMode.Height);
            texts.Add(new Text(Content.Load<Texture2D>("menu"), new Vector2(0, 0)));
            texts.Add(new Text(Content.Load<Texture2D>("win"), new Vector2(0, 0)));
            texts.Add(new Text(Content.Load<Texture2D>("fail"), new Vector2(0, 0)));


            dino = new Dino(Content.Load<Texture2D>("green"), new Vector2(1920 / 2, 900), graphics);
            Ubuntu32 = Content.Load<SpriteFont>("Ubuntu32");

            platforms.Add(new Platform(Content.Load<Texture2D>("platform"), new Vector2(1920 / 2 + 715, 1080/2 + 180), graphics));
            platforms.Add(new Platform(Content.Load<Texture2D>("platform"), new Vector2(0, 1080 / 2 + 180), graphics));
            platforms.Add(new Platform(Content.Load<Texture2D>("platform"), new Vector2(500, 1080 / 2 + 1), graphics));
            platforms.Add(new Platform(Content.Load<Texture2D>("platform"), new Vector2(1175, 1080 / 2 + 1), graphics));


            nest1Texture = Content.Load<Texture2D>("baby");
            nests.Add(new Nest(nest1Texture));
            nests[0]._position = new Vector2(0, 1080 / 2 + 35);

            nest2Texture = Content.Load<Texture2D>("baby");
            nests.Add(new Nest(nest1Texture));
            nests[1]._position = new Vector2(1775, 1080 / 2 + 35);

            nest3Texture = Content.Load<Texture2D>("baby");
            nests.Add(new Nest(nest3Texture));
            nests[2]._position = new Vector2(888, 1080 / 2 + 262);

            yumTexture = Content.Load<Texture2D>("yummy");
            _yum = new Yum(yumTexture);
            _yum._position = new Vector2(300, 300);

            fullTexture = Content.Load<Texture2D>("grown");

            hit = new Hit(Content.Load<Texture2D>("hit"), new Vector2(1920 / 2, 900));
            swipe = new Swipe(Content.Load<Texture2D>("greenLeft"), new Vector2(1920 / 2, 900));

            dead = new Dead(Content.Load<Texture2D>("dead"), new Vector2(1920 / 2, 900), graphics);

            piece = new Piece(Content.Load<Texture2D>("piece"), new Vector2(1920 / 2, 900));

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
                        menuFoodAmount += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        for (int i = 0; i < menuFoods.Count; i++)
                        {
                            menuFoods[i].Update(graphics.GraphicsDevice, gameTime, dino.dinoAngle, dino);
                        }
                        menuRandomFood(gameTime);
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
                            for (int i = 0; i < platforms.Count; i++)
                            {

                                if (dino.dinoJumpSpeed.Y > 0 && IsTouchingTop(dino.Rectangle, platforms[i].Rectangle, dino.dinoJumpSpeed) /*&& dino.dinoPosition.Y <= platforms[i].platformPosition.Y*/)
                                {
                                    dino.dinoJumpSpeed.Y = 0f;
                                    onPlatform = true;
                                }

                                if ((/*dino.dinoJumpSpeed.X > 0 &&*/ IsTouchingLeft(dino.Rectangle, platforms[i].Rectangle, dino.dinoJumpSpeed)) ||
                                    (/*dino.dinoJumpSpeed.X < 0 &&*/ IsTouchingRight(dino.Rectangle, platforms[i].Rectangle, dino.dinoJumpSpeed)))
                                {

                                    dino.dinoJumpSpeed.X = 0f;
                                    onPlatform = false;
                                }
                                

                                 if (dino.dinoJumpSpeed.Y < 0 && IsTouchingBottom(dino.Rectangle, platforms[i].Rectangle, dino.dinoJumpSpeed))
                                {
                                    dino.dinoJumpSpeed.Y = 0f;
                                    onPlatform = false;
                                }



                            }

                            if(dinoHealth == 3)
                            {
                                dino.dino = Content.Load<Texture2D>("green");
                                if(dino.dinoAngle == 0f)
                                {
                                    swipe.swipe = Content.Load<Texture2D>("greenRight");
                                }
                                else if(dino.dinoAngle == Math.PI)
                                {
                                    swipe.swipe = Content.Load<Texture2D>("greenLeft");
                                }
                            }
                            else if(dinoHealth == 2)
                            {
                                dino.dino = Content.Load<Texture2D>("yellow");
                                if (dino.dinoAngle == 0f)
                                {
                                    swipe.swipe = Content.Load<Texture2D>("yellowRight");
                                }
                                else if (dino.dinoAngle == Math.PI)
                                {
                                    swipe.swipe = Content.Load<Texture2D>("yellowLeft");
                                }
                            }
                            else
                            {
                                dino.dino = Content.Load<Texture2D>("red");
                                if (dino.dinoAngle == 0f)
                                {
                                    swipe.swipe = Content.Load<Texture2D>("redRight");
                                }
                                else if (dino.dinoAngle == Math.PI)
                                {
                                    swipe.swipe = Content.Load<Texture2D>("redLeft");
                                }
                            }

                            if (dinoHealth <= 0)
                            {
                                dead.dying = true;
                            }

                            if (hit.dinoHit == false && dead.dying == false) // display dino
                            {
                                dino.Update(gameTime, onPlatform, spinFlag);
                            }
                            else if(hit.dinoHit == true) // display hit animation
                            {
                                hit.Update(gameTime, dino.dinoAngle);
                            }
                            else if(dead.dying == true) // display dead animation
                            {
                                dead.Update(gameTime, dino.dinoPosition);
                            }

                            if(dino.spaceBarPressed && swipe.swiping == false)
                            {
                                swipe.swiping = true;
                            }

                            if(swipe.swiping == true)
                            {
                                swipe.Update(gameTime, dino);
                            }

                            if (dead.dying == false && dinoHealth <= 0)
                            {
                                reload();
                                currentGameState = GameState.Lose;
                            }

                            onPlatform = false;

                            meteorAmount += (float)gameTime.ElapsedGameTime.TotalSeconds;

                            for (int i = 0; i < meteors.Count; i++)
                            {
                                meteors[i].Update(graphics.GraphicsDevice);
                            }
                            randomMeteor();

                            for (int i = 0; i < meteors.Count; i++)
                            {
                                for (int j = 0; j < nests.Count; j++)
                                {
                                    if (nests[j].Rectangle.Intersects(meteors[i].Rectangle))
                                    {
                                        nests[j].babyHealth -= 1;
                                        meteorIndex = i;
                                        babyHit = true;
                                    }

                                    if(nests[j].babyHealth <= 0)
                                    {
                                        failText = "One of Your Babies Died";
                                        reload();
                                        currentGameState = GameState.Lose;
                                        exitLoop = true;
                                        break;
                                    }
                                }
                                if(exitLoop == true)
                                {
                                    break;
                                }
                            }
                            if (babyHit == true)
                            {
                                if(meteors.Count > 0)
                                {
                                    meteors.RemoveAt(meteorIndex);
                                    babyHit = false;
                                }                     
                            }



                            foodAmount += (float)gameTime.ElapsedGameTime.TotalSeconds;
                            
                            for (int i = 0; i < foods.Count; i++)
                            {
                                
                                if (foods[i].Rectangle.Intersects(dino.Rectangle) && swipe.swiping /*dino.hitCount == 0*/)
                                {
                                    //if (dino.hitCount == 0)
                                    //{
                                        foods[i].foodHitDino = true;
         
                                       // dino.hitCount = 1;
                                        
                                        
                                   // }
                                }

                                foods[i].Update(graphics.GraphicsDevice, gameTime, dino.dinoAngle, dino);
                                foods[i].foodHitDino = false;

                            }

                            /*
                            for (int i = 0; i < foods.Count; i++)
                            {

                                if (foods[i].Rectangle.Intersects(dino.Rectangle) && dino.spaceBarPressed == true)
                                {

                                    foods[i].foodHitDino = true;

                                }

                                foods[i].Update(graphics.GraphicsDevice, gameTime, dino.dinoAngle, dino);
                                foods[i].foodHitDino = false;
                            }*/
                                randomFood(gameTime);

                                                                                  
                            for (int i = 0; i < meteors.Count; i++)
                            {
                                if (meteors[i].Rectangle.Intersects(dino.Rectangle) && swipe.swiping/*Keyboard.GetState().IsKeyDown(Keys.Space)*/)
                                {
                                    piece.destoried = true;
                                    piece.piecePosition.X = meteors[i].meteorPosition.X;
                                    piece.piecePosition.Y = meteors[i].meteorPosition.Y + 100;
                                    meteors.RemoveAt(i);
                                }
                                else if (meteors[i].Rectangle.Intersects(dino.Rectangle) && Keyboard.GetState().IsKeyUp(Keys.Space))
                                {
                                    meteors.RemoveAt(i);
                                    dinoHealth -= 1;
                                    hit.dinoHit = true;

                                }
                            }
                            piece.Update(gameTime);

                            setBabyHitbyMeteor();

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

                    foreach (Food food in menuFoods)
                    {
                        food.Draw(spriteBatch);
                    }
                    texts[0].Draw(spriteBatch);
                    break;

                case GameState.Playing:
                    spriteBatch.Draw(volcano, new Rectangle(0, 0, GraphicsDevice.DisplayMode.Width/*800*/, GraphicsDevice.DisplayMode.Height/*480*/), Color.White); //background

                    foreach (Meteor meteor in meteors)
                    {
                        meteor.Draw(spriteBatch);
                    }

                    piece.Draw(spriteBatch);

                    foreach (Food food in foods)
                    {
                        food.Draw(spriteBatch);
                    }

                    foreach (Platform platform in platforms)
                    {
                        platform.Draw(spriteBatch);
                    }
                    
                    for (int i = 0; i < nests.Count; i++)
                    {
                        nests[i].Draw(spriteBatch);
                    }

                    if (hit.dinoHit == false && dead.dying == false && swipe.swiping == false)
                    {
                        dino.Draw(spriteBatch);
                    }
                    else if(hit.dinoHit == true)
                    {
                        hit.Draw(spriteBatch, dino.dinoPosition.X, dino.dinoPosition.Y);
                    }
                    else if(dead.dying == true)
                    {
                        dead.Draw(spriteBatch);
                    }
                    else if(swipe.swiping == true)
                    {
                        swipe.Draw(spriteBatch, dino.dinoPosition.X, dino.dinoPosition.Y);
                    }
                    
                    babyReceivedFood();
                    setBabyAsFull();
                    


                    break;

                case GameState.Win:
                    spriteBatch.Draw(volcano, new Rectangle(0, 0, GraphicsDevice.DisplayMode.Width/*800*/, GraphicsDevice.DisplayMode.Height/*480*/), Color.White); //background
                    texts[1].Draw(spriteBatch);
                    break;

                case GameState.Lose:
                    spriteBatch.Draw(volcano, new Rectangle(0, 0, GraphicsDevice.DisplayMode.Width/*800*/, GraphicsDevice.DisplayMode.Height/*480*/), Color.White); //background
                    texts[2].Draw(spriteBatch);
                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void randomMeteor() //spawn meteors
        {
            currRandom = random.Next(0, 3);
            int randomX = 0;
            if (currRandom == prevRandom)
            {
                if (currRandom == 0 || currRandom == 1)
                {
                    currRandom = 2;
                }
                else
                {
                    currRandom = 0;
                }
            }
            prevRandom = currRandom;
            if (currRandom == 0)
            {
                randomX = 0;
            }
            else if (currRandom == 1)
            {
                randomX = 888;
            }
            else if (currRandom == 2)
            {
                randomX = 1775;
            }
            if (meteorAmount > 6) // Spawn cool down (seconds)
            {
                meteorAmount = 0;
                if (meteors.Count < 2) // Amount of meteors allowed on the screen
                {
                    meteors.Add(new Meteor(Content.Load<Texture2D>("Meteor"), new Vector2(randomX, -350), graphics));
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
            int randomX_1 = random.Next(200, 700); ;
            int randomX_2 = random.Next(1100, 1600);

            if (foodAmount > 1) // Spawn cool down (seconds)
            {
                foodAmount = 0;
                if (foods.Count < 10) // Amount of foods allowed on the screen
                {
                    int foodType = random.Next(0, 3);
                    if (foodType == 0)
                    {
                        foods.Add(new Food(Content.Load<Texture2D>("Grapes"), new Vector2(randomX_1, -10), graphics));
                        foods.Add(new Food(Content.Load<Texture2D>("Grapes"), new Vector2(randomX_2, -10), graphics));
                    }
                    else if (foodType == 1)
                    {
                        foods.Add(new Food(Content.Load<Texture2D>("Apples"), new Vector2(randomX_1, -10), graphics));
                        foods.Add(new Food(Content.Load<Texture2D>("Apples"), new Vector2(randomX_2, -10), graphics));
                    }
                    else if (foodType == 2)
                    {
                        foods.Add(new Food(Content.Load<Texture2D>("Carrots"), new Vector2(randomX_1, -10), graphics));
                        foods.Add(new Food(Content.Load<Texture2D>("Carrots"), new Vector2(randomX_1, -10), graphics));
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

        public void BabyFail()
        {

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
                    if ( nest.foodFromDaddy < foodMax) //stops receving when reaching Max
                    {
                        nest.receivedFood = false;
                        foods.RemoveAt(foodIndex);
                        nest.foodFromDaddy += 1;
                    }
                }
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


        public bool IsTouchingLeft(Rectangle r1, Rectangle r2, Vector2 r1speed)
        {
            return r1.Right + r1speed.X > r2.Left &&
                   r1.Left < r2.Left &&
                   r1.Bottom > r2.Top &&
                   r1.Top < r2.Bottom;
        }

        public bool IsTouchingRight(Rectangle r1, Rectangle r2, Vector2 r1speed)
        {
            return r1.Left +  r1speed.X < r2.Right &&
                   r1.Right > r2.Right &&
                   r1.Bottom > r2.Top &&
                   r1.Top < r2.Bottom;
        }

        protected bool IsTouchingTop(Rectangle r1, Rectangle r2, Vector2 r1speed)
        {
            return r1.Bottom + r1speed.Y > r2.Top &&
                   r1.Top < r2.Top &&
                   r1.Right > r2.Left &&
                   r1.Left < r2.Right;
        }

        public bool IsTouchingBottom(Rectangle r1, Rectangle r2, Vector2 r1speed)
        {
            return r1.Top + r1speed.Y < r2.Bottom &&
                   r1.Bottom > r2.Bottom &&
                   r1.Right > r2.Left &&
                   r1.Left < r2.Right;
        }
        public void menuRandomFood(GameTime gameTime) //spwan foods
        {
            int randomX = random.Next(0, GraphicsDevice.DisplayMode.Width);
            if (menuFoodAmount > 0.2) // Spawn cool down (seconds)
            {
                menuFoodAmount = 0;
                if (menuFoods.Count < 40) // Amount of foods allowed on the screen
                {
                    int foodType = random.Next(0, 3);
                    if (foodType == 0)
                    {
                        menuFoods.Add(new Food(Content.Load<Texture2D>("Grapes"), new Vector2(randomX, -10), graphics));
                    }
                    else if (foodType == 1)
                    {
                        menuFoods.Add(new Food(Content.Load<Texture2D>("Apples"), new Vector2(randomX, -10), graphics));
                    }
                    else if (foodType == 2)
                    {
                        menuFoods.Add(new Food(Content.Load<Texture2D>("Carrots"), new Vector2(randomX, -10), graphics));
                    }
                }
            }

            for (int i = 0; i < menuFoods.Count; i++)
            {
                if (menuFoods[i].foodGround == true)
                {
                    menuFoods.RemoveAt(i); //Remove foods after time
                }
                else if (menuFoods[i].foodOutside == true)
                {
                    menuFoods.RemoveAt(i); //Remove foods when outside of screen
                    i--;
                }

            }
        }



    }
}