using Microsoft.Xna.Framework;

namespace WindowSystem.Controls
{
    public class DefaultTheme : Theme
    {
        public class MyLabel : LabelColors
        {
            public override Color borderColor => base.borderColor;

            public override Color textColor => base.textColor;

            public override Color selectionBorderColor => base.selectionBorderColor;

            public override Color backgroundColor => base.backgroundColor;
        }

        public class MyButton : ButtonColors
        {
        }

        public override LabelColors Label { get => new MyLabel(); protected set => base.Label = value; }

        public override ButtonColors Button { get => new MyButton(); protected set => base.Button = value; }



    }
}
