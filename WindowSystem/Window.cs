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
    public class Window : Control
    {
        private readonly ContentManager content;
        private readonly SpriteBatch spriteBatch;
        private SpriteFont windowTitle;
        

        public String Title { get; set; } = "Window";

        public Vector2 Size { get; set; }

        private Vector2 shadowOffset = new Vector2(4, 4);        
        private Color titleFontColor = Color.Black;
        private Color shadowColor = new Color(0.1f, 0.1f, 0.1f, 0.5f);
        private Color borderColor = Color.DarkGray;
        private Color titleBarColor = Color.White;
        private Color selectionBorderColor = Color.Orange;
        private Color windowBackgroundColor = Color.White; //new Color(0.5f, 0.5f, 0.5f, 0.5f);
        private Color seperatorColor = Color.GhostWhite;

        public int ZOrder { get; set; } = 0;

        public Rectangle closeHandle { get; set; }

        public Window(ContentManager content, SpriteBatch spriteBatch, Vector2 position, Vector2 size)
        {
            this.content = content;
            this.spriteBatch = spriteBatch;
            Position = position;
            Size = size;
            Bounds = new Rectangle(Position.ToPoint(), Size.ToPoint());
            closeHandle = new Rectangle((int)(Size.X - 25), 4, 15, 15);
            Selected = false;
        }

        public override void Resized()
        {
            closeHandle = new Rectangle((int)(Size.X - 25) ,4,15,15);
            base.Resized();

        }

        public bool IsOnCloseHandle(Vector2 position)
        {
            Rectangle r = new Rectangle((Position + new Vector2(Size.X - 25, 4)).ToPoint(),new Vector2(15,15).ToPoint()) ;
            return r.Contains(position.ToPoint());
        }

        public bool IsOnResizeHandle(Vector2 position)
        {
            Rectangle r = new Rectangle((Position + Size + new Vector2(-20, -20)).ToPoint(), new Point(20, 20));
            return r.Contains(position.ToPoint());
        }

        public bool IsOnWindowTitle(Vector2 position)
        {
            Rectangle r = new Rectangle(Position.ToPoint(), new Point((int)Size.X, 25));
            return r.Contains(position.ToPoint());

        }


        public override void Draw(GameTime gameTime)
        {
            Primitives2D.FillRectangle(spriteBatch, Position + shadowOffset, Size, shadowColor);
            Primitives2D.FillRectangle(spriteBatch, Position, Size, windowBackgroundColor);
            Primitives2D.FillRectangle(spriteBatch, Position, new Vector2(Size.X, 25), titleBarColor);
            Primitives2D.DrawLine(spriteBatch, Position + new Vector2(0, 25), Position + new Vector2(Size.X, 25), seperatorColor, 2f);

            if (Selected)
            {
                Primitives2D.DrawRectangle(spriteBatch, Position, Size,selectionBorderColor, 2f);
            }
            else
            {
                Primitives2D.DrawRectangle(spriteBatch, Position, Size, borderColor, 2f);

            }
            spriteBatch.DrawString(windowTitle, Title, Position + new Vector2(10, 3), titleFontColor);

            // CloseHandle
            Primitives2D.DrawLine(spriteBatch, Position + closeHandle.Location.ToVector2(), Position + closeHandle.Location.ToVector2() + new Vector2(15,15), borderColor, 2f);
            Primitives2D.DrawLine(spriteBatch, Position + closeHandle.Location.ToVector2() + new Vector2(0, 15), Position + closeHandle.Location.ToVector2() + new Vector2(15, 0), borderColor, 2f);

            // Resize Handle
            Primitives2D.DrawLine(spriteBatch, Position + Size + new Vector2(-10, 0), Position + Size + new Vector2(0, -10), borderColor, 2f);
            Primitives2D.DrawLine(spriteBatch, Position + Size + new Vector2(-15,0), Position + Size + new Vector2(0, -15), borderColor, 2f);
            Primitives2D.DrawLine(spriteBatch, Position + Size + new Vector2(-20, 0), Position + Size + new Vector2(0, -20), borderColor, 2f);
            base.Draw(gameTime);

        }

        public override void Initialize()
        {

        }

        public override void LoadContent()
        {
            windowTitle = content.Load<SpriteFont>("Fonts/WindowTitle");
        }

        public override void Update(GameTime gameTime)
        {
            foreach(Control c in GetChildren())
            {
                c.Update(gameTime);
            }
        }
  
    }
}
