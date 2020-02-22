using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using XTron;

namespace WindowSystem
{
    public abstract class Control : AbstractEntity
    {
        protected readonly List<Control> children = new List<Control>();

        public Rectangle Bounds { get; set; }

        public Rectangle OriginalBounds { get; set; }

        public bool Selected { get; set; } = false;

        public bool focus { get; private set; }

        public Vector2 Position { get; set; }

        public Vector2 MarginTL { get; set; } = new Vector2(0, 0);
        public Vector2 MarginBR { get; set; } = new Vector2(10, 10);

        public Vector2 Size { get; set; }

        public Vector2 StartPosition { get; set; }

        public Rectangle Startbounds { get; set; }

        public void AddChild(Control c)
        {
            c.Position += Position;
            Rectangle bounds = new Rectangle(c.Position.ToPoint(),  c.Bounds.Size);
            c.Bounds = bounds;
            children.Add(c);
            Resized();
        }

        public List<Control> GetChildren()
        {
            return children;
        }

        public void RemoveChild(Control c)
        {
            children.Remove(c);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach(Control c in children)
            {
                c.Draw(gameTime);
            }
        }

        public void StartUpdate(Vector2 pos)
        {
            this.StartPosition = Position;
            this.Startbounds = Bounds;
            foreach (Control c in children)
            {
                c.StartUpdate(pos);
            }

        }

        public void UpdateChildren(Vector2 delta)
        {
            Position =  StartPosition + delta;
            Bounds = new Rectangle(Startbounds.Location + delta.ToPoint(), Startbounds.Size);

            foreach (Control c in children)
            {
                c.UpdateChildren(delta);
            }
        }


        public void Focus()
        {
            this.focus = true;
        }

        public void LostFocus()
        {
            this.focus = false;
        }

        public virtual void Resized()
        {

            foreach (Control c in children)
            {

                if (c.Bounds.Size.X > Bounds.Size.X || c.Bounds.Size.X > Bounds.Size.Y)
                {
                    Point size = new Point(Bounds.Size.X,Bounds.Size.Y);
                    Rectangle bounds = new Rectangle(c.Bounds.Location, size);
                    c.Bounds = Bounds;
                    c.Size = Bounds.Size.ToVector2() - (c.Position - Position) - MarginBR;
                }
                c.Resized();
            }

        }
        public void Layout()
        {
            foreach (Control c in children)
            {
                c.Layout();
            }

        }


    }
}
