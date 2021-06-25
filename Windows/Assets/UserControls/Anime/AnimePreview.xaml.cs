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

namespace Windows.Assets.UserControls.Anime
{
    /// <summary>
    /// Interaction logic for AnimePreview.xaml
    /// </summary>
    public partial class AnimePreview : UserControl
    {
        public static readonly DependencyProperty LinkProperty = DependencyProperty.Register("PictureLink", typeof(Uri), typeof(AnimePreview));

        public Uri PictureLink
        {
            get { return (Uri)GetValue(LinkProperty); }
            set { SetValue(LinkProperty, value); }
        }

        public AnimePreview()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
