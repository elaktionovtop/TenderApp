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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TenderApp.Views
{
    public partial class ItemWindowButtonPanel : UserControl
    {
        public ItemWindowButtonPanel()
        {
            InitializeComponent();
        }

        private void OnExecuteButton_Click(object sender, RoutedEventArgs e)
        {
            ItemWindowHelper.BindingUpdating(Window.GetWindow(this));
        }
    }
}
