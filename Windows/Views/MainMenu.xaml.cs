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
using JavaScriptEngineSwitcher.V8;

namespace Windows.Views
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        public Uri ftrVideoSource { get; set; }

        public MainMenu()
        {
            InitializeComponent();

            ftrVideoSource = new UriBuilder("http://storage.googleapis.com/nodal-boulder-315702/VQZFVFVH43XP/st23_vivy-fluorite-eyes-song-episode-11.1624913946.mp4").Uri;

            V8JsEngine engine = new V8JsEngine();
            engine.ExecuteFile(@"C:\Users\aidan\source\repos\Layendanimator\Windows\Models\JavaScriptEngine\Anilist\FeaturedAnimeModel.js");
            if(engine.HasVariable("name"))
                Console.WriteLine(engine.GetVariableValue("name"));
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
