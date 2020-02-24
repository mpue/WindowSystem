using Microsoft.Xna.Framework;

namespace WindowSystem.Controls
{
    public class DarkTheme : Theme
    {
        private static Color DarkerGrey = new Color(100, 100, 100, 255);
        private static Color DarkerBlue = new Color(0, 0, 100, 255);

        public class MyLabel : LabelColors
        {
            public override Color borderColor => Color.LightGray;

            public override Color textColor => Color.White;

            public override Color selectionBorderColor => Color.White;

            public override Color backgroundColor => DarkerGrey;
        }

        public class MyButton : ButtonColors
        {
            public override Color borderColor => Color.LightGray;

            public override Color textColor => Color.White;

            public override Color selectionBorderColor => Color.White;

            public override Color backgroundColor => DarkerGrey;

        }

        public class MyCheckBox : CheckBoxColors
        {
            public override Color borderColor => Color.LightGray;

            public override Color textColor => Color.White;

            public override Color selectionBorderColor => Color.White;

            public override Color backgroundColor => DarkerGrey;

        }

        public class MyMenuBar : MenuBarColors
        {
            public override Color borderColor => Color.LightGray;

            public override Color textColor => Color.White;

            public override Color backgroundColor => DarkerGrey;

        }

        public class MyScrollPanel : ScrollPanelColors
        {
            public override Color borderColor => Color.LightGray;

            public override Color selectionBorderColor => Color.White;

            public override Color backgroundColor => DarkerGrey;

            public override Color scrollHandleColor => Color.LightGray;
        }

        public class MyTextEditor : TextEditorColors
        {
            public override Color borderColor => Color.LightGray;

            public override Color textColor => Color.White;

            public override Color selectionBorderColor => Color.White;

            public override Color backgroundColor => DarkerGrey;
        }

        public class MyWindow : WindowColors
        {
            public override Color titleFontColor => Color.White;

            public override Color shadowColor => base.shadowColor;

            public override Color borderColor => Color.LightGray;

            public override Color titleBarColor => DarkerGrey;

            public override Color selectionBorderColor => Color.White;

            public override Color windowBackgroundColor => DarkerGrey;

            public override Color seperatorColor => Color.DarkGray;
        }

        public override LabelColors Label { get => new MyLabel(); protected set => base.Label = value; }

        public override ButtonColors Button { get => new MyButton(); protected set => base.Button = value; }

        public override CheckBoxColors CheckBox { get => new MyCheckBox(); protected set => base.CheckBox = value; }

        public override MenuBarColors MenuBar { get => new MyMenuBar(); protected set => base.MenuBar = value; }

        public override ScrollPanelColors ScrollPanel { get => new MyScrollPanel(); protected set => base.ScrollPanel = value; }

        public override TextEditorColors TextEditor { get => new MyTextEditor(); protected set => base.TextEditor = value; }

        public override WindowColors Window { get => new MyWindow(); protected set => base.Window = value; }

    }
}
