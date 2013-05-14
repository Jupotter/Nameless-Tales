using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using LuaInterface;
using Lua511;

namespace RPGProject
{
    public class GameConsole
    {
        public string historique;
        public string text;
        public bool onA;
        Lua lua;

        public GameConsole()
        {
            historique = "";
            text = "";
            onA = false;

            lua = LuaManager.LuaVM;
            MethodInfo mInfo = typeof(GameConsole).GetMethod("show");
            LuaManager.registerLuaFunctions(this);
        }

        public void enterkey(Keys[] keys)
        {
            Console.WriteLine(keys[0].ToString());
            
               if ((keys.Count()>1))
                    {
                   bool shift = false;
                   Keys key = Keys.None;
                   
                   #region shift
                   if (keys[0] == Keys.LeftShift || keys[0] == Keys.RightShift) { shift = true; key = keys[1]; }
                   if (keys[1] == Keys.LeftShift || keys[1] == Keys.RightShift) { shift = true; key = keys[0]; } 
                   switch (key)
                        {
                            case Keys.D0: text += 0.ToString();
                                break;
                            case Keys.D1: text += 1.ToString();
                                break;
                            case Keys.D2: text += 2.ToString();
                                break;
                            case Keys.D3: text += 3.ToString();
                                break;
                            case Keys.D4: text += 4.ToString();
                                break;
                            case Keys.D5: text += 5.ToString();
                                break;
                            case Keys.D6: text += 6.ToString();
                                break;
                            case Keys.D7: text += 7.ToString();
                                break;
                            case Keys.D8: text += 8.ToString();
                                break;
                            case Keys.D9: text += 9.ToString();
                                break;
                            case Keys.A: text +="A";
                                break;
                            case Keys.B: text += "B";
                                break;
                            case Keys.C: text += "C";
                                break;
                            case Keys.D: text += "D";
                                break;
                            case Keys.E: text += "E";
                                break;
                            case Keys.F: text += "F";
                                break;
                            case Keys.G: text += "G";
                                break;
                            case Keys.H: text += "H";
                                break;
                            case Keys.I: text += "I";
                                break;
                            case Keys.J: text += "J";
                                break;
                            case Keys.K: text += "K";
                                break;
                            case Keys.L: text += "L";
                                break;
                            case Keys.M: text += "M";
                                break;
                            case Keys.N: text += "N";
                                break;
                            case Keys.O: text += "O";
                                break;
                            case Keys.P: text += "P";
                                break;
                            case Keys.Q: text += "Q";
                                break;
                            case Keys.R: text += "R";
                                break;
                            case Keys.S: text += "S";
                                break;
                            case Keys.T: text += "T";
                                break;
                            case Keys.U: text += "U";
                                break;
                            case Keys.V: text += "V";
                                break;
                            case Keys.W: text += "W";
                                break;
                            case Keys.X: text += "X";
                                break;
                            case Keys.Y: text += "Y";
                                break;
                            case Keys.Z: text += "Z";
                                break;
                            case Keys.OemPlus: text += "+";
                                break;
                            case Keys.OemBackslash: text += ">";
                                break;
                            case Keys.OemTilde: text += "%";
                                break;

                        }
                   #endregion

                   #region altgr
                   bool altgr = false;
                   if (keys[0] == Keys.LeftControl) { altgr = true; key = keys[1]; }
                   if (keys[1] == Keys.LeftControl) { altgr = true; key = keys[0]; }
                   if (altgr)
                   {
                       switch (key)
                       {
                           case Keys.A:
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
                           case Keys.D0: text += "@";
                               break;
                           case Keys.D1: 
                               break;
                           case Keys.D2: text += "~";
                               break;
                           case Keys.D3: text += "#";
                               break;
                           case Keys.D4: text += "{";
                               break;
                           case Keys.D5: text += "[";
                               break;
                           case Keys.D6: text += "|";
                               break;
                           case Keys.D7: text += "`";
                               break;
                           case Keys.D8: text += "\\";
                               break;
                           case Keys.D9: text += "^";
                               break;
                           case Keys.Decimal:
                               break;
                           case Keys.Delete:
                               break;
                           case Keys.Divide:
                               break;
                           case Keys.Down:
                               break;
                           case Keys.E: text += "€";
                               break;
                           case Keys.End:
                               break;
                           case Keys.Enter:
                               break;
                           case Keys.EraseEof:
                               break;
                           case Keys.Escape:
                               break;
                           case Keys.Execute:
                               break;
                           case Keys.Exsel:
                               break;
                           case Keys.F:
                               break;
                           case Keys.F1:
                               break;
                           case Keys.F10:
                               break;
                           case Keys.F11:
                               break;
                           case Keys.F12:
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
                           case Keys.OemPlus: text += "}";
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
                           case Keys.Tab:
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
                   }

                        #endregion

                    }
                    else
               {
                   #region normal
                   switch (keys[0])
                        {
                            case Keys.A: text += "a";
                                break;
                            case Keys.Add:
                                break;
                            case Keys.Apps:
                                break;
                            case Keys.Attn:
                                break;
                            case Keys.B: text += "b";
                                break;
                            case Keys.Back: if (text.Length >= 1)
                                {
                                    text = text.Substring(0, text.Length - 1);
                                }
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
                            case Keys.C: text += "c";
                                break;
                            case Keys.CapsLock:
                                break;
                            case Keys.ChatPadGreen:
                                break;
                            case Keys.ChatPadOrange:
                                break;
                            case Keys.Crsel:
                                break;
                            case Keys.D: text += "d";
                                break;
                            case Keys.D0: //text += "à";
                                break;
                            case Keys.D1: text += "&";
                                break;
                            case Keys.D2:// text += "é";
                                break;
                            case Keys.D3: text += "\"";
                                break;
                            case Keys.D4: text += "'";
                                break;
                            case Keys.D5: text += "(";
                                break;
                            case Keys.D6: text += "-";
                                break;
                            case Keys.D7: //text += "è";
                                break;
                            case Keys.D8: text += "_";
                                break;
                            case Keys.D9: //text += "ç";
                                break;
                            case Keys.Decimal:
                                break;
                            case Keys.Delete:
                                break;
                            case Keys.Divide:
                                break;
                            case Keys.Down:
                                break;
                            case Keys.E: text += "e";
                                break;
                            case Keys.End:
                                break;
                            case Keys.Enter: activate();
                                break;
                            case Keys.EraseEof:
                                break;
                            case Keys.Escape:
                                break;
                            case Keys.Execute:
                                break;
                            case Keys.Exsel:
                                break;
                            case Keys.F: text += "f";
                                break;
                            case Keys.F1:
                                break;
                            case Keys.F10:
                                break;
                            case Keys.F11:
                                break;
                            case Keys.F12:
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
                            case Keys.G: text += "g";
                                break;
                            case Keys.H: text += "h";
                                break;
                            case Keys.Help:
                                break;
                            case Keys.Home:
                                break;
                            case Keys.I: text += "i";
                                break;
                            case Keys.ImeConvert:
                                break;
                            case Keys.ImeNoConvert:
                                break;
                            case Keys.Insert:
                                break;
                            case Keys.J: text += "j";
                                break;
                            case Keys.K: text += "k";
                                break;
                            case Keys.Kana:
                                break;
                            case Keys.Kanji:
                                break;
                            case Keys.L: text += "l";
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
                            case Keys.M: text += "m";
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
                            case Keys.N: text += "n";
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
                            case Keys.O: text += "o";
                                break;
                            case Keys.Oem8:
                                break;
                            case Keys.OemAuto:
                                break;
                            case Keys.OemBackslash: text += "<";
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
                            case Keys.OemOpenBrackets: text += ")";
                                break;
                            case Keys.OemPeriod:
                                break;
                            case Keys.OemPipe: text += "*";
                                break;
                            case Keys.OemPlus: text += "=";
                                break;
                            case Keys.OemQuestion: text += "/";
                                break;
                            case Keys.OemQuotes:
                                break;
                            case Keys.OemSemicolon:
                                break;
                            case Keys.OemTilde:
                                break;
                            case Keys.P: text += "p";
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
                            case Keys.Q: text += "q";
                                break;
                            case Keys.R: text += "r";
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
                            case Keys.S: text += "s";
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
                            case Keys.Space: text += " ";
                                break;
                            case Keys.Subtract:
                                break;
                            case Keys.T: text += "t";
                                break;
                            case Keys.Tab:
                                break;
                            case Keys.U: text += "u";
                                break;
                            case Keys.Up:
                                break;
                            case Keys.V: text += "v";
                                break;
                            case Keys.VolumeDown:
                                break;
                            case Keys.VolumeMute:
                                break;
                            case Keys.VolumeUp:
                                break;
                            case Keys.W: text += "w";
                                break;
                            case Keys.X: text += "x";
                                break;
                            case Keys.Y: text += "y";
                                break;
                            case Keys.Z: text += "z";
                                break;
                            case Keys.Zoom:
                                break;
                            default:
                                break;
                        }
                   #endregion
               }
                
            

        }

        private void activate()
        {
            if (Tools.Quick.dicoFont[Tools.TypeFont.Texte].MeasureString(historique).Y > 500) historique = "";
            historique += Environment.NewLine;
            historique += text + Environment.NewLine;
            bool error = false;
            Action<object> HistoriqueAdd = addHistorique;

            if (text.ToUpper().StartsWith("TP"))
            {
                try
                {
                    switch (text.Split(' ')[1])
                    {
                        case "TEST": Tools.Quick.game.camera.modiposition(new Vector3(0, 0, 0), true);
                            break;

                    }
                    error = false;
                }
                catch { error = true; }
            }

            try
            {
                object[] ret = lua.DoString(text);
                if (ret != null)
                    Array.ForEach<object>(ret, HistoriqueAdd);
            }

            catch (Exception ex)
            {
                historique += ex.Message + Environment.NewLine;
            }


            //if (error) historique += Environment.NewLine + "Error";
            text = "";
        }

        void addHistorique(object s)
        {
            historique += s.ToString();
            historique += Environment.NewLine;
        }

        public void switchvisiblity()
        {
            onA = !onA;
        }

        [AttrLuaFunc("show", "Show a string in the console")]
        public void show(string s)
        {
            LuaManager.LuaVM.DoString("return " + s);
        }

        [AttrLuaFunc("help", "List available commands.")]
        public void help()
        {
            string ret = "\"";
            ret += "Available commands: \\n\\n";

            System.Collections.IDictionaryEnumerator Funcs = LuaManager.LuaFuncs.GetEnumerator();
            while (Funcs.MoveNext())
            {
                ret += (((LuaFuncDescriptor)Funcs.Value).getFuncHeader())+ "\\n";
            }
            ret += "\"";
            LuaManager.LuaVM.DoString("show (" + ret + ")");
        }

        [AttrLuaFunc("helpcmd", "Show help for a given command", "Command to get help of.")]
        public void help(String strCmd)
        {
            if (!LuaManager.LuaFuncs.ContainsKey(strCmd))
            {
                LuaManager.LuaVM.DoString("show (\"No such function or package: " + strCmd +"\")");
            }

            LuaFuncDescriptor pDesc = (LuaFuncDescriptor)LuaManager.LuaFuncs[strCmd];
            LuaManager.LuaVM.DoString("show \"" + pDesc.getFuncFullDoc() + "\"");
        }
    }
}
