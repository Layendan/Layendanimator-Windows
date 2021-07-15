﻿using System;
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
using Windows.Models.UserControlModels.Anime;

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

            Type animeType = typeof(Anime);

            var fetchFunc = new Func<string, string, string>((url, options) =>
            {
                var data = new StringContent(options, Encoding.UTF8, "application/json");

                var client = new HttpClient();

                var response = client.PostAsync(url, data);

                string result = response.Result.Content.ReadAsStringAsync().Result;

                return result;
            });

            var logFunc = new Func<object, object>((value) =>
            {
                Console.WriteLine(value);
                return value;
            });

            engine.EmbedHostType("Anime", animeType);
            engine.EmbedHostObject("fetch", fetchFunc);
            engine.EmbedHostObject("log", logFunc);

            engine.Execute(getPageContent(@"https://raw.githubusercontent.com/Layendan/Layendanimator/Layendan/Windows/Models/JavaScriptEngine/Anilist/FeaturedAnimeModel.js"));
            if(engine.HasVariable("title"))
                Console.WriteLine(engine.GetVariableValue("title"));

            //ftrVideoSource = new UriBuilder(engine.GetVariableValue("videoUri").ToString()).Uri;
            ftrVideoSource = new UriBuilder(@"http://streamani.net/goto.php?url=aHR0cHM6LyAdrefsdsdfwerFrefdsfrersfdsrfer363435349zdG9yYWdlLmdvb2dsZWFwaXMuY29tL2NvYmFsdC1hbGxpYW5jZS0zMTcwMDYvM1Q4Mk5TOEJNRTZOL3N0MjJfbmFydXRvLWVwaXNvZGUtMS4xNjI1OTQ2ODUwLm1wNA==").Uri;

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

        public string getPageContent(string url)
        {
            var client = new HttpClient();

            try
            {
                var response = client.GetAsync(url);

                string result = response.Result.Content.ReadAsStringAsync().Result;

                return result;
            } 
            catch (Exception ex)
            {
                Console.WriteLine($"Exception at HttpClient.GetAsync() in getPageContent() in MainMenu.xaml.cs: { ex.Message }");
                return null;
            }
            
        }
    }
}
