using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

namespace Windows.Assets.UserControls.MainMenu.FeaturedAnime
{
    /// <summary>
    /// Interaction logic for FeaturedAnime.xaml
    /// </summary>
    public partial class FeaturedAnime : UserControl
    {
        public static readonly DependencyProperty VideoSourceProperty = DependencyProperty.Register("VideoSource", typeof(string), typeof(FeaturedAnime));

        public string VideoSource
        {
            get { return (string)GetValue(VideoSourceProperty); }
            set { SetValue(VideoSourceProperty, value); }
        }

        public FeaturedAnime()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Video_MediaOpened(object sender, RoutedEventArgs e)
        {
            TimeSpan ts = new TimeSpan(0, 0, 3, 8, 0);
            //Video.Position = ts;
        }
    }
}
