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
using EU4ModUtil.Models.Data.Generic;

namespace EU4ModUtil.Views
{
    /// <summary>
    /// Interaction logic for RegionWindow.xaml
    /// </summary>
    public partial class RegionWindow : Window
    {
        TreeItem data;
        public RegionWindow(TreeItem data)
        {
            InitializeComponent();
            this.data = data;
        }
    }
}
