using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics; 


namespace _1001823_XNA_MiniProject6
{
    class Missile3D : IFireAble 
    {
        private Vector3 mFireFrom; 
        private Vector3 mDirection;
        private float mSpeed = 1f;//50.0f;   //default missile speed  
        private BoundingSphere mSphere;
        private SpaceShip mShip;
        private float mScale = 1.0f; 
        private Model mModel;
        private Matrix mWorld;
        private float mAspectRatio;

        private Matrix mMissileRotation;

        public static readonly Matrix ROTATE_UP = Matrix.CreateRotationZ(MathHelper.ToRadians(90));     
        public static readonly Matrix ROTATE_DOWN = Matrix.CreateRotationZ(MathHelper.ToRadians(270));  

        public Missile3D()
        {
        }

        public Missile3D LoadContent(ContentManager theContentManager, string theAssetName,
                            SpaceShip sShip, GraphicsDevice device)
        {
            mShip = sShip; 
            mAspectRatio = (float)device.Viewport.Width / (float)device.Viewport.Height;  

            mModel = theContentManager.Load<Model>(theAssetName);
            
            //mMissileRotation = ROTATE_UP; 
            
            return this; 
        }

        public Vector3 FireFrom
        {
            get { return mFireFrom; } 
            set { mFireFrom = value; } 
        }

        public Vector3 Direction
        {
            get { return mDirection; }
            set { mDirection = value * mSpeed; }
        }

        public float Speed
        {
            get { return mSpeed; }
            set { mSpeed = value; } 
        }

        public BoundingSphere Sphere
        {
            get { return mSphere; }
            set { mSphere = value; }
        }

        public Matrix World
        {
            get { return mWorld; } 
            set { mWorld = value; } 
        }

        private Vector2 mPosition;
        public Vector2 Position
        {
            get { return mPosition; }
            set { mPosition = value; } 
        }

        public SpaceShip Ship
        {
            get { return mShip; }
            set { mShip = value; } 
        }

        public float Scale
        {
            get { return mScale; }
            set { mScale = value; } 
        }

        public Matrix MissileRotation
        {
            get { return mMissileRotation; }
            set { mMissileRotation = value; }
        }

        public void CreateBoundingSphere()
        {
            foreach (ModelMesh mesh in mModel.Meshes)
            {
                if (mSphere.Radius == 0)
                    mSphere = mesh.BoundingSphere;
                else
                    mSphere = BoundingSphere.CreateMerged(mSphere, mesh.BoundingSphere);
            }

            mSphere.Center = mFireFrom;

            mSphere.Radius *= mScale; 
        }

        public Missile3D ShallowCopy()
        {
            Missile3D newMissile = (Missile3D)this.MemberwiseClone(); 
            newMissile.Sphere = new BoundingSphere(); 
            return newMissile; 
        }

        public void Draw(SpriteBatch theSpriteBatch)
        {
            foreach (ModelMesh mesh in mModel.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = this.mWorld;
                    effect.View = mShip.View;               ///these values come from the ship 
                    effect.Projection = mShip.Projection;
                }

                mesh.Draw();
            }
        }

        public void Update(GameTime gtime)
        {
            mFireFrom -= mDirection;

            mWorld = Matrix.CreateScale(mScale) * 
                    Matrix.CreateRotationY(MathHelper.ToRadians(90)) *    
                    mMissileRotation *  
                    Matrix.CreateTranslation(mFireFrom);    

            mSphere.Center -= mDirection;   
        } 
    }
}
