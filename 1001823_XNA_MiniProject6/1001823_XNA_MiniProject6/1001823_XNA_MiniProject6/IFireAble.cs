using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _1001823_XNA_MiniProject6
{
    interface IFireAble
    {
        //position from where the item/bullet is fired 
        Vector3 FireFrom
        {
            get;
            set; 
        }

        Vector3 Direction 
        {
            get;
            set;
        }

        BoundingSphere Sphere
        {
            get;
            set;
        } 

        Vector2 Position { get; set; }

        void Update(GameTime gTime);
        void Draw(SpriteBatch theSpriteBatch);
        void CreateBoundingSphere();  
    }
}
