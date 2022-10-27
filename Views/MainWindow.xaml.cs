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
using EU4ModUtil.Parsers;
using EU4ModUtil.Util;
using EU4ModUtil.Loaders;
using EU4ModUtil.Models.Data.Common;

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

                LoadMod();
                UpdateModInfoDisplay();
            }
        }

        private void LoadMod()
        {
            viewModel.mod = new Mod();
            viewModel.loader = new ModLoader(viewModel.mod, viewModel.appData);
            viewModel.loader.LoadMod();
            UpdateModInfoDisplay();
            UpdateTabDisplay();
        }

        private void LoadModInfo()
        {
            viewModel.loader.LoadMod();
            UpdateModInfoDisplay();
        }

        private void UpdateModInfoDisplay()
        {
            if (viewModel.appData.modPath != null && !viewModel.appData.modPath.Equals(""))
            {
                descriptorDisplay.Visibility = Visibility.Visible;
                modName.Content = viewModel.mod.descriptor.name;
                modPath.Content = viewModel.appData.modPath;
                thumbnail.Source = viewModel.mod.descriptor.bitmap != null ? viewModel.mod.descriptor.bitmap : viewModel.noImageBitmap;
                FillListBox(tagListBox, viewModel.mod.descriptor.tags);
                FillListBox(replacePathsListBox, viewModel.mod.descriptor.replacePaths);
                versionInfo.Text = "Version: " + viewModel.mod.descriptor.version + "\nGame Version: " + viewModel.mod.descriptor.supportedVersion;

                countryDataGrid.ItemsSource = viewModel.Countries;
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
            }
        }

        private void UpdateTabDisplay()
        {
            if (viewModel.appData.modPath != null && !viewModel.appData.modPath.Equals(""))
            {
                countriesTab.IsEnabled = true;
                provincesTab.IsEnabled = true;
                culturesTab.IsEnabled = true;
                religionTab.IsEnabled = true;
            }
            else
            {
                countriesTab.IsEnabled = false;
                provincesTab.IsEnabled = false;
                culturesTab.IsEnabled = false;
                religionTab.IsEnabled = false;
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
    }
}
