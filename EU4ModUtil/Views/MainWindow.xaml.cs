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

namespace EU4ModUtil
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal AppData appData;
        internal Mod mod;

        public MainWindow()
        {
            InitializeComponent();
            appData = new AppData();
            UpdateModInfoDisplay();
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

                LoadModInfo();
                UpdateModInfoDisplay();
            }
        }

        public void LoadModInfo()
        {
            TXTFileObject descriptor = TXTParser.Parse(appData.modPath + "/descriptor.mod");
            Trace.WriteLine(descriptor);
        }

        public void UpdateModInfoDisplay()
        {
            if (appData.modPath != null && !appData.modPath.Equals(""))
            {
                modName.Content = "modnamehere";
                modPath.Content = appData.modPath;
            }
            else
            {
                modName.Content = "No Mod Loaded";
                modPath.Content = "No Path Found";
            }
        }
    }
}
