using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using XTron;

namespace WindowSystem
{

    public class KeyboardEventArgs : EventArgs
    {
        internal Keys OriginalKey;

        public char key { get; set; }
        public bool shift { get; set; }


    }

    class KeyboardManager : AbstractEntity
    {

        public static KeyboardManager instance = null;

        public static KeyboardManager GetInstance()
        {
            if (instance == null)
            {
                instance = new KeyboardManager();
            }
            return instance;
        }
        private Dictionary<Keys, bool> InputMap = new Dictionary<Keys, bool>();

        KeyboardState lastState;
        float currentTime = 0;
        float interval = 0.2f;
        public bool KeyRepeat { get; set; } = false;


        public bool shiftPressed = false;
        private KeyboardManager()
        {
            var values = Enum.GetValues(typeof(Keys));

            foreach (Keys k in values)
            {
                InputMap[k] = false;
            }
        }

        public virtual void OnKeyDown(KeyboardEventArgs e)
        {
            EventHandler handler = KeyPressed;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public virtual void OnKeyUp(KeyboardEventArgs e)
        {
            EventHandler handler = KeyReleased;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public override void Initialize()
        {        
        }

        public override void LoadContent()
        {        
        }

        public override void Update(GameTime gameTime)
        {

            currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            KeyboardState state = Keyboard.GetState();

            var values = Enum.GetValues(typeof(Keys));

            foreach (Keys k in values)
            {
                if (state.IsKeyDown(k) && !InputMap[k])
                {
                    KeyboardEventArgs args = new KeyboardEventArgs();
                    args.shift = state.IsKeyDown(Keys.LeftShift);
                    shiftPressed = args.shift;
                    args.key = ResolveKey(k);
                    args.OriginalKey = k;
                    OnKeyDown(args);
                    InputMap[k] = true;
                }
                else if (state.IsKeyUp(k) && InputMap[k])
                {
                    KeyboardEventArgs args = new KeyboardEventArgs();
                    args.key = ResolveKey(k);
                    args.OriginalKey = k;
                    OnKeyUp(args);
                    InputMap[k] = false;
                }

            }

        }

        public char ResolveKey(Keys k)
        {
            char c = ' ';

            switch (k)
            {
                case Keys.None:
                    break;
                case Keys.Back:
                    return (char)k;                    
                case Keys.Tab:
                    break;
                case Keys.Enter:
                    return '\n';                    
                case Keys.CapsLock:
                    break;
                case Keys.Escape:
                    break;
                case Keys.Space:
                    break;
                case Keys.PageUp:
                    break;
                case Keys.PageDown:
                    break;
                case Keys.End:
                    return (char)k;
                case Keys.Home:
                    return (char)k;
                case Keys.Left:
                    return (char)k;
                case Keys.Up:
                    return (char)k;
                case Keys.Right:
                    return (char)k;
                case Keys.Down:
                    return (char)k;
                case Keys.Select:
                    break;
                case Keys.Print:
                    break;
                case Keys.Execute:
                    break;
                case Keys.PrintScreen:
                    break;
                case Keys.Insert:
                    break;
                case Keys.Delete:
                    break;
                case Keys.Help:
                    break;
                case Keys.D0:
                    return (char)0x30;                    
                case Keys.D1:
                    return (char)0x31;
                case Keys.D2:
                    return (char)0x32;
                case Keys.D3:
                    return (char)0x33;
                case Keys.D4:
                    return (char)0x34;
                case Keys.D5:
                    return (char)0x35;
                case Keys.D6:
                    return (char)0x36;
                case Keys.D7:
                    return (char)0x37;
                case Keys.D8:
                    return (char)0x38;
                case Keys.D9:
                    return (char)0x39;
                case Keys.A:
                case Keys.B:
                case Keys.C:
                case Keys.D:
                case Keys.E:
                case Keys.F:
                case Keys.G:
                case Keys.H:
                case Keys.I:
                case Keys.J:
                case Keys.K:
                case Keys.L:
                case Keys.M:
                case Keys.N:
                case Keys.O:
                case Keys.P:
                case Keys.Q:
                case Keys.R:
                case Keys.S:
                case Keys.T:
                case Keys.U:
                case Keys.V:
                case Keys.W:
                case Keys.X:
                case Keys.Y:
                case Keys.Z:
                    if (shiftPressed)                       
                        return k.ToString().ToCharArray()[0];
                    else
                        return k.ToString().ToLower().ToCharArray()[0];                        
                case Keys.LeftWindows:
                    break;
                case Keys.RightWindows:
                    break;
                case Keys.Apps:
                    break;
                case Keys.Sleep:
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
                case Keys.Multiply:
                    break;
                case Keys.Add:
                    break;
                case Keys.Separator:
                    break;
                case Keys.Subtract:
                    break;
                case Keys.Decimal:
                    break;
                case Keys.Divide:
                    break;
                case Keys.F1:
                    break;
                case Keys.F2:
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
                case Keys.NumLock:
                    break;
                case Keys.Scroll:
                    break;
                case Keys.LeftShift:
                    break;
                case Keys.RightShift:
                    break;
                case Keys.LeftControl:
                    break;
                case Keys.RightControl:
                    break;
                case Keys.LeftAlt:
                    break;
                case Keys.RightAlt:
                    break;
                case Keys.BrowserBack:
                    break;
                case Keys.BrowserForward:
                    break;
                case Keys.BrowserRefresh:
                    break;
                case Keys.BrowserStop:
                    break;
                case Keys.BrowserSearch:
                    break;
                case Keys.BrowserFavorites:
                    break;
                case Keys.BrowserHome:
                    break;
                case Keys.VolumeMute:
                    break;
                case Keys.VolumeDown:
                    break;
                case Keys.VolumeUp:
                    break;
                case Keys.MediaNextTrack:
                    break;
                case Keys.MediaPreviousTrack:
                    break;
                case Keys.MediaStop:
                    break;
                case Keys.MediaPlayPause:
                    break;
                case Keys.LaunchMail:
                    break;
                case Keys.SelectMedia:
                    break;
                case Keys.LaunchApplication1:
                    break;
                case Keys.LaunchApplication2:
                    break;
                case Keys.OemSemicolon:
                    break;
                case Keys.OemPlus:
                    break;
                case Keys.OemComma:
                    break;
                case Keys.OemMinus:
                    break;
                case Keys.OemPeriod:
                    break;
                case Keys.OemQuestion:
                    break;
                case Keys.OemTilde:
                    break;
                case Keys.OemOpenBrackets:
                    break;
                case Keys.OemPipe:
                    break;
                case Keys.OemCloseBrackets:
                    break;
                case Keys.OemQuotes:
                    break;
                case Keys.Oem8:
                    break;
                case Keys.OemBackslash:
                    break;
                case Keys.ProcessKey:
                    break;
                case Keys.Attn:
                    break;
                case Keys.Crsel:
                    break;
                case Keys.Exsel:
                    break;
                case Keys.EraseEof:
                    break;
                case Keys.Play:
                    break;
                case Keys.Zoom:
                    break;
                case Keys.Pa1:
                    break;
                case Keys.OemClear:
                    break;
                case Keys.ChatPadGreen:
                    break;
                case Keys.ChatPadOrange:
                    break;
                case Keys.Pause:
                    break;
                case Keys.ImeConvert:
                    break;
                case Keys.ImeNoConvert:
                    break;
                case Keys.Kana:
                    break;
                case Keys.Kanji:
                    break;
                case Keys.OemAuto:
                    break;
                case Keys.OemCopy:
                    break;
                case Keys.OemEnlW:
                    break;
            }

            return c;
        }

        public override void Draw(GameTime gameTime)
        {         
        }


        public event EventHandler KeyPressed;
        public event EventHandler KeyReleased;



    }
}
