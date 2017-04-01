using MetroFramework;
using MetroFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            List<Control> controls = new List<Control>();
            foreach (Control child in parent.Controls)
                controls.AddRange(GetSelfAndChildrenRecursive(child));
            controls.Add(parent);
            return controls;
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
