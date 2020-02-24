using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace WindowSystem
{
    public class Label : Control
    {
        private readonly Vector2 size;

        public String Text { get; set; } = "Label";

        private Color borderColor = WindowManager.GetInstance().Theme.Label.borderColor;
        private Color textColor = WindowManager.GetInstance().Theme.Label.textColor;
        private Color selectionBorderColor = WindowManager.GetInstance().Theme.Label.selectionBorderColor;
        private Color backgroundColor = WindowManager.GetInstance().Theme.Label.backgroundColor;

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
