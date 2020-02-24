using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using XTron;

namespace WindowSystem
{
    public class Button : Control
    {
        public String Text = "Button";

        private Color borderColor = WindowManager.GetInstance().Theme.Button.borderColor;
        private Color textColor = WindowManager.GetInstance().Theme.Button.textColor;
        private Color selectionBorderColor = WindowManager.GetInstance().Theme.Button.selectionBorderColor;
        private Color backgroundColor = WindowManager.GetInstance().Theme.Button.backgroundColor;


        private SpriteFont buttonFont;


        public Texture2D Icon { get; set; }

        public Button(Vector2 position, Vector2 size) : base()
        {
            this.Position = position;
            this.Size = size;
            buttonFont = content.Load<SpriteFont>("Fonts/WindowTitle");
            Bounds = new Rectangle(Position.ToPoint(), size.ToPoint());
        }

        public override void Initialize()
        {
        }

        public override void LoadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Resized()
        {
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
                if (Icon == null)
                {
                    if (ShowBorder)
                    {
                        Primitives2D.DrawRectangle(spriteBatch, Position, Size, borderColor);
                    }
                }

            }
            
            Vector2 fontSize = buttonFont.MeasureString(Text);

            if (Icon != null)
            {
                spriteBatch.Draw(Icon, Bounds, Color.White);
                spriteBatch.DrawString(buttonFont, Text, Position + (Size/2 -  fontSize / 2) + new Vector2(0,Size.Y/2 + 10), textColor);
            }
            else
            {
                spriteBatch.DrawString(buttonFont, Text, Position + (Size / 2 - fontSize / 2), textColor);
            }

            // Primitives2D.DrawRectangle(spriteBatch, Bounds.Location.ToVector2(), size, Color.Yellow);
        }

    }
}
