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
    public class Button : Control
    {
        private readonly ContentManager content;
        private readonly SpriteBatch spriteBatch;
        private readonly Vector2 size;

        public String Text = "Button";

        private Color borderColor = Color.DarkGray;
        private Color textColor = Color.Black;
        private Color selectionBorderColor = Color.Orange;
        private Color backgroundColor = Color.White;
        private SpriteFont buttonFont;

        public Texture2D Icon { get; set; }



        public Button(ContentManager content, SpriteBatch spriteBatch, Vector2 position, Vector2 size)
        {
            this.content = content;
            this.spriteBatch = spriteBatch;
            this.Position = position;
            this.size = size;
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
            // base.Resized();
        }

        public override void Draw(GameTime gameTime)
        {
            Primitives2D.FillRectangle(spriteBatch, Position, size, backgroundColor);
            if (Selected)
            {
                Primitives2D.DrawRectangle(spriteBatch, Position, size, selectionBorderColor);
               
            }
            else
            {
                if (Icon == null)
                {
                    Primitives2D.DrawRectangle(spriteBatch, Position, size, borderColor);
                }
            }
            
            Vector2 fontSize = buttonFont.MeasureString(Text);

            if (Icon != null)
            {
                spriteBatch.Draw(Icon, Bounds, Color.White);
                spriteBatch.DrawString(buttonFont, Text, Position + (size/2 -  fontSize / 2) + new Vector2(0,size.Y/2 + 10), textColor);
            }
            else
            {
                spriteBatch.DrawString(buttonFont, Text, Position + (size / 2 - fontSize / 2), textColor);
            }

            // Primitives2D.DrawRectangle(spriteBatch, Bounds.Location.ToVector2(), size, Color.Yellow);
        }



    }
}
