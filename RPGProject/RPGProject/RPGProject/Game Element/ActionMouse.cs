using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace RPGProject
{

    class ActionMouse
    {
        MouseState oldms;
        MouseState ms;
        Game1 game;
        float sensibilite = 5;
        Camera came;
        int valRoulette;
        public ActionMouse(Game1 game , Camera came)
        {
            this.came = came;
            oldms = Mouse.GetState();
            ms = Mouse.GetState();
            this.game = game;
        }

        public void actualise()
        {

            ms = Mouse.GetState();
            valRoulette = ms.ScrollWheelValue;
            switch (ms.RightButton)
            {
                case ButtonState.Pressed: RightClick();
                    break;
                case ButtonState.Released:
                    break;
                default:
                    break;
            }
            if (game.menuactif == null)
            {

                gestioncamera();

            }
            else
            {

            }

            oldms = Mouse.GetState();
        }

        public void click()
        {



        }

        public void gestioncamera()
        {
            if (oldms == ms) return;

          Vector2 newpos = new Vector2(ms.X, ms.Y);
            List<Sens> ls = new List<Sens>();
            
            float i = 0;
            float diff = 0;


            if (newpos.X > game.Window.ClientBounds.Center.X)
            {
                ls.Add(Sens.Droite);
            }
            else if (newpos.X < game.Window.ClientBounds.Center.X)
            { ls.Add(Sens.Gauche); }

            if (newpos.Y > game.Window.ClientBounds.Center.Y   )
            {
                ls.Add(Sens.Rotbas);
            }
            else if (newpos.Y < game.Window.ClientBounds.Center.Y )
            { ls.Add(Sens.Rothaut); }

            
            came.actucamera(ls);
            Mouse.SetPosition(game.Window.ClientBounds.Center.X, game.Window.ClientBounds.Center.Y);
        }

        public void RightClick()
        {
            if(game.menuactif!=null)
            {
                game.menuactif.RightClick(new Vector2(Mouse.GetState().X, Mouse.GetState().Y));
        }
        }
        
        public int compareMouseWheel(int val)
        {
            return valRoulette - val; 
        }

        public int getMouseWheel()
        {
            actualise();
            return ms.ScrollWheelValue;
        }
    }
}
