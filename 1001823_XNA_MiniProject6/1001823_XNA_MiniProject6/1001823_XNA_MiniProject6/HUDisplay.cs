using System;
using System.Collections.Generic; 
using System.Collections; 
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics; 
using Microsoft.Xna.Framework.Content; 
using Microsoft.Xna.Framework;  

namespace _1001823_XNA_MiniProject6
{
    class HUDisplay
    {
        private SpriteFont mFont { get; set; }  
        private Texture2D mRectangleBG;

        private Rectangle mSize; 
        private Color mBGcolor;

        public Hashtable DisplayItems = new Hashtable();  

        public String mScore { get; set; }
        public String mLevel { get; set; }
        public String mLives { get; set; }

        public HUDisplay(Rectangle size)
        {
            mSize = size; 
        }

        public Rectangle Size
        {
            get { return mSize; }
            set { mSize = value; } 
        }

        public Color BDcolor
        {
            get { return mBGcolor; }
            set { mBGcolor = value; } 
        }

        public void Load(ContentManager theContentManager, GraphicsDevice device, String theAssetName)
        {
            mRectangleBG = new Texture2D(device, 1, 1); 
            mRectangleBG.SetData(new[] { Color.Transparent }); 
            mFont = theContentManager.Load<SpriteFont>(theAssetName);  
        }

        public void Draw(SpriteBatch theSpriteBatch)
        {
            //theSpriteBatch.Draw(mRectangleBG, new Rectangle(10, 20, 200, 70), Color.LightGoldenrodYellow);
            theSpriteBatch.Draw(mRectangleBG, mSize, Color.LightGoldenrodYellow);

            /*
                theSpriteBatch.DrawString(mFont, "Score: " + mScore, new Vector2(12, 20), Color.White);
                theSpriteBatch.DrawString(mFont, "Level: " + mLevel, new Vector2(12, 40), Color.White);
                theSpriteBatch.DrawString(mFont, "Lives: " + mLives, new Vector2(12, 60), Color.White);
            */ 
            int index = 0; 
            foreach (DictionaryEntry entry in DisplayItems)
            {
                theSpriteBatch.DrawString(mFont, (String)entry.Key + entry.Value, 
                                    new Vector2( (mSize.X+2), ((mSize.Y) + (index * 20)) ), ///not good, I know ;)
                                    Color.White);
                index++; 
            }
        }
    }
}
