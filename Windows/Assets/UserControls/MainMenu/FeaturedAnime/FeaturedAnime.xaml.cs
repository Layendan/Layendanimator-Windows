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
        public static readonly DependencyProperty VideoSourceProperty = DependencyProperty.Register("VideoSource", typeof(Uri), typeof(FeaturedAnime));

        public Uri VideoSource
        {
            get { return (Uri)GetValue(VideoSourceProperty); }
            set { SetValue(VideoSourceProperty, value); }
        }

        public FeaturedAnime()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
