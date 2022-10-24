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

namespace EU4ModUtil
{
    public partial class MainWindow : Window
    {
        private void InitializeCountryDisplay()
        {
            //countryDataGrid.DataContext = viewModel.Countries;
            viewModel.Countries = new ObservableCollection<Country>() { new Country(null, 1), new Country(null, 2) };
        }

        private void UpdateCountryDisplay()
        {
            
            /*if (viewModel.mod.countries != null)
            {
                viewModel.Countries = new ObservableCollection<Country>(viewModel.mod.countries);
            }
            else
            {
                Trace.WriteLine("No countries found");
                viewModel.Countries = new ObservableCollection<Country>();
            }*/
            //CountryGrid.BindingGroup
            //CountryGrid.DataContext = DataGridItems;
        }
    }
}
