using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 

using Microsoft.Xna.Framework; 
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _1001823_XNA_MiniProject6
{
    class Missile : Sprite, IFireAble 
    {
        private Vector3 mFireFrom;
        private Vector3 mDirection;

        private int mSpeed = 50; //default missile speed  

        private BoundingSphere sphere; 
        private SpaceShip mSpaceShip; ///temp; this should be an interface 

        public Missile()
        {
        }


        public Vector3 FireFrom
        {
            get { return mFireFrom; }
            set
            {
                mFireFrom = value;  
                Vector3 temp = Game1.GDevice.Viewport.Project(value, //(Vector3.Zero, 
                                mSpaceShip.Projection, mSpaceShip.View,
                                    Matrix.CreateTranslation(Vector3.Zero)); 

                Position = new Vector2(temp.X, temp.Y); 
            } 
        }

        public Vector3 Direction
        {
            get { return mDirection; }
            set { mDirection = value * mSpeed; } 
        }


        public override void LoadContent(ContentManager theContentManager, string theAssetName) 
        {
            base.LoadContent(theContentManager, theAssetName);
            base.Scale = 0.1f; 
            CreateBoundingSphere(); 
        }

        public void LoadContent(ContentManager theContentManager, string theAssetName, 
                            SpaceShip sShip)
        {
            this.LoadContent(theContentManager, theAssetName);
            mSpaceShip = sShip; 
        }

        public int Speed
        {
            get { return mSpeed; }
            set { mSpeed = value; } 
        }

        public void Update(GameTime gtime)
        {
            //this changes the vector2D Position value 
            base.Move(new Vector2(mDirection.X, mDirection.Y));

            //unproject the new Position value to a Vector3D value 
            /*mFireFrom = Game1.GDevice.Viewport.Unproject(new Vector3(Position, 0),
                    mSpaceShip.Projection,
                     mSpaceShip.View, 
                //Matrix.CreateTranslation(new Vector3(0, -4.2f, 0))); 
                     mSpaceShip.World); */
            //sphere.Center = mFireFrom; 
            sphere.Center += mDirection; 
        }

        public new void Draw(SpriteBatch theSpriteBatch)
        {
            base.Draw(theSpriteBatch); 
        }

        public Missile ShallowCopy()
        {
            Missile newMissile = (Missile)this.MemberwiseClone();
            newMissile.Sphere = new BoundingSphere(); 
            return newMissile; 
        }

        public new Vector2 Position
        {
            get { return base.Position; }
            set { base.Position = value; } 
        }

        public BoundingSphere Sphere 
        {
            get { return sphere; }
            set { sphere = value; }
        }

        public SpaceShip SpaceShip
        {
            get { return this.mSpaceShip; }
            set { this.mSpaceShip = value; } 
        }

        public void CreateBoundingSphere() 
        {
            sphere = new BoundingSphere();
            //sphere.Center = mFireFrom;    
            sphere.Radius = Math.Max(base.Size.Height / 2, base.Size.Width / 2); 
            //sphere.Radius = 9.8f; 
        } 
    }
}
