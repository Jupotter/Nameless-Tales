using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
namespace RPGProject
{
 public   class InventoryInterface : DrawableGameComponent
    {
       

        public  Texture2D tex = null;

        public  Texture2D back = null;

        public  Texture2D backwind = null;

        Texture2D cadre = null;

        public Vector2 decalage = Vector2.Zero;
        
        Dictionary<Tools.Armorplace, Rectangle> elementPlace = new Dictionary<Tools.Armorplace,Rectangle>();

        Game1 game;

        InventoryStockableInterface isi;

        FenetreStat fenstat;

        public InventoryInterface(Texture2D image,Texture2D cadre, Texture2D background , Texture2D backindows , Game1 game):base(game)
        {
            backwind = backindows;
            this.game = game;
            tex = image;
            back = background;
            this.cadre = cadre;
            this.DrawOrder = 9000;
            Visible = false;
            game.Components.Add(this);
            decalage = new Vector2((Tools.Quick.graphics.PreferredBackBufferWidth/2 - back.Bounds.Width) / 2, ( Tools.Quick.graphics.PreferredBackBufferHeight -back.Bounds.Height ) / 2);
            Load("interfaceinit.conf");
            fenstat = new FenetreStat(Tools.Quick.player, cadre);

            isi = new InventoryStockableInterface(cadre,TypeSacoche.Crochet,TypeSacoche.Potion,new Rectangle(Tools.Quick.graphics.PreferredBackBufferWidth/2 ,Tools.Quick.graphics.PreferredBackBufferHeight/2,Tools.Quick.graphics.PreferredBackBufferWidth/2 ,Tools.Quick.graphics.PreferredBackBufferHeight/2),this);
        }

        public void Load(string filename)
        {
            elementPlace.Clear();
            StreamReader sr = new StreamReader("Data/" + filename);
            while (!sr.EndOfStream)
            {

                string s = sr.ReadLine();
                if (s.Substring(0, 1) != "#")
                {
                    
                    int code = Convert.ToInt16(s.Split(':')[0]);
                    string cordonner = s.Split(':')[1];
                    Rectangle r = new Rectangle(Convert.ToInt32(cordonner.Split()[0]), Convert.ToInt32(cordonner.Split()[1]),Convert.ToInt32( cordonner.Split()[2]), Convert.ToInt32(cordonner.Split()[3]));
                    this.elementPlace.Add((Tools.Armorplace)code, r);
                
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            game.spriteBatch.Begin();

            #region fond bois
            game.spriteBatch.Draw(backwind, new Rectangle(0, 0, Tools.Quick.graphics.PreferredBackBufferWidth/2, Tools.Quick.graphics.PreferredBackBufferHeight/2), Color.White);

            game.spriteBatch.Draw(backwind, new Rectangle(Tools.Quick.graphics.PreferredBackBufferWidth / 2, 0, Tools.Quick.graphics.PreferredBackBufferWidth / 2, Tools.Quick.graphics.PreferredBackBufferHeight / 2), Color.White);

            game.spriteBatch.Draw(backwind, new Rectangle(0, Tools.Quick.graphics.PreferredBackBufferHeight / 2, Tools.Quick.graphics.PreferredBackBufferWidth / 2, Tools.Quick.graphics.PreferredBackBufferHeight / 2), Color.White);

            game.spriteBatch.Draw(backwind, new Rectangle(Tools.Quick.graphics.PreferredBackBufferWidth / 2, Tools.Quick.graphics.PreferredBackBufferHeight / 2, Tools.Quick.graphics.PreferredBackBufferWidth / 2, Tools.Quick.graphics.PreferredBackBufferHeight / 2), Color.White);
            #endregion


            game.spriteBatch.Draw(back,Vector2.Zero+decalage, Color.White);

            fenstat.Draw(game.spriteBatch,new Rectangle( Tools.Quick.graphics.PreferredBackBufferWidth/2,0, Tools.Quick.graphics.PreferredBackBufferWidth/2,Tools.Quick.graphics.PreferredBackBufferHeight));
            
            foreach (Rectangle r in elementPlace.Values)
            {
                game.spriteBatch.Draw(tex,new Rectangle(Convert.ToInt32( r.Location.X + decalage.X),Convert.ToInt32(r.Location.Y + decalage.Y),r.Width,r.Height), Color.White);
            }
            base.Draw(gameTime);

            isi.Draw(game.spriteBatch);

            game.spriteBatch.Draw(cadre, new Rectangle(0, 0, Tools.Quick.graphics.PreferredBackBufferWidth / 2, Tools.Quick.graphics.PreferredBackBufferHeight), Color.White);

            game.spriteBatch.Draw(cadre, new Rectangle(Tools.Quick.graphics.PreferredBackBufferWidth / 2, 0, Tools.Quick.graphics.PreferredBackBufferWidth / 2, Tools.Quick.graphics.PreferredBackBufferHeight), Color.White);
           
            game.spriteBatch.End();
        
        }

        public void switchvisible()
        {
            Visible = !Visible;
            isi.switchvisible();
            fenstat.switchvisible();

        }

        public List<ClickableArea> getzoneclick()
        {
            return isi.onglet;
        }

        public void RightClick(Vector2 pos)
        {
            isi.generateinfobulle(pos);
        }
    }
}
