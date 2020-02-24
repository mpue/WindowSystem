using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using XTron;

namespace WindowSystem
{
    public class CheckBox : Control
    {
        private readonly Vector2 size;

        public String Text { get; set; } = "CheckBox";

        private Color borderColor = WindowManager.GetInstance().Theme.CheckBox.borderColor;
        private Color textColor = WindowManager.GetInstance().Theme.CheckBox.textColor;
        private Color selectionBorderColor = WindowManager.GetInstance().Theme.CheckBox.selectionBorderColor;
        private Color backgroundColor = WindowManager.GetInstance().Theme.CheckBox.backgroundColor;

        private SpriteFont labelFont;

        public Texture2D Icon { get; set; }

        public bool Checked { get; set; } = true;

        public CheckBox(Vector2 position, String text) : base()
        {
            Text = text;
            this.Position = position;
            labelFont = content.Load<SpriteFont>("Fonts/LabelFont");
            this.size = labelFont.MeasureString(Text) + new Vector2(16,0);
            Bounds = new Rectangle(Position.ToPoint(), size.ToPoint());
        }


        public override void OnClick(ControlEventArgs e)
        {
            Checked = !Checked;    
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
            Vector2 fontSize = labelFont.MeasureString(Text);
            
            spriteBatch.DrawString(labelFont, Text, Position + new Vector2(20,fontSize.Y/2), textColor);

            spriteBatch.FillRectangle(new Rectangle((int)Position.X, (int)Position.Y + (int)fontSize.Y / 2, 16, 16), backgroundColor);
            spriteBatch.DrawRectangle(new Rectangle((int)Position.X, (int)Position.Y + (int)fontSize.Y / 2, 16, 16), borderColor);
            
            if (Checked)
            {
                Primitives2D.DrawLine(spriteBatch, new Vector2((int)Position.X, (int)Position.Y + (int)fontSize.Y / 2), new Vector2((int)Position.X + 16, (int)Position.Y + 16 + (int)fontSize.Y / 2), borderColor);
                Primitives2D.DrawLine(spriteBatch, new Vector2((int)Position.X, (int)Position.Y + 16 + (int)fontSize.Y / 2), new Vector2((int)Position.X + 16, (int)Position.Y + (int)fontSize.Y / 2 ), borderColor);

            }
        }



    }
}
