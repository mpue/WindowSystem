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
    public class CheckBox : Control
    {
        private readonly ContentManager content;
        private readonly SpriteBatch spriteBatch;
        private readonly Vector2 size;

        public String Text { get; set; } = "CheckBox";

        private Color borderColor = Color.DarkGray;
        private Color textColor = Color.Black;
        private Color selectionBorderColor = Color.Orange;
        private Color backgroundColor = Color.White;
        private SpriteFont labelFont;

        public Texture2D Icon { get; set; }

        public bool Checked { get; set; } = true;

        public CheckBox(ContentManager content, SpriteBatch spriteBatch, Vector2 position, String text)
        {
            this.content = content;
            this.spriteBatch = spriteBatch;
            Text = text;
            this.Position = position;
            labelFont = content.Load<SpriteFont>("Fonts/LabelFont");
            this.size = labelFont.MeasureString(Text) + new Vector2(16,0);
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
            Vector2 fontSize = labelFont.MeasureString(Text);
            spriteBatch.DrawString(labelFont, Text, Position + new Vector2(20,fontSize.Y/2), textColor);

            spriteBatch.DrawRectangle(new Rectangle((int)Position.X, (int)Position.Y + (int)fontSize.Y / 2, 16, 16), Color.Black);
            if (Checked)
            {
                Primitives2D.DrawLine(spriteBatch, new Vector2((int)Position.X, (int)Position.Y + (int)fontSize.Y / 2), new Vector2((int)Position.X + 16, (int)Position.Y + 16 + (int)fontSize.Y / 2), Color.Black);
                Primitives2D.DrawLine(spriteBatch, new Vector2((int)Position.X, (int)Position.Y + 16 + (int)fontSize.Y / 2), new Vector2((int)Position.X + 16, (int)Position.Y + (int)fontSize.Y / 2 ), Color.Black);

            }
        }



    }
}
