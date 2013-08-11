using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework; 
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Timers; 

namespace _1001823_XNA_MiniProject6
{
    class AIPlayer
    {
        private SpaceShipManager mShipManager;
        private Timer timer = new Timer(), moveTimer = new Timer(); 
        bool bMoveRight = false;
        private static Random mRandom = new Random(); 

        public AIPlayer()
        {
            timer.Elapsed += new ElapsedEventHandler(TickFunction); 
            moveTimer.Elapsed += new ElapsedEventHandler(moveTimerFunction);
            timer.Interval = (1000) * 2; ///every two seconds 
            timer.Enabled = true;
            timer.Start();

            moveTimer.Interval = (1000) * 3;
            moveTimer.Enabled = true;
            moveTimer.Start();
        }

        public SpaceShipManager ShipManager
        {
            get { return mShipManager; }
            set { mShipManager = value; } 
        }

        public void InitShip()
        {
            mShipManager.CurrentShip.maxLeft = -6.0f;  
            mShipManager.CurrentShip.maxRight = 6.0f;  
        }

        public void Update(GameTime gameTime)
        {
            mShipManager.Update(gameTime);
            makeMove(); 
        }

        public void Draw(SpriteBatch theSpriteBatch)
        {
            mShipManager.Draw(theSpriteBatch); 
        }

        private void TickFunction(object source, ElapsedEventArgs e)
        {
            //mShipManager.Fire(SpaceShip.DOWN);  
            mShipManager.Fire3D(SpaceShip.DOWN); 
        } 

        private void moveTimerFunction(object source, ElapsedEventArgs e) 
        {
            bMoveRight = !bMoveRight;

            moveTimer.Stop(); 
            moveTimer.Interval = (1000) * mRandom.Next(1, 6); 
            moveTimer.Enabled = true;
            moveTimer.Start(); 
        }

        private void makeMove()
        {
            if (bMoveRight)
            {
                ShipManager.CurrentShip.Move(SpaceShip.RIGHT / 2);  ///half the normal speed
            }
            else
            {
                ShipManager.CurrentShip.Move(SpaceShip.LEFT / 2); 
            } 
        }
    }
}
