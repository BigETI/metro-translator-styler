using MetroFramework;
using MetroFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetroTranslatorStyler
{
    public static class Styler
    {
        private static void SetTheme(Control parent, MetroThemeStyle theme, MetroColorStyle style)
        {
            if (parent is IMetroControl)
            {
                ((IMetroControl)parent).Theme = theme;
                ((IMetroControl)parent).Style = style;
                parent.Refresh();
            }
            else if (parent is IMetroComponent)
            {
                ((IMetroComponent)parent).Theme = theme;
                ((IMetroComponent)parent).Style = style;
                parent.Refresh();
            }
            else if (parent is IMetroForm)
            {
                ((IMetroForm)parent).Theme = theme;
                ((IMetroForm)parent).Style = style;
                parent.Refresh();
            }
        }

        public static void LoadThemeAndStyle(Control parent)
        {
            MetroThemeStyle theme = TranslatorStyler.TranslatorStylerInterface.UseTheme;
            MetroColorStyle style = TranslatorStyler.TranslatorStylerInterface.UseStyle;
            IEnumerable<Control> controls = TranslatorStyler.GetSelfAndChildrenRecursive(parent);
            foreach (Control c in controls)
                SetTheme(c, theme, style);
        }

        public static void LoadReversedTheme(Control parent)
        {
            MetroThemeStyle theme = (TranslatorStyler.TranslatorStylerInterface.UseTheme == MetroThemeStyle.Dark) ? MetroThemeStyle.Light : MetroThemeStyle.Dark;
            MetroColorStyle style = TranslatorStyler.TranslatorStylerInterface.UseStyle;
            IEnumerable<Control> controls = TranslatorStyler.GetSelfAndChildrenRecursive(parent);
            foreach (Control c in controls)
                SetTheme(c, theme, style);
        }
    }
}
