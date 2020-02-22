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
    public class TextEditorControl : Control
    {
        private readonly ContentManager content;
        private readonly SpriteBatch spriteBatch;
        

        public String Text { get; set; }  = "Lorem ipsum dolor sit amet, et his phaedrum intellegat, \n"+
            "ut dicta propriae ullamcorper eos. Movet putent constituam est id. Solet nonumy duo id,\n "+
            "docendi constituam duo no, dicta movet dissentiet pro at. Est te intellegat delicatissimi,\n" +
            " vel no iudico quando intellegebat.\n"+
            "Mutat audire sed te, sale dolor tacimates pro cu, ut nam elit mandamus oportere.\n" +
            "An sed etiam antiopam.Ea dicunt quaeque conceptam quo.Accusata interpretaris mei eu, agam impedit vis ad. \n" +
            "Qui eu audiam scriptorem, utamur pertinax ocurreret et nam.Ad usu natum interesset, et nulla scripserit ius.\n";

        private Color borderColor = Color.DarkGray;
        private Color textColor = Color.Black;
        private Color selectionBorderColor = Color.Orange;
        private Color backgroundColor = Color.White;
        private SpriteFont editorFont;
        private bool cursorVisible = false;
        private String cursor = "_";

        private float currentTime = 0;
        private float blinkInterval = 1.0f;

        private RasterizerState rasterizerState;

        public TextEditorControl(ContentManager content, SpriteBatch spriteBatch, Vector2 position, Vector2 size)
        {
            this.content = content;
            this.spriteBatch = spriteBatch;
            this.Position = position;
            this.Size = size;
            editorFont = content.Load<SpriteFont>("Fonts/WindowTitle");
            Bounds = new Rectangle(Position.ToPoint(), size.ToPoint());
            rasterizerState = new RasterizerState();
            rasterizerState.ScissorTestEnable = true;
        }

        public override void Initialize()
        {
        }

        public override void LoadContent()
        {

        }

        public override void Update(GameTime gameTime)
        {

            currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (currentTime > blinkInterval)
            {
                currentTime = 0;
                cursorVisible = !cursorVisible;
            }

            if (cursorVisible)
            {
                cursor = "_";
            }
            else
            {
                cursor = "";
            }
                        
                
        }

        public override void Draw(GameTime gameTime)
        {
            Primitives2D.FillRectangle(spriteBatch, Position, Size, backgroundColor);
            if (Selected)
            {
                Primitives2D.DrawRectangle(spriteBatch, Position, Size, selectionBorderColor);

            }
            else
            {
                Primitives2D.DrawRectangle(spriteBatch, Position, Size, borderColor);
            }

            RasterizerState state = spriteBatch.GraphicsDevice.RasterizerState;

            spriteBatch.GraphicsDevice.RasterizerState = rasterizerState;
            //Copy the current scissor rect so we can restore it after
            Rectangle currentRect = spriteBatch.GraphicsDevice.ScissorRectangle;

            //Set the current scissor rectangle
            spriteBatch.GraphicsDevice.ScissorRectangle = Bounds;
            spriteBatch.DrawString(editorFont, Text + cursor, Position + new Vector2(20, 20), Color.Black);
            spriteBatch.GraphicsDevice.ScissorRectangle = currentRect;
            spriteBatch.GraphicsDevice.RasterizerState = state;

            // Primitives2D.DrawRectangle(spriteBatch, Bounds.Location.ToVector2(), size, Color.Yellow);
        }
                                                                                                                                        
        public void Add(char s)
        {
            if (s >= 0x20)
            {
                Text += new String(s, 1);
            }
            else if (s == 0x08)
            {
                Text = Text.Substring(0, Text.Length - 1);
            }
            else if (s == 0x0a)
            {
                Text += "\n";
            }


        }
            

    }
}
