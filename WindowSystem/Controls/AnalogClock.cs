using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using XTron;

namespace WindowSystem
{
    public class AnalogClock : Control
    {
               private readonly float radius;
        private float currentTime = 0;

        private Texture2D backgroundImage;
        private Texture2D glassImage;

        private Color borderColor = WindowManager.GetInstance().Theme.Label.borderColor;
        private Color textColor = WindowManager.GetInstance().Theme.Label.textColor;
        private Color selectionBorderColor = WindowManager.GetInstance().Theme.Label.selectionBorderColor;
        private Color backgroundColor = WindowManager.GetInstance().Theme.Label.backgroundColor;

        

        public Texture2D Icon { get; set; }

        private DateTime time;

        public AnalogClock(Vector2 position, float radius) : base()
        {
         
            this.Position = position;
            this.Size = new Vector2(radius * 2, radius * 2);
            Bounds = new Rectangle((Position - Size/2).ToPoint(), Size.ToPoint());
            this.radius = radius;
            backgroundImage = content.Load<Texture2D>("Sprites/clock_bg");
            glassImage = content.Load<Texture2D>("Sprites/clock_glass");

        }

        public override void Initialize()
        {
        }

        public override void LoadContent()
        {
        }

        public override void Update(GameTime gameTime) 
        {
            Bounds = new Rectangle((Position - Size / 2).ToPoint(), Size.ToPoint());
            currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (currentTime >= 1.0f)
            {
                currentTime = 0;
                time = DateTime.Now;
            }
            foreach (Control c in GetChildren())
            {
                c.Update(gameTime);
            }
        }

        public override void Resized()
        {
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(backgroundImage, Position - Size/2, Color.White);
            Primitives2D.DrawCircle(spriteBatch, Position, Size.X / 2, 64, borderColor, 2.0f);

            Vector2 secondsPosition = Position + new Vector2(radius * 0.7f, 0);
            Vector2 currentSeconds = rotatePoint((int)secondsPosition.X, (int)secondsPosition.Y, (int)Position.X, (int)Position.Y, -6 * time.Second).ToVector2();

            Vector2 minutesPosition = Position + new Vector2(radius*0.7f, 0);
            Vector2 currentMinutes = rotatePoint((int)minutesPosition.X, (int)minutesPosition.Y, (int)Position.X, (int)Position.Y, -6 * time.Minute).ToVector2();

            Vector2 hoursPosition = Position + new Vector2(radius * 0.6f, 0);
            Vector2 currentHours = rotatePoint((int)hoursPosition.X, (int)hoursPosition.Y, (int)Position.X, (int)Position.Y, -6 * (time.Hour - 2)).ToVector2();


            Primitives2D.DrawLine(spriteBatch, Position, currentSeconds, borderColor, 2.0f);
            Primitives2D.DrawLine(spriteBatch, Position, currentMinutes, borderColor, 3.0f);
            Primitives2D.DrawLine(spriteBatch, Position, currentHours, borderColor, 3.0f);

            spriteBatch.Draw(glassImage, Position - Size / 2, Color.White);
        }

        public static Point rotatePoint(int x, int y, int xC, int yC, float angle)
        {
            float rot = (float)MathHelper.ToRadians(angle +90);
            double dx = xC + (x - xC) * Math.Cos(rot) + (y - yC) * Math.Sin(rot);
            double dy = yC - (x - xC) * Math.Sin(rot) + (y - yC) * Math.Cos(rot);
            int x_ = (int)dx;
            int y_ = (int)dy;
            return new Point(x_, y_);
        }
    }
}
