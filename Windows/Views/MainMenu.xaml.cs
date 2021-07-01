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
using System.Net.Http;
using Newtonsoft.Json;

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
            V8JsEngine engine = new V8JsEngine();

            ftrVideoSource = new UriBuilder("http://storage.googleapis.com/nodal-boulder-315702/VQZFVFVH43XP/st23_vivy-fluorite-eyes-song-episode-11.1624913946.mp4").Uri;

            var fetchFunc = new Func<string, string, string>((url, options) =>
            {
                var json = JsonConvert.SerializeObject(options);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var client = new HttpClient();

                var response = client.PostAsync(url, data);

                string result = response.Result.Content.ReadAsStringAsync().Result;

                return result;
            });

            engine.EmbedHostObject("fetch", fetchFunc);

            engine.ExecuteFile(@"C:\Users\aidan\source\repos\Layendanimator\Windows\Models\JavaScriptEngine\Anilist\FeaturedAnimeModel.js");
            if(engine.HasVariable("name"))
                Console.WriteLine(engine.GetVariableValue("name"));

            engine.Dispose();
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
