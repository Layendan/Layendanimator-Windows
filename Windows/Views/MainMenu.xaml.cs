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

namespace Windows.Views
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if(!(Keyboard.IsKeyDown(Key.LeftShift) ^ Keyboard.IsKeyDown(Key.RightShift)))
            {
                MMScrollViewer.ScrollToVerticalOffset(MMScrollViewer.VerticalOffset - e.Delta);
                e.Handled = true;
            }
        }
    }
}
