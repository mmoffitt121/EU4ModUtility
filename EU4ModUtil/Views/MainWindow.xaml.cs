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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EU4ModUtil.Models.Data;
using EU4ModUtil.Parsers;
using EU4ModUtil.Util;
using EU4ModUtil.Loaders;
using System.Windows.Media.Imaging;

namespace EU4ModUtil
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal AppData appData;
        internal Mod mod;
        internal ModLoader loader;
        internal BitmapImage noImageBitmap;

        public MainWindow()
        {
            InitializeComponent();
            appData = new AppData();
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
                appData.modPath = fileDialog.FileName.Substring(0, fileDialog.FileName.Length - 4);

                LoadMod();
                UpdateModInfoDisplay();
            }
        }

        private void LoadMod()
        {
            mod = new Mod();
            loader = new ModLoader(mod, appData);
            loader.LoadMod();
            UpdateModInfoDisplay();
        }

        private void LoadModInfo()
        {
            loader.LoadMod();
            UpdateModInfoDisplay();
        }

        private void UpdateModInfoDisplay()
        {
            if (appData.modPath != null && !appData.modPath.Equals(""))
            {
                descriptorDisplay.Visibility = Visibility.Visible;
                modName.Content = mod.descriptor.name;
                modPath.Content = appData.modPath;
                thumbnail.Source = mod.descriptor.bitmap;
                FillListBox(tagListBox, mod.descriptor.tags);
                FillListBox(replacePathsListBox, mod.descriptor.replacePaths);
                versionInfo.Text = "Version: " + mod.descriptor.version + "\nGame Version: " + mod.descriptor.supportedVersion;
            }
            else
            {
                descriptorDisplay.Visibility = Visibility.Hidden;
                modName.Content = "No Mod Loaded";
                modPath.Content = "No Path Found";
                thumbnail.Source = noImageBitmap;
                tagListBox.Items.Clear();
                replacePathsListBox.Items.Clear();
                versionInfo.Text = "";
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
            noImageBitmap = new BitmapImage();
            noImageBitmap.BeginInit();
            noImageBitmap.UriSource = new Uri("pack://application:,,,/Images/NoImageFound.png");
            noImageBitmap.EndInit();
            thumbnail.Source = noImageBitmap;
        }
    }
}
