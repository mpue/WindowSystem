using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using XTron;

namespace WindowSystem
{
    public class TextEditorControl : Control
    {
        private readonly StringBuilder sb = new StringBuilder();

        private int row = 0;
        private int col = 0;

        public String Text { 
            get
            {
                sb.Clear();

                foreach(string s in buffers)
                {
                    sb.Append(s);
                    sb.Append("\n");
                }

                return sb.ToString();
            } 
            set
            {
                buffers.Clear();
                string[] lines = value.Split(new[] { '\n' });

                foreach(string s in lines)
                {
                    buffers.Add(s);
                }                
            } 
        }

        private Color borderColor = Color.DarkGray;
        private Color textColor = Color.Black;
        private Color selectionBorderColor = Color.Orange;
        private Color backgroundColor = Color.White;
        private SpriteFont editorFont;
        private bool cursorVisible = false;
        private String cursor = "_";

        List<string> buffers = new List<string>();

        private float currentTime = 0;
        private float blinkInterval = 1.0f;

        

        public TextEditorControl(Vector2 position, Vector2 size) : base()
        {
            this.Position = position;
            this.Size = size;
            editorFont = content.Load<SpriteFont>("Fonts/WindowTitle");
            Bounds = new Rectangle(Position.ToPoint(), size.ToPoint());
            ControlClicked += HandleEditorClick;

        }

        public override void HandleKeyPressed(object sender, EventArgs e)
        {
            KeyboardEventArgs args = e as KeyboardEventArgs;
            Add(args.key);
        }

        private void HandleEditorClick(object sender, EventArgs e)
        {
            ControlEventArgs args = e as ControlEventArgs;
            Vector2 mousePosition = args.Position - Position;

            float textHeight = editorFont.LineSpacing * buffers.Count;

            int currentRow = (int)(mousePosition.Y / editorFont.LineSpacing) - 1;
            if (currentRow > buffers.Count - 1)
            {
                currentRow = buffers.Count - 1;
            }
            if (currentRow < 0)
            {
                currentRow = 0;
            }

            row = currentRow;

            float currentWidth = editorFont.MeasureString(buffers[currentRow]).X;
            float averageWidth = currentWidth / buffers[currentRow].Length;
            int currentCol = (int)(mousePosition.X / averageWidth ) - 1;

            if (currentCol > buffers[currentRow].Length - 1)
            {
                currentCol = buffers[currentRow].Length - 1;
            }

            if (currentCol < 0)
            {
                currentCol = 0;
            }

            col = currentCol;
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
            string currentString = buffers[row].Substring(0, col);
            Vector2 fontMetrics = editorFont.MeasureString(currentString);

            spriteBatch.DrawString(editorFont, Text, Position + new Vector2(20, 20), textColor);
            spriteBatch.DrawString(editorFont, cursor, Position + new Vector2(fontMetrics.X,row * editorFont.LineSpacing) + new Vector2(20, 20), textColor);

            // Primitives2D.DrawRectangle(spriteBatch, Bounds.Location.ToVector2(), size, Color.Yellow);
        }
                                                                                                                                        
        public void Add(char s)
        {
            if (s == 0x20)
            {
                buffers[row] = buffers[row].Insert(col, " ");
                col++;
            }
            else if (s >= 0x30)
            {
                buffers[row] = buffers[row].Insert(col, new string(s, 1));
                col++;
            }

            else if (s == 0x08)
            {
                if (col > 0)
                {
                    buffers[row] = buffers[row].Remove(col-1, 1);
                    col--;
                }
            }
            // enter
            else if (s == 0x0a)
            {
                String second = buffers[row].Substring(col);
                buffers[row] = buffers[row].Substring(0, col) ;
                buffers.Insert(row+1, second);
                col = 0;
                row++;

            }
            // end
            else if (s == 0x23)
            {
                col = buffers[row].Length;
            }
            // home
            else if (s == 0x24)
            {
                col = 0;
            }
            // cursor left
            else if (s == 0x25)
            {
                if (col > 0)
                {
                    col--;
                }
                else
                {
                    col = buffers[row - 1].Length;
                    if (row > 0)
                    {
                        row--;
                    }
                }
            }
            // cursor right
            else if (s == 0x27)
            {
                if (col < buffers[row].Length)
                {
                    col++;
                }
                else
                {
                    if (row < buffers.Count - 2)
                    {
                        col = 0;
                        row++;
                    }
                }
            }
            // cursor up 
            else if (s == 0x26)
            {
                if (row > 0)
                {
                    row--;
                }                            
            }
            // cursor down
            else if (s == 0x28)
            {
                if (row < buffers.Count - 2)
                {
                    row++;
                }
            }

        }       
    }
}
