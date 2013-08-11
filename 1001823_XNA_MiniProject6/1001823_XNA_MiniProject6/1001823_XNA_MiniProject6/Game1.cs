using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics; 
using Microsoft.Xna.Framework.Input; 
using Microsoft.Xna.Framework.Media; 



namespace _1001823_XNA_MiniProject6
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game 
    {
        GraphicsDeviceManager graphics;  
        SpriteBatch spriteBatch;

        public enum GameState { PLAY, GAME_OVER } 

        SpriteBG background;
        HUDisplay pDisplay, eDisplay, gOverDisplay;  
        SpaceShipManager playerManager;
        AIPlayer enemy; 
        KeyboardState prevKeyboardState;  
        MouseState prevState; 

        SoundFX2 BGsound;
        BasicSoundFX missileSound; 

        public static GraphicsDevice GDevice; 


        GameData gData;
        GameState state; 

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content"; 
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here 
            background = new SpriteBG();
            eDisplay = new HUDisplay(new Rectangle(10, 60, 200, 80));
            pDisplay = new HUDisplay(new Rectangle(600, 300, 200, 80));
            gOverDisplay = new HUDisplay(new Rectangle(150, 200, 200, 80));  
            playerManager = new SpaceShipManager(3); 
            enemy = new AIPlayer();
            enemy.ShipManager = new SpaceShipManager(5);
            gData = new GameData(); 

            //prevKeyboardState = Keyboard.GetState(); 
            prevState = Mouse.GetState(); 
            this.IsMouseVisible = true;   

            base.Initialize(); GDevice = GraphicsDevice; 
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() 
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here 
            background.Load(GraphicsDevice, this.Content, "Images/StarNight"); 
            eDisplay.Load(this.Content, GraphicsDevice, "Fonts/SpriteFont1"); 
            pDisplay.Load(this.Content, GraphicsDevice, "Fonts/SpriteFont1");
            gOverDisplay.Load(this.Content, GraphicsDevice, "Fonts/SpriteFont1");
            playerManager.Load(this.Content, GraphicsDevice, "Models/eurofighter", SpaceShipManager.BOTTOM);  
            //playerManager.LoadArms(this.Content, "Images/missile"); 
            playerManager.Load3DArms(this.Content, "Models/Lava_Lamp_Missile", 
                            GraphicsDevice, Missile3D.ROTATE_UP); 

            enemy.ShipManager.Load(this.Content, GraphicsDevice, "Models/Ship", SpaceShipManager.TOP);
            //enemy.ShipManager.LoadArms(this.Content, "Images/missile1"); 
            enemy.ShipManager.Load3DArms(this.Content, "Models/Lava_Lamp_Missile", 
                            GraphicsDevice, Missile3D.ROTATE_DOWN); 
            enemy.InitShip();

            BGsound = new SoundFX2(this.Content, "Sounds/game_GB_sound");  
            BGsound.Loop = true; 
            BGsound.Play();   

            missileSound = new BasicSoundFX(this.Content, "Sounds/missile_sound");

            gData.GameLevel = 1;
            gData.PlayerShips = playerManager.NUM_SHIPS; 
            gData.EnemyShips = enemy.ShipManager.NUM_SHIPS; 

            gData.EnemyHits = 0;   
            gData.PlayerHits = 0;

            state = GameState.PLAY; 
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            switch(state)
            {
                case GameState.PLAY: 
                    // TODO: Add your update logic here 
                    background.Update(gameTime); 

                    playerManager.Update(gameTime); 
                    enemy.Update(gameTime);

                    //if(enemy.ShipManager.CurrentShip.CheckCollision(playerManager.CurrentShip.Sphere))  
                    if (enemy.ShipManager.CheckCollision(playerManager.FiredItems))
                    {
                        gData.EnemyHits += 1;
                        if (gData.EnemyHits > 9)
                        {
                            gData.EnemyHits = 0;
                            if (!enemy.ShipManager.NextShip())
                            {
                                NextLevel(); 
                            }
                        }
                    }

                    if (playerManager.CheckCollision(enemy.ShipManager.FiredItems)) 
                    {
                        gData.PlayerHits += 1;
                        if (gData.PlayerHits > 2)
                        {
                            gData.PlayerHits = 0;
                            if(!playerManager.NextShip())
                            {
                                state = GameState.GAME_OVER;
                                gOverDisplay.DisplayItems["STATUS: "] = "Game Over! YOU LOSE!!";
                                gOverDisplay.DisplayItems["MESSAGE: "] = "Press 'R' to Play again OR ESC to Exit.";
                                BGsound.Stop(); BGsound = null; 
                            }
                        }
                    }
            break; 
                case GameState.GAME_OVER:
            break;
        }
            pDisplay.DisplayItems["Player Ship: "] = playerManager.NUM_SHIPS + "";
            pDisplay.DisplayItems["Hits: "] = gData.PlayerHits + "";
            eDisplay.DisplayItems["Level: "] = gData.GameLevel + ""; 

            eDisplay.DisplayItems["Enemy Ship: "] = enemy.ShipManager.NUM_SHIPS + "";
            eDisplay.DisplayItems["Hits: "] = gData.EnemyHits + ""; 

            HandleMouse(Mouse.GetState());

            handleKeyboard(Keyboard.GetState()); 

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here 
            spriteBatch.Begin(); 
                background.Draw(spriteBatch);
                pDisplay.Draw(spriteBatch);
                eDisplay.Draw(spriteBatch);
                if (state == GameState.GAME_OVER) gOverDisplay.Draw(spriteBatch); 
                //playerManager.Draw(spriteBatch);  
                //enemy.Draw(spriteBatch);  
            spriteBatch.End();

            ///these has to be drawn outside cos they can't be 
            ///drawn inside a spritebatch.begin() 
            enemy.ShipManager.CurrentShip.Draw(); 
            playerManager.CurrentShip.Draw();
            enemy.Draw(spriteBatch);  
            playerManager.Draw(spriteBatch); 

            base.Draw(gameTime);
        }


        public void NextLevel() 
        {
            if (gData.GameLevel == 1)
            {
                gData.GameLevel = 2; 
                playerManager.NUM_SHIPS = 3;
                enemy.ShipManager.NUM_SHIPS = 7; 
            }
            else if (gData.GameLevel == 2)
            {
                gData.GameLevel = 3;
                playerManager.NUM_SHIPS = 3;
                enemy.ShipManager.NUM_SHIPS = 9; 
            }
            else
            {
                state = GameState.GAME_OVER;
                gOverDisplay.DisplayItems["STATUS: "] = "YOU WIN!!!";
                gOverDisplay.DisplayItems["MESSAGE: "] = "Press 'R' to Play again OR ESC to Exit.";
                BGsound.Stop(); BGsound = null; 
            }
        }


        private void handleKeyboard(KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Left) == true)
            {
                playerManager.CurrentShip.Move(SpaceShip.LEFT);

                if (prevKeyboardState.IsKeyDown(Keys.Left) == false)
                {

                }
            }
            else if (keyboardState.IsKeyDown(Keys.Right) == true)
            {
                playerManager.CurrentShip.Move(SpaceShip.RIGHT);

                if (prevKeyboardState.IsKeyDown(Keys.Right) == false)
                {

                }
            }
            
            if (keyboardState.IsKeyDown(Keys.Up) == true)
            {
                ///ironic, isn't it? ;)
                playerManager.CurrentShip.Move(SpaceShip.DOWN); 
            }
            else if (keyboardState.IsKeyDown(Keys.Down) == true)
            {
                playerManager.CurrentShip.Move(SpaceShip.UP); 
            }


            if (keyboardState.IsKeyDown(Keys.F) == true)
            {
                if (prevKeyboardState.IsKeyDown(Keys.F) == false)
                {
                    //playerManager.Fire(SpaceShip.UP);  
                    playerManager.Fire3D(SpaceShip.UP);   
                    missileSound.Play();  
                }
            }
            else if (keyboardState.IsKeyDown(Keys.S) == true)
            {
                if (prevKeyboardState.IsKeyDown(Keys.S) == false)
                {
                    FileManager.GetFileManager.SaveData(gData, "GameData", "GameData.xml"); 
                }
            }
            else if (keyboardState.IsKeyDown(Keys.L) == true)
            {
                if (prevKeyboardState.IsKeyDown(Keys.L) == false)
                {
                    gData = FileManager.GetFileManager.ReadFromFile("GameData", "GameData.xml");
                }
            } 

            if (keyboardState.IsKeyDown(Keys.U) == true)
            {
                if (prevKeyboardState.IsKeyDown(Keys.U) == false)
                {
                    BGsound.Volume(SoundFX2.VOLUMEUP);
                }
            }
            else if (keyboardState.IsKeyDown(Keys.D) == true)
            {
                if (prevKeyboardState.IsKeyDown(Keys.D) == false)
                {
                    BGsound.Volume(SoundFX2.VOLUMEDOWN);
                }
            }
            
            
            if (keyboardState.IsKeyDown(Keys.Space) == true)
            {
                if (prevKeyboardState.IsKeyDown(Keys.Space) == false)
                {
                    if (BGsound.IsPlaying())
                    {
                        BGsound.Stop();  
                    }
                    else
                    {
                        BGsound.Play();  
                    }
                }
            }
            
            if (keyboardState.IsKeyDown(Keys.Escape) == true)
            {
                if (!(state == GameState.GAME_OVER))
                {
                    FileManager.GetFileManager.SaveData(gData, "GameData", "GameData.xml");
                }
                this.Exit(); 
            }


            if (keyboardState.IsKeyDown(Keys.R) == true)
            {
                if (prevKeyboardState.IsKeyDown(Keys.R) == false)
                {
                    if (state == GameState.GAME_OVER) 
                    {
                        this.Initialize();
                        this.LoadContent(); 
                    }
                }
            }
            

            prevKeyboardState = keyboardState; 
        }


        public void HandleMouse(MouseState curState)
        {
            if (curState.LeftButton == ButtonState.Pressed &&
                    prevState.LeftButton == ButtonState.Released)
            {
                
            }

            prevState = curState;
        }
    }
}
