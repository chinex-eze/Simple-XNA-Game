using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework; 

namespace _1001823_XNA_MiniProject6
{
    class SpriteBG
    {
        public SpriteBG()
        {

        }

        private Vector2 mTextureSize, mPosition, mOrigin;
        public float mScale = 1.0f;
        private int mScreenHeight, mScreenWidth; 

        public Texture2D mSpriteTexture { get; set; } 

        //returns a ref to this object so that it's 
        ///possible to do this new SpriteBG().Load(...) 
        public SpriteBG Load(GraphicsDevice graphicsDevice, ContentManager theContentManager, 
                            String theAssetName)
        {
            mSpriteTexture = theContentManager.Load<Texture2D>(theAssetName);
            //mTextureSize = new Vector2(mSpriteTexture.Width, mSpriteTexture.Height);
            mTextureSize = new Vector2(0, mSpriteTexture.Height);
            mScreenHeight = graphicsDevice.Viewport.Height;  
            mScreenWidth = graphicsDevice.Viewport.Width;
            mPosition = new Vector2(mScreenWidth / 2, mScreenHeight / 2);
            mOrigin = new Vector2(mSpriteTexture.Width / 2, 0);
            //the scale is the greater value
            //mScale = (mScreenHeight / mSpriteTexture.Height > mScreenWidth / mSpriteTexture.Width) ?
                    //(mScreenHeight / mSpriteTexture.Height) : (mScreenWidth / mSpriteTexture.Width); 
            mScale = 1.5f; 

            return this; 
        }

        public void Update(GameTime gameTime)
        {
            float elapsed = 100 * ((float)gameTime.ElapsedGameTime.TotalSeconds);
            mPosition.Y += elapsed;
            mPosition.Y = mPosition.Y % mSpriteTexture.Height;
        }

        public void Draw(SpriteBatch theSpriteBatch)
        {
            // Draw the texture, if it is still onscreen.
            if (mPosition.Y < mScreenHeight)
            {
                theSpriteBatch.Draw(mSpriteTexture, mPosition, null,
                     Color.White, 0, mOrigin, mScale, SpriteEffects.None, 0f); 
            }
            // Draw the texture a second time, behind the first,
            // to create the scrolling illusion.
            theSpriteBatch.Draw(mSpriteTexture, mPosition - mTextureSize, null,
                 Color.White, 0, mOrigin, mScale, SpriteEffects.None, 0f); 
        }
    }
}
