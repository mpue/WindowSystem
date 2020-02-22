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
    public class View3D : Control
    {
        private readonly ContentManager content;
        private readonly SpriteBatch spriteBatch;
        private readonly Vector2 size;



        private Color borderColor = Color.DarkGray;
        private Color textColor = Color.Black;
        private Color selectionBorderColor = Color.Orange;
        private Color backgroundColor = Color.White;



        public View3D(ContentManager content, SpriteBatch spriteBatch, Vector2 position, String text)
        {
            this.content = content;
            this.spriteBatch = spriteBatch;
            this.Position = position;
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
        }



    }
}
