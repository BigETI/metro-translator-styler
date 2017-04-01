using MetroFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroTranslatorStyler
{
    public interface ITranslatorStylerInterface
    {
        string Language
        {
            get;
            set;
        }

        string AssemblyName
        {
            get;
        }

        IEnumerable<Language> Languages
        {
            get;
        }

        MetroThemeStyle UseTheme
        {
            get;
            set;
        }

        MetroColorStyle UseStyle
        {
            get;
            set;
        }

        void SaveSettings();
    }
}
