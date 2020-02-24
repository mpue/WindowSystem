using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using XTron;

namespace WindowSystem
{
    public class ScrollPanel : Control
    {
        private Color borderColor = WindowManager.GetInstance().Theme.ScrollPanel.borderColor;
        private Color selectionBorderColor = WindowManager.GetInstance().Theme.ScrollPanel.selectionBorderColor;
        private Color backgroundColor = WindowManager.GetInstance().Theme.ScrollPanel.backgroundColor;

        private Vector2 HandleStartPos;
        private Vector2 HandlePos;
        private Rectangle ScrollbarHandle;
        private Vector2 scrollPosition = new Vector2(0,0);
        private Vector2 scollbarPos = new Vector2(25, 0);
        private Rectangle Scrollbar;

        private RasterizerState rasterizerState;
        private readonly Control control;
        private Vector2 offset;

        private Vector2 controlScale = new Vector2(1, 1);

        bool mouseDown = false;

        public ScrollPanel(Vector2 position, Vector2 size, Control control) : base()
        {
            this.Position = position;
            Size = size;
            Bounds = new Rectangle(Position.ToPoint(), Size.ToPoint());
            rasterizerState = new RasterizerState();
            rasterizerState.ScissorTestEnable = true;
            this.control = control;
            control.Bounds = new Rectangle((Position + control.Position + scrollPosition).ToPoint(), control.Bounds.Size);
            offset = Position - control.Position;
            KeyboardManager.GetInstance().KeyPressed += HandleKeyPressed;
            HandlePos = new Vector2(Position.X + Size.X - scollbarPos.X + 2, Position.Y) + scrollPosition;
            HandleStartPos = HandlePos;
            ScrollbarHandle = new Rectangle(HandleStartPos.ToPoint(), new Vector2(scollbarPos.X - 2, 50).ToPoint());
            Scrollbar  = new Rectangle( new Vector2(Position.X + Size.X - scollbarPos.X, Position.Y).ToPoint(), new Vector2(scollbarPos.X, Bounds.Height - control.MarginBR.Y / 2).ToPoint());

            controlScale = new Vector2(control.Bounds.Height / Bounds.Height, control.Bounds.Width / Bounds.Width);

        }

        public override void OnMouseDown(ControlEventArgs e)
        {
            if (!mouseDown)
            {
                if (IsInsideScrollBarRange(e.Position))
                {
                    mouseDown = true;
                    HandleStartPos = scrollPosition;
                }

            }
        }

        public override void OnMouseUp(ControlEventArgs e)
        {
            if (mouseDown)
            {
                mouseDown = false;               
            }
        }

        private bool IsInsideScrollBarRange(Vector2 mousePosition)
        {
            return ScrollbarHandle.Contains(mousePosition.ToPoint());
        }
        

        public void SetScrollPosition(Vector2 scrollPosition)
        {
            this.scrollPosition = scrollPosition;
            // control.Bounds = new Rectangle((control.Position + scrollPosition).ToPoint(), control.Bounds.Size);
        }

        public override void HandleKeyPressed(object sender, EventArgs e)
        {
            control.HandleKeyPressed(sender, e);
            KeyboardEventArgs args = e as KeyboardEventArgs;
            /*
            if (args.OriginalKey == Microsoft.Xna.Framework.Input.Keys.Up)
            {
                SetScrollPosition(scrollPosition + new Vector2(0, -10));
            }
            else if (args.OriginalKey == Microsoft.Xna.Framework.Input.Keys.Down)
            {
                SetScrollPosition(scrollPosition + new Vector2(0, 10));
            }
            */
            
        }

        public override void OnMouseMove(ControlEventArgs e)
        {
            ButtonState state = e.state.LeftButton;

            if (mouseDown)
            {
                Vector2 delta = e.Delta ;
                delta.X = 0;
                scrollPosition = HandleStartPos + delta;
                scrollPosition.Y = scrollPosition.Y;


                if (scrollPosition.Y < 0)
                {
                    scrollPosition.Y = 0;
                }                     

                if (scrollPosition.Y + ScrollbarHandle.Height > Bounds.Height)
                {
                    scrollPosition.Y = Bounds.Height - ScrollbarHandle.Height;
                }
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
            Bounds = new Rectangle(Position.ToPoint(), Size.ToPoint());
            ScrollbarHandle = new Rectangle(new Vector2(Position.X + Size.X - scollbarPos.X + 2, Position.Y + scrollPosition.Y).ToPoint(), new Vector2(scollbarPos.X - 2, 50).ToPoint());
            this.control.Bounds = new Rectangle(Position.ToPoint() + control.Position.ToPoint() - scrollPosition.ToPoint(), control.Size.ToPoint());
            this.control.Position = Position + offset - (scrollPosition / controlScale.Y);
            this.control.Update(gameTime);
        }

        public override void Resized()
        {
            HandlePos = new Vector2(Position.X + Size.X - scollbarPos.X + 2, Position.Y);
            ScrollbarHandle.Location = HandlePos.ToPoint();
            Scrollbar = new Rectangle(new Vector2(Position.X + Size.X - scollbarPos.X, Position.Y).ToPoint(), new Vector2(scollbarPos.X, Bounds.Height - control.MarginBR.Y / 2).ToPoint());
            
            this.control.Resized();
        }

        public override void Draw(GameTime gameTime)
        {
            Primitives2D.FillRectangle(spriteBatch, Position, Size, backgroundColor);
            if (Selected)
            {
                Primitives2D.DrawRectangle(spriteBatch, Position, Size, selectionBorderColor);
            }
            else
            {
                Primitives2D.DrawRectangle(spriteBatch, Position, Size, borderColor);
            }

            RasterizerState state = spriteBatch.GraphicsDevice.RasterizerState;

            spriteBatch.GraphicsDevice.RasterizerState = rasterizerState;
            //Copy the current scissor rect so we can restore it after
            Rectangle currentRect = spriteBatch.GraphicsDevice.ScissorRectangle;

            //Set the current scissor rectangle
            spriteBatch.GraphicsDevice.ScissorRectangle = Bounds;

            control.Draw(gameTime);

            spriteBatch.GraphicsDevice.ScissorRectangle = currentRect;
            spriteBatch.GraphicsDevice.RasterizerState = state;

            Primitives2D.DrawRectangle(spriteBatch, new Vector2(Position.X + Size.X - scollbarPos.X,Position.Y), new Vector2(scollbarPos.X, Bounds.Height - control.MarginBR.Y /2), borderColor);
            Primitives2D.FillRectangle(spriteBatch, ScrollbarHandle, borderColor);

        }

    }
}
