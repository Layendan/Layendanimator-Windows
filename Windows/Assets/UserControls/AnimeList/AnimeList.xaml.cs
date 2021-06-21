using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AnimeModel = Windows.Models.UserControlModels.Anime;

namespace Windows.Assets.UserControls
{
    public partial class AnimeList : UserControl
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(AnimeList));
        public static readonly DependencyProperty BodyProperty = DependencyProperty.Register("Body", typeof(List<AnimeModel.Anime>), typeof(AnimeList));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public List<AnimeModel.Anime> Body
        {
            get { return (List<AnimeModel.Anime>)GetValue(BodyProperty); }
            set { SetValue(BodyProperty, value); }
        }

        public AnimeList()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void ScrollBar_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftShift) ^ Keyboard.IsKeyDown(Key.RightShift))
            {
                ScrollViewer.ScrollToHorizontalOffset(ScrollViewer.HorizontalOffset - (e.Delta / 100));
                e.Handled = true;
            }
        }
    }
}
