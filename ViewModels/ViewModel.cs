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

        public Dictionary<(int, int, int), Province> ProvinceDict;

        public void UpdateProvinceDict()
        {
            if (mod == null || mod.provinces == null) { return; }

            ProvinceDict = new Dictionary<(int, int, int), Province>();
            foreach (Province province in mod.provinces)
            {
                ProvinceDict.TryAdd(((int)province.R, (int)province.G, (int)province.B), province);
            }
        }

        public Dictionary<string, Country> CountryDict;

        public void UpdateCountryDict()
        {
            if (mod == null || mod.countries == null) { return; }

            CountryDict = new Dictionary<string, Country>();
            foreach (Country c in mod.countries)
            {
                CountryDict.TryAdd(c.Tag, c);
            }
        }

        public BitmapImage MapDisplay
        {
            get
            {
                return mod.provincesImage;
            }
        }

        public void NewCountry(int index)
        {
            mod.countries.ForEach(c => c.Index = (c.Index > index ? c.Index + 1 : c.Index));
            mod.countries.Insert(index + 1, new Country(index + 1));
            NotifyPropertyChanged(nameof(Countries));
            UpdateCountryDict();
        }

        public void DeleteCountry(int index)
        {
            mod.countries.ForEach(c => c.Index = (c.Index > index ? c.Index - 1 : c.Index));
            mod.countries.Remove(mod.countries[index]);
            NotifyPropertyChanged(nameof(Countries));
            UpdateCountryDict();
        }

        public int NewProvince(int index, bool water)
        {
            mod.provinces = mod.provinces.OrderBy(p => p.Number).ToList();

            if (index < 0) index = 0;

            int provNum = mod.provinces[index].Number + 1;
            int i = index + 1;
            while (i != mod.provinces.Count())
            {
                if (provNum != mod.provinces[i].Number)
                {
                    (int, int, int) clr = water ? mod.provinces.GetUniqueProvinceColor(ColorManager.ColorMode.Sea) : mod.provinces.GetUniqueProvinceColor(ColorManager.ColorMode.Land);
                    Province p = new Province(provNum, clr, "New Province");
                    p.IsCity = !water;
                    p.ProvinceType = water ? ProvinceType.Sea : ProvinceType.Land;
                    mod.provinces.Insert(i, p);
                    NotifyPropertyChanged(nameof(Provinces));
                    return i;
                }
                provNum++;
                i++;
            }

            (int, int, int) colr = water ? mod.provinces.GetUniqueProvinceColor(ColorManager.ColorMode.Sea) : mod.provinces.GetUniqueProvinceColor(ColorManager.ColorMode.Land);
            Province pf = new Province(provNum, colr, "New Province");
            pf.IsCity = !water;
            pf.ProvinceType = water ? ProvinceType.Sea : ProvinceType.Land;
            mod.provinces.Add(pf);
            NotifyPropertyChanged(nameof(Provinces));
            UpdateProvinceDict();
            return mod.provinces.Count() - 1;
        }

        public void DeleteProvince(int index)
        {
            mod.provinces.Remove(mod.provinces[index]);
            NotifyPropertyChanged(nameof(Provinces));
            UpdateProvinceDict();
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

        private Culture selectedCulture;
        public Culture SelectedCulture 
        {
            get
            {
                return selectedCulture;
            }
            set
            {
                Trace.WriteLine("Setting Selected Culture");
                selectedCulture = value;
                NotifyPropertyChanged(nameof(SelectedCulture));
                Trace.WriteLine(SelectedCulture);
            }
        }

        public void NewCultureGroup(int index)
        {
            mod.cultureGroups.Insert(index + 1, new CultureGroup("new_group"));
        }

        public void NewCulture(CultureGroup parent)
        {
            Culture newCulture = new Culture("new_culture");
            newCulture.Parent = parent;
            parent.Cultures.Add(newCulture);
        }

        public void NewCulture(int index)
        {
            Trace.WriteLine(index);
            Culture newCulture = new Culture("new_culture");
            newCulture.Parent = mod.cultureGroups[index];
            mod.cultureGroups[index].Cultures.Add(newCulture);
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

        public void LoadModInfo()
        {
            mod = new Mod();
            loader = new ModLoader(mod, appData);
            loader.LoadMod();
            UpdateProvinceDict();
            UpdateCountryDict();
        }
    }
}
