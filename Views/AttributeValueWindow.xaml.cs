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
using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;
using EU4ModUtil.Models.Data;

namespace EU4ModUtil.Views
{
    /// <summary>
    /// Interaction logic for AttributeValueWindow.xaml
    /// </summary>
    public partial class AttributeValueWindow : Window
    {
        AttributeValueObject obj;

        public AttributeValueWindow(AttributeValueObject obj)
        {
            InitializeComponent();

            this.obj = obj;
            DataContext = this.obj;

            valueItemGrid.ItemsSource = this.obj.values;

            if (!string.IsNullOrEmpty(this.obj.Attribute))
            {
                Title = this.obj.Attribute;
                attributeName.Content = this.obj.Attribute.Replace("_", "__");
            }
            else
            {
                Title = "Item";
                attributeName.Content = "Item";
            }
            
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            if (obj.values == null) obj.values = new List<AttributeValueObject>();
            if (valueItemGrid.SelectedIndex == -1)
            {
                obj.values.Add(new AttributeValueObject());
            }
            else
            {
                obj.values.Insert(valueItemGrid.SelectedIndex, new AttributeValueObject());
            }

            obj.NotifyPropertyChanged(nameof(obj.Values));
            UpdateWindow();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            obj.values.Remove(obj.values[valueItemGrid.SelectedIndex]);

            obj.NotifyPropertyChanged(nameof(obj.Values));
            UpdateWindow();
        }

        public void DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DataGridRow row = e.Source as DataGridRow;

                AttributeValueWindow attrWin = new AttributeValueWindow(obj.values[row.GetIndex()]);
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
            valueItemGrid.ItemsSource = null;
            valueItemGrid.ItemsSource = this.obj.values;
        }
    }
}
