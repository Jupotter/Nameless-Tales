using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
namespace RPGProject
{
    class ActionKeyboard
    {
        int countk = 0;
        Keys[] down ;
         KeyboardState ks;
         bool b = true;
         
        public void valider()
        {
            ks = Keyboard.GetState();
            if (b)
            {
                
                ks = Keyboard.GetState();
                if (ks.GetPressedKeys().Count() > 0)
                {
                    countk = ks.GetPressedKeys().Count();
                    down = ks.GetPressedKeys();
                    b = false;
                }
            }
            else
            {
                if (countk != ks.GetPressedKeys().Count())
                {

                     if (Tools.Quick.game.gameconsole.onA) Tools.Quick.game.gameconsole.enterkey(down);

                    switch (down[0])
                    {
                        case Keys.A: Tools.Quick.game.switchPhysics();
                            break;
                        case Keys.Add:
                            break;
                        case Keys.Apps:
                            break;
                        case Keys.Attn:
                            break;
                        case Keys.B:
                            break;
                        case Keys.Back:
                            break;
                        case Keys.BrowserBack:
                            break;
                        case Keys.BrowserFavorites:
                            break;
                        case Keys.BrowserForward:
                            break;
                        case Keys.BrowserHome:
                            break;
                        case Keys.BrowserRefresh:
                            break;
                        case Keys.BrowserSearch:
                            break;
                        case Keys.BrowserStop:
                            break;
                        case Keys.C:
                            break;
                        case Keys.CapsLock:
                            break;
                        case Keys.ChatPadGreen:
                            break;
                        case Keys.ChatPadOrange:
                            break;
                        case Keys.Crsel:
                            break;
                        case Keys.D:
                            break;
                        case Keys.D0:
                            break;
                        case Keys.D1:
                            break;
                        case Keys.D2:
                            break;
                        case Keys.D3:
                            break;
                        case Keys.D4:
                            break;
                        case Keys.D5:
                            break;
                        case Keys.D6:
                            break;
                        case Keys.D7:
                            break;
                        case Keys.D8:
                            break;
                        case Keys.D9:
                            break;
                        case Keys.Decimal:
                            break;
                        case Keys.Delete:
                            break;
                        case Keys.Divide:
                            break;
                        case Keys.Down:
                            break;
                        case Keys.E: Tools.Quick.player.interact();
                            break;
                        case Keys.End:
                            break;
                        case Keys.Enter:
                            break;
                        case Keys.EraseEof:
                            break;
                        case Keys.Escape:     //Tools.Quick.game.Exit();
                            Tools.Quick.game.switchMenu(TypeMenu.Principal);
                            break;
                        case Keys.Execute:
                            break;
                        case Keys.Exsel:
                            break;
                        case Keys.F:
			                Tools.Quick.game.switchinoutdoor();
                            break;
                        case Keys.F1:
                            break;
                        case Keys.F10:
                            break;
                        case Keys.F11:
                            break;
                        case Keys.F12: Tools.Quick.game.gameconsole.switchvisiblity();
                            break;
                        case Keys.F13:
                            break;
                        case Keys.F14:
                            break;
                        case Keys.F15:
                            break;
                        case Keys.F16:
                            break;
                        case Keys.F17:
                            break;
                        case Keys.F18:
                            break;
                        case Keys.F19:
                            break;
                        case Keys.F2:
                            break;
                        case Keys.F20:
                            break;
                        case Keys.F21:
                            break;
                        case Keys.F22:
                            break;
                        case Keys.F23:
                            break;
                        case Keys.F24:
                            break;
                        case Keys.F3:
                            break;
                        case Keys.F4:
                            break;
                        case Keys.F5:
                            break;
                        case Keys.F6:
                            break;
                        case Keys.F7:
                            break;
                        case Keys.F8:
                            break;
                        case Keys.F9:
                            break;
                        case Keys.G:
                            break;
                        case Keys.H:
                            break;
                        case Keys.Help:
                            break;
                        case Keys.Home:
                            break;
                        case Keys.I:
                            break;
                        case Keys.ImeConvert:
                            break;
                        case Keys.ImeNoConvert:
                            break;
                        case Keys.Insert:
                            break;
                        case Keys.J:
                            break;
                        case Keys.K:
                            break;
                        case Keys.Kana:
                            break;
                        case Keys.Kanji:
                            break;
                        case Keys.L:
                            break;
                        case Keys.LaunchApplication1:
                            break;
                        case Keys.LaunchApplication2:
                            break;
                        case Keys.LaunchMail:
                            break;
                        case Keys.Left:
                            break;
                        case Keys.LeftAlt:
                            break;
                        case Keys.LeftControl:
                            break;
                        case Keys.LeftShift:
                            break;
                        case Keys.LeftWindows:
                            break;
                        case Keys.M:
                            break;
                        case Keys.MediaNextTrack:
                            break;
                        case Keys.MediaPlayPause:
                            break;
                        case Keys.MediaPreviousTrack:
                            break;
                        case Keys.MediaStop:
                            break;
                        case Keys.Multiply:
                            break;
                        case Keys.N:
                            break;
                        case Keys.None:
                            break;
                        case Keys.NumLock:
                            break;
                        case Keys.NumPad0:
                            break;
                        case Keys.NumPad1:
                            break;
                        case Keys.NumPad2:
                            break;
                        case Keys.NumPad3:
                            break;
                        case Keys.NumPad4:
                            break;
                        case Keys.NumPad5:
                            break;
                        case Keys.NumPad6:
                            break;
                        case Keys.NumPad7:
                            break;
                        case Keys.NumPad8:
                            break;
                        case Keys.NumPad9:
                            break;
                        case Keys.O:
                            break;
                        case Keys.Oem8:
                            break;
                        case Keys.OemAuto:
                            break;
                        case Keys.OemBackslash:
                            break;
                        case Keys.OemClear:
                            break;
                        case Keys.OemCloseBrackets:
                            break;
                        case Keys.OemComma:
                            break;
                        case Keys.OemCopy:
                            break;
                        case Keys.OemEnlW:
                            break;
                        case Keys.OemMinus:
                            break;
                        case Keys.OemOpenBrackets:
                            break;
                        case Keys.OemPeriod:
                            break;
                        case Keys.OemPipe:
                            break;
                        case Keys.OemPlus:
                            break;
                        case Keys.OemQuestion:
                            break;
                        case Keys.OemQuotes:
                            break;
                        case Keys.OemSemicolon:
                            break;
                        case Keys.OemTilde: 
                            break;
                        case Keys.P:
                            break;
                        case Keys.Pa1:
                            break;
                        case Keys.PageDown:
                            break;
                        case Keys.PageUp:
                            break;
                        case Keys.Pause:
                            break;
                        case Keys.Play:
                            break;
                        case Keys.Print:
                            break;
                        case Keys.PrintScreen:
                            break;
                        case Keys.ProcessKey:
                            break;
                        case Keys.Q:
                            break;
                        case Keys.R:
                            break;
                        case Keys.Right:
                            break;
                        case Keys.RightAlt:
                            break;
                        case Keys.RightControl:
                            break;
                        case Keys.RightShift:
                            break;
                        case Keys.RightWindows:
                            break;
                        case Keys.S:
                            break;
                        case Keys.Scroll:
                            break;
                        case Keys.Select:
                            break;
                        case Keys.SelectMedia:
                            break;
                        case Keys.Separator:
                            break;
                        case Keys.Sleep:
                            break;
                        case Keys.Space:
                            break;
                        case Keys.Subtract:
                            break;
                        case Keys.T:
                            break;
                        case Keys.Tab: Tools.Quick.game.switchMenu(TypeMenu.Inventaire);
                            break;
                        case Keys.U:
                            break;
                        case Keys.Up:
                            break;
                        case Keys.V:
                            break;
                        case Keys.VolumeDown:
                            break;
                        case Keys.VolumeMute:
                            break;
                        case Keys.VolumeUp:
                            break;
                        case Keys.W:
                            break;
                        case Keys.X:
                            break;
                        case Keys.Y:
                            break;
                        case Keys.Z:
                            break;
                        case Keys.Zoom:
                            break;
                        default:
                            break;
                    }
                    b = true;
                }
                
            }
    }
       
    }
}
