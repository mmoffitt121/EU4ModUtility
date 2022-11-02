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
using EU4ModUtil.Models.Data.Map;

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

        public int NewProvince(int index, bool water)
        {
            mod.provinces = mod.provinces.OrderBy(p => p.Number).ToList();

            int provNum = mod.provinces[index].Number + 1;
            int i = index + 1;
            while (i != mod.provinces.Count())
            {
                if (provNum != mod.provinces[i].Number)
                {
                    (int, int, int) clr = water ? mod.provinces.GetUniqueProvinceColor(ColorManager.ColorMode.Sea) : mod.provinces.GetUniqueProvinceColor(ColorManager.ColorMode.Land);
                    mod.provinces.Insert(i, new Province(provNum, clr, "New Province"));
                    NotifyPropertyChanged(nameof(Provinces));
                    return i;
                }
                provNum++;
                i++;
            }

            (int, int, int) colr = water ? mod.provinces.GetUniqueProvinceColor(ColorManager.ColorMode.Sea) : mod.provinces.GetUniqueProvinceColor(ColorManager.ColorMode.Land);
            mod.provinces.Add(new Province(provNum, colr, "New Province"));
            NotifyPropertyChanged(nameof(Provinces));
            return mod.provinces.Count() - 1;
        }

        public void DeleteProvince(int index)
        {
            mod.provinces.Remove(mod.provinces[index]);
            NotifyPropertyChanged(nameof(Provinces));
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

        public List<Province> Provinces
        {
            get
            {
                if (mod == null || mod.provinces == null) return null;
                return new List<Province>(mod?.provinces);
            }
            set
            {
                if (mod != null)
                {
                    mod.provinces = value;
                }
                NotifyPropertyChanged(nameof(Provinces));
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
