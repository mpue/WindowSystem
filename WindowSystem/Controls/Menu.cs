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
    public class Menu : Control
    {
        private Color backgroundColor = Color.White;
        private SpriteFont menuFont;

        public bool IsOpen { get; set; }

        public string Title { get; set; } = "Menu";
        private Color textColor = Color.Black;

        public Menu(Vector2 position, Vector2 size) : base()
        {
            this.Position = position;
            this.Size = size;
            menuFont = content.Load<SpriteFont>("Fonts/WindowTitle");

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
            Vector2 fontSize = menuFont.MeasureString(Title);
            spriteBatch.DrawString(menuFont, Title, Position + (Size / 2 - fontSize / 2), textColor);

            if (IsOpen)
            {
                base.Draw(gameTime);
            }
                        
        }

    }
}
