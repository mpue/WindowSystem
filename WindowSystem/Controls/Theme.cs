using Microsoft.Xna.Framework;

namespace WindowSystem.Controls
{
    public abstract class Theme
    {
        public class ButtonColors {
            public virtual Color borderColor { get; } = Color.DarkGray;
            public virtual Color textColor { get; } = Color.Black;
            public virtual Color selectionBorderColor { get; } = Color.Orange;
            public virtual Color backgroundColor { get; } = Color.White;
        }
        public class CheckBoxColors
        {
            public virtual Color borderColor { get; } = Color.DarkGray;
            public virtual Color textColor { get; } = Color.Black;
            public virtual Color selectionBorderColor { get; } = Color.Orange;
            public virtual Color backgroundColor { get; } = Color.White;
        }

        public class LabelColors
        {
            public virtual Color borderColor { get; } = Color.DarkGray;
            public virtual Color textColor { get; } = Color.Black;
            public virtual Color selectionBorderColor { get; } = Color.Orange;
            public virtual Color backgroundColor { get; } = Color.White;
        }

        public class MenuBarColors
        {
            public virtual Color backgroundColor { get; } = Color.White;
            public virtual Color borderColor { get; } = Color.DarkGray;
            public virtual Color textColor { get; } = Color.Black;
        }

        public class TextEditorColors
        {
            public virtual Color borderColor { get; } = Color.DarkGray;
            public virtual Color textColor { get; } = Color.Black;
            public virtual Color selectionBorderColor { get; } = Color.Orange;
            public virtual Color backgroundColor { get; } = Color.White;
        }

        public class ScrollPanelColors
        {
            public virtual Color borderColor { get; } = Color.DarkGray;
            public virtual Color selectionBorderColor { get; } = Color.Orange;
            public virtual Color backgroundColor { get; } = Color.White;
            public virtual Color scrollHandleColor { get; } = Color.DarkGray;
            
        }

        public class WindowColors
        {
            public virtual Color titleFontColor { get; } = Color.Black;
            public virtual Color shadowColor { get; } = new Color(0.1f, 0.1f, 0.1f, 0.5f);
            public virtual Color borderColor { get; } = Color.DarkGray;
            public virtual Color titleBarColor { get; } = Color.White;
            public virtual Color selectionBorderColor { get; } = Color.Orange;
            public virtual Color windowBackgroundColor { get; } = Color.White; //new Color(0.5f, 0.5f, 0.5f, 0.5f);
            public virtual Color seperatorColor { get; } = Color.GhostWhite;
        }

        public virtual ButtonColors Button { get; protected set; } = new ButtonColors();
        public virtual CheckBoxColors CheckBox { get; protected set; } = new CheckBoxColors();
        public virtual LabelColors Label { get; protected set; } = new LabelColors();
        public virtual MenuBarColors MenuBar { get; protected set; } = new MenuBarColors();
        public virtual TextEditorColors TextEditor { get; protected set; } = new TextEditorColors();
        public virtual ScrollPanelColors ScrollPanel { get; protected set; } = new ScrollPanelColors();
        public virtual WindowColors Window { get; protected set; } = new WindowColors();

    }
}
