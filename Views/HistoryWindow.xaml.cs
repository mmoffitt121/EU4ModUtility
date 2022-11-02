using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using EU4ModUtil.Models.Data.Common;
using EU4ModUtil.Models.Data.History;
using EU4ModUtil.Models.Data.Map;
using EU4ModUtil.Models.Data;

namespace EU4ModUtil.Views
{
    /// <summary>
    /// Interaction logic for HistoryWindow.xaml
    /// </summary>
    public partial class HistoryWindow : Window
    {
        Country country;
        Province province;
        internal HistoryWindow(Country country)
        {
            InitializeComponent();

            this.country = country;
            DataContext = this.country;

            historyItemGrid.ItemsSource = this.country.History;
            countryName.Content = this.country.Tag + " - " + this.country.LocalizedName;

            country.Changed = true;

            UpdateWindow();
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            if (country.History == null) country.History = new List<HistoryEntry>();
            if (historyItemGrid.SelectedIndex == -1)
            {
                country.History.Add(new HistoryEntry(new AttributeValueObject()));
            }
            else
            {
                country.History.Insert(historyItemGrid.SelectedIndex, new HistoryEntry(new AttributeValueObject()));
            }

            country.NotifyPropertyChanged(nameof(country.History));
            UpdateWindow();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            country.History.Remove(country.History[historyItemGrid.SelectedIndex]);

            country.NotifyPropertyChanged(nameof(country.History));
            UpdateWindow();
        }

        private void Sort(object sender, RoutedEventArgs e)
        {
            country.History.Sort(HistoryEntry.Comparison);

            country.NotifyPropertyChanged(nameof(country.History));
            UpdateWindow();
        }

        public void DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DataGridRow row = e.Source as DataGridRow;

                AttributeValueWindow attrWin = new AttributeValueWindow(country.History[row.GetIndex()]);
                attrWin.Owner = this;

                attrWin.ShowDialog();
                UpdateWindow();
            }
        }

        void DataGrid_Unloaded(object sender, RoutedEventArgs e)
        {
            var grid = (DataGrid)sender;
            grid.CommitEdit(DataGridEditingUnit.Row, true);
        }

        public void UpdateWindow()
        {
            historyItemGrid.ItemsSource = null;
            historyItemGrid.ItemsSource = this.country.History;
        }
    }
}
