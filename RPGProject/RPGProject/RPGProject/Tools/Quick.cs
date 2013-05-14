using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Windows.Forms;

namespace RPGProject.Tools
{
    static class Quick
    {
        static public Game1 game;
        static public List<Object> objectlist = new List<Object>();
        static public SaveLoad saveload = new SaveLoad();
        static public OnClick onClick = new OnClick() ;
        static public Dictionary<string, Model> dicoModel = new Dictionary<string, Model>();
		static public Dictionary<BiomeType, Texture2D> groundTexture;
        static public Point bounds;
		static public ContentManager content;
		static public SpriteBatch spriteBatch;
		static public GraphicsDevice device;
		static public GraphicsDeviceManager graphics;
		static public MouseState ms;
		static public KeyboardState ks;
		static public Screen screen = Screen.PrimaryScreen;
        static public Dictionary<string, Texture2D> elementInterface;
        static public Personnage player;
        static public Dictionary<TypeFont, SpriteFont> dicoFont = new Dictionary<TypeFont, SpriteFont>();
        static public Texture2D[] biomeTextures;
        static public Texture2D biomeAtlas;
        static public Texture3D biome3D;
        static public Dictionary<int, Texture2D> dicoTextureObjectInventory = new Dictionary<int, Texture2D>();
		static public ActionMouse actmouse;
        static public List<ClickableArea> activeClickableArea;
        static public int distInteract = 5;
        static public Dictionary<string, GameObject> gameobject = new Dictionary<string, GameObject>();

		static public void init()
		{
            loadTextures();

            elementInterface.Add("Back", content.Load<Texture2D>("InterfaceComponent\\Back"));
            elementInterface.Add("Surface", content.Load<Texture2D>("InterfaceComponent\\Surface"));
            elementInterface.Add("Life", content.Load<Texture2D>("InterfaceComponent\\Life"));
            elementInterface.Add("Mana", content.Load<Texture2D>("InterfaceComponent\\Mana"));
            elementInterface.Add("Loupe", content.Load<Texture2D>("InterfaceComponent\\loupered"));
            initDicoFont();
            initDicoTextureInventoryObject();
            iniDicoModel();
            initObject();
            initMapObject();
		}

       static void loadTextures()
        {
            groundTexture = new Dictionary<BiomeType, Texture2D>();
            elementInterface = new Dictionary<string, Texture2D>();


            groundTexture.Add(BiomeType.None, content.Load<Texture2D>("Textures/Sand"));
            groundTexture.Add(BiomeType.Water, content.Load<Texture2D>("Textures/Sand"));
            groundTexture.Add(BiomeType.SubtropicalDesert, content.Load<Texture2D>("Textures/Sand"));
            groundTexture.Add(BiomeType.GrasslandDesert, content.Load<Texture2D>("Textures/Grass"));
            groundTexture.Add(BiomeType.Savanna, content.Load<Texture2D>("Textures/Gravel"));
			groundTexture.Add(BiomeType.Shrubland, content.Load<Texture2D>("Textures\\dirt3"));
            groundTexture.Add(BiomeType.Taiga, content.Load<Texture2D>("Textures/stone"));
            groundTexture.Add(BiomeType.Tundra, content.Load<Texture2D>("Textures/stone2"));
            groundTexture.Add(BiomeType.TemperateForest, content.Load<Texture2D>("Textures/Grass2"));
            groundTexture.Add(BiomeType.TropicalRainforest, content.Load<Texture2D>("Textures/Grass2"));
            groundTexture.Add(BiomeType.TemperateRainforest, content.Load<Texture2D>("Textures/Grass2"));

            biomeTextures = new Texture2D[groundTexture.Count];
            biome3D = new Texture3D(device, 256, 256, groundTexture.Count, false, groundTexture[0].Format);
            Color[] texData = new Color[256 * 256*11];

            foreach (BiomeType t in groundTexture.Keys)
            {
                biomeTextures[(int)t] = groundTexture[t];
                
                biomeTextures[(int)t].GetData(texData, 256*256*((int)t), 256*256);
                
            }

            biome3D.SetData(texData);

            //biomeAtlas = content.Load<Texture2D>("Texture/biomeAtlas");
            
        }
       static private void initDicoFont()
        {
          dicoFont.Add(TypeFont.Texte,content.Load<SpriteFont>("Arial"));
        }
       static private void initDicoTextureInventoryObject()
        {
            int i =0;
            StreamReader sr = new StreamReader(".\\Data\\InventoryTexture.conf");
            while (!sr.EndOfStream)
            {
                string s = sr.ReadLine();
             dicoTextureObjectInventory.Add(i,content.Load<Texture2D>("InventoryObjectTexture\\" + s));
             i++;
            }
        }
       static private void iniDicoModel()
        {
            StreamReader sr = new StreamReader(".\\Data\\DicoObject.txt");
            while (!sr.EndOfStream)
            {
                string s = sr.ReadLine();
                if (s != "name|chemin")
                {
                    dicoModel.Add(s.Split('|')[0], content.Load<Model>(s.Split('|')[1]));   
                }
            }
        }
       static public void initObject()
        {
            StreamReader sr = new StreamReader(".\\Data\\Objects.txt");
            while (!sr.EndOfStream)
            {
                string s = sr.ReadLine();
                if (!s.StartsWith("code|name|value|weight|takeable|modelname"))
                {
                    objectlist.Add(new Object(Convert.ToInt32(s.Split('|')[0]), s.Split('|')[1], Convert.ToInt32(s.Split('|')[2]), Convert.ToInt32(s.Split('|')[3]), Convert.ToBoolean(s.Split('|')[4]), dicoModel[s.Split('|')[5]], dicoTextureObjectInventory[Convert.ToInt32(s.Split('|')[6])]));
                }
            }
        }
       static public void initMapObject()
       {
           string s;
           StreamReader sr = new StreamReader(".\\Data\\gameObject.txt");
           while (!sr.EndOfStream)
           {
               s = sr.ReadLine();
               Console.WriteLine("add : " + s + " to game object");
               GameObject go = new GameObject(new ObjectModel(content.Load<Model>(s.Split(':')[1]), Vector3.Zero));
               gameobject.Add(s.Split(':')[0], go);
           }
       }
    }
}
