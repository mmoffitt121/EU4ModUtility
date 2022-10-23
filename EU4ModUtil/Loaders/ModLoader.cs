using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Diagnostics;
using EU4ModUtil.Models.Data;
using EU4ModUtil.Parsers;

namespace EU4ModUtil.Loaders
{
    internal class ModLoader
    {
        private Mod mod;
        private AppData appData;

        internal void LoadMod(string path)
        {
            appData.modPath = path;
            LoadMod();
        }

        internal void LoadMod()
        {
            TXTFileObject desc = TXTParser.Parse(appData.modPath + "/descriptor.mod");
            mod.descriptor = new Descriptor(desc);
            LoadImage();
        }

        internal void LoadImage()
        {
            mod.descriptor.bitmap = new BitmapImage();
            mod.descriptor.bitmap.BeginInit();
            mod.descriptor.bitmap.UriSource = new Uri(appData.modPath + "/" + mod.descriptor.picture);
            mod.descriptor.bitmap.EndInit();
        }

        public ModLoader(Mod mod, AppData appData)
        {
            this.mod = mod;
            this.appData = appData;
        }
    }
}
