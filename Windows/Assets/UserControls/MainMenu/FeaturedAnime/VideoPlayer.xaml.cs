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

namespace Windows.Assets.UserControls.MainMenu.FeaturedAnime
{
    /// <summary>
    /// Interaction logic for VideoPlayer.xaml
    /// </summary>
    public partial class VideoPlayer : UserControl
    {
        public VideoPlayer()
        {
            InitializeComponent();

            Keyboard.Focus(Video);
        }

        private void Video_MediaOpened(object sender, RoutedEventArgs e)
        {
            TimeSpan ts = new TimeSpan(0, 0, 3, 8, 0);
            Video.Position = ts;
        }

        private void MuteBtn_Click(object sender, RoutedEventArgs e)
        {
            Video.IsMuted = !Video.IsMuted;
        }
    }
}
