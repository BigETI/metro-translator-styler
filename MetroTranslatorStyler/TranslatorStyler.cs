using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MetroTranslatorStyler
{
    public static class TranslatorStyler
    {

        private static ITranslatorStylerInterface translatorStylerInterface;

        public static ITranslatorStylerInterface TranslatorStylerInterface
        {
            get
            {
                return translatorStylerInterface;
            }
        }

        public static IEnumerable<Control> GetSelfAndChildrenRecursive(Control parent)
        {
            List<Control> ret = new List<Control>();
            if (parent != null)
            {
                foreach (Control child in parent.Controls)
                    ret.AddRange(GetSelfAndChildrenRecursive(child));
                ret.Add(parent);
            }
            return ret;
        }

        public static IEnumerable<ToolStripMenuItem> GetAllToolStripMenuItemsRecursive(ToolStripMenuItem parent)
        {
            List<ToolStripMenuItem> ret = new List<ToolStripMenuItem>();
            if (parent != null)
            {
                foreach (ToolStripItem child in parent.DropDownItems)
                {
                    if (child is ToolStripMenuItem)
                        ret.AddRange(GetAllToolStripMenuItemsRecursive((ToolStripMenuItem)child));
                }
                ret.Add(parent);
            }
            return ret;
        }

        public static IEnumerable<ToolStripMenuItem> GetAllToolStripMenuItemsRecursive(ContextMenuStrip parent)
        {
            List<ToolStripMenuItem> ret = new List<ToolStripMenuItem>();
            if (parent != null)
            {
                foreach (ToolStripItem child in parent.Items)
                {
                    if (child is ToolStripMenuItem)
                        ret.AddRange(GetAllToolStripMenuItemsRecursive((ToolStripMenuItem)child));
                }
            }
            return ret;
        }

        public static void EnumToComboBox<T>(ComboBox comboBox, T[] exclusions = null)
        {
            comboBox.Items.Clear();
            Array arr = Enum.GetValues(typeof(T));
            foreach (var e in arr)
            {
                bool s = true;
                if (exclusions != null)
                {
                    foreach (var ex in exclusions)
                    {
                        if (ex.Equals(e))
                        {
                            s = false;
                            break;
                        }
                    }
                }
                if (s)
                    comboBox.Items.Add(e);
            }
        }

        public static void EnumerableToComboBox<T>(ComboBox comboBox, IEnumerable<T> arr)
        {
            comboBox.Items.Clear();
            foreach (var e in arr)
                comboBox.Items.Add(e);
        }

        public static void LoadTranslationStyle(Control parent, ITranslatorStylerInterface translatorStylerInterface)
        {
            TranslatorStyler.translatorStylerInterface = translatorStylerInterface;
            Translator.LoadLanguage(parent);
            Styler.LoadThemeAndStyle(parent);
        }
    }
}
