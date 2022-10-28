using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.ComponentModel;
using EU4ModUtil.Models.Data;
using EU4ModUtil.Parsers;
using EU4ModUtil.Util;
using EU4ModUtil.Loaders;
using EU4ModUtil.Writers;
using EU4ModUtil.Models.Data.Common;

namespace EU4ModUtil
{
    internal class ViewModel : INotifyPropertyChanged
    {
        // Models
        internal AppData appData;
        internal Mod mod;
        internal ModLoader loader;
        internal ModWriter writer;
        internal BitmapImage noImageBitmap;

        // ViewModels
        public List<Country> Countries
        {
            get
            {
                if (mod == null || mod.countries == null) return null;
                return new List<Country>(mod?.countries);
            }
            set
            {
                if (mod != null)
                {
                    mod.countries = value;
                }
                NotifyPropertyChanged(nameof(Countries));
            }
        }
        public void NewCountry(int index)
        {
            mod.countries.ForEach(c => c.Index = (c.Index > index ? c.Index + 1 : c.Index));
            mod.countries.Insert(index + 1, new Country(index + 1));
            NotifyPropertyChanged(nameof(Countries));
        }

        public void DeleteCountry(int index)
        {
            mod.countries.ForEach(c => c.Index = (c.Index > index ? c.Index - 1 : c.Index));
            mod.countries.Remove(mod.countries[index]);
            NotifyPropertyChanged(nameof(Countries));
        }

        public List<Culture> Cultures
        {
            get
            {
                return mod?.Cultures;
            }
        }

        public List<CultureGroup> CultureGroups
        {
            get
            {
                return mod?.cultureGroups;
            }
            set
            {
                NotifyPropertyChanged(nameof(Cultures));
                NotifyPropertyChanged(nameof(CultureGroups));
            }
        }

        public List<Religion> Religions
        {
            get
            {
                return mod?.religions;
            }
            set
            {
                if (mod != null)
                {
                    mod.religions = value;
                }
                NotifyPropertyChanged("Religions");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
