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
using EU4ModUtil.ViewModels;
using EU4ModUtil.Models.Data.Map;
using EU4ModUtil.Models.Macros.Provinces;

namespace EU4ModUtil.Views
{
    /// <summary>
    /// Interaction logic for SetProvinceValueWindow.xaml
    /// </summary>
    public partial class SetProvinceValueWindow : Window
    {
        private SetProvinceValueViewModel vm;

        public SetProvinceValueWindow(List<Province> provinces)
        {
            InitializeComponent();

            vm = new SetProvinceValueViewModel(provinces);

            DataContext = vm;
            provinceDataGrid.ItemsSource = vm.Provinces;
            provinceDataGrid.Items.Refresh();

            DisableFields();
        }

        private void UpdateSelected(object sender, SelectionChangedEventArgs e)
        {
            if (vm == null) { return; }

            DisableFields();

            vm.ColumnType = (ProvinceMacroColumnType)columnTypeBox.SelectedItem;

            switch (vm.ColumnType)
            {
                case ProvinceMacroColumnType.Select:
                    break;
                case ProvinceMacroColumnType.ID:
                    break;
                case ProvinceMacroColumnType.Name:
                case ProvinceMacroColumnType.LocalisedName:
                case ProvinceMacroColumnType.LocalisedAdjective:
                case ProvinceMacroColumnType.Owner:
                case ProvinceMacroColumnType.Controller:
                case ProvinceMacroColumnType.Culture:
                case ProvinceMacroColumnType.Religion:
                case ProvinceMacroColumnType.DiscoveredBy:
                case ProvinceMacroColumnType.Unrest:
                case ProvinceMacroColumnType.CapitalName:
                case ProvinceMacroColumnType.Area:
                case ProvinceMacroColumnType.Continent:
                case ProvinceMacroColumnType.Cores:
                case ProvinceMacroColumnType.TradeGood:
                case ProvinceMacroColumnType.TradeNode:
                case ProvinceMacroColumnType.Tax:
                case ProvinceMacroColumnType.Production:
                case ProvinceMacroColumnType.Manpower:
                    stringOptionsTextBox.Visibility = Visibility.Visible;
                    break;
                case ProvinceMacroColumnType.Color:
                    InitializeColorFields();
                    rgbOptionsTextbox.Visibility = Visibility.Visible;
                    break;
                case ProvinceMacroColumnType.City:
                case ProvinceMacroColumnType.HRE:
                case ProvinceMacroColumnType.Impassable:
                case ProvinceMacroColumnType.ProvinceType:
                case ProvinceMacroColumnType.Climate:
                case ProvinceMacroColumnType.Winter:
                case ProvinceMacroColumnType.Monsoon:
                default:
                    return;
            }
        }

        private void DisableFields()
        {
            stringOptionsTextBox.Visibility = Visibility.Hidden;
            rgbOptionsTextbox.Visibility = Visibility.Hidden;
        }

        private void InitializeColorFields()
        {
            UpdateR(null, null);
            UpdateG(null, null);
            UpdateB(null, null);
        }

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

        private void RunColorMacro()
        {
            ColorOperationType rOpType = (ColorOperationType)rMacroTypeBox.SelectedItem;
            ColorOperationType gOpType = (ColorOperationType)gMacroTypeBox.SelectedItem;
            ColorOperationType bOpType = (ColorOperationType)bMacroTypeBox.SelectedItem;

            int rIncModifier;
            switch (rOpType)
            {
                case ColorOperationType.Increment:
                    rIncModifier = 1;
                    break;
                case ColorOperationType.Decrement:
                    rIncModifier = -1;
                    break;
                default:
                    rIncModifier = 0;
                    break;
            }

            int gIncModifier;
            switch (gOpType)
            {
                case ColorOperationType.Increment:
                    gIncModifier = 1;
                    break;
                case ColorOperationType.Decrement:
                    gIncModifier = -1;
                    break;
                default:
                    gIncModifier = 0;
                    break;
            }

            int bIncModifier;
            switch (bOpType)
            {
                case ColorOperationType.Increment:
                    bIncModifier = 1;
                    break;
                case ColorOperationType.Decrement:
                    bIncModifier = -1;
                    break;
                default:
                    bIncModifier = 0;
                    break;
            }

            if (rOpType != ColorOperationType.None) ProvinceMacroHandler.SetR(vm.Provinces, Int32.Parse(rBox.Text), Int32.Parse(rIncrementBox.Text) * rIncModifier, Int32.Parse(rIntervalBox.Text));
            if (gOpType != ColorOperationType.None) ProvinceMacroHandler.SetG(vm.Provinces, Int32.Parse(gBox.Text), Int32.Parse(gIncrementBox.Text) * gIncModifier, Int32.Parse(gIntervalBox.Text));
            if (bOpType != ColorOperationType.None) ProvinceMacroHandler.SetB(vm.Provinces, Int32.Parse(bBox.Text), Int32.Parse(bIncrementBox.Text) * bIncModifier, Int32.Parse(bIntervalBox.Text));
        }

