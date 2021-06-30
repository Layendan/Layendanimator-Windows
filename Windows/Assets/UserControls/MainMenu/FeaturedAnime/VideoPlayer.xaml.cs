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
using Windows.Models;
using JavaScriptEngineSwitcher.ChakraCore;

namespace Windows.Assets.UserControls.MainMenu.FeaturedAnime
{
    /// <summary>
    /// Interaction logic for VideoPlayer.xaml
    /// </summary>
    public partial class VideoPlayer : UserControl
    {
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("VideoSource", typeof(Uri), typeof(VideoPlayer));

        public Uri VideoSource
        {
            get { return (Uri)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public VideoPlayer()
        {
            InitializeComponent();

            //JavaScript engine testing
            ChakraCoreJsEngine jsEngine = new ChakraCoreJsEngine();
            jsEngine.Execute(@"{ isVideoMuted = false; }");
            Console.WriteLine(jsEngine.GetVariableValue("isVideoMuted"));
            jsEngine.SetVariableValue("isVideoMuted", true);
            Console.WriteLine(jsEngine.GetVariableValue("isVideoMuted"));
            jsEngine.Dispose();
        }

        private void MuteBtn_Click(object sender, RoutedEventArgs e)
        {
            Video.IsMuted = !Video.IsMuted;
        }
    }
}
