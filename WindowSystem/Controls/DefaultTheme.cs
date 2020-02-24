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
            public override Color borderColor => base.borderColor;

            public override Color textColor => base.textColor;

            public override Color selectionBorderColor => base.selectionBorderColor;

            public override Color backgroundColor => base.backgroundColor;
        }

        public class MyCheckBox : CheckBoxColors
        {
            public override Color borderColor => base.borderColor;

            public override Color textColor => base.textColor;

            public override Color selectionBorderColor => base.selectionBorderColor;

            public override Color backgroundColor => base.backgroundColor;
        }

        public class MyMenuBar : MenuBarColors
        {
            public override Color backgroundColor => base.backgroundColor;

            public override Color borderColor => base.borderColor;

            public override Color textColor => base.textColor;
        }

        public class MyScrollPanel : ScrollPanelColors
        {
            public override Color borderColor => base.borderColor;

            public override Color selectionBorderColor => base.selectionBorderColor;

            public override Color backgroundColor => base.backgroundColor;
        }

        public class MyTextEditor : TextEditorColors
        {
            public override Color borderColor => base.borderColor;

            public override Color textColor => base.textColor;

            public override Color selectionBorderColor => base.selectionBorderColor;

            public override Color backgroundColor => base.backgroundColor;
        }

        public class MyWindow : WindowColors
        {
            public override Color titleFontColor => base.titleFontColor;

            public override Color shadowColor => base.shadowColor;

            public override Color borderColor => base.borderColor;

            public override Color titleBarColor => base.titleBarColor;

            public override Color selectionBorderColor => base.selectionBorderColor;

            public override Color windowBackgroundColor => base.windowBackgroundColor;

            public override Color seperatorColor => base.seperatorColor;
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
