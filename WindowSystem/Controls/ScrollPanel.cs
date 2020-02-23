using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using XTron;

namespace WindowSystem
{
    public class ScrollPanel : Control
    {

        private Color borderColor = Color.DarkGray;
        private Color selectionBorderColor = Color.Orange;
        private Color backgroundColor = Color.White;

        private RasterizerState rasterizerState;
        private Vector2 scrollPosition = new Vector2(0,0);
        private readonly Control control;
        private Vector2 offset;
        private Vector2 scollbarPos = new Vector2(25, 0);

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
            if (args.OriginalKey == Microsoft.Xna.Framework.Input.Keys.Up)
            {
                SetScrollPosition(scrollPosition + new Vector2(0, -10));
            }
            else if (args.OriginalKey == Microsoft.Xna.Framework.Input.Keys.Down)
            {
                SetScrollPosition(scrollPosition + new Vector2(0, 10));
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
            this.control.Bounds = new Rectangle(Position.ToPoint() + control.Position.ToPoint() - scrollPosition.ToPoint(), control.Size.ToPoint());
            this.control.Position = Position + offset - scrollPosition;
            this.control.Update(gameTime);
        }

        public override void Resized()
        {
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
            Primitives2D.FillRectangle(spriteBatch, new Vector2(Position.X + Size.X - scollbarPos.X + 2, Position.Y + scrollPosition.Y), new Vector2(scollbarPos.X-2,50), borderColor);

        }

    }
}
