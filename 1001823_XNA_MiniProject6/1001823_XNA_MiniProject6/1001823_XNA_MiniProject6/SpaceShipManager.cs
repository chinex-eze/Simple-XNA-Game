using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;  


namespace _1001823_XNA_MiniProject6
{   
    class SpaceShipManager
    {
        private int mNUM_SHIPS = 3;             ///defaults to 3 ships 
        private SpaceShip mCurrentShip = null;
        private List<IFireAble> mFiredItems = new List<IFireAble>(); 
        private Missile missile = new Missile(); 
        private Missile3D missile3D = new Missile3D();  

        public static readonly Vector3 BOTTOM = new Vector3(0, -4.20f, 0);
        public static readonly Vector3 TOP = new Vector3(0, 4.20f, 0); 

        public SpaceShipManager()
        {
            //NextShip();
        }

        public SpaceShipManager(int num_ships)
        {
            mNUM_SHIPS = num_ships;  
        }

        public void Load(ContentManager theContentManager, GraphicsDevice graphicsDevice,
                                    string theAssetName, Vector3 initPosition)
        {    
            mCurrentShip = new SpaceShip();
            mCurrentShip.Load(theContentManager, graphicsDevice, theAssetName);
            mCurrentShip.Position = initPosition;

                if (initPosition == SpaceShipManager.TOP)
                {
                    mCurrentShip.RotationAngle = 183f;
                    //mCurrentShip.Scale = 0.2f; 
                    mCurrentShip.World = Matrix.CreateRotationZ(MathHelper.ToRadians(183)) *
                                Matrix.CreateTranslation(SpaceShipManager.TOP); 
                }
                else
                {
                    mCurrentShip.Scale = 0.06f;
                    mCurrentShip.RotationAngle = 180f;
                    mCurrentShip.World = Matrix.CreateScale(0.06f) * 
                                Matrix.CreateRotationZ(MathHelper.ToRadians(180)) *
                                Matrix.CreateTranslation(SpaceShipManager.BOTTOM);
                }
                mCurrentShip.GetBoundingSphere(); 
        }


        public void LoadArms(ContentManager theContentManager, string theAssetName)
        {
            missile.LoadContent(theContentManager, theAssetName);
            missile.SpaceShip = this.mCurrentShip; 
        } 


        public void Load3DArms(ContentManager theContentManager, string theAssetName, 
                            GraphicsDevice device, Matrix missileRotate)  
        {
            missile3D.LoadContent(theContentManager, theAssetName, 
                        this.mCurrentShip, device);
            missile3D.Scale = 0.006f;
            missile3D.MissileRotation = missileRotate; 
        }

        public int NUM_SHIPS
        {
            get { return mNUM_SHIPS; }
            set { mNUM_SHIPS = value; }
        }

        public SpaceShip CurrentShip
        {
            get { return mCurrentShip; }
            //set { mCurrentShip = value; }
        }

        public bool NextShip()
        {
            if (mNUM_SHIPS > 0)
            { 
                mNUM_SHIPS -= 1;
                return true; 
            }
            return false;  
        }


        public List<IFireAble> FiredItems
        {
            get { return mFiredItems; } 
        }


        public void Fire(Vector3 direction)
        {
            mFiredItems.Add(mCurrentShip.Fire(missile.ShallowCopy(), direction));  
        }


        public void Fire3D(Vector3 direction)
        {
            mFiredItems.Add(mCurrentShip.Fire(missile3D.ShallowCopy(), direction));
        }


        public void Update(GameTime gTime)
        {
            for (int num = 0; num < mFiredItems.Count; num++ ) 
            { 
                mFiredItems[num].Update(gTime);
                if (mFiredItems[num].Position.Y < SpaceShip.maxHeight.Y ||   
                    mFiredItems[num].Position.Y > SpaceShip.maxDepth.Y)      
                {
                    mFiredItems.RemoveAt(num); 
                }
            } 
        }

        public void Draw(SpriteBatch theSpriteBatch)
        {
            try
            {
                foreach (IFireAble firedItem in mFiredItems)
                {
                    firedItem.Draw(theSpriteBatch);
                }
            }
            catch(InvalidOperationException e)
            {
                //do nothing ;) ....shit happens
            }
        }

        public bool CheckCollision(IEnumerable<IFireAble> shells)
        {
            List<IFireAble> shellsList = (List<IFireAble>)shells; 

            for (int num = 0; num < shellsList.Count; num++)
            {
                if (mCurrentShip.CheckCollision(shellsList[num].Sphere))
                {
                    shellsList.RemoveAt(num); 
                    return true; 
                }
            }
            return false; 
        }

        public void RemoveItem(int index)
        {
            try
            {
                mFiredItems.RemoveAt(index);
            }
            catch (ArgumentOutOfRangeException e)
            {
                //do nothing...
            }
        }
    }
}