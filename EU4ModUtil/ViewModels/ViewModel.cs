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
using EU4ModUtil.Models.Data;
using EU4ModUtil.Parsers;
using EU4ModUtil.Util;
using EU4ModUtil.Loaders;
using EU4ModUtil.Models.Data.Common;
using System.ComponentModel;

namespace EU4ModUtil
{
    internal class ViewModel
    {
        // Models
        internal AppData appData;
        internal Mod mod;
        internal ModLoader loader;
        internal BitmapImage noImageBitmap;

        // ViewModels
        internal ObservableCollection<Country> Countries { get; set; }

        private string text;
        public string Text 
        { 
            get
            {
                return text;
            }
            set
            {
                text = value;
                NotifyPropertyChanged("Text");
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