        private void RunMacro(object sender, RoutedEventArgs e)
        {
            switch (vm.ColumnType)
            {
                case ProvinceMacroColumnType.Select:
                    break;
                case ProvinceMacroColumnType.ID:
                    break;
                case ProvinceMacroColumnType.Name:
                    ProvinceMacroHandler.SetName(vm.Provinces, stringOptionsValueBox.Text);
                    break;
                case ProvinceMacroColumnType.LocalisedName:
                    ProvinceMacroHandler.SetLocalizedName(vm.Provinces, stringOptionsValueBox.Text);
                    break;
                case ProvinceMacroColumnType.LocalisedAdjective:
                    ProvinceMacroHandler.SetLocalizedAdjective(vm.Provinces, stringOptionsValueBox.Text);
                    break;
                case ProvinceMacroColumnType.Owner:
                    ProvinceMacroHandler.SetOwner(vm.Provinces, stringOptionsValueBox.Text);
                    break;
                case ProvinceMacroColumnType.Controller:
                    ProvinceMacroHandler.SetController(vm.Provinces, stringOptionsValueBox.Text);
                    break;
                case ProvinceMacroColumnType.Culture:
                    ProvinceMacroHandler.SetCulture(vm.Provinces, stringOptionsValueBox.Text);
                    break;
                case ProvinceMacroColumnType.Religion:
                    ProvinceMacroHandler.SetReligion(vm.Provinces, stringOptionsValueBox.Text);
                    break;
                case ProvinceMacroColumnType.Color:
                    RunColorMacro();
                    break;
                case ProvinceMacroColumnType.Cores:
                    ProvinceMacroHandler.SetCores(vm.Provinces, stringOptionsValueBox.Text);
                    break;
                case ProvinceMacroColumnType.TradeGood:
                    ProvinceMacroHandler.SetTradeGood(vm.Provinces, stringOptionsValueBox.Text);
                    break;
                case ProvinceMacroColumnType.TradeNode:
                    ProvinceMacroHandler.SetTradeNode(vm.Provinces, stringOptionsValueBox.Text);
                    break;
                case ProvinceMacroColumnType.Tax:
                    ProvinceMacroHandler.SetTax(vm.Provinces, stringOptionsValueBox.Text);
                    break;
                case ProvinceMacroColumnType.Production:
                    ProvinceMacroHandler.SetProduction(vm.Provinces, stringOptionsValueBox.Text);
                    break;
                case ProvinceMacroColumnType.Manpower:
                    ProvinceMacroHandler.SetManpower(vm.Provinces, stringOptionsValueBox.Text);
                    break;
                case ProvinceMacroColumnType.City:
                case ProvinceMacroColumnType.HRE:
                case ProvinceMacroColumnType.DiscoveredBy:
                    ProvinceMacroHandler.SetDiscoveredBy(vm.Provinces, stringOptionsValueBox.Text);
                    break;
                case ProvinceMacroColumnType.Unrest:
                    ProvinceMacroHandler.SetUnrest(vm.Provinces, stringOptionsValueBox.Text);
                    break;
                case ProvinceMacroColumnType.CapitalName:
                    ProvinceMacroHandler.SetCapitalName(vm.Provinces, stringOptionsValueBox.Text);
                    break;
                case ProvinceMacroColumnType.Area:
                    ProvinceMacroHandler.SetArea(vm.Provinces, stringOptionsValueBox.Text);
                    break;
                case ProvinceMacroColumnType.Continent:
                    ProvinceMacroHandler.SetContinent(vm.Provinces, stringOptionsValueBox.Text);
                    break;
                case ProvinceMacroColumnType.Impassable:
                case ProvinceMacroColumnType.ProvinceType:
                case ProvinceMacroColumnType.Climate:
                case ProvinceMacroColumnType.Winter:
                case ProvinceMacroColumnType.Monsoon:
                default:
                    return;
            }
        }

        private void RunMacroAndClose(object sender, RoutedEventArgs e)
        {
            RunMacro(sender, e);
            Close();
        }
    }
}
