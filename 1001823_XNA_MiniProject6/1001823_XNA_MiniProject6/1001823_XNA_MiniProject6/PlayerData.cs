using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using Microsoft.Xna.Framework.Storage;  

namespace _1001823_XNA_MiniProject6
{
    [Serializable()] 
    public class PlayerData
    {
        public PlayerData()
        {
        }

        public int Scores
        {
            get;
            set;
        }

        public int Lives
        {
            get;
            set; 
        }

        ///TODO: this should apply to both player and enemy ship 
        public int Level { get; set; } 

        public int Hits { get; set; } 
    }
}
