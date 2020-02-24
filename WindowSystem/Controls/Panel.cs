using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace WindowSystem
{
    public class Panel : Control
    {
        private Color borderColor = WindowManager.GetInstance().Theme.Label.borderColor;
        private Color selectionBorderColor = WindowManager.GetInstance().Theme.Label.selectionBorderColor;
        private Color backgroundColor = WindowManager.GetInstance().Theme.Label.backgroundColor;

        public Panel(Vector2 position, Vector2 size) : base()
        {            
            this.Position = position;
            this.Size = size;             
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
            foreach (Control child in children)
            {
                child.Update(gameTime);
            }
        }

        public override void Resized()
        {
        }

        public override void Draw(GameTime gameTime)
        {
            if (Visible)
            {

                foreach (Control c in children)
                {
                    Vector2 oldPos = c.Position;
                    c.Position += Position;
                    c.Bounds = new Rectangle(c.Position.ToPoint(), c.Size.ToPoint());
                    c.Draw(gameTime);
                    c.Position = oldPos;
                    c.Bounds = new Rectangle(c.Position.ToPoint(), c.Size.ToPoint());
                }
                

            }
        }


    }
}
