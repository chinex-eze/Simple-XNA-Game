using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace _1001823_XNA_MiniProject6
{
    public class Sprite
    {
        public Sprite()
        {
            Position = Vector2.Zero;
        }


        public Sprite(ContentManager theContentManager, string theAssetName)
        {
            mSpriteTexture = theContentManager.Load<Texture2D>(theAssetName);
            Size = new Rectangle(0, 0, (int)(mSpriteTexture.Width * Scale), (int)(mSpriteTexture.Height * Scale));  
            Position = Vector2.Zero; 
        }

        public Sprite(ContentManager theContentManager, string theAssetName, Vector2 position)
        {
            mSpriteTexture = theContentManager.Load<Texture2D>(theAssetName);
            Size = new Rectangle(0, 0, (int)(mSpriteTexture.Width * Scale), (int)(mSpriteTexture.Height * Scale));  
            Position = position;
        }


        public Vector2 Position;// { get; set; } 

        public Texture2D mSpriteTexture { get; set; } 

        public Rectangle Size;// { get; set; }

        private float mScale = 1.0f; 

        //direction constants
        public static readonly Vector2 LEFT = new Vector2(-1, 0);
        public static readonly Vector2 RIGHT = new Vector2(1, 0);
        public static readonly Vector2 UP = new Vector2(0, -1);
        public static readonly Vector2 DOWN = new Vector2(0, 1);

        public float Scale
        {
            get { return mScale; }

            set
            {
                mScale = value;
                //Recalculate the Size of the Sprite with the new scale 
                Size = new Rectangle(0, 0, (int)(mSpriteTexture.Width * Scale), (int)(mSpriteTexture.Height * Scale)); 
            } 
        }


        public virtual void LoadContent(ContentManager theContentManager, string theAssetName)
        {
            mSpriteTexture = theContentManager.Load<Texture2D>(theAssetName); 
            
            Size = new Rectangle(0, 0, (int)(mSpriteTexture.Width * Scale), (int)(mSpriteTexture.Height * Scale));
            Position = Vector2.Zero; 
        }

        public void Draw(SpriteBatch theSpriteBatch)
        { 
            theSpriteBatch.Draw(mSpriteTexture, Position, 
                    new Rectangle(0, 0, mSpriteTexture.Width, mSpriteTexture.Height), Color.White, 
                    0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0); 
        }

        public void Move(Vector2 direction)
        {
            Position += direction; 
        }
    }
}
