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
    public class GameData
    {
        //private static GameData instance = null;     
        //private static object myLocker = new object();  

        public GameData()
        {
        }

        /*public static GameData GetGameData
        {
            get
            {
                lock (myLocker)
                {
                    if (instance == null)
                    {
                        instance = new GameData();
                    }
                    return instance;
                }
            }
        }*/

        /////////////here here//////////////
        public int PlayerShips { get; set; }  
        public int EnemyShips { get; set; }  

        public int GameLevel { get; set; }

        public int PlayerHits { get; set; } 
        public int EnemyHits { get; set; } 
    }
}
