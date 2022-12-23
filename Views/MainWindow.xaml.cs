using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using EU4ModUtil.Models.Data.Map;
using EU4ModUtil.Parsers;
using EU4ModUtil.Util;
using EU4ModUtil.Loaders;
using EU4ModUtil.Writers;
using EU4ModUtil.Models.Data.Common;
using EU4ModUtil.Views;

namespace EU4ModUtil
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModel viewModel { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            viewModel = new ViewModel();
            this.DataContext = viewModel;
            
            viewModel.appData = new AppData();
            UpdateModInfoDisplay();
            InitializeImages();
        }

        #region Mod Loading
        private void Select_Mod_Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
            fileDialog.Filter = "mod files (*.mod)|*.mod";

            if (fileDialog.ShowDialog() == true)
            {
                if (!System.IO.Directory.Exists(fileDialog.FileName.Substring(0, fileDialog.FileName.Length - 4)))
                {
                    string msg = "No folder associated with '" + fileDialog.FileName + "'. Please make sure the folder is named the same as the .mod file, and that you selected the .mod file outside the mod folder.";
                    MessageBox.Show(msg, "No Folder Found");
                    return;
                }
                viewModel.appData.modPath = fileDialog.FileName.Substring(0, fileDialog.FileName.Length - 4);

                LoadModInfo();
                UpdateModInfoDisplay();
                UpdateTabDisplay();
            }
        }

        private void Select_Game_Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
            fileDialog.Filter = "exe files (*.exe)|*.exe";

            /*if (fileDialog.ShowDialog() == true)
            {
                if (!System.IO.Directory.Exists(fileDialog.FileName.Substring(0, fileDialog.FileName.Length - 4)))
                {
                    string msg = "No folder associated with '" + fileDialog.FileName + "'. Please make sure the folder is named the same as the .mod file, and that you selected the .mod file outside the mod folder.";
                    MessageBox.Show(msg, "No Folder Found");
                    return;
                }
                viewModel.appData.modPath = fileDialog.FileName.Substring(0, fileDialog.FileName.Length - 4);

                LoadMod();
                UpdateModInfoDisplay();
            }*/
        }

        private void LoadModInfo()
        {
            viewModel.LoadModInfo();
        }

        #endregion

        #region Display

        private void UpdateModInfoDisplay()
        {
            if (viewModel.appData.modPath != null && !viewModel.appData.modPath.Equals(""))
            {
                descriptorDisplay.Visibility = Visibility.Visible;
                modName.Content = viewModel.mod?.descriptor?.name;
                modPath.Content = viewModel.appData?.modPath;
                thumbnail.Source = viewModel.mod?.descriptor?.bitmap != null ? viewModel.mod.descriptor.bitmap : viewModel.noImageBitmap;
                FillListBox(tagListBox, viewModel.mod?.descriptor?.tags);
                FillListBox(replacePathsListBox, viewModel.mod?.descriptor?.replacePaths);
                versionInfo.Text = "Version: " + viewModel.mod?.descriptor?.version + "\nGame Version: " + viewModel.mod?.descriptor?.supportedVersion;

                countryDataGrid.ItemsSource = viewModel.Countries;
                provinceDataGrid.ItemsSource = viewModel.Provinces;
                cultureTreeView.ItemsSource = viewModel.CultureGroups;

                mapDisplay.Source = viewModel.MapDisplay;
            }
            else
            {
                descriptorDisplay.Visibility = Visibility.Hidden;
                modName.Content = "No Mod Loaded";
                modPath.Content = "No Path Found";
                thumbnail.Source = viewModel.noImageBitmap;
                tagListBox.Items.Clear();
                replacePathsListBox.Items.Clear();
                versionInfo.Text = "";

                countryDataGrid.ItemsSource = null;
                provinceDataGrid.ItemsSource = null;
                cultureTreeView.ItemsSource = null;
            }
        }

        private void UpdateTabDisplay()
        {
            if (viewModel.appData.modPath != null && !viewModel.appData.modPath.Equals(""))
            {
                mapTab.IsEnabled = true;
                countriesTab.IsEnabled = true;
                provincesTab.IsEnabled = true;
                culturesTab.IsEnabled = true;
            }
            else
            {
                mapTab.IsEnabled = false;
                countriesTab.IsEnabled = false;
                provincesTab.IsEnabled = false;
                culturesTab.IsEnabled = false;
            }
        }

        private void FillListBox(ListBox listBox, List<string> items)
        {
            if (items == null || items.Count == 0)
            {
                return;
            }

            listBox.Items.Clear();

            foreach (string item in items)
            {
                listBox.Items.Add(item);
            }
        }

        private void InitializeImages()
        {
            viewModel.noImageBitmap = new BitmapImage();
            viewModel.noImageBitmap.BeginInit();
            viewModel.noImageBitmap.UriSource = new Uri("pack://application:,,,/Images/NoImageFound.png");
            viewModel.noImageBitmap.EndInit();
            thumbnail.Source = viewModel.noImageBitmap;
        }

        void DataGrid_Unloaded(object sender, RoutedEventArgs e)
        {
            var grid = (DataGrid)sender;
            grid.CommitEdit(DataGridEditingUnit.Row, true);
        }
        #endregion

        #region Country Data

        private void SaveCountries(object sender, RoutedEventArgs e)
        {
            viewModel.writer = new ModWriter(viewModel.mod, viewModel.appData);
            List<string> changed = viewModel.writer.WriteCountries();
            string changedStr = "Save complete.\n\nFiles edited:\n" + string.Join('\n', changed).Replace("/", "\\");
            MessageBox.Show(changedStr, "Save Complete", MessageBoxButton.OK);

            UpdateModInfoDisplay();
            UpdateTabDisplay();
        }

        private void NewCountry(object sender, RoutedEventArgs e)
        {
            viewModel.NewCountry(countryDataGrid.Items.IndexOf(countryDataGrid.SelectedItem));
            countryDataGrid.ItemsSource = viewModel.Countries;
        }

        private void DeleteCountry(object sender, RoutedEventArgs e)
        {
            viewModel.DeleteCountry(countryDataGrid.Items.IndexOf(countryDataGrid.SelectedItem));
            countryDataGrid.ItemsSource = viewModel.Countries;
        }

        private void OpenHistory(object sender, RoutedEventArgs e)
        {
            int index = countryDataGrid.Items.IndexOf(countryDataGrid.SelectedItem);
            if (index == -1) return;

            HistoryWindow historyWindow = new HistoryWindow(viewModel.Countries[index]);
            historyWindow.Owner = this;

            historyWindow.ShowDialog();
            countryDataGrid.ItemsSource = viewModel.Countries;
        }

        private void OpenProvinceHistory(object sender, RoutedEventArgs e)
        {
            
        }

        private void OpenCountryValues(object sender, RoutedEventArgs e)
        {
            int index = countryDataGrid.Items.IndexOf(countryDataGrid.SelectedItem);
            if (index == -1) return;

            AttributeValueWindow attrWindow = new AttributeValueWindow(new AttributeValueObject(viewModel.Countries[index].Tag, viewModel.Countries[index].OtherCountryValues));
            attrWindow.Owner = this;

            attrWindow.ShowDialog();
            countryDataGrid.ItemsSource = viewModel.Countries;
        }

        private void CheckCountryDupes(object sender, RoutedEventArgs e)
        {
            if (viewModel?.Countries?.Count == 0)
            {
                MessageBox.Show("No duplicates found", "Duplicate Check Complete", MessageBoxButton.OK);
                return;
            }

            StringBuilder dupes = new StringBuilder();
            for (int i = 0; i < viewModel.Countries.Count; i++)
            {
                for (int j = i + 1; j < viewModel.Countries.Count; j++)
                {
                    if (viewModel.Countries[i].Tag.Equals(viewModel.Countries[j].Tag))
                    {
                        dupes.Append(i + ": " + viewModel.Countries[i].Tag + " and " + j + ": " + viewModel.Countries[j].Tag + "\n");
                    }
                }
            }
            
            MessageBox.Show(dupes.ToString().Equals("") ?  "No duplicates found" : dupes.ToString(), "Duplicate Check Complete", MessageBoxButton.OK);
        }

        #endregion

        #region Province Data

        private void SaveProvinces(object sender, RoutedEventArgs e)
        {
            viewModel.writer = new ModWriter(viewModel.mod, viewModel.appData);
            List<string> changed = viewModel.writer.WriteProvinces();
            string changedStr = "Save complete.\n\nFiles edited:\n" + string.Join('\n', changed).Replace("/", "\\");
            MessageBox.Show(changedStr, "Save Complete", MessageBoxButton.OK);

            UpdateModInfoDisplay();
            UpdateTabDisplay();
        }

        private void NewWaterProvince(object sender, RoutedEventArgs e)
        {
            int prov = viewModel.NewProvince(provinceDataGrid.Items.IndexOf(provinceDataGrid.SelectedItem), true);
            provinceDataGrid.ItemsSource = viewModel.Provinces;

            if (CopyOnAdd.IsChecked == true)
            {
                Clipboard.SetText(viewModel.mod.provinces[prov].ColorHex);
            }
        }

        private void NewLandProvince(object sender, RoutedEventArgs e)
        {
            int prov = viewModel.NewProvince(provinceDataGrid.Items.IndexOf(provinceDataGrid.SelectedItem), false);
            provinceDataGrid.ItemsSource = viewModel.Provinces;

            if (CopyOnAdd.IsChecked == true)
            {
                Clipboard.SetText(viewModel.mod.provinces[prov].ColorHex);
            }
        }

        private void DeleteProvince(object sender, RoutedEventArgs e)
        {
            viewModel.DeleteProvince(provinceDataGrid.Items.IndexOf(provinceDataGrid.SelectedItem));
            provinceDataGrid.ItemsSource = viewModel.Provinces;
        }

        private void CopyHex(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(((Button)sender).Tag.ToString());
        }
        #endregion

        #region Culture Data
        private void OpenRegions(object sender, RoutedEventArgs e)
        {
            RegionWindow regionWindow = new RegionWindow(viewModel.mod.superRegions);
            regionWindow.ShowDialog();
        }

        private void SelectCultureTreeviewItem(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            viewModel.SelectedCulture = (Culture)cultureTreeView.SelectedItem;

            // Single Values
            cultureNameBox.Text = viewModel.SelectedCulture.Name;
            cultureLocalizedNameBox.Text = viewModel.SelectedCulture.LocalizedName;
            culturePrimaryCountryBox.Text = viewModel.SelectedCulture.Primary;

            // Name Lists
            cultureDynastyNamesBox.Text = viewModel.SelectedCulture?.DynastyNames == null ? "" : string.Join("\n", viewModel.SelectedCulture.DynastyNames);
            cultureMaleNamesBox.Text = viewModel.SelectedCulture?.MaleNames == null ? "" : string.Join("\n", viewModel.SelectedCulture.MaleNames);
            cultureFemaleNamesBox.Text = viewModel.SelectedCulture?.FemaleNames == null ? "" : string.Join("\n", viewModel.SelectedCulture.FemaleNames);
        }

        private void ApplyCultureDynastyNames(object sender, RoutedEventArgs e)
        {
            if (viewModel == null || viewModel.SelectedCulture == null) { return; }
            viewModel.SelectedCulture.DynastyNames = new List<string>(cultureDynastyNamesBox.Text.Split('\n'));
            for (int i = 0; i < viewModel.SelectedCulture.DynastyNames.Count; i++)
            {
                viewModel.SelectedCulture.DynastyNames[i] = viewModel.SelectedCulture.DynastyNames[i].Trim();
            }
        }

        private void ApplyCultureMaleNames(object sender, RoutedEventArgs e)
        {
            if (viewModel == null || viewModel.SelectedCulture == null) { return; }
            viewModel.SelectedCulture.MaleNames = new List<string>(cultureMaleNamesBox.Text.Split('\n'));
            for (int i = 0; i < viewModel.SelectedCulture.MaleNames.Count; i++)
            {
                viewModel.SelectedCulture.MaleNames[i] = viewModel.SelectedCulture.MaleNames[i].Trim();
            }
        }

        private void ApplyCultureFemaleNames(object sender, RoutedEventArgs e)
        {
            if (viewModel == null || viewModel.SelectedCulture == null) { return; }
            viewModel.SelectedCulture.FemaleNames = new List<string>(cultureFemaleNamesBox.Text.Split('\n'));
            for (int i = 0; i < viewModel.SelectedCulture.FemaleNames.Count; i++)
            {
                viewModel.SelectedCulture.FemaleNames[i] = viewModel.SelectedCulture.FemaleNames[i].Trim();
            }
        }

        private void ApplyCultureChanges(object sender, RoutedEventArgs e)
        {
            if (viewModel == null || viewModel.SelectedCulture == null) { return; }
            viewModel.SelectedCulture.Name = cultureNameBox.Text.Trim();
            viewModel.SelectedCulture.LocalizedName = cultureLocalizedNameBox.Text.Trim();
            viewModel.SelectedCulture.Primary = culturePrimaryCountryBox.Text.Trim();
        }

        private void AddCulture(object sender, RoutedEventArgs e)
        {
            if (cultureTreeView?.SelectedItem != null && ((Culture)cultureTreeView.SelectedItem).Parent != null)
            {
                viewModel.NewCulture(((Culture)cultureTreeView.SelectedItem).Parent);
            }
            else if (cultureTreeView?.SelectedItem != null)
            {
                viewModel.NewCulture(cultureTreeView.Items.IndexOf(cultureTreeView.SelectedItem));
            }
            else if (cultureTreeView.Items.Count > 0)
            {
                viewModel.NewCulture((CultureGroup)cultureTreeView.Items[0]);
            }
            else
            {
                MessageBox.Show("No culture groups found. Are any loaded?", "Warning", MessageBoxButton.OK);
            }

            cultureTreeView.Items.Refresh();
        }

        private void AddCultureGroup(object sender, RoutedEventArgs e)
        {
            int index = cultureTreeView.Items.IndexOf(cultureTreeView.SelectedItem);
            if (index == -1)
            {
                index = cultureTreeView.Items.IndexOf(((Culture)cultureTreeView.SelectedItem).Parent);
            }
            if (index == -1)
            {
                index = viewModel.CultureGroups.Count;
            }

            viewModel.NewCultureGroup(index);

            cultureTreeView.Items.Refresh();
        }

        private void DeleteCulture(object sender, RoutedEventArgs e)
        {
            if (cultureTreeView.SelectedItem != null)
            {
                if (((Culture)cultureTreeView.SelectedItem).Parent == null)
                {
                    viewModel.CultureGroups.Remove((CultureGroup)cultureTreeView.SelectedItem);
                }
                else
                {
                    ((Culture)cultureTreeView.SelectedItem).Parent.Cultures.Remove((Culture)cultureTreeView.SelectedItem);
                }
                cultureTreeView.Items.Refresh();
            }
        }

        private void SaveCultures(object sender, RoutedEventArgs e)
        {
            viewModel.writer = new ModWriter(viewModel.mod, viewModel.appData);
            bool success = viewModel.writer.WriteCultures();
            string changedStr = success ? "Save Complete" : "Save Failed";
            MessageBox.Show(changedStr, changedStr, MessageBoxButton.OK);

            UpdateModInfoDisplay();
            UpdateTabDisplay();
        }
        #endregion
    }
}
