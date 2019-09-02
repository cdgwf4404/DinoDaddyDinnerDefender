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
        public Tree tree;
        public DeadBaby deadBaby;

        //public Cloud cloud;

        List<Text> texts = new List<Text>();
        List<Meteor> meteors = new List<Meteor>();
        List<Food> foods = new List<Food>();
        List<Food> menuFoods = new List<Food>();
        //List<Nest> nests = new List<Nest>();
        List<SpriteAnimatedNest> nests = new List<SpriteAnimatedNest>();
        List<Platform> platforms = new List<Platform>();
        List<SoundEffect> soundEffects = new List<SoundEffect>();
        List<Cloud> clouds = new List<Cloud>();
        List<Win> wins = new List<Win>();
        List<Chomp> chomps = new List<Chomp>();
        List<Grown> growns = new List<Grown>();
        List<Sprite> menus = new List<Sprite>();
        // List<SpriteAnimated> eggs = new List<SpriteAnimated>();
        //List<Nest> eggs = new List<Nest>();

        SoundEffectInstance eatInstance;


        public int meteorIndex = 0;
        public int prevRandom = -1;
        public int currRandom = -1;

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

        public Texture2D menuTexture;
        public Texture2D menuTextureFull;
        public Texture2D menuTexture1deaths;
        public Texture2D menuTexture2deaths;
        public Texture2D menuTexture3deaths;

        public float meteorAmount = 0;
        public float foodAmount = 0;
        public float menuFoodAmount = 0;

        public bool onPlatform = false;
        public bool babyOnPlatform = false;

        public int foodMax = 6;

        TimeSpan foodTimeout = TimeSpan.FromSeconds(0);

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
            meteors = new List<Meteor>();
            foods = new List<Food>();

            foreach (SpriteAnimatedNest nest in nests)

            {
                nest.foodFromDaddy = 0;
                nest.receivedFood = false;
                nest.babyIsfull = false;
                nest.babyHealth = 1;
                nest.grown = false;
                nest.animated = true;
                nest.frame = 20;
            }
            hit.dinoHit = false;
            dead.dying = false;
            deadBaby.babyDying = false;
            swipe.swiping = false;
            exitLoop = false;

            for (int i = 0; i < chomps.Count; i++)
            {
                chomps[i].chomping = false;
            }
            for (int i = 0; i < growns.Count; i++)
            {
                growns[i].spwan = false;
                growns[i].hp = 1;
            }

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
            failSong = Content.Load<Song>("JP_Harmonica");
            MediaPlayer.Play(gameplaySong);
            MediaPlayer.IsRepeating = true;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            volcano = Content.Load<Texture2D>("sky");
            tree = new Tree(Content.Load<Texture2D>("tree"));


            clouds.Add(new Cloud(Content.Load<Texture2D>("cloud"), new Rectangle(0, 0, 4000, 1080)));
            clouds.Add(new Cloud(Content.Load<Texture2D>("cloud"), new Rectangle(4000, 0, 4000, 1080)));

            Rectangle textRec = new Rectangle(0, 0, GraphicsDevice.DisplayMode.Width, GraphicsDevice.DisplayMode.Height);
            texts.Add(new Text(Content.Load<Texture2D>("menu"), new Vector2(0, 0)));
            texts.Add(new Text(Content.Load<Texture2D>("win"), new Vector2(0, 0)));
            //texts.Add(new Text(Content.Load<Texture2D>("fail"), new Vector2(0, 0)));
            texts.Add(new Text(Content.Load<Texture2D>("failed2"), new Vector2(0, 0)));

            chomps.Add(new Chomp(Content.Load<Texture2D>("chompRight1"), new Vector2(0, 0)));
            chomps.Add(new Chomp(Content.Load<Texture2D>("chompRight1"), new Vector2(0, 0)));
            chomps.Add(new Chomp(Content.Load<Texture2D>("chompRight1"), new Vector2(0, 0)));

            wins.Add(new Win(Content.Load<Texture2D>("winDaddy"), new Vector2(graphics.GraphicsDevice.DisplayMode.Width / 2 - 210, graphics.GraphicsDevice.DisplayMode.Height / 2 + 262), 250, 147));
            wins.Add(new Win(Content.Load<Texture2D>("winBaby"), new Vector2(graphics.GraphicsDevice.DisplayMode.Width / 2, graphics.GraphicsDevice.DisplayMode.Height / 2 + 290), 237, 121));

            SoundEffect.MasterVolume = 0.2f;
            soundEffects.Add(Content.Load<SoundEffect>("DD_HitByMeteor"));
            soundEffects.Add(Content.Load<SoundEffect>("BabyGrow"));
            soundEffects.Add(Content.Load<SoundEffect>("JumpSFX"));
            soundEffects.Add(Content.Load<SoundEffect>("MeteorExplode"));
            soundEffects.Add(Content.Load<SoundEffect>("TailSwipe"));

            dino = new Dino(Content.Load<Texture2D>("green"), new Vector2(1920 / 2, 900), graphics);
            Ubuntu32 = Content.Load<SpriteFont>("Ubuntu32");

            platforms.Add(new Platform(Content.Load<Texture2D>("platform"), new Vector2(1920 / 2 + 715, 1080 / 2 + 180), graphics));
            platforms.Add(new Platform(Content.Load<Texture2D>("platform"), new Vector2(0, 1080 / 2 + 180), graphics));
            platforms.Add(new Platform(Content.Load<Texture2D>("platform"), new Vector2(500, 1080 / 2 + 1), graphics));
            platforms.Add(new Platform(Content.Load<Texture2D>("platform"), new Vector2(1175, 1080 / 2 + 1), graphics));

            growns.Add(new Grown(Content.Load<Texture2D>("winBaby"), graphics));
            growns.Add(new Grown(Content.Load<Texture2D>("winBaby"), graphics));
            growns.Add(new Grown(Content.Load<Texture2D>("winBaby"), graphics));


            nest1Texture = Content.Load<Texture2D>("DinoBabyEggLoopAnimationA");
            //nests.Add(new Nest(nest1Texture));
            nests.Add(new SpriteAnimatedNest(nest1Texture));
            nests[0]._position = new Vector2(0, 1080 / 2 + 22);

            nest2Texture = Content.Load<Texture2D>("DinoBabyEggLoopAnimationB");
            //nests.Add(new Nest(nest1Texture));
            nests.Add(new SpriteAnimatedNest(nest1Texture));
            nests[1]._position = new Vector2(1775, 1080 / 2 + 22);

            nest3Texture = Content.Load<Texture2D>("DinoBabyEggLoopAnimationC");
            //nests.Add(new Nest(nest3Texture));
            nests.Add(new SpriteAnimatedNest(nest3Texture));
            nests[2]._position = new Vector2(888, 1080 / 2 + 252);

            menuTextureFull = Content.Load<Texture2D>("DinoDaddyLifeMenuFull");
            menus.Add(new Sprite(menuTextureFull));
            menus[0].spritePosition = new Vector2(0, 990);

            menuTexture1deaths = Content.Load<Texture2D>("DinoDaddyLifeMenu1deaths");
            menus.Add(new Sprite(menuTexture1deaths));
            menus[1].spritePosition = new Vector2(0, 990);

            menuTexture2deaths = Content.Load<Texture2D>("DinoDaddyLifeMenu2deaths");
            menus.Add(new Sprite(menuTexture2deaths));
            menus[2].spritePosition = new Vector2(0, 990);

            menuTexture3deaths = Content.Load<Texture2D>("DinoDaddyLifeMenu3deaths");
            menus.Add(new Sprite(menuTexture3deaths));
            menus[3].spritePosition = new Vector2(0, 990);


            fullTexture = Content.Load<Texture2D>("grown");

            hit = new Hit(Content.Load<Texture2D>("hit"), new Vector2(1920 / 2, 900));
            swipe = new Swipe(Content.Load<Texture2D>("greenLeft"), new Vector2(1920 / 2, 900));

            dead = new Dead(Content.Load<Texture2D>("dead"), new Vector2(1920 / 2, 900), graphics);
            deadBaby = new DeadBaby(Content.Load<Texture2D>("deadBaby"), new Vector2(1920 / 2, 900), graphics);

            piece = new Piece(Content.Load<Texture2D>("Explosion"), new Vector2(1920 / 2, 900));

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

                        if (clouds[0].cloudRec.X + clouds[0].cloud.Width <= 0)
                        {
                            clouds[0].cloudRec.X = clouds[1].cloudRec.X + clouds[1].cloud.Width;
                        }
                        if (clouds[1].cloudRec.X + clouds[1].cloud.Width <= 0)
                        {
                            clouds[1].cloudRec.X = clouds[0].cloudRec.X + clouds[0].cloud.Width;
                        }
                        tree.Update(gameTime);
                        clouds[0].Update(gameTime);
                        clouds[1].Update(gameTime);

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

                        animateNests();

                        if (clouds[0].cloudRec.X + clouds[0].cloud.Width <= 0)
                        {
                            clouds[0].cloudRec.X = clouds[1].cloudRec.X + clouds[1].cloud.Width;
                        }
                        if (clouds[1].cloudRec.X + clouds[1].cloud.Width <= 0)
                        {
                            clouds[1].cloudRec.X = clouds[0].cloudRec.X + clouds[0].cloud.Width;
                        }
                        tree.Update(gameTime);
                        clouds[0].Update(gameTime);
                        clouds[1].Update(gameTime);
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

                            if (dinoHealth == 3)
                            {
                                menuTexture = Content.Load<Texture2D>("DinoDaddyLifeMenuFull");
                                dino.dino = Content.Load<Texture2D>("green");
                                if (dino.dinoAngle == 0f)
                                {
                                    swipe.swipe = Content.Load<Texture2D>("greenRight");
                                }
                                else if (dino.dinoAngle == Math.PI)
                                {
                                    swipe.swipe = Content.Load<Texture2D>("greenLeft");
                                }
                            }
                            else if (dinoHealth == 2)
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
                                dino.Update(gameTime, onPlatform, spinFlag, soundEffects);
                            }
                            else if (hit.dinoHit == true) // display hit animation
                            {
                                hit.Update(gameTime, dino.dinoAngle);
                            }
                            else if (dead.dying == true) // display dead animation
                            {
                                dead.Update(gameTime, dino.dinoPosition);
                            }

                            if (dino.spaceBarPressed && swipe.swiping == false)
                            {
                                swipe.swiping = true;
                            }

                            if (swipe.swiping == true)
                            {
                                swipe.Update(gameTime, dino);
                            }

                            if (dead.dying == false && dinoHealth <= 0)
                            {
                                MediaPlayer.Play(failSong);
                                MediaPlayer.IsRepeating = false;
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
                                    if (nests[j].Rectangle.Intersects(meteors[i].Rectangle) && nests[j].grown == false)
                                    {
                                        nests[j].babyHealth -= 1;
                                        meteorIndex = i;
                                        babyHit = true;

                                    }

                                    if (nests[j].babyHealth <= 0)
                                    {
                                        deadBaby.babyDying = true;
                                        deadBaby.Update(gameTime, nests[j]._position);
                                        if (deadBaby.babyDying == false)
                                        {
                                            MediaPlayer.Play(failSong);
                                            MediaPlayer.IsRepeating = false;
                                            reload();
                                            currentGameState = GameState.Lose;
                                            exitLoop = true;
                                            break;
                                        }
                                    }
                                }
                                if (exitLoop == true)
                                {
                                    break;
                                }
                            }
                            if (babyHit == true)
                            {
                                if (meteors.Count > 0)
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
                                    if (foods[i].hitCount == 0 && dino.hitCount == 0)
                                    {

                                        foods[i].foodHitDino = true;
                                        dino.hitCount = 1;
                                        foods[i].hitCount = 1;

                                    }
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



                            for (int i = 0; i < meteors.Count; i++)
                            {
                                if (meteors[i].Rectangle.Intersects(dino.Rectangle) && swipe.swiping/*Keyboard.GetState().IsKeyDown(Keys.Space)*/)
                                {
                                    soundEffects[3].Play();
                                    piece.destoried = true;
                                    piece.piecePosition.X = meteors[i].meteorPosition.X;
                                    piece.piecePosition.Y = meteors[i].meteorPosition.Y + 100;
                                    meteors.RemoveAt(i);
                                }
                                else if (meteors[i].Rectangle.Intersects(dino.Rectangle) && Keyboard.GetState().IsKeyUp(Keys.Space))
                                {
                                    soundEffects[0].Play();
                                    meteors.RemoveAt(i);
                                    dinoHealth -= 1;
                                    hit.dinoHit = true;

                                }
                            }
                            piece.Update(gameTime);

                            setBabyHitbyMeteor();

                            for (int i = 0; i < nests.Count; i++)
                            {
                                if (nests[i].receivedFood)
                                {
                                    nests[i].animated = false;

                                    if (nests[i].foodFromDaddy < foodMax && deadBaby.babyDying == false) //stops receving when reaching Max
                                    {
                                        eatInstance = soundEffects[1].CreateInstance();
                                        eatInstance.Volume = .7f;
                                        eatInstance.Play();
                                        nests[i].receivedFood = false;

                                        foods.RemoveAt(foodIndex);

                                        nests[i].foodFromDaddy += 1;

                                        if (nests[i].foodFromDaddy == 1)
                                        {
                                            if (dino.dinoPosition.X > nests[i]._position.X)
                                            {
                                                chomps[i].chomp = Content.Load<Texture2D>("chompRight1");
                                                chomps[i].frame = 6;
                                                nests[i]._texture = Content.Load<Texture2D>("DinobabyWaiting1");
                                                nests[i].frame = 80;

                                            }
                                            else
                                            {
                                                chomps[i].chomp = Content.Load<Texture2D>("chompLeft1");
                                                chomps[i].frame = 6;
                                                nests[i]._texture = Content.Load<Texture2D>("DinobabyWaiting1");
                                                nests[i].frame = 80;
                                            }
                                            chomps[i].chomping = true;
                                        }
                                        else if (nests[i].foodFromDaddy == 2)
                                        {
                                            if (dino.dinoPosition.X > nests[i]._position.X)
                                            {
                                                chomps[i].chomp = Content.Load<Texture2D>("chompRight2");
                                                chomps[i].frame = 6;
                                                nests[i]._texture = Content.Load<Texture2D>("DinobabyWaiting2");
                                                nests[i].frame = 80;
                                            }
                                            else
                                            {
                                                chomps[i].chomp = Content.Load<Texture2D>("chompLeft2");
                                                chomps[i].frame = 6;
                                                nests[i]._texture = Content.Load<Texture2D>("DinobabyWaiting2");
                                                nests[i].frame = 80;
                                            }
                                            chomps[i].chomping = true;
                                        }
                                        else if (nests[i].foodFromDaddy == 3)
                                        {
                                            if (dino.dinoPosition.X > nests[i]._position.X)
                                            {
                                                chomps[i].chomp = Content.Load<Texture2D>("chompRight3");
                                                chomps[i].frame = 5;
                                                nests[i]._texture = Content.Load<Texture2D>("DinobabyWaiting3");
                                                nests[i].frame = 80;
                                            }
                                            else
                                            {
                                                chomps[i].chomp = Content.Load<Texture2D>("chompLeft3");
                                                chomps[i].frame = 5;
                                                nests[i]._texture = Content.Load<Texture2D>("DinobabyWaiting3");
                                                nests[i].frame = 80;
                                            }
                                            chomps[i].chomping = true;
                                        }
                                        else if (nests[i].foodFromDaddy == 4)
                                        {
                                            if (dino.dinoPosition.X > nests[i]._position.X)
                                            {
                                                chomps[i].chomp = Content.Load<Texture2D>("chompRight4");
                                                chomps[i].frame = 6;
                                                nests[i]._texture = Content.Load<Texture2D>("DinobabyWaiting4");
                                                nests[i].frame = 80;
                                            }
                                            else
                                            {
                                                chomps[i].chomp = Content.Load<Texture2D>("chompLeft4");
                                                chomps[i].frame = 5;
                                                nests[i]._texture = Content.Load<Texture2D>("DinobabyWaiting4");
                                                nests[i].frame = 80;
                                            }
                                            chomps[i].chomping = true;
                                        }
                                        else if (nests[i].foodFromDaddy == 5)
                                        {
                                            if (dino.dinoPosition.X > nests[i]._position.X)
                                            {
                                                chomps[i].chomp = Content.Load<Texture2D>("chompRight5");
                                                chomps[i].frame = 5;
                                            }
                                            else
                                            {
                                                chomps[i].chomp = Content.Load<Texture2D>("chompLeft5");
                                                chomps[i].frame = 5;
                                            }
                                            chomps[i].chomping = true;
                                        }
                                        else if (nests[i].foodFromDaddy == 6)
                                        {
                                            growns[i].spwan = true;
                                            growns[i].grownPosition = nests[i]._position;
                                            nests[i].grown = true;
                                        }

                                    }
                                }
                            }
                            randomFood(gameTime);

                            for (int i = 0; i < chomps.Count; i++)
                            {
                                if (chomps[i].chomping == true)
                                {
                                    chomps[i].Update(gameTime, nests[i]._position);
                                    //nests[i].animated = false;//didn't change
                                }
                            }

                            for (int i = 0; i < platforms.Count; i++)
                            {
                                for (int j = 0; j < growns.Count; j++)
                                {
                                    if (growns[j].grownSpeed.Y > 0 && IsTouchingTop(growns[j].Rectangle, platforms[i].Rectangle, growns[j].grownSpeed) /*&& dino.dinoPosition.Y <= platforms[i].platformPosition.Y*/)
                                    {
                                        growns[j].grownSpeed.Y = 0f;
                                        babyOnPlatform = true;
                                    }

                                    if ((/*dino.dinoJumpSpeed.X > 0 &&*/ IsTouchingLeft(growns[j].Rectangle, platforms[i].Rectangle, growns[j].grownSpeed)) ||
                                        (/*dino.dinoJumpSpeed.X < 0 &&*/ IsTouchingRight(growns[j].Rectangle, platforms[i].Rectangle, growns[j].grownSpeed)))
                                    {

                                        growns[j].grownSpeed.X = 0f;
                                        babyOnPlatform = false;
                                    }


                                    if (growns[j].grownSpeed.Y < 0 && IsTouchingBottom(growns[j].Rectangle, platforms[i].Rectangle, growns[j].grownSpeed))
                                    {
                                        growns[j].grownSpeed.Y = 0f;
                                        babyOnPlatform = false;
                                    }
                                }

                            }

                            for (int i = 0; i < growns.Count; i++)
                            {
                                if (growns[i].spwan == true)
                                {
                                    for (int j = 0; j < meteors.Count; j++)
                                    {
                                        growns[i].Update(gameTime, dino.dinoPosition, babyOnPlatform, meteors[j].Rectangle);
                                    }
                                }
                                if (growns[i].hp == 0)
                                {
                                    MediaPlayer.Play(failSong);
                                    MediaPlayer.IsRepeating = false;
                                    reload();
                                    currentGameState = GameState.Lose;
                                    exitLoop = true;
                                    break;
                                }
                            }

                            babyOnPlatform = false;

                            babyIsFed();
                            gameWon();

                            updatedNests(gameTime);

                            base.Update(gameTime);
                        }
                        break;

                    case GameState.Win:
                        for (int i = 0; i < wins.Count; i++)
                        {
                            wins[i].Update(gameTime);
                        }
                        if (clouds[0].cloudRec.X + clouds[0].cloud.Width <= 0)
                        {
                            clouds[0].cloudRec.X = clouds[1].cloudRec.X + clouds[1].cloud.Width;
                        }
                        if (clouds[1].cloudRec.X + clouds[1].cloud.Width <= 0)
                        {
                            clouds[1].cloudRec.X = clouds[0].cloudRec.X + clouds[0].cloud.Width;
                        }
                        tree.Update(gameTime);
                        clouds[0].Update(gameTime);
                        clouds[1].Update(gameTime);
                        if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                        {
                            currentGameState = GameState.Playing;
                        }
                        break;

                    case GameState.Lose:
                        if (clouds[0].cloudRec.X + clouds[0].cloud.Width <= 0)
                        {
                            clouds[0].cloudRec.X = clouds[1].cloudRec.X + clouds[1].cloud.Width;
                        }
                        if (clouds[1].cloudRec.X + clouds[1].cloud.Width <= 0)
                        {
                            clouds[1].cloudRec.X = clouds[0].cloudRec.X + clouds[0].cloud.Width;
                        }
                        tree.Update(gameTime);
                        clouds[0].Update(gameTime);
                        clouds[1].Update(gameTime);
                        if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                        {
                            MediaPlayer.Play(gameplaySong);
                            MediaPlayer.IsRepeating = true;
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
                    clouds[0].Draw(spriteBatch);
                    clouds[1].Draw(spriteBatch);
                    tree.Draw(spriteBatch);



                    foreach (Food food in menuFoods)
                    {
                        food.Draw(spriteBatch);
                    }
                    texts[0].Draw(spriteBatch);
                    break;

                case GameState.Playing:
                    spriteBatch.Draw(volcano, new Rectangle(0, 0, GraphicsDevice.DisplayMode.Width/*800*/, GraphicsDevice.DisplayMode.Height/*480*/), Color.White); //background
                    clouds[0].Draw(spriteBatch);
                    clouds[1].Draw(spriteBatch);
                    tree.Draw(spriteBatch);


                    for (int i = 0; i < nests.Count; i++)
                    {
                        if (nests[i].animated == true)
                        {
                            nests[i].Update(gameTime, nests[i]._position);
                        }
                    }

                    switch (dinoHealth)
                    {
                        case 3:
                            menus[0].Draw(spriteBatch);
                            break;

                        case 2:
                            menus[1].Draw(spriteBatch);
                            break;

                        case 1:
                            menus[2].Draw(spriteBatch);
                            break;
                        case 0:
                            menus[3].Draw(spriteBatch);
                            break;

                        default:

                            break;
                    }

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

                        if (chomps[i].chomping == true && deadBaby.babyDying == false)
                        {
                            chomps[i].Draw(spriteBatch);
                        }
                        else if (nests[i].babyHealth > 0 && nests[i].foodFromDaddy < 6)
                        //else if (nests[i].babyHealth > 0 && nests[i].foodFromDaddy < 6 && nests[i].receivedFood == false)//false received didn't change
                         //else if (nests[i].receivedFood == false)
                        {
                            nests[i].Draw(spriteBatch);
                        }
                        else if (growns[i].spwan == true)
                        {
                            growns[i].Draw(spriteBatch);
                           
                        }
                        else if (deadBaby.babyDying == true)
                        {
                            deadBaby.Draw(spriteBatch);
                        }
                    }

                    //deadBaby.Draw(spriteBatch);

                    if (hit.dinoHit == false && dead.dying == false && swipe.swiping == false)
                    {
                        dino.Draw(spriteBatch);
                    }
                    else if (hit.dinoHit == true)
                    {
                        hit.Draw(spriteBatch, dino.dinoPosition.X, dino.dinoPosition.Y);
                    }
                    else if (dead.dying == true)
                    {
                        dead.Draw(spriteBatch);
                    }
                    else if (swipe.swiping == true)
                    {
                        swipe.Draw(spriteBatch, dino.dinoPosition.X, dino.dinoPosition.Y);
                    }


                    //babyReceivedFood();
                    setBabyAsFull();



                    break;

                case GameState.Win:
                    spriteBatch.Draw(volcano, new Rectangle(0, 0, GraphicsDevice.DisplayMode.Width/*800*/, GraphicsDevice.DisplayMode.Height/*480*/), Color.White); //background
                    clouds[0].Draw(spriteBatch);
                    clouds[1].Draw(spriteBatch);
                    tree.Draw(spriteBatch);

                    for (int i = 0; i < wins.Count; i++)
                    {
                        wins[i].Draw(spriteBatch);
                    }

                    texts[1].Draw(spriteBatch);
                    break;

                case GameState.Lose:
                    spriteBatch.Draw(volcano, new Rectangle(0, 0, GraphicsDevice.DisplayMode.Width/*800*/, GraphicsDevice.DisplayMode.Height/*480*/), Color.White); //background
                    clouds[0].Draw(spriteBatch);
                    clouds[1].Draw(spriteBatch);
                    tree.Draw(spriteBatch);

                    texts[2].Draw(spriteBatch);
                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void randomMeteor() //spawn meteors
        {
            int randomX = 0;

            if (meteorAmount > 6) // Spawn cool down (seconds)
            {
                meteorAmount = 0;
                if (meteors.Count < 2) // Amount of meteors allowed on the screen
                {
                    currRandom = random.Next(0, 3);
                    if (currRandom == prevRandom)
                    {
                        while (currRandom == prevRandom)
                        {
                            currRandom = random.Next(0, 3);
                        }
                    }
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
                    prevRandom = currRandom;
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
                if (foods.Count < 3) // Amount of foods allowed on the screen
                {
                    int foodType = random.Next(0, 3);
                    if (foodType == 0)
                    {
                        foods.Add(new Food(Content.Load<Texture2D>("Grapes_Large"), new Vector2(randomX_1, -10), graphics));
                        foods.Add(new Food(Content.Load<Texture2D>("Grapes_Large"), new Vector2(randomX_2, -10), graphics));
                    }
                    else if (foodType == 1)
                    {
                        foods.Add(new Food(Content.Load<Texture2D>("Apple_Large"), new Vector2(randomX_1, -10), graphics));
                        foods.Add(new Food(Content.Load<Texture2D>("Apple_Large"), new Vector2(randomX_2, -10), graphics));
                    }
                    else if (foodType == 2)
                    {
                        foods.Add(new Food(Content.Load<Texture2D>("Orange_Large"), new Vector2(randomX_1, -10), graphics));
                        foods.Add(new Food(Content.Load<Texture2D>("Orange_Large"), new Vector2(randomX_2, -10), graphics));
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
                        foodTimeout = TimeSpan.FromSeconds(0);
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

            foreach (SpriteAnimatedNest nest in nests)
            // foreach (Nest nest in nests)
            {
                if (nest.babyIsfull)
                    totalFull += 1;

            }
            return totalFull;

        }




        /*
        private void babyReceivedFood()
        {
            for (int i = 0; i < nests.Count; i++)
            {
                if (nests[i].receivedFood)
                {
                    if ( nests[i].foodFromDaddy < foodMax) //stops receving when reaching Max
                    {
                        eatInstance = soundEffects[1].CreateInstance();
                        eatInstance.Volume = .7f;
                        eatInstance.Play();
                        nests[i].receivedFood = false;
                        foods.RemoveAt(foodIndex);
                        nests[i].foodFromDaddy += 1;

                        if(nests[i].foodFromDaddy == 1)
                        {
                            chomps[i].chomp = Content.Load<Texture2D>("chompRight1");
                        }
                        else if(nests[i].foodFromDaddy == 2)
                        {
                            chomps[i].chomp = Content.Load<Texture2D>("chompRight2");
                        }
                        else if (nests[i].foodFromDaddy == 3)
                        {
                            chomps[i].chomp = Content.Load<Texture2D>("chompRight3");
                        }
                        else if (nests[i].foodFromDaddy == 4)
                        {
                            chomps[i].chomp = Content.Load<Texture2D>("chompRight4");
                        }

                        chomps[i].Update(gameTime, nests[i]._position);

                        chomps[i].chomping = true;
                    }
                }
            }
        }
        */
        private void setBabyAsFull()
        {
            foreach (SpriteAnimatedNest nest in nests)
            //foreach (Nest nest in nests)
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
                        //nests[j].animated = false;//didn't change

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
                    if (nests[j].Rectangle.Intersects(meteorRectangle) && nests[j].grown == false)
                    {
                        nests[j].hitByMeteor = true;
                        meteorIndex = i;

                    }
                }

            }

        }

        public void nestUpdates(GameTime gameTime)
        {
            for (int i = 0; i < nests.Count; i++)
            {
                if (nests[i].animated == true)
                {

                    nests[i].Update(gameTime, nests[i]._position);
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
            return r1.Left + r1speed.X < r2.Right &&
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
                        menuFoods.Add(new Food(Content.Load<Texture2D>("Grapes_Large"), new Vector2(randomX, -10), graphics));
                    }
                    else if (foodType == 1)
                    {
                        menuFoods.Add(new Food(Content.Load<Texture2D>("Apple_Large"), new Vector2(randomX, -10), graphics));
                    }
                    else if (foodType == 2)
                    {
                        menuFoods.Add(new Food(Content.Load<Texture2D>("Orange_Large"), new Vector2(randomX, -10), graphics));
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

        public void animateNests()
        {
            foreach (SpriteAnimatedNest nest in nests)
            {
                if (nest.receivedFood == false)
                {
                    nest.animated = true;
                    nest.frame = 20;
                }
            }


        }

        public void updatedNests(GameTime gameTime)
        {
            for (int i = 0; i < nests.Count; i++)
            {

                if (nests[i].animated == true)
                {
                    nests[i].Update(gameTime, nests[i]._position);
                }

            }



        }




    }
}