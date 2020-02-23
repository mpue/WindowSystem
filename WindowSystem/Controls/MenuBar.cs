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
    public class MenuBar : Control
    {
        private Color backgroundColor = Color.White;
        private Color borderColor = Color.DarkGray;
        private Color textColor = Color.Black;


        public MenuBar(Vector2 position, Vector2 size) : base()
        {
            this.Position = position;
            this.Size = size;

            Bounds = new Rectangle(Position.ToPoint(), size.ToPoint());

            ControlClicked += HandleControlClicked;
        }

        private void HandleControlClicked(object sender, EventArgs e)
        {
            ControlEventArgs args = e as ControlEventArgs;

            foreach (Menu menu in children)
            {
                if (menu.Bounds.Contains(args.Position)) {
                    menu.IsOpen = true;
                }
                else
                {
                    menu.IsOpen = false;
                }
                   
            }
        }


        

        public Menu[] AddMenus(String[] names)
        {
            Menu[] menus = new Menu[names.Length];

            for(int i = 0; i < names.Length;i++)
            {
                menus[i] = AddMenu(names[i]);
            }

            return menus;
        }

        public Menu AddMenu(String text)
        {
            Menu menu = new Menu(Position + new Vector2(children.Count * 100, 5), new Vector2(80,25));
            menu.Title = text;
            children.Add(menu);
            return menu;
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
            Primitives2D.FillRectangle(spriteBatch, Position, Size, backgroundColor);

            foreach (Menu menu in children)
            {
                menu.Draw(gameTime);
            }

            Primitives2D.DrawRectangle(spriteBatch, Bounds.Location.ToVector2(), Size, borderColor);
        }

    }
}
