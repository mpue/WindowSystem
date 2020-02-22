using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XTron;
using static WindowSystem.KeyboardManager;

namespace WindowSystem
{
    public class WindowManager : Control
    {
        public static readonly float GLOBAL_SCALE = 2.0f;

        private Vector2 mouseDownPosition = new Vector2(0, 0);
        private Vector2 mousePosition = new Vector2(0, 0);
        private Vector2 mouseDelta = new Vector2(0, 0);
        private Vector2 dragStartPos = new Vector2(0, 0);
        private Vector2 dragStartSize = new Vector2(0, 0);

        public float Zoom { get; set; } = 1.0f;

        private readonly ContentManager content;
        private readonly SpriteBatch spriteBatch;

        private Window currentWindow;
        private List<Window> windows = new List<Window>();
        private Texture2D background;
        private KeyboardManager keyboardManager;
        
        private enum Mode
        {
            SELECT,
            MOVE,
            RESIZE
        }

        private Mode mode = Mode.SELECT;

        public WindowManager(ContentManager content, SpriteBatch spriteBatch)
        {
            this.content = content;
            this.spriteBatch = spriteBatch;
            this.keyboardManager = new KeyboardManager();

            this.keyboardManager.KeyPressed += HandleKeyPressed;
            this.keyboardManager.KeyReleased += HandleKeyReleased;
            background = content.Load<Texture2D>("Wallpaper/thirsty");
         
        }

        private void HandleKeyReleased(object sender, EventArgs e)
        {
            KeyboardEventArgs args = e as KeyboardEventArgs;   

        }

        private void HandleKeyPressed(object sender, EventArgs e)
        {
            KeyboardEventArgs args = e as KeyboardEventArgs;
            if (currentWindow != null)
            {
                foreach (Control child in currentWindow.GetChildren())
                {
                    if (child is TextEditorControl && child.Selected)
                    {
                        TextEditorControl te = child as TextEditorControl;
                        te.Add(args.key);
                        break;
                    }
                }

            }

        }

        public MouseState mouseState { get; private set; }
        private MouseState lastState;

        public Window AddWindow(Vector2 position, Vector2 size)
        {
            Window win = new Window(content, spriteBatch, position, size);

            int maxZ = 0;

            foreach (Window w in windows)
            {
                if (w.ZOrder > maxZ)
                {
                    maxZ = w.ZOrder;
                }
            }

            maxZ++;

            win.ZOrder = maxZ;
            win.LoadContent();
            windows.Add(win);
            return win;
        }

        public override void Draw(GameTime gameTime)
        {
            if (background != null)
            {
                spriteBatch.Draw(background, new Vector2(0, 0), Color.White);

            }               

            foreach(Window window in windows)
            {
                window.Draw(gameTime);
            }

            base.Draw(gameTime);
        }

        public override void Initialize()
        {
        }

        public override void LoadContent()
        {
        }

        private void HandleSelection()
        {
            List<Window> possibleSelected = new List<Window>();

            // deselect all and check which windows might be selected
            foreach (Window w in windows)
            {
                w.Selected = false;

                if (w.Bounds.Contains(mousePosition.ToPoint()))
                {
                    possibleSelected.Add(w);
                }
            }

            if (possibleSelected.Count == 0)
            {
                currentWindow = null;
                return;
            }

            // Get the one with the topmost zIndex
            int maxZ = 0;

            foreach (Window w in possibleSelected)
            {
                if (w.ZOrder > maxZ)
                {
                    currentWindow = w;
                }
                else
                {
                    w.Selected = false;
                }
            }

            

            // set selected window
            if (currentWindow != null)
            {
                currentWindow.Selected = true;

                // get the one from the remaining with the largest zIndex and swap if there's one

                int z = 0;

                Window onTop = null;

                foreach (Window w in windows)
                {
                    if (w.ZOrder > currentWindow.ZOrder)
                    {
                        onTop = w;
                    }
                }

                if (onTop != null)
                {
                    z = onTop.ZOrder;
                    onTop.ZOrder = currentWindow.ZOrder;
                    currentWindow.ZOrder = z;
                }

                // process event handler for selected control

                
                ProcessEvents(currentWindow);

            }
 
        }

        private void ProcessEvents(Control control)
        {
            foreach (Control child in control.GetChildren())
            {
                if (child.Bounds.Contains(mousePosition))
                {
                    child.Selected = true;
                    if (currentWindow != null)
                        currentWindow.Selected = false;
                    if (child is Button)
                    {
                        Button b = child as Button;
                        b.OnClick(new Button.ButtonEventArgs());
                    }
                }
                else
                {
                    child.Selected = false;
                }

            }

        }

        private void handleMouseDown()
        {
            ProcessEvents(this);
            HandleWindowActions();
            HandleSelection();

            windows = windows.OrderBy(o => o.ZOrder).ToList();

            if (mouseState.LeftButton == ButtonState.Pressed)
            {

                mouseDownPosition = mousePosition;

                if (currentWindow != null)
                {
                    dragStartPos = currentWindow.Position;
                    dragStartSize = currentWindow.Size;

                    if (currentWindow.IsOnResizeHandle(mousePosition))
                    {
                        mode = Mode.RESIZE;
                    }
                    else
                    {
                        mode = Mode.MOVE;
                        currentWindow.StartUpdate(dragStartPos);
                    }    

                }
            }
        }

        private void HandleWindowActions()
        {
            foreach (Window w in windows)
            {
                if (w.IsOnCloseHandle(mousePosition))
                {
                    windows.Remove(w);
                    break;
                }
            }

        }

        public void Close(Window w)
        {
            windows.Remove(w);
        }

        private void handleMouseUp()
        {
            mode = Mode.SELECT;
            windows = windows.OrderBy(o => o.ZOrder).ToList();            
         }

        private void handleMouseMove()
        {
            mousePosition.X = mouseState.X / Zoom; 
            mousePosition.Y = mouseState.Y / Zoom;

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (currentWindow != null)
                {
                    mouseDelta = mousePosition - mouseDownPosition;
                    if (mode == Mode.RESIZE) { 
                        Vector2 size = dragStartSize + mouseDelta;
                        currentWindow.Size = size;
                        Rectangle bounds = currentWindow.Bounds;
                        bounds.Size = size.ToPoint();
                        currentWindow.Bounds = bounds;
                        currentWindow.Resized();
                    }
                    else { 
                        Vector2 pos = dragStartPos + mouseDelta;
                        currentWindow.Position = pos;
                        currentWindow.UpdateChildren(mouseDelta);
                        Rectangle bounds = currentWindow.Bounds;
                        bounds.Location = pos.ToPoint();
                        currentWindow.Bounds = bounds;
                    }

                }
            }
        }

        public override void Update(GameTime gameTime)
        {

            keyboardManager.Update(gameTime);
            mouseState = Mouse.GetState();

            if (lastState != mouseState)
            {
                if (lastState.LeftButton != mouseState.LeftButton || lastState.RightButton != mouseState.RightButton)
                {
                    if (mouseState.LeftButton == ButtonState.Pressed || mouseState.RightButton == ButtonState.Pressed)
                    {
                        handleMouseDown();
                    
                    } 
                    else if(mouseState.LeftButton == ButtonState.Released || mouseState.RightButton == ButtonState.Released)
                    {
                        handleMouseUp();
                    }
                    
                }
                else
                {
                    handleMouseMove();
                }

                lastState = mouseState;
            }

            foreach (Window w in windows)
            {
                w.Update(gameTime);
            }

            // UpdateChildren(mouseDelta);
        }
    }
}
