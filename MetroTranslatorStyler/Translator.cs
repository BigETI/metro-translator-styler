﻿using MetroFramework.Controls;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;

namespace MetroTranslatorStyler
{
    public static class Translator
    {
        
        private static ResourceManager languageResourceManager = null;
        
        private static void InitLanguage()
        {
            if (languageResourceManager == null)
            {
                try
                {
                    CultureInfo ci = new CultureInfo(TranslatorStyler.TranslatorStylerInterface.Language);
                    Assembly a = Assembly.Load(TranslatorStyler.TranslatorStylerInterface.AssemblyName);
                    languageResourceManager = new ResourceManager(TranslatorStyler.TranslatorStylerInterface.AssemblyName + ".Languages." + TranslatorStyler.TranslatorStylerInterface.Language, a);
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.Message);
                }
            }
        }

        public static bool TryTranslate(string input, out string output)
        {
            bool ret = false;
            output = input;
            if (input.StartsWith("{$") && input.EndsWith("$}") && (input.Length > 4))
            {
                output = GetTranslation(input.Substring(2, input.Length - 4));
                ret = true;
            }
            return ret;
        }

        public static void LoadLanguage(Control parent)
        {
            try
            {
                string translated = "";
                InitLanguage();
                IEnumerable<Control> controls = TranslatorStyler.GetSelfAndChildrenRecursive(parent);
                foreach (Control c in controls)
                {
                    if (TryTranslate(c.Text, out translated))
                        c.Text = translated;
                    if (c is ComboBox)
                    {
                        ComboBox cb = (ComboBox)c;
                        for (int i = 0; i < cb.Items.Count; i++)
                        {
                            if (TryTranslate((string)cb.Items[i], out translated))
                                cb.Items[i] = translated;
                        }
                        if (cb is MetroComboBox)
                        {
                            MetroComboBox mcb = (MetroComboBox)cb;
                            if (TryTranslate(mcb.PromptText, out translated))
                                mcb.PromptText = translated;
                        }
                    }
                    else if (c is ListView)
                    {
                        ListView lv = (ListView)c;
                        foreach (ColumnHeader col in lv.Columns)
                        {
                            if (TryTranslate(col.Text, out translated))
                                col.Text = translated;
                        }
                        foreach (ListViewGroup grp in lv.Groups)
                        {
                            if (TryTranslate(grp.Header, out translated))
                                grp.Header = translated;
                        }
                    }
                    else if (c is ToolStrip)
                    {
                        foreach (ToolStripItem tsi in ((ToolStrip)c).Items)
                        {
                            if (TryTranslate(tsi.Text, out translated))
                                tsi.Text = translated;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
            }
        }

        public static string GetTranslation(string key)
        {
            string ret = "{$" + key + "$}";
            if (languageResourceManager != null)
            {
                try
                {
                    ret = languageResourceManager.GetString(key);
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.Message);
                }
            }
            return ret;
        }

        public static void ChangeLanguage(Language language)
        {
            if (TranslatorStyler.TranslatorStylerInterface.Language != language.Culture)
            {
                TranslatorStyler.TranslatorStylerInterface.Language = language.Culture;
                TranslatorStyler.TranslatorStylerInterface.SaveSettings();
                Application.Restart();
            }
        }
    }
}
