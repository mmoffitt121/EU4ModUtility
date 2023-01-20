using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using EU4ModUtil.Models.Data.Map;
using EU4ModUtil.ViewModels;
using EU4ModUtil.Models.Macros.Provinces;

namespace EU4ModUtil.Views
{
    /// <summary>
    /// Interaction logic for AddProvinceWindow.xaml
    /// </summary>
    public partial class AddProvinceWindow : Window
    {
        private List<Province> provinces;
        private int startIndex;
        private AddProvinceViewModel vm;

        private void UpdateR(object sender, SelectionChangedEventArgs e)
        {
            if (vm == null) { return; }

            vm.ROperation = (ColorOperationType)rMacroTypeBox.SelectedItem;

            switch (vm.ROperation)
            {
                case ColorOperationType.Fixed:
                    rBox.IsEnabled = true;
                    rIncrementBox.IsEnabled = false;
                    rIntervalBox.IsEnabled = false;
                    break;
                case ColorOperationType.Increment:
                    rBox.IsEnabled = true;
                    rIncrementBox.IsEnabled = true;
                    rIntervalBox.IsEnabled = true;
                    break;
                case ColorOperationType.Decrement:
                    rBox.IsEnabled = true;
                    rIncrementBox.IsEnabled = true;
                    rIntervalBox.IsEnabled = true;
                    break;
                case ColorOperationType.None:
                    rBox.IsEnabled = false;
                    rIncrementBox.IsEnabled = false;
                    rIntervalBox.IsEnabled = false;
                    break;
                default:
                    break;
            }
        }
        private void UpdateG(object sender, SelectionChangedEventArgs e)
        {
            if (vm == null) { return; }

            vm.GOperation = (ColorOperationType)gMacroTypeBox.SelectedItem;

            switch (vm.GOperation)
            {
                case ColorOperationType.Fixed:
                    gBox.IsEnabled = true;
                    gIncrementBox.IsEnabled = false;
                    gIntervalBox.IsEnabled = false;
                    break;
                case ColorOperationType.Increment:
                    gBox.IsEnabled = true;
                    gIncrementBox.IsEnabled = true;
                    gIntervalBox.IsEnabled = true;
                    break;
                case ColorOperationType.Decrement:
                    gBox.IsEnabled = true;
                    gIncrementBox.IsEnabled = true;
                    gIntervalBox.IsEnabled = true;
                    break;
                case ColorOperationType.None:
                    gBox.IsEnabled = false;
                    gIncrementBox.IsEnabled = false;
                    gIntervalBox.IsEnabled = false;
                    break;
                default:
                    break;
            }
        }
        private void UpdateB(object sender, SelectionChangedEventArgs e)
        {
            if (vm == null) { return; }

            vm.BOperation = (ColorOperationType)bMacroTypeBox.SelectedItem;

            switch (vm.BOperation)
            {
                case ColorOperationType.Fixed:
                    bBox.IsEnabled = true;
                    bIncrementBox.IsEnabled = false;
                    bIntervalBox.IsEnabled = false;
                    break;
                case ColorOperationType.Increment:
                    bBox.IsEnabled = true;
                    bIncrementBox.IsEnabled = true;
                    bIntervalBox.IsEnabled = true;
                    break;
                case ColorOperationType.Decrement:
                    bBox.IsEnabled = true;
                    bIncrementBox.IsEnabled = true;
                    bIntervalBox.IsEnabled = true;
                    break;
                case ColorOperationType.None:
                    bBox.IsEnabled = false;
                    bIncrementBox.IsEnabled = false;
                    bIntervalBox.IsEnabled = false;
                    break;
                default:
                    break;
            }
        }

        public AddProvinceWindow(List<Province> provinces, int startIndex)
        {
            InitializeComponent();

            this.provinces = provinces;
        }
    }
}
