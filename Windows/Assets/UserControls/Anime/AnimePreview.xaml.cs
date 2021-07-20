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

            //Anime.PreviewMouseDown += Anime_PreviewMouseDown;
        }

        //Simulate Click
        private void Anime_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Anime.PreviewMouseUp += Anime_PreviewMouseUp;
        }

        //Simulate mouse up of click
        private void Anime_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if(IsMouseOver)
            {
                Anime.PreviewMouseUp -= Anime_PreviewMouseUp;
                try
                {
                    Views.AnimeInformation animeInformation = new Views.AnimeInformation();
                    animeInformation.setSource(image.Source);
                    ((NavigationWindow)Application.Current.MainWindow).Navigate(animeInformation);
                    
                } 
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception at navigationWindow.Navigate() in Anime_PreviewMouseUp() in AnimePreview.xaml.cs: { ex.Message }");
                }
            }
        }
    }
}
