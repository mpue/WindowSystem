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
    public class Label : Control
    {
        private readonly Vector2 size;

        public String Text { get; set; } = "Label";

        private Color borderColor = Color.DarkGray;
        private Color textColor = Color.Black;
        private Color selectionBorderColor = Color.Orange;
        private Color backgroundColor = Color.White;
        private SpriteFont labelFont;

        public Texture2D Icon { get; set; }

        public Label(Vector2 position, String text) : base()
        {
            Text = text;
            this.Position = position;
            labelFont = content.Load<SpriteFont>("Fonts/LabelFont");
            this.size = labelFont.MeasureString(Text);
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
            Vector2 fontSize = labelFont.MeasureString(Text);
            spriteBatch.DrawString(labelFont, Text, Position + new Vector2(0,fontSize.Y/2), textColor);
        }

    }
}
