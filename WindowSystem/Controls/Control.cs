﻿using System;
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

        public GraphicsDeviceManager graphicsDevice { get; protected set; }
        public ContentManager content { get; protected set; }
        public SpriteBatch spriteBatch { get; protected set; }

        public virtual void HandleKeyPressed(object sender, EventArgs e)
        {

        }


        public void AddChild(Control c)
        {
            if(!(this is WindowManager))
            {
                c.Position += Position;
                Rectangle bounds = new Rectangle(c.Position.ToPoint(),  c.Bounds.Size);
                if (bounds.Width > 0 && bounds.Height > 0)
                {
                    c.Bounds = bounds;
                }

                    
            }
            children.Add(c);
            Resized();
        }

        protected Control()
        {
            if (this is WindowManager)
            {
                return;
            }
               
            this.content = WindowManager.GetInstance().content;
            this.spriteBatch = WindowManager.GetInstance().spriteBatch;
            this.graphicsDevice = WindowManager.GetInstance().graphicsDevice;
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

        public virtual void OnClick(ControlEventArgs e)
        {
            EventHandler handler = ControlClicked;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public class ControlEventArgs : EventArgs
        {
            public ControlEventArgs(Vector2 position)
            {
                Position = position;
            }

            public Vector2 Position { get; set; }
        }

        public event EventHandler ControlClicked;

    }
}
